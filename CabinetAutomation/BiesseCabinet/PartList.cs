using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CabinetAutomation.BiesseBeamSaw;

namespace CabinetAutomation.BiesseCabinet
{
	public class PartList : List<Part>, ICloneable
	{
		/// <summary>
		/// Expands a part with quantity q into q parts each of quantity 1.
		/// </summary>
		private Boolean expanded = false;
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

		public PartList()
		{
		}

		public PartList(PartList partList)
		{
			this.expanded = partList.Expanded;

			foreach (Part p in partList)
			{
				this.Add(p.Clone());
			}
		}

		public Boolean Expanded
		{
			get
			{
				return this.expanded;
			}
		}

		public HashSet<BoardType> BoardTypes
		{
			get
			{
				IEqualityComparer<BoardType> equalityComparer = new BoardType(String.Empty, String.Empty, 0);
				HashSet<BoardType> boardTypes = new HashSet<BoardType>(equalityComparer);

				foreach (Part p in this)
				{
					BoardType boardType = p.BoardType;

					if (p.H.HasValue && !boardTypes.Contains<BoardType>(boardType))
					{
						boardTypes.Add(boardType);
					}
				}

				return boardTypes;
			}
		}

		public PartList PartsAfterFilter(BoardType boardType)
		{
			PartList parts = new PartList();

			parts.expanded = this.expanded;

			foreach (Part p in this)
			{
				if (p.H.HasValue && p.BoardType.Equals(boardType))
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
			PartList parts = this.Clone();

			foreach (Part p in this)
			{
				p.Quantity *= quantity;
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

		#region ICloneable Members

		/// <summary>
		/// Deep clones the current PartList
		/// </summary>
		/// <returns>Copy of current PartList</returns>
		public PartList Clone()
		{
			return new PartList(this);
		}

		#endregion

		#region ICloneable Members

		object ICloneable.Clone()
		{
			return new PartList(this);
		}

		#endregion
	}
}
