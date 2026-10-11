using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework.Interfaces;

namespace matchingengine.Tests;

public class BaseTest
{
    protected IWebDriver Driver = null!;

    [SetUp]
    public void SetUp()
    {
        bool headless = Environment.GetEnvironmentVariable("HEADLESS") == "true";

        var options = new ChromeOptions();
        if (headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
        }

        Driver = new ChromeDriver(options);

        if (!headless)
        {
            Driver.Manage().Window.Maximize();
        }
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            if (Driver != null &&
                TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();

                var folder = Path.Combine(
                    TestContext.CurrentContext.WorkDirectory,
                    "Screenshots");

                Directory.CreateDirectory(folder);

                var filePath = Path.Combine(
                    folder,
                    $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png");

                screenshot.SaveAsFile(filePath);

                TestContext.AddTestAttachment(filePath, "Screenshot on failure");
            }
        }
        finally
        {
            Driver?.Dispose();
        }
    }
}
