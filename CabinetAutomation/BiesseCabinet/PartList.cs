using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CabinetAutomation.BiesseBeamSaw;

namespace CabinetAutomation.BiesseCabinet
{
	public class PartList : List<Part>
	{
		/// <summary>
		/// Expands a part with quantity q into q parts each of quantity 1.
		/// </summary>
		private Boolean expanded = false;

		public Boolean Expanded
		{
			get
			{
				return this.expanded;
			}
		}

		private static Dictionary<String, Int32> ColumnHeaders;

		static PartList()
		{
			ColumnHeaders = new Dictionary<String, Int32>();

			String h1 = "Code	Name	L	H	P	Grain	Colour	Material	DESCRIZIONE	TIPOLOGIA	AREALAVORO1	AREALAVORO2	FILECAM1	FILECAM2	Quantity	L2	H2	P2	BORDOSOPRA	BORDODESTRO	BORDOSOTTO	BORDOSINISTRO	Customer	BloccoAppartenenza	TOPEDGENAME	RIGHTEDGENAME	BOTTOMEDGENAME	LEFTEDGENAME	PRIORITYTOP	PRIORITYRIGHT	PRIORITYBOTTOM	PRIORITYLEFT	OWNERNAME	RAWL	RAWH	RAWP	NAMEVENEERPLANE	NAMEVENEERBELOW	IDVENEERPLANE	IDVENEERBELOW";
			String[] h2 = h1.Split('\t');

			for (int i = 0; i < h2.Length; i++)
			{
				String h = h2[i];

				ColumnHeaders[h] = i;
			}			
		}

		public HashSet<Decimal> ThicknessValues
		{
			get
			{
				HashSet<Decimal> values = new HashSet<Decimal>();

				foreach (Part p in this)
				{
					if (p.H.HasValue)
					{
						values.Add(p.H.Value);
					}
				}

				return values;
			}
		}

		public HashSet<BoardType> BoardTypes
		{
			get
			{
				HashSet<BoardType> boardTypes = new HashSet<BoardType>();

				foreach (Part p in this)
				{
					if (p.H.HasValue)
					{
						boardTypes.Add(p.BoardType);
					}
				}

				return boardTypes;
			}
		}

		public PartList PartsAfterFilter(Decimal thickness)
		{
			PartList parts = new PartList();

			parts.expanded = this.expanded;

			foreach (Part p in this)
			{
				if (p.H.HasValue && p.H.Value == thickness)
				{
					parts.Add(new Part(p));
				}
			}

			return parts;
		}

		public PartList PartsWithoutFileCamX()
		{
			PartList parts = new PartList();

			parts.expanded = this.expanded;

			foreach (Part p in this)
			{
				if (!String.IsNullOrEmpty(p.FileCamX))
				{
					parts.Add(new Part(p));
				}
			}

			return parts;
		}

		public PartList Multiply(Int32 quantity)
		{
			PartList parts = new PartList();

			foreach (Part p in this)
			{
				Part copy = p.Clone();

				copy.Quantity *= quantity;

				parts.Add(copy);
			}

			return parts;
		}

		public PartList PartsAfterExpanding()
		{
			PartList parts = new PartList();

			parts.expanded = true;

			foreach (Part p in this)
			{
				for (int i = 0; i < p.Quantity; i++)
				{
					Part copy = new Part(p);

					copy.Quantity = 1;
					parts.Add(copy);
				}
			}

			return parts;
		}
	}
}
