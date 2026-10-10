using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace matchingengine.Tests;

public class Tests
{
    [Test]
    public void SolutionsMenuTest()
    {
        IWebDriver driver = new ChromeDriver();

        try
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.matchingengine.com/");

            Thread.Sleep(2000);

            var solutions = driver.FindElement(By.Id("nav-toggle-solutions"));
            solutions.Click();

            Thread.Sleep(1000);

            Assert.That(
                solutions.GetAttribute("aria-expanded"),
                Is.EqualTo("true"));

            var links = driver.FindElements(
                By.CssSelector("header section[role='menu'] [class*='BrandNavSubMenu_children'] a"));

            var actualLinks = links
                .Where(link => link.Displayed)
                .Select(link => link.Text.Trim())
                .Where(text => text != "")
                .ToList();

            var expectedLinks = new List<string>
            {
                "Repertoire management",
                "Repertoire and usage matching",
                "Data ingestion and integration",
                "Distribution processing",
                "Member management",
                "Member self service"
            };

            Assert.That(actualLinks, Is.EqualTo(expectedLinks));
        }
        finally
        {
            driver.Quit();
        }
    }
}