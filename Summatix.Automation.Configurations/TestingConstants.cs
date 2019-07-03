using System;

namespace Summatix.Automation.Configuration
{
	public class TestingConstants
	{
		/// <summary>
		/// Timeout for selenium operations.
		/// </summary>
		public static TimeSpan PageTimeout => Settings.PageTimeout;

		public static int PageSize = 25;
		/// <summary>
		/// Some scenarios require multiple devices to be registered and/or activated.
		/// This is the number of devices that should be tested for these scenarios.
		/// </summary>
		public static int ActiveDevicesToTest = 10;

		/// <summary>
		/// Similar concept to defining a number of active devices that we want to test
		/// with. However this is for "Pending" devices.
		/// Devices in "Pending" status haven't completed registration.
		/// </summary>
		public static int NumberOfPendingDevices = 3;

		/// <summary>
		/// Specify the maximum number of medical history items (medical conditions, medications, etc)
		/// that a patient can have added when a random generation happens
		/// </summary>
		public static int MaxNumOfMedicalHistoryItems = 10;

		/// <summary>
		/// The default test account email address.
		/// If the email couldn't be determined by looking at environment variables, then this email is used.
		/// </summary>
		public const string DefaultTestEmail = "";

		/// <summary>
		/// This is the password for the test gmail account above.
		/// </summary>
		public static string TestGmailPassword = "";

		public static readonly string TestAccountPassword = "";

		public static readonly string TestOperationsAccountPassword = "";

		public static string DownloadFilePath = @"C:\Temp\TestData";
	}
}