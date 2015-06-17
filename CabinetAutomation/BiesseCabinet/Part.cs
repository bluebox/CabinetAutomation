using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CabinetAutomation.BiesseCabinet
{
	public class Part : IComparable, ICloneable
	{
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

		/// <summary>
		/// -L in cm.
		/// </summary>
		public Decimal? L2;

		/// <summary>
		/// -H in cm.
		/// </summary>
		public Decimal? H2;

		/// <summary>
		/// -P in cm.
		/// </summary>
		public Decimal? P2;

		public String OwnerName = String.Empty;

		/// <summary>
		/// BordoSopra = BoardTop
		/// </summary>
		public String BordoSopra;

		/// <summary>
		/// BordoSopra = BoardTop
		/// </summary>
		public String BoardTop
		{
			get
			{
				return BordoSopra;
			}

			set
			{
				this.BordoSopra = value;
			}
		}

		/// <summary>
		/// BordoDestro = BoardRight
		/// </summary>
		public String BordoDestro;

		/// <summary>
		/// BordoDestro = BoardRight
		/// </summary>
		public String BoardRight
		{
			get
			{
				return this.BordoDestro;
			}

			set
			{
				this.BordoDestro = value;
			}
		}

		/// <summary>
		/// BordoSotto = BoardBellow
		/// </summary>
		public String BordoSotto;

		/// <summary>
		/// BordoSotto = BoardBellow
		/// </summary>
		public String BoardBellow
		{
			get
			{
				return this.BordoSotto;
			}

			set
			{
				this.BordoSotto = value;
			}
		}

		/// <summary>
		/// BordoSinistro = BoardLeft
		/// </summary>
		public String BordoSinistro;

		/// <summary>
		/// BordoSinistro = BoardLeft
		/// </summary>
		public String BoardLeft
		{
			get
			{
				return this.BordoSinistro;
			}

			set
			{
				this.BordoSinistro = value;
			}
		}

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
			this.L2 = p.L2;
			this.H2 = p.H2;
			this.P2 = p.P2;
			this.BordoSopra = p.BordoSopra;
			this.BordoDestro = p.BordoDestro;
			this.BordoSotto = p.BordoSotto;
			this.BordoSinistro = p.BordoSinistro;
			this.Customer = p.Customer;
			this.BloccoAppartenenza = p.BloccoAppartenenza;
			this.TopEdgeName = p.TopEdgeName;
			this.RightEdgeName = p.RightEdgeName;
			this.BottomEdgeName = p.BottomEdgeName;
			this.LeftEdgeName = p.LeftEdgeName;
			this.OwnerName = p.OwnerName;
		}

		public static Part FromCsvLine(String[] parts, Int32 rowNumber)
		{
			if (null == parts)
			{
				return null;
			}

			if (parts.Length < 36)
			{
				Console.WriteLine("Ignoring row {0} with less than 15 columns", rowNumber);

				return null;
			}

			Part p = new Part();

			p.Code = parts[0].Trim();

			if (0 == p.Code.Length)
			{
				return null;
			}

			if ("Code".Equals(p.Code))
			{
				Console.WriteLine("Ingore header row {0}.", rowNumber);

				return null;
			}

			p.Name = parts[1].Trim();

			try
			{
				p.L = Decimal.Parse(parts[2]);
			}
			catch (FormatException)
			{
				p.L = null;
			}

			try
			{
				p.H = Decimal.Parse(parts[3]);
			}
			catch (FormatException)
			{
				p.H = null;
			}

			try
			{
				p.P = Decimal.Parse(parts[4]);
			}
			catch (FormatException)
			{
				p.P = null;
			}

			if (null != parts[5])
			{
				p.Grain = parts[5];
			}

			if (null != parts[6])
			{
				p.Colour = parts[6];
			}

			if (null != parts[7])
			{
				p.Material = parts[7];
			}

			if (null != parts[8])
			{
				p.Descrizione = parts[8];
			}

			if (null != parts[9])
			{
				p.Tipologia = parts[9];
			}

			if (null != parts[12])
			{
				p.FileCam1 = parts[12];
			}

			if (null != parts[13])
			{
				p.FileCam2 = parts[13];
			}

			try
			{
				p.Quantity = Int32.Parse(parts[14]);
			}
			catch (FormatException)
			{
				Console.WriteLine("Unable to parse Quantity for row {0}", rowNumber);

				return null;
			}

			p.BordoSopra = parts[18];
			p.BordoDestro = parts[19];
			p.BordoSotto = parts[20];
			p.BordoSinistro = parts[21];

			p.Customer = parts[22];
			p.BloccoAppartenenza = parts[23];
			p.TopEdgeName = parts[24];
			p.RightEdgeName = parts[25];
			p.BottomEdgeName = parts[26];
			p.LeftEdgeName = parts[27];

			if (parts.Length > 32 && null != parts[32])
			{
				p.OwnerName = parts[32];
			}

			return p;
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

			{
				Decimal? temp = this.L2;

				this.L2 = this.P2;
				this.P2 = temp;
			}

			{
				String temp = this.BoardTop;

				this.BoardTop = this.BoardRight;
				this.BoardRight = this.BoardBellow;
				this.BoardBellow = this.BoardLeft;
				this.BoardLeft = temp;
			}

			{
				String temp = this.TopEdgeName;

				this.TopEdgeName = this.RightEdgeName;
				this.RightEdgeName = this.BottomEdgeName;
				this.BottomEdgeName = this.LeftEdgeName;
				this.LeftEdgeName = temp;
			}
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

			if (!s.StartsWith("PVC"))
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
			Part p1 = this;
			Part p2 = obj as Part;

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
