using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CabinetAutomation
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// new CsvParser("basse-cabinet-output.csv");
			BarcodeGenerator.Test("Hinshitsu");

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
