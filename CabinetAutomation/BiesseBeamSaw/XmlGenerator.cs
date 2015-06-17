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
		public void Generate(Int32 quantity, PartList parts, String outputFilePathFormat)
		{
			parts = parts.Multiply(quantity);
			
			HashSet<Decimal> thicknessValues = parts.ThicknessValues;

			foreach (Decimal thickness in thicknessValues)
			{
				String outputFilePath = String.Format(outputFilePathFormat, thickness);
				CutList cutList = new CutList(thickness, parts);

				XmlDocument document = new XmlDocument();

				cutList.MakeTree(document, thickness);

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
