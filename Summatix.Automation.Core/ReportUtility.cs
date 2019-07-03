using System.IO;
using System.Reflection;
using System;
using System;
using System.Diagnostics;
using System.Text;

namespace Summatix.Automation.Core
{
	public class ReportUtility
	{
		private const string ReportsFolderName = "Reports";
		private const string JavaScriptFileExt = "*.js";

		public static void CopyReportJavascriptFiles()
		{
			var srcReportDir = Path.Combine(Assembly.GetExecutingAssembly().GetDirectoryName(),
				ReportsFolderName);
			var srcJsFiles = Directory.GetFiles(srcReportDir, JavaScriptFileExt);
			var dstReportDir = Path.Combine(Directory.GetCurrentDirectory(), ReportsFolderName);

			foreach (var srcJsFile in srcJsFiles)
			{
				var srcJsFileName = Path.GetFileName(srcJsFile);
				if (srcJsFileName == null) continue;
				var dstJsFile = Path.Combine(dstReportDir, srcJsFileName);

				if (File.Exists(dstJsFile))
					continue;
				IOUtility.Copy(srcJsFile, dstJsFile);
			}
		}
	}
}