using matchingengine.Pages;
using matchingengine.TestData;

namespace matchingengine.Tests;

public class Tests : BaseTest
{
    [Test]
    public void SolutionsMenuTest()
    {
        var homePage = new HomePage(Driver);

        homePage.Open();
        homePage.ExpandSolutionsMenu();

        Assert.Multiple(() =>
        {
            Assert.That(homePage.IsSolutionsMenuExpanded(), Is.True);
            Assert.That(
                homePage.GetSolutionLinks(),
                Is.EqualTo(ExpectedContent.SolutionLinks));
        });
    }

    [Test]
    public void DistributionProcessingTest()
    {
        var homePage = new HomePage(Driver);

        homePage.Open();
        homePage.ExpandSolutionsMenu();
        homePage.OpenDistributionProcessing();

        var page = new DistributionProcessingPage(Driver);

        page.ScrollToSection();

        Assert.Multiple(() =>
        {
            Assert.That(page.IsCorrectUrl(), Is.True);
            Assert.That(
                page.GetHeading(),
                Is.EqualTo(ExpectedContent.SolutionsHeading));
            Assert.That(
                page.GetIntroduction(),
                Is.EqualTo(ExpectedContent.Introduction));
            Assert.That(
                page.GetBulletPoints(),
                Is.EqualTo(ExpectedContent.BulletPoints));
            Assert.That(
                page.GetCards(),
                Is.EqualTo(ExpectedContent.Cards));
        });
    }
}