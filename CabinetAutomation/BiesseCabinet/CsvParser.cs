using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualBasic.FileIO;

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

		public CsvParser(String fileName)
		{
			this.Load(fileName);
		}

		public CsvParser()
		{
		}

		public void Load(String fileName)
		{
			this.Parts.Clear();

			using (TextFieldParser parser = new TextFieldParser(fileName))
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(";", "\t", ",");

				for (Int32 i = 0; !parser.EndOfData; i++)
				{
					String[] parts = parser.ReadFields();

					if (i == 0)
					{
						continue;
					}

					Part p = Part.FromCsvLine(parts, i);

					if (p != null)
					{
						Console.WriteLine("{0}: {1} {2}", p.Code, p.Description, p.Type);

						p.HinshitsuIntelligentGrainRotate();

						this.Parts.Add(p);
					}
				}
			}
		}
	}
}
