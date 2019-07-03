
using BoDi;
using OpenQA.Selenium;
using Serilog;
using Summatix.Automation.Configuration;
using Summatix.Automation.WebDriver;
using System;
using TechTalk.SpecFlow;

namespace Summatix.Automation.Core.Hooks
{
	[Binding]
	public class BeforeAfterScenario
	{
		private readonly IObjectContainer _objectContainer;
		private readonly WebDriverFactory _webDriverFactory = new WebDriverFactory();
		private IWebDriver _webDriver;

		public BeforeAfterScenario(IObjectContainer objectContainer)
		{
			switch (Settings.Target)
			{
				case "Chrome":
				case "Safari":
				case "ChromeOnAndroid":
				case "SafariOniOS":
					_objectContainer = objectContainer;
					break;
				default:
					throw new PlatformNotSupportedException(
						$"Your target '{Settings.Target}' is not supported");
			}
		}

		[BeforeScenario(Order = 0)]
		public void BeforeScenario()
		{
			RegisterOptions();
			RegisterWebDriver();
		}

		[BeforeScenario("@Pending", Order = 1)]
		public void PendingScenario() => throw new PendingStepException();

		[AfterScenario(Order = 0)]
		public void AfterScenarioLogScenarioExceptions()
		{

			LogScenarioExceptions();

		}

		private void LogScenarioExceptions()
		{
			using (var log = new LoggerConfiguration()
				.WriteTo.Console()
				.CreateLogger())
			{
				var error = ScenarioContext.Current?.TestError;
				if (error == null) return;
				log.Error($"{ScenarioContext.Current.ScenarioInfo.Title} : {error.Message} : {error.StackTrace}");
				log.Error($"{error.InnerException}");
			}
		}

		[AfterScenario(Order = 1)]
		public void AfterScenarioCloseWebDriver()
		{
			DisposeWebDriver();
		}


		private void RegisterOptions()
		{
			// the various "portals" of the application will have different pages
			// and hence different URLs
			var options = new Options
			{
				SiteUri = new Uri(Settings.SiteUrl),
			};
			_objectContainer.RegisterInstanceAs(options);
		}

		private void RegisterWebDriver()
		{
			_webDriver = _webDriverFactory.Create(Settings.Target);
			_objectContainer.RegisterInstanceAs(_webDriver);
			_objectContainer.RegisterInstanceAs<IWebDriver>(_webDriver);
		}

		private void DisposeWebDriver()
		{
			_webDriver.Quit();
			_webDriver.Dispose();
		}
	}
}
