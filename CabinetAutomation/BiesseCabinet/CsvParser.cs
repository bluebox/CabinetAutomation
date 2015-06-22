using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Windows.Forms;

namespace CabinetAutomation.BiesseCabinet
{
	/// <summary>
	/// Takes a csv output from basse cabinet and reads
	/// it into memory
	/// </summary>
	public class CsvParser
	{
		public Char[] CsvSplitCharacters = new Char[] { ';' };
		public PartList Parts = new PartList();
		private List<String> parseErrors = new List<String>();

		public static List<Decimal> AllowedHeight = new List<Decimal>(new Decimal[] {
			8, 18, 25
		});

		public CsvParser(String fileName)
		{
			this.Load(fileName);
		}

		public CsvParser()
		{
		}

		private void Load(String fileName)
		{
			this.Parts.Clear();

			using (TextFieldParser parser = new TextFieldParser(fileName))
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(";", "\t", ",");

				for (Int32 i = 0; !parser.EndOfData; i++)
				{
					String[] columns = parser.ReadFields();

					if (i == 0)
					{
						continue;
					}

					Part p = PartFromCsvLine(columns, i);

					if (p != null)
					{
						Console.WriteLine("{0}: {1} {2}", p.Code, p.Description, p.Type);

						// p.HinshitsuIntelligenceSetGrain();

						this.Parts.Add(p);
					}
				}
			}

			if (this.parseErrors.Count > 0)
			{
				StringBuilder sb = new StringBuilder();

				foreach(String s in this.parseErrors)
				{
					sb.AppendLine(s);
				}

				MessageBox.Show(sb.ToString());
			}
		}

		public void Log(String type, String message)
		{
			String line = String.Format("{0}: {1}", type, message);

			parseErrors.Add(line);

			Console.WriteLine(line);
		}

		public Part PartFromCsvLine(String[] columns, Int32 rowNumber)
		{
			if (null == columns)
			{
				return null;
			}

			if (columns.Length < 36)
			{
				Log("WARNING", String.Format("Ignoring row {0} with less than 32 columns", rowNumber));

				return null;
			}

			Part p = new Part();

			p.Code = columns[0].Trim();

			if (0 == p.Code.Length)
			{
				return null;
			}

			if ("Code".Equals(p.Code))
			{
				Log("INFO", String.Format("Ingore header row {0}.", rowNumber));

				return null;
			}

			p.Name = columns[1].Trim();

			try
			{
				p.L = Decimal.Parse(columns[2]);
			}
			catch (FormatException)
			{
				p.L = null;
			}

			try
			{
				p.H = Decimal.Parse(columns[3]);

				if (!AllowedHeight.Contains(p.H.Value))
				{
					String line = String.Format(
						"Row {0} ({1}, {2}) Height must be [8, 18, 25], found {3}",
						rowNumber, p.Code, p.Name, p.H);
					this.Log("WARNING", line);
				}
			}
			catch (FormatException)
			{
				p.H = null;
			}

			try
			{
				p.P = Decimal.Parse(columns[4]);
			}
			catch (FormatException)
			{
				p.P = null;
			}

			if (null != columns[5])
			{
				p.Grain = columns[5];
			}

			if (null != columns[6])
			{
				p.Colour = columns[6];
			}

			if (null != columns[7])
			{
				p.Material = columns[7];
			}

			if (null != columns[8])
			{
				p.Descrizione = columns[8];
			}

			if (null != columns[9])
			{
				p.Tipologia = columns[9];
			}

			if (null != columns[12])
			{
				p.FileCam1 = columns[12];
			}

			if (null != columns[13])
			{
				p.FileCam2 = columns[13];
			}

			try
			{
				p.Quantity = Int32.Parse(columns[14]);
			}
			catch (FormatException)
			{
				Console.WriteLine("Unable to parse Quantity for row {0}", rowNumber);

				return null;
			}

			//p.BordoSopra = columns[18];
			//p.BordoDestro = columns[19];
			//p.BordoSotto = columns[20];
			//p.BordoSinistro = columns[21];

			p.Customer = columns[22];
			p.BloccoAppartenenza = columns[23];
			p.TopEdgeName = columns[24];
			p.RightEdgeName = columns[25];
			p.BottomEdgeName = columns[26];
			p.LeftEdgeName = columns[27];

			if (columns.Length > 32 && null != columns[32])
			{
				p.OwnerName = columns[32];
			}

			return p;
		}
	}
}
