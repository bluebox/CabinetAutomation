using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zen.Barcode;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace CabinetAutomation
{
	/// <summary>
	/// Useless wrapper over 
	/// 
	/// http://barcoderender.codeplex.com/
	/// Barcode.Render.Release.3.1.10729
	/// </summary>
	public class BarcodeGenerator
	{
		private static Code128BarcodeDraw Code128 = BarcodeDrawFactory.Code128WithChecksum;
		private const Int32 Height = 80;

		public static void Test(String code)
		{
			String filePath = String.Format("{0}.Png", code);

			Save(code, filePath);

			System.Diagnostics.Process.Start(filePath);
		}

		public static void Save(String code, String filePath)
		{
			Image image = Code128.Draw(code, Height);
			String extension = Path.GetExtension(filePath);

			ImageFormat format;

			if (String.Compare(".jpg", extension, true) == 0)
			{
				format = ImageFormat.Jpeg;
			}
			else if (String.Compare(".png", extension, true) == 0)
			{
				format = ImageFormat.Png;
			}
			else
			{
				throw new ArgumentException("filePath must be jpg or png");
			}

			image.Save(filePath, format);
		}

		public static Image Get(String code)
		{
			return Code128.Draw(code, Height);

		}
	}
}
