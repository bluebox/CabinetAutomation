using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace CabinetAutomation.BiesseCabinet
{
	/// <summary>
	/// Takes a csv output from basse cabinet and reads
	/// it into memory
	/// </summary>
	public class CsvParser
	{
		public Char[] CsvSplitCharacters = new Char[] { ';' };
		public PartList Parts = new PartList();

		public CsvParser(String fileName)
		{
			this.Load(fileName);
		}

		public CsvParser()
		{
		}

		public void Load(String fileName)
		{
			this.Parts.Clear();

			using (TextFieldParser parser = new TextFieldParser(fileName))
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(";", "\t", ",");

				for (Int32 i = 0; !parser.EndOfData; i++)
				{
					String[] parts = parser.ReadFields();

					Part p = Part.FromCsvLine(parts, i);

					if (p != null)
					{
						Console.WriteLine("{0}: {1} {2}", p.Code, p.Description, p.Type);

						this.Parts.Add(p);
					}
				}
			}
		}
	}

	public class PartList : List<Part>
	{
		public Boolean Expanded = false;

		public Int32 TotalQuantity
		{
			get
			{
				Int32 t = 0;

				foreach (Part p in this)
				{
					t += p.Quantity;
				}

				return t;
			}
		}

		public HashSet<Decimal> ThicknessValues
		{
			get
			{
				HashSet<Decimal> values = new HashSet<Decimal>();

				foreach (Part p in this)
				{
					if (p.Height.HasValue)
					{
						values.Add(p.Height.Value);
					}
				}

				return values;
			}
		}

		public PartList Filter(Decimal thickness)
		{
			PartList parts = new PartList();

			parts.Expanded = this.Expanded;

			foreach (Part p in this)
			{
				if (p.H.HasValue && p.H.Value == thickness)
				{
					parts.Add(p);
				}
			}

			return parts;
		}

		public PartList RemoveParthWithoutFileCamX()
		{
			PartList parts = new PartList();

			parts.Expanded = this.Expanded;

			foreach (Part p in this)
			{
				if (!String.IsNullOrEmpty(p.FileCamX))
				{
					parts.Add(p);
				}
			}

			return parts;
		}

		public PartList Expand()
		{
			PartList parts = new PartList();

			parts.Expanded = true;

			foreach (Part p in this)
			{
				for (int i = 0; i < p.Quantity; i++)
				{
					parts.Add(p);
				}
			}

			return parts;
		}
	}

	public class Part : IComparable
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
		/// Alias for Descrizione = Description
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
		public Double? L2;

		/// <summary>
		/// -H in cm.
		/// </summary>
		public Double? H2;

		/// <summary>
		/// -P in cm.
		/// </summary>
		public Double? P2;

		public String OwnerName = String.Empty;

		public static Part FromCsvLine(String[] parts, Int32 rowNumber)
		{
			if (null == parts)
			{
				return null;
			}

			if (parts.Length < 15)
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

			if (parts.Length > 32 && null != parts[32])
			{
				p.OwnerName = parts[32];
			}

			return p;
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

			return String.Compare(p1.CodePadded, p2.CodePadded);
		}

		#region IComparable Members

		public Int32 CompareTo(object obj)
		{
			return Part.Compare(this, obj as Part);
		}

		#endregion
	}
}
