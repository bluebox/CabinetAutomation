using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Drawing;
using CabinetAutomation.BiesseCabinet;

namespace CabinetAutomation.BiesseCNC
{
	public class CFSize
	{
		public static XSize A4 = CFSize.FromMillimeter(210, 297);
		public static XSize Letter = CFSize.FromMillimeter(210, 279.4);
		public static XSize Legal = CFSize.FromMillimeter(210, 215.9);

		public static XSize FromMillimeter(Double width, Double height)
		{
			XSize size = new XSize();

			size.Width = XUnit.FromMillimeter(width);
			size.Height = XUnit.FromMillimeter(height);

			return size;
		}
	}

	public class CFMargin {
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

	/// <summary>
	/// Given an array of parts, This will generate barcode labels.
	/// with pdf.
	/// </summary>
	public class LabelGenerator
	{
		public XSize PageSize;

		public Int32 RowPerPage = 4;
		public Int32 ColumnPerPage = 2;

		public CFMargin PageMargin;
		public CFMargin LabelMargin;

		public String customerName = String.Empty;
		public String customerMobile = String.Empty;
		public String dueDate = String.Empty;

		public LabelGenerator()
		{
			this.PageSize = CFSize.A4;
			this.PageMargin = new CFMargin(5);
			this.LabelMargin = new CFMargin(5);
		}

		public void SaveToPdf(String filePath, PartList parts)
		{
			parts = parts.Expand();
			parts = parts.RemoveParthWithoutFileCam1();
			parts.Sort();

			if (null == filePath)
			{
				throw new ArgumentException("Pdf document path cannot be null.");
			}

			String extension = Path.GetExtension(filePath);

			if (!String.Equals(".pdf", extension, StringComparison.CurrentCultureIgnoreCase))
			{
				throw new ArgumentException("Pdf document must have .pdf extension");
			}

			using (PdfDocument document = new PdfDocument())
			{
				document.Info.Author = "Mithun Dhali, HINSHITSU Manufacturing Private Limited";
				document.Info.Creator = "HINSHISU Cabinet Automation";

				Int32 labelsPerPage = this.RowPerPage * this.ColumnPerPage;
				Int32 numberOfPage = (int)(Math.Ceiling(parts.Count * 1.0 / labelsPerPage));

				XSize pageSizeAfterMargin = new XSize(
					this.PageSize.Width - this.PageMargin.Left - this.PageMargin.Right,
					this.PageSize.Height - this.PageMargin.Top - this.PageMargin.Bottom);
				XPen pen = new XPen(XColor.FromKnownColor(KnownColor.Purple), XUnit.FromPoint(1));

				XSize labelSize = new XSize(
					pageSizeAfterMargin.Width / this.ColumnPerPage,
					pageSizeAfterMargin.Height / this.RowPerPage
				);

				XSize labelSizeAfterMargin = new XSize(
					labelSize.Width - this.LabelMargin.Left - this.LabelMargin.Right,
					labelSize.Height - this.LabelMargin.Top - this.LabelMargin.Bottom
				);

				XPoint pageMarginOffset = new XPoint(this.PageMargin.Left, this.PageMargin.Top);
				XPoint labelMarginOffset = new XPoint(this.LabelMargin.Left, this.LabelMargin.Top);
				PdfPage page = null;
				XGraphics graphics = null;

				for (int i = 0; i < parts.Count; i++)
				{
					Part part = parts[i];
					Int32 r = (i / this.ColumnPerPage) % this.RowPerPage;
					Int32 c = i % this.ColumnPerPage;

					if (r == 0 && c == 0)
					{
						page = document.AddPage();

						page.Height = this.PageSize.Height;
						page.Width = this.PageSize.Width;

						graphics = XGraphics.FromPdfPage(page);

						graphics.DrawRectangle(pen, new XRect(this.PageMargin.Left, this.PageMargin.Top, pageSizeAfterMargin.Width, pageSizeAfterMargin.Height));
					}

					XPoint labelOffset = new XPoint(
						pageMarginOffset.X + c * labelSize.Width + this.LabelMargin.Left,
						pageMarginOffset.Y + r * labelSize.Height + this.LabelMargin.Top);

					XRect labelRectangle = new XRect(labelOffset, labelSizeAfterMargin);

					this.DrawLabel(graphics, labelRectangle, part);

					Console.WriteLine("{0} {1} {2} {3}", labelRectangle.X, labelRectangle.Y, labelRectangle.Width, labelRectangle.Height);
				}

				document.Save(filePath);
			}
		}

