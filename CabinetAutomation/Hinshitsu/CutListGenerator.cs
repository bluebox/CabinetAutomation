using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CabinetAutomation.BiesseCabinet;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CabinetAutomation.Hinshitsu
{
	public class CutListGenerator
	{
		private static Dictionary<String, String> NameMap = new Dictionary<string, string>();

		static CutListGenerator()
		{
			CutListGenerator.NameMap.Add("LEFT LATERAL SIDE", "SIDE");
			CutListGenerator.NameMap.Add("RIGHT LATERAL SIDE", "SIDE");
		}

		protected PartList parts;
		PartComparerForCutList comparer = new PartComparerForCutList();
		private Boolean grouped = true;

		public CutListGenerator(PartList parts, Boolean grouped)
		{
			this.parts = parts.Clone();
			this.grouped = grouped;
		}

		private PartList Group(PartList list)
		{
			PartList grouped = new PartList();

			for (int i = 0; i < parts.Count; i++)
			{
				Part p = parts[i];

				if (grouped.Count == 0)
				{
					grouped.Add(p);

					continue;
				}

				Part last = grouped.Last();

				if (this.comparer.Compare(last, p) == 0)
				{
					last.Quantity += p.Quantity;

					continue;
				}

				grouped.Add(p);
			}

			return grouped;
		}

		public void Generate(String filePath)
		{
			foreach (Part p in this.parts)
			{
				p.Name = GetMappedName(p.Name);
			}

			this.parts.Sort(this.comparer);

			if (this.grouped)
			{
				this.parts = this.Group(this.parts);
			}

			FileInfo fileInfo = new FileInfo(filePath);

			if (fileInfo.Exists)
			{
				fileInfo.Delete();
			}

			ExcelPackage excel = new ExcelPackage(fileInfo);
			var worksheet = excel.Workbook.Worksheets.Add("CutList");
			int r = 1;
			int c;

			c = 1;

			worksheet.Cells[r, c++].Value = "OwnerName";
			worksheet.Cells[r, c++].Value = "Name";
			worksheet.Cells[r, c++].Value = "L";
			worksheet.Cells[r, c++].Value = "H";
			worksheet.Cells[r, c++].Value = "P";
			worksheet.Cells[r, c++].Value = "Grain";
			worksheet.Cells[r, c++].Value = "Material";
			worksheet.Cells[r, c++].Value = "Quantity";
			
			r++;

			for (int i = 0; i < parts.Count; i++)
			{
				Part part = parts[i];

				if (i != 0)
				{
					Part lastPart = parts[i - 1];

					if (!lastPart.OwnerName.Equals(part.OwnerName))
					{
						this.AddRowTitle(worksheet, ref r, 8, part.OwnerName);
					}
				}
				else
				{
					this.AddRowTitle(worksheet, ref r, 8, part.OwnerName);
				}
				
				c = 1;
	
				worksheet.Cells[r, c++].Value = part.OwnerName;
				worksheet.Cells[r, c++].Value = part.Name;
				worksheet.Cells[r, c++].Value = part.L;
				worksheet.Cells[r, c++].Value = part.H;
				worksheet.Cells[r, c++].Value = part.P;

				try
				{
					worksheet.Cells[r, c++].Value = int.Parse(part.Grain);
				}
				catch(FormatException)
				{
					worksheet.Cells[r, c++].Value = part.Grain;
				}

				worksheet.Cells[r, c++].Value = part.Material;
				worksheet.Cells[r, c++].Value = part.Quantity;

				r++;
			}

			excel.Save();
		}

		private void AddRowTitle(ExcelWorksheet worksheet, ref int r, int columnCount, string title)
		{
			int c = 1;

			worksheet.Cells[r, c].Value = title;
			worksheet.Cells[r, c, r, c + columnCount - 1].Merge = true;
			worksheet.Cells[r, c, r, c + columnCount - 1].Style.Font.Bold = true;
			worksheet.Cells[r, c, r, c + columnCount - 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

			r++;
		}

		public static String GetMappedName(String name)
		{
			if (CutListGenerator.NameMap.ContainsKey(name))
			{
				return CutListGenerator.NameMap[name];
			}

			return name;
		}
	}

	class PartComparerForCutList : IComparer<Part>
	{
		#region IComparer<Part> Members

		public int Compare(Part x, Part y)
		{
			int c = 0;

			c = String.Compare(x.OwnerName, y.OwnerName);

			if (c != 0)
			{
				return c;
			}

			c = String.Compare(x.Material, y.Material);

			if (c != 0)
			{
				return c;
			}

			c = String.Compare(x.Name, y.Name);

			if (c != 0)
			{
				return c;
			}

			if (x.L.HasValue && y.L.HasValue)
			{
				c = Decimal.Compare(x.L.Value, y.L.Value);

				if (c != 0)
				{
					return c;
				}
			}

			if (x.H.HasValue && y.H.HasValue)
			{
				c = Decimal.Compare(x.H.Value, y.H.Value);

				if (c != 0)
				{
					return c;
				}
			}

			if (x.P.HasValue && y.P.HasValue)
			{
				c = Decimal.Compare(x.P.Value, y.P.Value);

				if (c != 0)
				{
					return c;
				}
			}

			c = String.Compare(x.Grain, y.Grain);

			if (c != 0)
			{
				return c;
			}

			return c;
		}

		#endregion
	}
}
