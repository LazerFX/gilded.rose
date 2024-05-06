$null = dotnet build;
.\GildedRose\bin\Debug\net8.0\GildedRose.exe 30 > test-output.txt

$files = @{
    ReferenceObject  = (Get-Content -Path .\test-output.txt)
    DifferenceObject = (Get-Content -Path .\GildedRoseTests\ApprovalTest.ThirtyDays.verified.txt)
}
$output = Compare-Object @files

if ($null -eq $output) {
    Write-Host "No differences."
} else {
    $output | Format-Table
}

Write-Host "Press any key to continue"
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
Remove-Item .\test-output.txt