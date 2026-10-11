using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace matchingengine.Pages;

public class DistributionProcessingPage
{
    private readonly IWebDriver Driver;

    private const string Path =
        "/Music-and-copyright-solutions/Distribution-processing";
    private readonly By Section = By.Id("distribution-processing-overview");
    private readonly By Heading = By.TagName("h2");
    private readonly By Introduction = By.CssSelector("header p");
    private readonly By BulletPoints = By.CssSelector("header li");
    private readonly By Cards = By.CssSelector("[class*='Card_card']");
    private readonly By CardBody = By.CssSelector("[class*='Card_body']");
    private readonly By CardHeading = By.TagName("h5");

    public DistributionProcessingPage(IWebDriver driver)
    {
        Driver = driver;
    }

    public bool IsCorrectUrl()
    {
        return Driver.Url.Contains(Path);
    }

    public void ScrollToSection()
    {
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        var section = Driver.FindElement(Section);

        ((IJavaScriptExecutor)Driver).ExecuteScript(
            "arguments[0].scrollIntoView({block: 'center'});",
            section);

        wait.Until(d =>
            d.FindElement(Section)
                .FindElement(Heading).Displayed);
    }

    public string GetHeading()
    {
        return Driver.FindElement(Section)
            .FindElement(Heading).Text.Trim();
    }

    public string GetIntroduction()
    {
        return Driver.FindElement(Section)
            .FindElement(Introduction).Text.Trim();
    }

    public List<string> GetBulletPoints()
    {
        return Driver.FindElement(Section)
            .FindElements(BulletPoints)
            .Select(item => item.Text.Trim())
            .ToList();
    }

    public List<(string Title, string Description)> GetCards()
    {
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));

        wait.Until(d =>
            d.FindElement(Section)
                .FindElements(Cards)
                .Count == 6);

        var cards = Driver.FindElement(Section)
            .FindElements(Cards);

        return cards.Select(card => (
            Title: card.FindElement(CardHeading).Text.Trim(),
            Description: card.FindElement(
                CardBody).Text.Trim()
        )).ToList();
    }
}
