using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace CabinetAutomation.Cix
{

	/// <summary>
	/// Reads a cix file and updates the vertical repetation VTR to 2.
	/// TODO: Reads a cix file and updates orgin list ORLST depending
	/// on the part.
	/// </summary>
	public class Updater
	{
		public readonly String file;
		private static Regex BeginMacroRegex = new Regex(@"BEGIN MACRO(.*?)END MACRO", RegexOptions.Compiled | RegexOptions.Singleline);
		private static Regex SideRegex = new Regex(@"PARAM,NAME=SIDE,VALUE=[1-3]", RegexOptions.Compiled | RegexOptions.Singleline);

		public Updater(String file)
		{
			this.file = file;
		}

		public void DoMagic()
		{
			String s = File.ReadAllText(file);
			String d = Path.GetDirectoryName(file);
			String d2 = Path.Combine(d, "HinshitsuCIX");
			String f2 = Path.Combine(d2, Path.GetFileName(file));

			if (!Directory.Exists(d2))
				Directory.CreateDirectory(d2);

			StringBuilder sb = new StringBuilder(s);


			foreach (Match m in BeginMacroRegex.Matches(s))
			{
				int beginIndex = m.Index;
				String ms = m.Value;
				Boolean bh = ms.Contains("NAME=BH");

				if (!bh)
				{
					continue;
				}

				MatchCollection sides = SideRegex.Matches(ms);

				if (sides.Count == 0)
				{
					continue;
				}

				int vtrIndex = ms.IndexOf("PARAM,NAME=VTR,VALUE=0");

				vtrIndex += beginIndex + "PARAM,NAME=VTR,VALUE=0".Length - 1;

				char ch = sb[vtrIndex];

				Debug.Assert(Char.IsNumber(ch));

				sb[vtrIndex] = '2';

			}

			File.WriteAllText(f2, sb.ToString());
		}
	}
}
