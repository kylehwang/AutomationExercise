using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Summatix.Automation.Configuration;

namespace Summatix.Automation.Pages.Common
{
	public abstract class BasePage
	{
		WebDriverWait wait;
		protected BasePage(IWebDriver webDriver)
		{
			WebDriver = webDriver;
			wait = new WebDriverWait(WebDriver, TestingConstants.PageTimeout);
		}

		protected IWebDriver WebDriver { get; }
		public string PageTitleName => WebDriver.Title;
		public object PageTitle { get; private set; }

		public virtual bool VerifyPage(string title)
		{
			return title.Equals(PageTitleName);
		}
	}
}
