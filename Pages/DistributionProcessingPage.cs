using OpenQA.Selenium;

namespace matchingengine.Pages;

public class DistributionProcessingPage
{
    private readonly IWebDriver Driver;

    private const string Path =
        "/Music-and-copyright-solutions/Distribution-processing";

    private readonly By Section = By.Id("distribution-processing-overview");

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
        var section = Driver.FindElement(Section);

        ((IJavaScriptExecutor)Driver).ExecuteScript(
            "arguments[0].scrollIntoView({block: 'center'});",
            section);

        Thread.Sleep(2000);
    }

    public string GetHeading()
    {
        return Driver.FindElement(Section)
            .FindElement(By.TagName("h2")).Text.Trim();
    }

    public string GetIntroduction()
    {
        return Driver.FindElement(Section)
            .FindElement(By.CssSelector("header p")).Text.Trim();
    }

    public List<string> GetBulletPoints()
    {
        return Driver.FindElement(Section)
            .FindElements(By.CssSelector("header li"))
            .Select(item => item.Text.Trim())
            .ToList();
    }

    public List<(string Title, string Description)> GetCards()
    {
        var cards = Driver.FindElement(Section)
            .FindElements(By.CssSelector("[class*='Card_card']"));

        return cards.Select(card => (
            Title: card.FindElement(By.TagName("h5")).Text.Trim(),
            Description: card.FindElement(
                By.CssSelector("[class*='Card_body']")).Text.Trim()
        )).ToList();
    }
}
