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
        Driver = new ChromeDriver();
        Driver.Manage().Window.Maximize();
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
