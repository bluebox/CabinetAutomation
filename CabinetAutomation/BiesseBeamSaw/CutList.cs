using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using CabinetAutomation.BiesseCabinet;
using System.Xml;

namespace CabinetAutomation.BiesseBeamSaw
{
	[XmlRoot]
	[Serializable]
	public class CutList
	{
		[XmlAttribute]
		public String NParts;

		[XmlAttribute]
		public String NBoards = "1";

		public BoardType BoardType;
		public List<BeamSawPart> beamSawParts;

		public CutList(BoardType boardType, PartList parts, bool GroupBySize)
		{

			BeamSawPartComparer c = new BeamSawPartComparer(); 

			this.BoardType = boardType;

			parts = parts.PartsAfterFilter(boardType);
			
			parts.Sort(new PartComparerForXmlCutlist());

			this.beamSawParts = new List<BeamSawPart>();

			for (int i = 0; i < parts.Count; i++)
			{
				Part part = parts[i];
				BeamSawPart beamSawPart = new BeamSawPart(parts[i]);

				if (!GroupBySize)
				{
					this.beamSawParts.Add(beamSawPart);

					continue;
				}

				if (this.beamSawParts.Count == 0)
				{
					this.beamSawParts.Add(beamSawPart);

					continue;
				}

				BeamSawPart lastBeamSawPart = this.beamSawParts.Last();

				if (c.Compare(lastBeamSawPart, beamSawPart) == 0)
				{
					lastBeamSawPart.qMin += beamSawPart.qMin;

					lastBeamSawPart.IDesc += "_" + beamSawPart.IDesc;
					lastBeamSawPart.IIDesc += "_" + beamSawPart.IIDesc;

					continue;
				}

				this.beamSawParts.Add(beamSawPart);
			}

			this.NParts = this.beamSawParts.Count.ToString();
		}

		public XmlElement MakeTree(XmlDocument document)
		{
			XmlElement c = document.CreateElement("CutList");

			c.SetAttribute("NParts", this.NParts);
			c.SetAttribute("NBoards", this.NBoards);

			for (int i = 0; i < this.beamSawParts.Count; i++)
			{
				BeamSawPart part = this.beamSawParts[i];
				
				c.AppendChild(part.MakeTree(document));
			}

			Board board = new Board();

			board.BoardType = this.BoardType;
			board.Thickness = this.BoardType.Thickness.ToString();
			c.AppendChild(board.MakeTree(document));
			document.AppendChild(c);

			return c;
		}
	}

	class BeamSawPartComparer : IComparer<BeamSawPart>
	{
		#region IComparer<BeamSawPart> Members

		/// <summary>
		/// This assumes that the materals for both part are same.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public int Compare(BeamSawPart x, BeamSawPart y)
		{
			int c;

			c = String.Compare(x.L, y.L);

			if (c != 0)
			{
				return c;
			}

			c = String.Compare(x.W, y.W);

			if (c != 0)
			{
				return c;
			}

			c = String.Compare(x.Grain, y.Grain);

			if (c != 0)
			{
				return c;
			}

			return 0;
		}

		#endregion
	}

	class PartComparerForXmlCutlist : IComparer<Part>
	{
		#region IComparer<Part> Members

		/**
		 * Compares by 
		 */
		public int Compare(Part x, Part y)
		{
			int c;

			c = String.Compare(x.Material, y.Material);

			if (c != 0)
			{
				return c;
			}

			c = String.Compare(x.Colour, y.Colour);

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

			return 0;
		}

		#endregion
	}

	/// <summary>
	///  <Part id='P15' L='110.00' W='720.00' qMin='1' Grain='0'></Part>
	/// </summary>
	public class BeamSawPart
	{
		[XmlAttribute]
		public String id;

		[XmlAttribute]
		public String L;
		[XmlAttribute]
		public String W;

		[XmlAttribute]
		public Int32 qMin = 1;

		[XmlAttribute]
		public String Grain = "0";

		public String IDesc;
		public String IIDesc;

		public BeamSawPart(Part part)
		{
			if (part.L == null)
			{
				throw new ArgumentNullException("part.L");
			}

			if (part.P == null)
			{
				throw new ArgumentNullException("part.P");
			}

			this.L = part.L.Value.ToString();
			this.W = part.P.Value.ToString();

			if (!String.IsNullOrEmpty(part.Grain))
			{
				this.Grain = part.Grain;
			}

			this.IDesc = String.Format("{0}__{1}__{2}", part.Code, part.OwnerName, part.Name);

			if (!String.IsNullOrEmpty(part.Code))
			{
				this.IIDesc = part.Material;
			}

			this.qMin = part.Quantity;
		}

		public XmlElement MakeTree(XmlDocument document)
		{
			XmlElement c = document.CreateElement("Part");

			c.SetAttribute("id", this.id);
			c.SetAttribute("L", this.L);
			c.SetAttribute("W", this.W);
			c.SetAttribute("Grain", this.Grain);

			c.SetAttribute("qMin", this.qMin.ToString());

			if (!String.IsNullOrEmpty(this.IDesc))
			{
				c.SetAttribute("IDesc", this.IDesc);
			}

			if (!String.IsNullOrEmpty(this.IIDesc))
			{
				c.SetAttribute("IIDesc", this.IIDesc);
			}

			return c;
		}
	}

	[Serializable]
	public class Board
	{
		[XmlAttribute]
		public String id = "B1";

		[XmlAttribute]
		public String L = "2420.00";
		[XmlAttribute]
		public String W = "1210.00";
		[XmlAttribute]
		public String Thickness;

		[XmlAttribute]
		public String TTrim = "0.00";
		[XmlAttribute]
		public String LTrim = "0.00";

		[XmlAttribute]
		public String MatNo = "0";
		[XmlAttribute]
		public String MatCode = "Default";

		[XmlAttribute]
		public String Qty = "55555";

		[XmlAttribute]
		public String Stock = "0";

		public BoardType BoardType;

		public XmlElement MakeTree(XmlDocument document)
		{
			XmlElement c = document.CreateElement("Board");

			c.SetAttribute("id", this.id);
			c.SetAttribute("L", this.L);
			c.SetAttribute("W", this.W);
			c.SetAttribute("Thickness", this.Thickness);

			c.SetAttribute("TTrim", this.TTrim);
			c.SetAttribute("LTrim", this.LTrim);
			c.SetAttribute("MatNo", this.MatNo);
			c.SetAttribute("MatCode", this.MatCode);
			c.SetAttribute("Qty", this.Qty);
			c.SetAttribute("Stock", this.Stock);

			return c;
		}
	}
}
