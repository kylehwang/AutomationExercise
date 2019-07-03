using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using Summatix.Automation.Configuration;
using System;
using System.IO;

namespace Summatix.Automation.WebDriver
{
	public class WebDriverFactory
	{
		private string _key;

		private IWebDriver _webDriver;

		public IWebDriver Create(string key)
		{
			if (_key == key && _webDriver != null) return _webDriver;

			_key = key;
			_webDriver = DoCreate(key);
			return _webDriver;
		}

		private IWebDriver DoCreate(string key)
		{
			switch (key)
			{
				case "Firefox":
					var firefoxOptions = new FirefoxOptions { AcceptInsecureCertificates = true };
					return new FirefoxDriver(firefoxOptions);
				case "Internet Explorer":
				case "IE":
					var ieOptions =
						new InternetExplorerOptions { IntroduceInstabilityByIgnoringProtectedModeSettings = true };
					return new InternetExplorerDriver(ieOptions);
				case "Chrome":
					var chromeOptions = new ChromeOptions();
					chromeOptions.AddUserProfilePreference("download.default_directory", TestingConstants.DownloadFilePath);
					if (!Directory.Exists(TestingConstants.DownloadFilePath)) Directory.CreateDirectory(TestingConstants.DownloadFilePath);

					chromeOptions.AddExcludedArgument("ignore-certificate-errors");
					chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
					chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
					return new ChromeDriver(chromeOptions);
				case "Safari":
					var options = new SafariOptions();
					options.AddAdditionalCapability(CapabilityType.BrowserName, "safari");
					options.AddAdditionalCapability(CapabilityType.PlatformName, "MAC");
					options.AddAdditionalCapability(CapabilityType.Version, "12");
					options.AddAdditionalCapability(CapabilityType.AcceptSslCertificates, true);
					options.AddAdditionalCapability(CapabilityType.IsJavaScriptEnabled, true);
					var capabilities = options.ToCapabilities();
					var driver = new RemoteWebDriver(new Uri(Settings.RemoteServerAddress), capabilities, TimeSpan.FromSeconds(120));
					driver.Manage().Timeouts().ImplicitWait = Settings.PageTimeout;
					return driver;
				case "ChromeOnAndroid":
					var androidCaps = new DesiredCapabilities();
					androidCaps.SetCapability("platformName", "Android");
					androidCaps.SetCapability("deviceName", Settings.DeviceName);
					androidCaps.SetCapability("browserName", MobileBrowserType.Chrome);
					androidCaps.SetCapability("timeouts", -1);
					androidCaps.SetCapability("download.prompt_for_download", false);
					// Dismiss the keyboard on the Android device
					androidCaps.SetCapability("unicodeKeyboard", true);
					androidCaps.SetCapability("resetKeyboard", true);
					androidCaps.SetCapability("clearSystemFiles", true);

					var androidDriver = new RemoteWebDriver(new Uri(Settings.RemoteServerAddress), androidCaps);
					androidDriver.Manage().Timeouts().ImplicitWait = Settings.PageTimeout;
					return androidDriver;
			}

			throw new ArgumentException(@"Invalid browser key", nameof(key));
		}
	}
}