		private String Substring(String s, int maxLength)
		{
			if (s == null)
			{
				return String.Empty;
			}

			if (s.Length <= maxLength)
			{
				return s;
			}

			return s.Substring(0, maxLength);
		}

		/// <summary>
		/// Draws the given part inside the given rectangle.
		/// </summary>
		/// <param name="rectangle">The bounds.</param>
		/// <param name="part">The part.</param>
		private void DrawLabel(XGraphics g, XRect r, Part p)
		{
			XPen pen = new XPen(XColor.FromKnownColor(KnownColor.Purple), XUnit.FromPoint(1));
			XFont arial8 = new XFont("arial", 8, XFontStyle.Regular);
			XFont arialLarge = new XFont("arial", 20, XFontStyle.Regular);
			XBrush brush = XBrushes.Black;

			XUnit y = r.Top;
			XUnit x = r.Left + XUnit.FromMillimeter(5);
			String barcodeText = p.FileCam1;

			// Extra top space
			y += XUnit.FromMillimeter(5);

			if (!String.IsNullOrEmpty(barcodeText))
			{
				Image image = BarcodeGenerator.Get(barcodeText);
				XImage xImage = XImage.FromGdiPlusImage(image);
				XUnit x1 = XUnit.FromMillimeter(r.Left + (r.Width - xImage.PointWidth) / 2);

				XPoint point = new XPoint(x, y + 5);

				g.DrawImage(xImage, point);

				y += XUnit.FromPoint(xImage.PointHeight);
				y += XUnit.FromMillimeter(6);

				g.DrawString(barcodeText, arial8, brush, new XPoint(x, y));
				y += XUnit.FromMillimeter(5);
			}

			String line1 = String.Format("{0} - {1} - {2}", 
				p.Code, Substring(p.Description, 20), Substring(p.Type, 20));

			g.DrawString(line1, arial8, brush, new XPoint(x, y));
			y += XUnit.FromMillimeter(5);

			String line2 = p.OwnerName;

			if (line2.Length > 0)
			{
				g.DrawString(line2, arial8, brush, new XPoint(x, y));
				y += XUnit.FromMillimeter(5);
			}

			String line3 = String.Format("{0} {1} {2}", 
				Substring(p.Grain, 15), Substring(p.Colour, 15), 
				Substring(p.Material, 15));

			if (line3.Length <= 0)
			{
				line3 = String.Empty;
			}
			{
				g.DrawString(line3, arial8, brush, new XPoint(x, y));
				y += XUnit.FromMillimeter(5);
			}

			y += XUnit.FromMillimeter(5);

			String line4 = String.Format("{0}       {1} X {2} X {3}", p.Code, p.Length, p.Depth, p.Height);

			if (line4.Length <= 0)
			{
				line4 = String.Empty;
			}
			{
				g.DrawString(line4, arialLarge, brush, new XPoint(x, y));
				y += XUnit.FromMillimeter(7);
			}

			String line5 = String.Format("{0} {1}", 
				Substring(this.customerName, 15),
				this.customerMobile);

			g.DrawString(line5, arial8, brush, new XPoint(x, y));
			y += XUnit.FromMillimeter(5);

			String line6 = String.Format("Due date: {0}", this.dueDate);

			g.DrawString(line6, arial8, brush, new XPoint(x, y));
			y += XUnit.FromMillimeter(5);

			g.DrawRectangle(pen, r);
		}
	}
}
