using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace CabinetAutomation
{
	/// <summary>
	/// Takes a csv output from basse cabinet and reads
	/// it into memory
	/// </summary>
	public class CsvParser
	{
		public Char[] CsvSplitCharacters = new Char[] { ';' };
		public List<Piece> Pieces = new List<Piece>();

		public CsvParser(String fileName)
		{
			this.Load(fileName);
		}

		public CsvParser()
		{
		}

		public void Load(String fileName)
		{
			this.Pieces.Clear();

			using (TextFieldParser parser = new TextFieldParser(fileName))
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(";");

				for (Int32 i = 0; !parser.EndOfData; i++)
				{
					String[] parts = parser.ReadFields();

					Piece p = Piece.FromCsvLine(parts, i);

					if (p != null)
					{
						Console.WriteLine("{0}: {1} {2}", p.Code, p.Description, p.Type);

						this.Pieces.Add(p);
					}
				}
			}
		}
	}

	public class Piece
	{
		public String Code;
		public String Name;
		/// <summary>
		/// L = longueur = Length
		/// in mm.
		/// </summary>
		public Double? L;

		/// <summary>
		/// H = hauteur = Height
		/// in mm.
		/// </summary>
		public Double? H;

		/// <summary>
		/// P = profondeur = depth
		/// in mm.
		/// </summary>
		public Double? P;

		/// <summary>
		/// L = longueur = Length
		/// in mm.
		/// </summary>
		public Double? Length
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
		public Double? Height
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
		public Double? Depth
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

		public String Grain;
		public String Colour;
		public String Material;
		
		/// <summary>
		/// Descrizione = Description
		/// </summary>
		public String Descrizione;

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
		public String Tipologia;


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
		public String FileCam1;
		public String FileCam2;

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

		public String OwnerName;

		public static Piece FromCsvLine(String[] parts, Int32 rowNumber)
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

			Piece p = new Piece();

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
				p.L = Double.Parse(parts[2]);
			}
			catch (FormatException)
			{
				p.L = null;
			}

			try
			{
				p.H = Double.Parse(parts[3]);
			}
			catch (FormatException)
			{
				p.H = null;
			}

			try
			{
				p.P = Double.Parse(parts[4]);
			}
			catch (FormatException)
			{
				p.P = null;
			}

			p.Grain = parts[5];
			p.Colour = parts[6];
			p.Material = parts[7];
			p.Descrizione = parts[8];
			p.Tipologia = parts[9];
			p.FileCam1 = parts[12];

			try
			{
				p.Quantity = Int32.Parse(parts[14]);
			}
			catch (FormatException)
			{
				Console.WriteLine("Unable to parse Quantity for row {0}", rowNumber);
				
				return null;
			}

			p.OwnerName = parts[30];

			if (p.OwnerName == null)
			{
				p.OwnerName = String.Empty;
			}

			return p;
		}
	}
}
