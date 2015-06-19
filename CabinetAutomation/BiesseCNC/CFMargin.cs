using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PdfSharp.Drawing;

namespace CabinetAutomation.BiesseCNC
{
	public class CFMargin
	{
		public XUnit Top;
		public XUnit Bottom;
		public XUnit Left;
		public XUnit Right;

		/// <summary>
		/// Creates a margin object with m millimeter margin on 
		/// all side.
		/// </summary>
		/// <param name="m">Margin in millimeter</param>
		public CFMargin(Double m)
		{
			this.Top = this.Bottom = this.Left = this.Right = XUnit.FromMillimeter(m);
		}

		public CFMargin(Double vertical, Double horizontal)
		{
			this.Top = this.Bottom = XUnit.FromMillimeter(vertical);
			this.Left = this.Right = XUnit.FromMillimeter(horizontal);
		}
	}
}
