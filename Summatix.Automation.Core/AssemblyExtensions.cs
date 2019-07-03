using System;
using System.IO;
using System.Reflection;

namespace Summatix.Automation.Core
{
	public static class AssemblyExtensions
	{
		public static string GetDirectoryName(this Assembly assembly)
		{
			var codeBase = Assembly.GetExecutingAssembly().CodeBase;
			var uri = new UriBuilder(codeBase);
			var path = Uri.UnescapeDataString(uri.Path);
			return Path.GetDirectoryName(path);
		}
	}
}
