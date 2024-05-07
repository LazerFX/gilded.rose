# Refactoring Steps

This document will cover what refactoring steps have been taken to the Gilded Rose Kata, in order.  A 'before' section, before the step has been taken, and an 'after' section, covering how the outcome functioned, and what changes were made to the proposed plan.

## 1 - Make the application testable

Given the raw kata, the first step is to make the application testable.  In order to do this, I intend to add a simple DI container, using the default built-in .NET host process container.  I will put a wrapper around the System.Console.WriteLine function to make it DI-able, and will inject this into the codebase.  I will then capture the output, compare it to the filesystem and ensure that this output matches.

Proposed steps to do this:

1. Write a simple PowerShell script to build, run the application and then compare output of the application with the `ApprovalTest.ThirtyDays.verified.txt` text file. (The current application output).  Run this after each step below to ensure that there is no failure, until we can automate this process.
1. Take the current `Main` function and drop it into a class we can inject.  Update Main with a nice DI process, and inject the old `Main` function into the new DI environment.
1. Create a DI wrapper for `Console.WriteLine`, and use that in the new `Main` function.
1. Using the new DI wrapper, update the Tests so that they create a DI container, inject a StringBuilder wrapped class and can get the text output.  This can then be compared against the `ApprovalTest.ThirtyDays.verified.txt` output file.

### 1 - In progress - each time we commit, we'll make a note here what has changed

Temporary testing process - `cd` into `src`, and then run `.\test.ps1`

Output will be either:

    No differences.
    Press any key to continue

or

    InputObject       SideIndicator
    -----------       -------------
    OMGHAI!           =‌>
    OMGHAI!  Wazaaap? <‌=

Which is what would happen if you appended `Wazaaap?` to the original output of the application.

---

Created a `RoseRunner` class.

---

Added Dependency Injection and a basic host environment with no additional injection.

---

Added the DI for an `IWriter` class that is implemented in a `ProductionWriter` output.  To confirm, this has correctly been iterating each step to show proper output using the `.\test.ps1` command.

---

Added "Warnings As Errors" to promote clean code style and good quality output

---

Enabled Nullable environment to pass warnings-as-errors and also increase code quality.

--

Added nullable annotation to Item class (Come at me, Goblin!)

--

Basic host builder created for tests, and an injected `TestWriter` `IWriter` implementation (As opposed to the `ProductionWriter` used in the live code).  At some point, I'll change it to include a nicer, chainable host build process but for now it works.  I don't like duplicated code like this between test and live for building the environment, as that makes the actual build environment an untested point in the code, so this is on a to-do to correct later.

### 1 - Post-code change commentary

I noted, part way through, that it may be unclear why I'm changing an already existing and working piece of code.  First, I mis-read the code in the `ThirtyDays` test, not fully realising that it was running completely.  Second, I have a personal preference that, in a well designed system, there are no references to pure-static singleton objects (`Console.WriteLine` or `DateTime.Now` for instance) without having them testable and understandable.  Therefore, I did not wish to implement the code with `Console.WriteLine` in-situ, and modifying the output was (in my mind) a clear benefit.  I confess, seeing `fakeoutput` in the `ThirtyDays` `Fact` made me assume that it was a faked test - mea culpa.

## 2 - Look at adding tests to the codebase, and refactoring

So we now have a global test suite, a DI container, and an infrastructure we can hopefully rely on to make the code more reliable and testable.  So what is the code doing?  Well, it's got a bunch of nested IF/Else statements, which are (as always) going to be hard to pick apart.  It's doing it in a massive loop.  It's got a *lot* of magic numbers.  It's got text values for item names scattered throughout.  It's not clear which part goes with which area, and what conditions cause each part of the code to run.

I think the first step will be to add some explicit tests for the conditions in the Kata text, and go on from there; probably removing the magic numbers as we go.

### 2 - In progress - each time we commit, we'll make a note here what has changed

Generic `stuff` object test added.

---

Added a set of `constant` values for the item names, and included this in the test.  Did not change any code yet.  First one to do was Aged Brie, which noted an interesting thing - Aged Brie increases by 2 after it goes negative (Due to quality going up in two separate loops).  Given the instructions are not to change things, we will keep this as-is, however it's obvious that simply by doing this refactor we can see that there are possible rules that should be considered further.  If this was a business product, I'd be keeping a list of these items, and raising them with the product owners to confirm this is how they wanted it to work.

---

Added `BackstagePass` values, bracketing the times when we have no value increase, standard value increase and increased value increase, and then the sharp drop-off to zero on negative SellIn.

---

Added `Sulfuras` tests.  The values are basically constant, unchanging, based on input.

---

Now we can start removing the string magic values and updating them through the code.

---

