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
			parts = parts.Multiply(this.Quantity);
			
			foreach (BoardType boardType in parts.BoardTypes)
			{
				String outputFilePath = String.Format(outputFilePathFormat, boardType);
				CutList cutList = new CutList(boardType, parts);

				XmlDocument document = new XmlDocument();

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
