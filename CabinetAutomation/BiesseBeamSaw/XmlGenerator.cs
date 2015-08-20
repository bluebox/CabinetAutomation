using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CabinetAutomation.BiesseCabinet;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace CabinetAutomation.BiesseBeamSaw
{
	public class XmlGenerator
	{
		public Int32 Quantity;
		public PartList parts;
		public Int32 GrainType;

		public XmlGenerator()
		{
			this.Quantity = 1;
			this.parts = new PartList();
		}

		public XmlGenerator(PartList parts)
		{
			this.Quantity = 1;
			this.parts = parts.Clone();
		}

		public void Generate(String outputFilePathFormat)
		{
			if (this.Quantity != 1)
			{
				this.parts = this.parts.Multiply(this.Quantity);
			}

			foreach (BoardType boardType in this.parts.BoardTypes)
			{
				foreach (bool grouped in new bool[] { false, true })
				{
					String outputFilePath = String.Format(outputFilePathFormat, grouped ? "" : "/ungrouped", boardType);
					CutList cutList = new CutList(boardType, parts, grouped);
					XmlDocument document = new XmlDocument();

					Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));

					cutList.MakeTree(document);

					var settings = new XmlWriterSettings
					{
						Indent = true,
						IndentChars = @"    ",
						NewLineChars = Environment.NewLine,
						NewLineHandling = NewLineHandling.Replace,
					};

					using (TextWriter textWriter = new StreamWriter(outputFilePath))
					{
						using (var writer = XmlWriter.Create(textWriter, settings))
						{
							document.Save(writer);
						}
					}
				}
			}
		}
	}
}