Adding a `MaxQuality` value to our `KeyItems` class, we can see the name perhaps wasn't right - for now, we'll call it `KeyValues`.  Most modern IDE's allow you to F2 and rename a class, and auto-update throughout, which simplifies this sort of refactor.  Then, we'll update `MaxQuality` through the code.  The important thing to note is that we also update this in the tests - if we want to change the value of `MaxQuality` in the future, we can do so without changing any other values.  If we want to make sure nobody accidentally breaks the tests then we can explicitly call-out a test that has a `MaxQuality == 50` as a guard-test, though some would frown upon this.

---

Now we can start looking at simplifying some of the logic.  One of the easiest things you can do is invert an `if` statement.  An example is the `SellIn < 0` clause, where the `AgedBrie` value is wrapping everything else.  If we invert the if statement, we can put that clause into its own area, and reduce the nesting of the rest of the code.

---

After doing this, we can see clearly now that there are two nested if statements with no associated code.  We can combine these if statements to reduce nesting.

---

We can now continue this with the `BackstagePass` value.

---

While doing this, we can clarify the code for BackstagePass - and item subtracted from itself is equal to Zero, so let's add a constant (`BackstagePassOverageValue`), and set it to this.  Don't forget to update the tests!

---

And now we have an extra nested `Sulfuras` if value which can be combined.

---

Now that we're progressing, I'm finding the `Items[i].<value>` construct to be quite annoying.  I'm going to replace the `for` loop with a `foreach` loop, and just operate on an `item` individually.

---

The IDE now highlights a number of changes, once we've removed the unnecessary index accessors, related to using the compound (`++` and `--`) operators.  We'll apply those as they also make the code easier to read.

---

Now we can start simplifying the addition loop.  One thing to note is we have an `if not... else if / else if` combination.  So if we're not `AgedBrie` or `BackstagePass` we do something, otherwise we do something else, and if we're `AgedBrie` we continue.  This can be separated out into its own set of conditionals, inverting the if/else statement.

---

Now we can see that we have two conditions - one for AgedBrie and one for BackstagePass.  Let's separate those out.

---

Now we can handle the magic values for the `BackstagePass` a bit better.  We're going to completely refactor this piece of code as it's really awful what it's doing right now - there's no clear way to set upper/lower bounds or more importantly to change how much is increased each step (for instance, if we want to increase by 4 when it's under 6, we'd have to work out how it functions in our head, and add another `item.Quality++` line.  Easy right now, while the code is in our head; hard later when we're trying to understand what is going on and how to handle this from scratch).

---

That looks like it has made things worse - those names are horrific.  Let's create a nested class for the BackstagePass values, and doe some cleaning up of the names.  Clearing up a few of the names to be more meaningful, a little shorter, and logically combined should help with readability in the future.

---

We can cut out any reference to `Sulfuras` by simply returning early.  Many would argue that separate return points in a function are a negative, and I would usually agree, but here we're in the middle of refactoring - we need to split into functions later, so let's simplify the code as much as possible to facilitate that.  Remember - refactoring doesn't mean your code is perfect *as you do it*, merely that you're progressing in a way that will enable you to get better code later, in simple, non-destructive, testable steps - and sometimes that increases complexity or adds non-optimal sub-steps.

---

Now we have several nested `if` statements that do nothing, we can coalesce them.

### 2 - Post code change commentary

We've greatly simplified the codebase.  It's now relatively easy to separate the flow into what each item does, and while we could continue going (separating out items into individual flows, for instance), it's worthwhile taking a step back and looking at the big picture before we do too much more.  So let's do that now.

## 3 - Consider how to update the application for future needs

Now we're at the point where we can see the wood from the trees.  This gives us a decision - how do we proceed from here?  We've got a goal - if an item is 'conjured', it should reduce Quality at an x2 rate compared to a non-Conjured item.  So we need a unified quality handling, so that can be added in.  It's not clear from the requirements whether we should assume anything with `Conjured` at the start is a Conjured special instance of that item - i.e. how could a `Conjured Aged Brie` work in that instance, or a `Conjured Sulfuras`?  I'm going to go from the basis that an item that starts with `Conjured` is going to operate as if it is a Standard Item - i.e. a non-named special - and reduces at double-rate.  This says to me that, if we have an item that is in the 'special' list, it goes off to a special function that handles those.  Otherwise, it goes to a standard function.  This then allows us to separate the code into sub-functions, and we can create one for each category, simplifying a lot of the if statements.  So that's the next 'goal' of this process.

1. Separate the code into `Standard` and `Special` flows.
1. Keep those clear and minimal
1. Add a `Conjured` flow to the `Special` flows.

### 3 - In progress - each time we commit, we'll make a note here what has changed
