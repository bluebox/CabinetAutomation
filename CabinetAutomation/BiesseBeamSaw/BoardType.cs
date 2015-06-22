using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CabinetAutomation.BiesseBeamSaw
{
	public class BoardType : IComparable<BoardType>, IEquatable<BoardType>, IEqualityComparer<BoardType>
	{
		public String Material;
		public String Color;
		public Decimal Thickness;

		public BoardType(String material, String color, Decimal thickness)
		{
			this.Material = material;
			this.Color = color;
			this.Thickness = thickness;
		}

		public override String ToString()
		{
			StringBuilder sb = new StringBuilder();
			
			sb.Append(this.Thickness);

			if (!String.IsNullOrEmpty(this.Material))
			{
				sb.Append('-');
				sb.Append(this.Material);
			}

			if (!String.IsNullOrEmpty(this.Color))
			{
				sb.Append('-');
				sb.Append(this.Color);
			}

			return sb.ToString();
		}

		#region IComparable<BoardType> Members

		public int CompareTo(BoardType other)
		{
			int c1 = String.Compare(this.Material, other.Material);

			if (c1 != 0)
				return c1;

			int c2 = String.Compare(this.Color, other.Color);

			if (c2 != 0)
				return c2;

			return Decimal.Compare(this.Thickness, other.Thickness);
		}

		#endregion

		#region IEquatable<BoardType> Members

		public bool Equals(BoardType other)
		{
			BoardType x = this;
			BoardType y = other;

			return String.Equals(x.Material, y.Material) && String.Equals(x.Color, y.Color) && Decimal.Equals(x.Thickness, y.Thickness);
		}

		#endregion

		#region IEqualityComparer<BoardType> Members

		public bool Equals(BoardType x, BoardType y)
		{
			return x.Equals(y);
		}

		public int GetHashCode(BoardType obj)
		{
			return this.Material.GetHashCode() + this.Color.GetHashCode() + this.Thickness.GetHashCode();
		}

		#endregion
	}
}
