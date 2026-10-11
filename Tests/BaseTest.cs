using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
        Driver?.Quit();
        Driver?.Dispose();
    }
}
