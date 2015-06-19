using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CabinetAutomation.BiesseCNC
{
	public class BarcodeFormat
	{
		public const Int32 FullFilename = 0;

		/// <summary>
		/// First 4 character of folder name followed by 
		/// 4 charaters of folder name.
		/// </summary>
		public const Int32 Folder4Filename4 = 1;

		public static Int32 Default
		{
			get
			{
				return BarcodeFormat.Folder4Filename4;
			}
		}

		public static Int32 Parse(String s)
		{
			if (s.Equals("Filename"))
				return FullFilename;

			if (s.Equals("Folder - Filename"))
				return Folder4Filename4;

			return Default;
		}

	}
}
