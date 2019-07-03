using Summatix.Automation.Configuration;
using System.Net;
using System.Net.Security;
using TechTalk.SpecFlow;

namespace Summatix.Automation.Core.Hooks
{
	[Binding]
	public class BeforeAfterFeature
	{
		[BeforeFeature(Order = 0)]
		public static void BeforeFeature()
		{
			EnableTrustedHosts();

			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
		}

		[AfterFeature(Order = 0)]
		public static void AfterFeature()
		{
			// nothing to do
		}

		[AfterFeature(Order = int.MaxValue)]
		public static void CleanupChecks()
		{
			// implement checks to make sure that resources are being cleaned up  after each feature
		}

		private static void EnableTrustedHosts()
		{
			ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) =>
			{
				if (errors == SslPolicyErrors.None)
				{
					return true;
				}
				if (!(sender is HttpWebRequest request))
					return false;
				var hostName = Settings.SiteUrl;
				return hostName.Contains(request.RequestUri.Host);
			};
		}

	}
}