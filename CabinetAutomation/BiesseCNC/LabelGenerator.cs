﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Drawing;
using CabinetAutomation.BiesseCabinet;
using System.Windows.Forms;
using CabinetAutomation.BiesseBeamSaw;

namespace CabinetAutomation.BiesseCNC
{
	/// <summary>
	/// Given an array of parts, This will generate barcode labels.
	/// with pdf.
	/// </summary>
	public class LabelGenerator
	{

		public Int32 Quantity = 1;
		public Int32 barcodeFormat = BiesseCNC.BarcodeFormat.Default;
		public Int32 grainType = GrainType.Default;
		public Boolean edgeBinding = false;

		static readonly XPen LightGrayPen = new XPen(XColor.FromKnownColor(KnownColor.LightGray), XUnit.FromPoint(1));
		static readonly XPen BlackPen = new XPen(XColor.FromKnownColor(KnownColor.Black), XUnit.FromPoint(1));
		static readonly XPen Black3ptPen = new XPen(XColor.FromKnownColor(KnownColor.Black), XUnit.FromPoint(3));
		static readonly XFont Arial8 = new XFont("Arial", 8, XFontStyle.Regular);
		static readonly XFont ArialLarge = new XFont("Arial", 14, XFontStyle.Regular);
		static readonly XBrush blackBrush = XBrushes.Black;
		static readonly XPen PurplePen = new XPen(XColor.FromKnownColor(KnownColor.Purple), XUnit.FromPoint(1));

		static Decimal Binding00 = new Decimal(0);
		static Decimal Binding08 = new Decimal(0.8);
		static Decimal Binding10 = new Decimal(1.0);
		static Decimal Binding13 = new Decimal(1.3);
		static Decimal Binding20 = new Decimal(2.0);

		public PageSpecification page = PageSpecification.A4Oddy4x2;

		public LabelGenerator()
		{
		}

