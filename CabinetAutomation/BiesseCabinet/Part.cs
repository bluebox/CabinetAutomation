using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CabinetAutomation.BiesseBeamSaw;

namespace CabinetAutomation.BiesseCabinet
{
	public class Part : IComparable, ICloneable
	{
		/// <summary>
		/// Bad side back side of the board.
		/// The `bad` side is the `white` / `inner` side.
		/// The `good` side may have color or finish or may be white.
		/// </summary>
		public static List<String> PartsWithStickerOnBadSide = new List<String>(new String[]{
			"TOP PANEL",
			"BOTTOM",
			"HORIZONTAL PARTITION",
//			"HORIZONTAL CENTRAL RAIL",
			"VERTICAL PARTITION", // Both side is bad/inner.
			"BACK PANEL", // No edge or machining.
			"LEFT LATERAL SIDE",
			"RIGHT LATERAL SIDE",
			"TOP STRETCHER",
			"DOOR PANEL"
		});

		public static List<String> PartsWithStickerOnGoodSide = new List<String>(new String[]{
			"SHELF",
			"FRONT TOE KICK",
		});

		public String Code = String.Empty;

		public String CodePadded
		{
			get
			{
				return this.Code.PadLeft(3, '0');
			}
		}

		public String Name = String.Empty;

		/// <summary>
		/// L = longueur = Length
		/// in mm.
		/// </summary>
		public Decimal? L;

		/// <summary>
		/// H = hauteur = Height
		/// in mm.
		/// </summary>
		public Decimal? H;

		/// <summary>
		/// P = profondeur = depth
		/// in mm.
		/// </summary>
		public Decimal? P;

		/// <summary>
		/// L = longueur = Length
		/// in mm.
		/// </summary>
		public Decimal? Length
		{
			get
			{
				return this.L;
			}

			set
			{
				this.L = value;
			}
		}

		/// <summary>
		/// Alias for H = hauteur = Height
		/// in mm.
		/// </summary>
		public Decimal? Height
		{
			get
			{
				return this.H;
			}

			set
			{
				this.H = value;
			}
		}

		/// <summary>
		/// Alias for P = profondeur = depth
		/// in mm.
		/// </summary>
		public Decimal? Depth
		{
			get
			{
				return this.P;
			}

			set
			{
				this.P = value;
			}
		}

		public String Grain = String.Empty;
		public String Colour = String.Empty;
		public String Material = String.Empty;

		/// <summary>
		/// Descrizione = Description
		/// </summary>
		public String Descrizione = String.Empty;

		/// <summary>
		/// Descrizione = Description
		/// </summary>
		public String Description
		{
			get
			{
				return this.Descrizione;
			}

			set
			{
				this.Descrizione = value;
			}
		}

		/// <summary>
		/// Tipologia = Type
		/// Ex: Back panel
		/// Ex: Lateral side
		/// Ex: Door panel
		/// </summary>
		public String Tipologia = String.Empty;


		/// <summary>
		/// Tipologia = Type
		/// Ex: Back panel
		/// Ex: Lateral side
		/// Ex: Door panel
		/// </summary>
		public String Type
		{
			get
			{
				return this.Tipologia;
			}

			set
			{
				this.Tipologia = value;
			}
		}

		/// <summary>
		/// The cix file name for CNC machine.
		/// </summary>
		public String FileCam1 = String.Empty;
		public String FileCam2 = String.Empty;

		public String FileCamX
		{
			get
			{
				if (!String.IsNullOrEmpty(this.FileCam1))
				{
					return this.FileCam1;
				}

				if (!String.IsNullOrEmpty(this.FileCam2))
				{
					if (this.FileCam2.Length == 8 + 1 + 3)
					{
						if (this.FileCam2[3] == 's')
						{
							StringBuilder sb = new StringBuilder(this.FileCam2);

							sb[3] = 'p';

							return sb.ToString();
						}
					}
				}

				return String.Empty;
			}
		}

		public Int32 Quantity;

		///// <summary>
		///// -L in cm.
		///// </summary>
		//public Decimal? L2;

		///// <summary>
		///// -H in cm.
		///// </summary>
		//public Decimal? H2;

		///// <summary>
		///// -P in cm.
		///// </summary>
		//public Decimal? P2;

		public String OwnerName = String.Empty;

		///// <summary>
		///// BordoSopra = BoardTop
		///// </summary>
		//public String BordoSopra;

		///// <summary>
		///// BordoSopra = BoardTop
		///// </summary>
		//public String BoardTop
		//{
		//    get
		//    {
		//        return BordoSopra;
		//    }

		//    set
		//    {
		//        this.BordoSopra = value;
		//    }
		//}

		///// <summary>
		///// BordoDestro = BoardRight
		///// </summary>
		//public String BordoDestro;

		///// <summary>
		///// BordoDestro = BoardRight
		///// </summary>
		//public String BoardRight
		//{
		//    get
		//    {
		//        return this.BordoDestro;
		//    }

		//    set
		//    {
		//        this.BordoDestro = value;
		//    }
		//}

		///// <summary>
		///// BordoSotto = BoardBellow
		///// </summary>
		//public String BordoSotto;

		///// <summary>
		///// BordoSotto = BoardBellow
		///// </summary>
		//public String BoardBellow
		//{
		//    get
		//    {
		//        return this.BordoSotto;
		//    }

		//    set
		//    {
		//        this.BordoSotto = value;
		//    }
		//}

