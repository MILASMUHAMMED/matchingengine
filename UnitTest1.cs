using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace matchingengine;

public class Tests
{
[Test]
    public void MatchingEnginePageTitleTest()
    {
        IWebDriver driver = new ChromeDriver();

        try
        {
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl("https://www.matchingengine.com/");

            Assert.That(
                driver.Title,
                Does.Contain("Matching Engine by Spanish Point Technologies Ltd.")
            );
        }
        finally
        {
            driver.Quit();
        }
    }

}