		public void SaveToPdf(String filePath, PartList parts)
		{

			parts = parts.Multiply(this.Quantity);
			parts = parts.PartsAfterExpanding();
			parts = parts.PartsWithoutFileCamX();
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
				document.Info.CreationDate = DateTime.Now;
				// document.Info.Producer = "HINSHISU Cabinet Automation";

				Int32 numberOfPage = (int)(Math.Ceiling(parts.Count * 1.0 / this.page.LabelsPerPage));


				PdfPage page = null;
				XGraphics graphics = null;

				for (int i = 0, p = 0; i < parts.Count; i++)
				{
					Part part = parts[i];
					Int32 r = (i / this.page.ColumnPerPage) % this.page.RowPerPage;
					Int32 c = i % this.page.ColumnPerPage;

					if (r == 0 && c == 0)
					{
						page = document.AddPage();
						p += 1;

						page.Height = this.page.PageSize.Height;
						page.Width = this.page.PageSize.Width;

						graphics = XGraphics.FromPdfPage(page);

											
						String pageLine = String.Format("Page {0} of {1}", p, numberOfPage);

						graphics.DrawString(pageLine, ArialLarge, blackBrush, this.page.PageSize.Width/2, 5, XStringFormats.TopCenter);
						// graphics.DrawRectangle(pen, new XRect(this.PageMargin.Left, this.PageMargin.Top, pageSizeAfterMargin.Width, pageSizeAfterMargin.Height));
					}

					XRect labelRectangle = this.page.GetLabelRectangle(r, c);

					this.DrawLabel(graphics, labelRectangle, part, this.barcodeFormat);

					Console.WriteLine("{0} {1} {2} {3}", labelRectangle.X, labelRectangle.Y, labelRectangle.Width, labelRectangle.Height);
				}

				try
				{
					document.Save(filePath);
				}
				catch (IOException ex)
				{
					MessageBox.Show(ex.Message);
				}
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
		/// <param name="graphics"></param>
		/// <param name="rectangle">The bounds.</param>
		/// <param name="part">The part.</param>
		private void DrawLabel(XGraphics graphics, XRect rectangle, Part part, Int32 format)
		{
			if (Part.PartsWithStickerOnGoodSide.Contains(part.Name))
			{
				part = part.Clone();

				part.MirrorVertically();
			}
			else
			{
				if (!Part.PartsWithStickerOnBadSide.Contains(part.Name))
				{
					MessageBox.Show(String.Format("Part: {0} Sticker logic not defined. Assuming sticker goes to white/inner side.", part.Name), part.Name);
				}
			}

			if (part.Length < part.Depth)
			{
				part = part.Clone();

				part.Rotate();
			}

			// graphics.DrawRectangle(PurplePen, rectangle);

			if (this.page.Name == PageSpecification.A4Oddy4x2Name)
			{
				this.DrawA44x2(graphics, rectangle, part);
			}

			if (this.page.Name == PageSpecification.A4M36x2Name)
			{
				this.DrawA46x2(graphics, rectangle, part);
			}

			if (this.edgeBinding)
			{
				this.DrawEdgeBinding(graphics, rectangle, part);
			}
		}

		private void DrawA44x2(XGraphics graphics, XRect rectangle, Part part)
		{
			XUnit y = rectangle.Top + XUnit.FromMillimeter(5);
			XUnit x = rectangle.Left + XUnit.FromMillimeter(20);
			String barcodeText = part.FileCamX;
			String barcodeLabel = barcodeText;

			// Extra top space
			y += XUnit.FromMillimeter(5);

			if (!String.IsNullOrEmpty(barcodeText))
			{
				Image image = BarcodeGenerator.Get(barcodeText);
				XImage xImage = XImage.FromGdiPlusImage(image);
				XUnit x1 = XUnit.FromMillimeter(rectangle.Left + (rectangle.Width - xImage.PointWidth) / 2);

				XPoint point = new XPoint(x, y + 5);

				graphics.DrawImage(xImage, point);

				y += XUnit.FromPoint(xImage.PointHeight);
				y += XUnit.FromMillimeter(6);

				graphics.DrawString(barcodeText, Arial8, blackBrush, new XPoint(x, y));
				y += XUnit.FromMillimeter(5);
			}

			String line1 = String.Format("{0} - {1} - {2}",
				part.Code, Substring(part.Description, 20), Substring(part.Type, 20));

			graphics.DrawString(line1, Arial8, blackBrush, new XPoint(x, y));
			y += XUnit.FromMillimeter(5);

			String line2 = part.OwnerName;

			if (line2.Length > 0)
			{
				graphics.DrawString(line2, Arial8, blackBrush, new XPoint(x, y));
				y += XUnit.FromMillimeter(5);
			}

			String line3 = String.Format("{0} {1} {2}",
				Substring(part.Grain, 15), Substring(part.Colour, 15),
				Substring(part.Material, 15));

			graphics.DrawString(line3, Arial8, blackBrush, new XPoint(x, y));
			y += XUnit.FromMillimeter(5);

			{
				String line4 = String.Format("{2} X {1} X {3}",
					part.Code.PadLeft(3, ' '), part.Length, part.Depth, part.Height);

				y += XUnit.FromMillimeter(0);
				graphics.DrawString(line4, ArialLarge, blackBrush, new XPoint(x, y));
				y += XUnit.FromMillimeter(3);

			}

			String line5 = part.Customer;

			graphics.DrawString(line5, Arial8, blackBrush, new XPoint(x, y));
			y += XUnit.FromMillimeter(5);
		}

		private void DrawA46x2(XGraphics graphics, XRect rectangle, Part part)
		{
			XUnit y = rectangle.Top + XUnit.FromMillimeter(5);
			XUnit x = rectangle.Center.X;
			XUnit lineHeight = new XUnit(graphics.MeasureString("Line", Arial8).Height);
			XUnit lineHeightLarge = new XUnit(graphics.MeasureString("Line", ArialLarge).Height);
			// Space from top for edge binding.
			y += lineHeight;

			String line1 = String.Format("{0} - {1} - {2}",
				part.Code, part.FileCamX, Substring(part.Description, 20));

			graphics.DrawString(line1, Arial8, blackBrush, new XPoint(x, y), XStringFormats.TopCenter);
			y += lineHeight;

			String line2 = part.OwnerName;

			graphics.DrawString(line2, Arial8, blackBrush, new XPoint(x, y), XStringFormats.TopCenter);
			y += lineHeight;

			String line3 = String.Format("{0} {1} {2}",
				Substring(part.Grain, 15), Substring(part.Colour, 15),
				Substring(part.Material, 15));

			graphics.DrawString(line3, Arial8, blackBrush, new XPoint(x, y), XStringFormats.TopCenter);
			y += lineHeight;

			{
				String line4 = String.Format("{2} X {1} X {3}",
					part.Code.PadLeft(3, ' '), part.Length, part.Depth, part.Height);

				graphics.DrawString(line4, ArialLarge, blackBrush, new XPoint(x, y), XStringFormats.TopCenter);
				y += lineHeightLarge;
			}

			String line5 = part.Customer;

			graphics.DrawString(line5, Arial8, blackBrush, new XPoint(x, y), XStringFormats.TopCenter);
			y += lineHeight;
		}

		String GetEdgeBindingString(Decimal d, Boolean horizontal)
		{

			if (d <= new Decimal(0.5))
			{
				if (d == 0)
					return String.Empty;

				return d.ToString("0.# eb") ;
			}

			if (d <= new Decimal(1.0))
			{
				return d.ToString("0.# eb ") + (horizontal ? "-" : "|");
			}

			return d.ToString("0.# eb") + (horizontal ? "=" : "||");
		}

		Decimal GetEdgeBinding(String s)
		{
			if (String.IsNullOrEmpty(s))
			{
				return 0;
			}

			if (!s.StartsWith("PVC") && !s.StartsWith("XMD"))
			{
				return 0;
			}

			try 
			{
				return Decimal.Parse(s.Substring(3));
			}
			catch (FormatException)
			{
				// TODO: Handle error.
			}

			MessageBox.Show("Invalid edge binding information {0}", s);

			return 0;
		}

		/// <summary>
		/// Draws the given part inside the given rectangle.
		/// </summary>
		/// <param name="graphics"></param>
		/// <param name="rectangle">The bounds.</param>
		/// <param name="part">The part.</param>
		void DrawEdgeBinding(XGraphics graphics, XRect rectangle, Part part)
		{
			double d = XUnit.FromMillimeter(3);
			XUnit vertical = XUnit.FromMillimeter(1);
			XUnit horizontal = XUnit.FromMillimeter(10);
			XPoint point;
			Decimal binding;
			String bindingLabel;

			rectangle = new XRect(rectangle.X + d, rectangle.Y + d, rectangle.Width - 2 * d, rectangle.Height - 2 * d);

			binding = GetEdgeBinding(part.TopEdgeName);
			bindingLabel = GetEdgeBindingString(binding, true);
			point = new XPoint(rectangle.Center.X, rectangle.Top + vertical);
			graphics.DrawString(bindingLabel, Arial8, blackBrush, point, XStringFormats.TopCenter);
			DrawEdgeBindingLine(graphics, rectangle.TopLeft, rectangle.TopRight, binding);

			binding = GetEdgeBinding(part.BottomEdgeName);
			bindingLabel = GetEdgeBindingString(binding, true);
			point = new XPoint(rectangle.Center.X, rectangle.Bottom - vertical);
			graphics.DrawString(bindingLabel, Arial8, blackBrush, point, XStringFormats.BottomCenter);
			DrawEdgeBindingLine(graphics, rectangle.BottomLeft, rectangle.BottomRight, binding);

			binding = GetEdgeBinding(part.LeftEdgeName);
			bindingLabel = GetEdgeBindingString(binding, true);
			point = new XPoint(rectangle.Left + horizontal, rectangle.Center.Y);
			graphics.DrawString(bindingLabel, Arial8, blackBrush, point, XStringFormats.Center);
			DrawEdgeBindingLine(graphics, rectangle.TopLeft, rectangle.BottomLeft, binding);

			binding = GetEdgeBinding(part.RightEdgeName);
			bindingLabel = GetEdgeBindingString(binding, true);
			point = new XPoint(rectangle.Right - horizontal, rectangle.Center.Y);
			graphics.DrawString(bindingLabel, Arial8, blackBrush, point, XStringFormats.Center);
			DrawEdgeBindingLine(graphics, rectangle.BottomRight, rectangle.TopRight, binding);
		}

		void DrawEdgeBindingLine(XGraphics graphics, XPoint p1, XPoint p2, Decimal binding)
		{
			double d = 1.5;

			if (binding >= Binding08 && binding <= Binding10)
			{
				graphics.DrawLine(BlackPen, p1, p2);
			}
			else if (binding > Binding10)
			{
				if (p1.X == p2.X)
				{
					graphics.DrawLine(BlackPen, p1.X - d, p1.Y, p2.X - d, p2.Y);
					graphics.DrawLine(BlackPen, p1.X + d, p1.Y, p2.X + d, p2.Y);
				}
				else if (p1.Y == p2.Y)
				{
					graphics.DrawLine(BlackPen, p1.X, p1.Y - d, p2.X, p2.Y - d);
					graphics.DrawLine(BlackPen, p1.X, p1.Y + d, p2.X, p2.Y + d);
				}
				else
				{
					graphics.DrawLine(Black3ptPen, p1, p2);
				}
			}
		}
	}
}
