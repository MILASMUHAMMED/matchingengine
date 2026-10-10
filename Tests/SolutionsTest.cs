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

    [Test]
    public void DistributionProcessingTest()
    {
        IWebDriver driver = new ChromeDriver();

        try
        {
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.matchingengine.com/");

            Thread.Sleep(2000);

            var cookieButtons = driver.FindElements(
                By.CssSelector("#CybotCookiebotDialogBodyButtonDecline, #CybotCookiebotDialogBodyLevelButtonLevelOptinAllowallSelection"));

            var cookieButton = cookieButtons.FirstOrDefault(button => button.Displayed);

            if (cookieButton != null)
            {
                cookieButton.Click();
                Thread.Sleep(1000);
            }

            driver.FindElement(By.Id("nav-toggle-solutions")).Click();

            Thread.Sleep(1000);

            driver.FindElement(By.LinkText("Distribution processing")).Click();

            Thread.Sleep(2000);

            Assert.That(driver.Url, Does.Contain("/Music-and-copyright-solutions/Distribution-processing"));

            var section = driver.FindElement(By.Id("distribution-processing-overview"));

            ((IJavaScriptExecutor)driver).ExecuteScript(
            "arguments[0].scrollIntoView({block: 'center'});", section);

            Thread.Sleep(2000);

            var heading = section.FindElement(By.TagName("h2")).Text.Trim();

            var expectedHeading = "All-in-one solution for scale";

            Assert.That(heading, Is.EqualTo(expectedHeading));

            var intro = section.FindElement(By.CssSelector("header p")).Text.Trim();

            var expectedIntro = "Imagine an technology solution for collective management organisations that stays ahead of industry trends.";

            Assert.That(intro, Is.EqualTo(expectedIntro));

            var bullets = section.FindElements(By.CssSelector("header li"))
                .Select(item => item.Text.Trim())
                .ToList();

            var expectedBullets = new List<string>
            {
                    "Distribute royalty payments quickly",
                    "Provide full detail of music usage to members",
                    "Reduce cost-to-distribution ratios."
            };

            Assert.That(bullets, Is.EqualTo(expectedBullets));

            var cards = section.FindElements(
                By.CssSelector("[class*='Card_card']"));

            var actualCards = cards.Select(card => new
            {
                Title = card.FindElement(By.TagName("h5")).Text.Trim(),
                Description = card.FindElement(
                    By.CssSelector("[class*='Card_body']")).Text.Trim()
            }).ToList();

            var expectedCards = new List<(string Title, string Description)>
            {
                (
                    "Prevent missing payments",
                    "Matching Engine has advanced matching capability that ensures maximum automation to get money to Collective Management Organisation members quickly, while also preventing any missing payments."
                ),
                (
                    "Comply with international standards",
                    "The system complies with international metadata and encoding standards (ISWC, ISRC, DDEX, Unicode), and supports multilingual and non-latin script works."
                ),
                (
                    "Run analytics and queries",
                    "Collective Management Organisations can use Rest API to integrate other parts of their system. Its full data lakehouse gives access to underlying repertoire data to run analytics and queries."
                ),
                (
                    "Manage different collection share pictures",
                    "Matching Engine is designed to manage different collection share pictures based on territories and right types. This includes advanced support for carve outs and carve ins."
                ),
                (
                    "Minimise manual intervention",
                    "Advanced agreement support facilitates implementation of direct licensing carve outs and carve ins, without manual intervention on a work-by-work basis."
                ),
                (
                    "Tackle fluctuating data volumes",
                    "The Matching Engine system is based on a modern cloud-based architecture that scales dynamically to support fluctuating data volumes for Collective Management Organisations."
                )
            };

            Assert.That(
                actualCards.Select(card => (card.Title, card.Description)).ToList(),
                Is.EqualTo(expectedCards));
        }
        finally
        {
            driver.Quit();
        }
    }

}