		///// <summary>
		///// BordoSinistro = BoardLeft
		///// </summary>
		//public String BordoSinistro;

		///// <summary>
		///// BordoSinistro = BoardLeft
		///// </summary>
		//public String BoardLeft
		//{
		//    get
		//    {
		//        return this.BordoSinistro;
		//    }

		//    set
		//    {
		//        this.BordoSinistro = value;
		//    }
		//}

		public String Customer;
		public String BloccoAppartenenza;

		public String TopEdgeName;
		public String RightEdgeName;
		public String BottomEdgeName;
		public String LeftEdgeName;

		public Part()
		{
		}

		public Part(Part p)
		{
			this.Code = p.Code;
			this.Name = p.Name;
			this.L = p.L;
			this.H = p.H;
			this.P = p.P;
			this.Grain = p.Grain;
			this.Colour = p.Colour;
			this.Material = p.Material;
			this.Descrizione = p.Descrizione;
			this.Tipologia = p.Tipologia;
			this.FileCam1 = p.FileCam1;
			this.FileCam2 = p.FileCam2;
			this.Quantity = p.Quantity;
			//this.L2 = p.L2;
			//this.H2 = p.H2;
			//this.P2 = p.P2;
			//this.BordoSopra = p.BordoSopra;
			//this.BordoDestro = p.BordoDestro;
			//this.BordoSotto = p.BordoSotto;
			//this.BordoSinistro = p.BordoSinistro;
			this.Customer = p.Customer;
			this.BloccoAppartenenza = p.BloccoAppartenenza;
			this.TopEdgeName = p.TopEdgeName;
			this.RightEdgeName = p.RightEdgeName;
			this.BottomEdgeName = p.BottomEdgeName;
			this.LeftEdgeName = p.LeftEdgeName;
			this.OwnerName = p.OwnerName;
		}

		public BoardType BoardType
		{
			get
			{
				return new BoardType(this.Material, this.Colour, this.H.Value);
			}
		}

		/// <summary>
		/// Rotates the board anti clock wise.
		/// </summary>
		public void Rotate()
		{
			{
				Decimal? temp = this.L;

				this.L = this.P;
				this.P = temp;
			}

			//{
			//    Decimal? temp = this.L2;

			//    this.L2 = this.P2;
			//    this.P2 = temp;
			//}

			//{
			//    String temp = this.BoardTop;

			//    this.BoardTop = this.BoardRight;
			//    this.BoardRight = this.BoardBellow;
			//    this.BoardBellow = this.BoardLeft;
			//    this.BoardLeft = temp;
			//}

			{
				String temp = this.TopEdgeName;

				this.TopEdgeName = this.RightEdgeName;
				this.RightEdgeName = this.BottomEdgeName;
				this.BottomEdgeName = this.LeftEdgeName;
				this.LeftEdgeName = temp;
			}
		}

		/// <summary>
		/// Mirror the part across a vertical mirror.
		/// </summary>
		public void MirrorVertically()
		{
			String temp = this.LeftEdgeName;
			this.LeftEdgeName = this.RightEdgeName;
			this.RightEdgeName = temp;
		}

		/// <summary>
		/// HinshitsuQuoteOpenIntelligenceQuoteCloseSetGrain
		/// 
		/// We set the grain by making L > P.
		/// 
		/// If L < P, we rotate the board anti-clockwise.
		/// </summary>
		public void HinshitsuIntelligenceSetGrain()
		{
			if (!"0".Equals(this.Grain))
			{
				return;
			}

			if (!this.L.HasValue || !this.P.HasValue)
			{
				return;
			}

			if (this.L.Value < this.P.Value)
			{
				this.Rotate();
			}
		}

		public String GetEdgeBinding(String s)
		{
			if (String.IsNullOrEmpty(s))
			{
				return String.Empty;
			}

			if (!s.StartsWith("PVC") && !s.StartsWith("XMD"))
			{
				return String.Empty;
			}

			s = s.Substring(3).Trim();

			return s;
		}

		#region IComparable Members

		public Int32 CompareTo(Part part)
		{
			return (this as IComparable).CompareTo(part);
		}

		int IComparable.CompareTo(object obj)
		{
			return Compare(this, obj as Part);
		}

		public static Int32 Compare(Part p1, Part p2)
		{
			Int32 m1 = String.Compare(p1.Material, p2.Material);

			if (m1 != 0)
			{
				return m1;
			}

			Int32 c1 = String.Compare(p1.Colour, p2.Colour);

			if (c1 != 0)
			{
				return c1;
			}

			if (p1.H.HasValue && p2.H.HasValue)
			{
				Int32 h1 = Decimal.Compare(p1.H.Value, p2.H.Value);

				if (h1 != 0)
				{
					return h1;
				}
			}

			if (p1.P.HasValue && p2.P.HasValue)
			{
				Int32 d1 = Decimal.Compare(p1.P.Value, p2.P.Value);

				if (d1 != 0)
				{
					return d1;
				}
			}

			if (p1.L.HasValue && p2.L.HasValue)
			{
				Int32 l1 = Decimal.Compare(p1.L.Value, p2.L.Value);

				if (l1 != 0)
				{
					return l1;
				}
			}

			return String.Compare(p1.CodePadded, p2.CodePadded);
		}

		#endregion

		#region ICloneable Members

		public Part Clone()
		{
			return (this as ICloneable).Clone() as Part;
		}

		object ICloneable.Clone()
		{
			return new Part(this);
		}

		#endregion
	}
}
