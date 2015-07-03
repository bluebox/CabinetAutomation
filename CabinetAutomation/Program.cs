using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CabinetAutomation.Cix;
using System.IO;

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
			// CsvParser parser = new CsvParser("basse-cabinet-output.csv");
			// LabelGenerator gen = new LabelGenerator();

			// gen.SaveToPdf(String.Format("test-{0}.pdf", DateTime.Now.Ticks), parser.Parts);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new CabinetAutomation());

			//using (StreamWriter sw = new StreamWriter("004pTPPN-modified.cix"))
			//{
			//    using (Tokenizer p = new Tokenizer("004pTPPN.cix"))
			//    {
			//        while (true)
			//        {
			//            object token = p.Next();

			//            if (token == null)
			//            {
			//                break;
			//            }

			//            Console.Write("_");
			//            Console.Write(token);

			//            sw.Write(token);
			//        }
			//    }
			//}

			//System.Diagnostics.Process.Start("004pTPPN-modified.cix");
		}
	}
}
