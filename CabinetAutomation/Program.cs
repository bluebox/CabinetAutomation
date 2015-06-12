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
			CsvParser parser = new CsvParser("basse-cabinet-output.csv");
			LabelGenerator gen = new LabelGenerator();

			gen.SaveToPdf(String.Format("test-{0}.pdf", DateTime.Now.Ticks), parser.Parts);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
//			Application.Run(new Form1());
		}
	}
}
