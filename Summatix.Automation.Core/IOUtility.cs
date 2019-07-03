using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Summatix.Automation.Core
{
	public class IOUtility
	{
		public static void Copy(string srcFile, string dstFile, bool overwrite = true)
		{
			if (!File.Exists(srcFile)) return;
			string parentDirectory = null;
			var directoryInfo = new DirectoryInfo(dstFile).Parent;
			if (directoryInfo != null)
				parentDirectory = directoryInfo.FullName;

			if ((parentDirectory != null) && !Directory.Exists(parentDirectory))
				Directory.CreateDirectory(parentDirectory);
			File.Copy(srcFile, dstFile, overwrite);
		}

		public static byte[] ReadResourceFromUri(string resourceUri)
		{
			var tempFilePath = Path.GetTempFileName();
			byte[] imageData = null;

			try
			{
				using (var webClient = new WebClient())
				{
					webClient.DownloadFile(new Uri(resourceUri), tempFilePath);

					imageData = ReadFileBytes(tempFilePath);
				}
			}
			finally
			{
				File.Delete(tempFilePath);
			}

			return imageData;
		}

		public static byte[] ReadFileBytes(string srcFile)
		{
			var bytes = new List<byte>();

			using (var fileStream = File.OpenRead(srcFile))
			{
				int lastByte = fileStream.ReadByte();

				while (lastByte != -1)
				{
					bytes.Add((byte)lastByte);

					lastByte = fileStream.ReadByte();
				}
			}

			return bytes.ToArray();
		}

		public static void ResizeImage(Stream fileStream, string destinationFileName)
		{
			var image = Image.FromStream(fileStream);

			var height = 500;
			var width = image.Width * height / image.Height;

			var destRect = new Rectangle(0, 0, width, height);
			var destImage = new Bitmap(width, height);

			destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

			using (var graphics = Graphics.FromImage(destImage))
			{
				graphics.CompositingMode = CompositingMode.SourceCopy;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

				using (var wrapMode = new ImageAttributes())
				{
					wrapMode.SetWrapMode(WrapMode.TileFlipXY);
					graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
				}
			}

			destImage.Save(destinationFileName);
		}

	}
}
