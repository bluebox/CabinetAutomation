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

		public List<BeamSawPart> parts;

		public CutList(Decimal thickness, PartList parts)
		{
			parts = parts.Expand().Filter(thickness);
			parts.Sort();

			this.parts = new List<BeamSawPart>();

			for (int i = 0; i < parts.Count; i++)
			{
				Part part = parts[i];

				if (!part.Height.HasValue || !thickness.Equals(part.Height))
				{
					continue;
				}

				BeamSawPart beamSawPart = new BeamSawPart(part);

				beamSawPart.id = String.Format("P{0}", i + 1);

				this.parts.Add(beamSawPart);
			}

			this.NParts = this.parts.Count.ToString();

		}

		public XmlElement MakeTree(XmlDocument document, Decimal thickness)
		{
			XmlElement c = document.CreateElement("CutList");

			c.SetAttribute("NParts", this.NParts);
			c.SetAttribute("NBoards", this.NBoards);

			for (int i = 0; i < this.parts.Count; i++)
			{
				BeamSawPart part = this.parts[i];
				
				c.AppendChild(part.MakeTree(document));
			}

			Board board = new Board();

			board.Thickness = thickness.ToString();
			c.AppendChild(board.MakeTree(document));
			document.AppendChild(c);

			return c;
		}
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

//		[XmlAttribute]
//		public String qMin;
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

			if (!String.IsNullOrEmpty(part.FileCam1))
			{
				this.IDesc = part.FileCam1;
			}

			if (!String.IsNullOrEmpty(part.Code))
			{
				this.IIDesc = part.Code;
			}
		}

		public XmlElement MakeTree(XmlDocument document)
		{
			XmlElement c = document.CreateElement("Part");

			c.SetAttribute("id", this.id);
			c.SetAttribute("L", this.L);
			c.SetAttribute("W", this.W);
			c.SetAttribute("Grain", this.Grain);

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
