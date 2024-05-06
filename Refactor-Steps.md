# Refactoring Steps

This document will cover what refactoring steps have been taken to the Gilded Rose Kata, in order.  A 'before' section, before the step has been taken, and an 'after' section, covering how the outcome functioned, and what changes were made to the proposed plan.

## 1 - Make the application testable

### Before

Given the raw kata, the first step is to make the application testable.  In order to do this, I intend to add a simple DI container, using the default built-in .NET host process container.  I will put a wrapper around the System.Console.WriteLine function to make it DI-able, and will inject this into the codebase.  I will then capture the output, compare it to the filesystem and ensure that this output matches.

Proposed steps to do this:

1. Write a simple PowerShell script to build, run the application and then compare output of the application with the `ApprovalTest.ThirtyDays.verified.txt` text file. (The current application output).  Run this after each step below to ensure that there is no failure, until we can automate this process.
1. Take the current `Main` function and drop it into a class we can inject.  Update Main with a nice DI process, and inject the old `Main` function into the new DI environment.
1. Create a DI wrapper for `Console.WriteLine`, and use that in the new `Main` function.
1. Using the new DI wrapper, update the Tests so that they create a DI container, inject a StringBuilder wrapped class and can get the text output.  This can then be compared against the `ApprovalTest.ThirtyDays.verified.txt` output file.

### In progress - each time we commit, we'll make a note here what has changed

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

Added the DI for an `IWriter` class that is implemented in a ProductionWriter output.  To confirm, this has correctly been iterating each step to show proper output using the `.\test.ps1` command.

---

Added "Warnings As Errors" to promote clean code style and good quality output

---

Enabled Nullable environment to pass warnings-as-errors and also increase code quality.
