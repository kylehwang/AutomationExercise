using OpenQA.Selenium;
using Summatix.Automation.Core;
using Summatix.Automation.Pages;
using TechTalk.SpecFlow;

namespace Summatix.Automation.Tests.Steps
{
	[Binding]
	public class LogInSteps : BaseSteps
	{
		private readonly GoogleHomePage _googleHomePage;
		public LogInSteps(IWebDriver webDriver,
			GoogleHomePage googleHomePage,
			Options options) : base(webDriver, options)
		{
			_googleHomePage = googleHomePage;
		}

		[Given(@"I have navigated to Google home pahe")]
		public void GivenIHaveNavigatedToGoogleHomePahe()
		{
			WebDriver.Navigate().GoToUrl(Options.SiteUri);
		}
	}
}
