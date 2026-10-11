# Matching Engine UI Tests

UI automation tests for https://www.matchingengine.com/

## What is tested

1. Open the Matching Engine home page
2. Expand **Solutions** in the header section
3. Check the list of solutions shown in the menu
4. Click **Distribution processing** from the Solutions list
5. Scroll to the **All-in-one solution for scale** section
6. Check the section heading, introduction, bullet points and the six feature cards

## Tech stack

- C# / .NET 10
- Selenium WebDriver
- NUnit
- Google Chrome
- GitHub Actions

## Project structure

- `Pages` - page objects for the home page and the Distribution processing page
- `Tests` - test classes and `BaseTest` (browser setup, screenshot on failure)
- `TestData` - expected text used in the assertions

## How to run

You need the .NET 10 SDK and Google Chrome installed.

```
dotnet test
```

To run in headless mode (Windows PowerShell):

```
$env:HEADLESS="true"; dotnet test
```

## CI

The tests run automatically in GitHub Actions on every push to `master` and on pull requests, using headless Chrome.

## Notes

- Explicit waits is used.
- The cookie banner is declined if it appears.
- Currently in the webpage it shows "Imagine **an** technology solution...", which looks like a typo. The test expects the text as it is on the website today.
