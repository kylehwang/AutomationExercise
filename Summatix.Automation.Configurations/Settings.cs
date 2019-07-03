using System;

namespace Summatix.Automation.Configuration
{
	public class Settings
	{
		public static TimeSpan MaxEmailWaitTime = new TimeSpan(0, Convert.ToInt32(SumConfigurationManager.GetSetting<string>("MaxEmailWaitTime")), 0);
		public static TimeSpan PageTimeout => new TimeSpan(0, 0, Convert.ToInt32(SumConfigurationManager.GetSetting<string>("PageTimeOut")));

		//public static string DeviceSimulatorUri = SummatixConfigurationManager.GetSetting<string>("DeviceSimulatorUri");

		public static string SummatixConnectionString = SumConfigurationManager.GetConnectionString("Azure.Sql.Connection");

		public static string Target => SumConfigurationManager.GetSetting<string>("Target");

		public static string SiteUrl => SumConfigurationManager.GetSetting<string>("Web.Url");

		// Mobile Web Automation
		public static string RemoteServerAddress => SumConfigurationManager.GetSetting<string>("RemoteServerAddress");

		public static string AppleDeveloperTeamId => SumConfigurationManager.GetSetting<string>("AppleDeveloperTeamId");

		public static string PlatformVersion => SumConfigurationManager.GetSetting<string>("PlatformVersion");

		// Device name and unique device identifier of the connected physical device
		public static string DeviceName => SumConfigurationManager.GetSetting<string>("DeviceName");

		public static string DeviceId => SumConfigurationManager.GetSetting<string>("DeviceId");

		public static string SeqAddress => SumConfigurationManager.GetSetting<string>("Seq.Address");

		public static string MinimumLogLevel => SumConfigurationManager.GetSetting<string>("MinimumLoggingLevel");
	}
}
