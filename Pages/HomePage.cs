using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace matchingengine.Pages;

public class HomePage
{
    private readonly IWebDriver Driver;

    private const string Url = "https://www.matchingengine.com/";

    private const string Path =
        "/Music-and-copyright-solutions/Distribution-processing";
    private readonly By SolutionsToggle = By.Id("nav-toggle-solutions");
    private readonly By SolutionLinks = By.CssSelector(
        "header section[role='menu'] [class*='BrandNavSubMenu_children'] a");
    private readonly By CookieButtons = By.CssSelector(
        "#CybotCookiebotDialogBodyButtonDecline, " +
        "#CybotCookiebotDialogBodyLevelButtonLevelOptinAllowallSelection");
    private readonly By DistributionProcessingLink = By.LinkText("Distribution processing");

    public HomePage(IWebDriver driver)
    {
        Driver = driver;
    }

    public void Open()
    {
        Driver.Navigate().GoToUrl(Url);

        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

        wait.Until(d =>
            ((IJavaScriptExecutor)d).ExecuteScript(
                "return document.readyState")?.ToString() == "complete");

        var cookieButton = Driver.FindElements(CookieButtons)
            .FirstOrDefault(button => button.Displayed);

        if (cookieButton != null)
        {
            cookieButton.Click();

            wait.Until(d => !d.FindElements(CookieButtons)
                .Any(button => button.Displayed));
        }
    }

    public void ExpandSolutionsMenu()
    {
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

        var solutions = wait.Until(d =>
        {
            var element = d.FindElement(SolutionsToggle);
            return element.Displayed && element.Enabled ? element : null;
        });

        if (solutions.GetAttribute("aria-expanded") != "true")
        {
            solutions.Click();
        }

        wait.Until(d =>
            d.FindElement(SolutionsToggle)
            .GetAttribute("aria-expanded") == "true");
    }

    public bool IsSolutionsMenuExpanded()
    {
        return Driver.FindElement(SolutionsToggle)
            .GetAttribute("aria-expanded") == "true";
    }

    public List<string> GetSolutionLinks()
    {
        return Driver.FindElements(SolutionLinks)
            .Where(link => link.Displayed)
            .Select(link => link.Text.Trim())
            .Where(text => text != "")
            .ToList();
    }

    public void OpenDistributionProcessing()
    {
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

        wait.Until(d =>
        {
            var link = d.FindElement(DistributionProcessingLink);
            return link.Displayed && link.Enabled ? link : null;
        }).Click();

        wait.Until(d => d.Url.Contains(Path));
    }
}
