using OpenQA.Selenium;

namespace matchingengine.Pages;

public class HomePage
{
    private readonly IWebDriver Driver;

    private const string Url = "https://www.matchingengine.com/";

    private readonly By SolutionsToggle = By.Id("nav-toggle-solutions");

    private readonly By SolutionLinks = By.CssSelector(
        "header section[role='menu'] [class*='BrandNavSubMenu_children'] a");

    private readonly By CookieButtons = By.CssSelector(
        "#CybotCookiebotDialogBodyButtonDecline, " +
        "#CybotCookiebotDialogBodyLevelButtonLevelOptinAllowallSelection");

    public HomePage(IWebDriver driver)
    {
        Driver = driver;
    }

    public void Open()
    {
        Driver.Navigate().GoToUrl(Url);
        Thread.Sleep(2000);

        var button = Driver.FindElements(CookieButtons)
            .FirstOrDefault(b => b.Displayed);

        if (button != null)
        {
            button.Click();
            Thread.Sleep(1000);
        }
    }

    public void ExpandSolutionsMenu()
    {
        Driver.FindElement(SolutionsToggle).Click();
        Thread.Sleep(1000);
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
        Driver.FindElement(By.LinkText("Distribution processing")).Click();
        Thread.Sleep(2000);
    }
}
