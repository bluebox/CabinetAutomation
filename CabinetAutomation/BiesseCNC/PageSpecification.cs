using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PdfSharp.Drawing;

namespace CabinetAutomation.BiesseCNC
{
	public class PageSpecification
	{
		public static XSize A4 = new XSize(XUnit.FromMillimeter(210), XUnit.FromMillimeter(297));
		public static XSize Letter = new XSize(XUnit.FromMillimeter(210), XUnit.FromMillimeter(279.4));
		public static XSize Legal = new XSize(XUnit.FromMillimeter(210), XUnit.FromMillimeter(215.9));

		public readonly String Name;

		public readonly XSize PageSize;

		public readonly Int32 RowPerPage;
		public readonly Int32 ColumnPerPage;

		public readonly CFMargin PageMargin;
		public readonly CFMargin LabelMargin;

		public static PageSpecification A4Oddy4x2;
		public static PageSpecification A4M36x2;

		public const String A4Oddy4x2Name = "Oddy A4 4x2";
		public const String A4M36x2Name = "M3 A4 6x2";

		static PageSpecification()
		{
			PageSpecification p;

			p = new PageSpecification(A4Oddy4x2Name, A4, 4, 2, new CFMargin(10, 5), new CFMargin(5));

			A4Oddy4x2 = p;

			p = new PageSpecification(A4M36x2Name, A4, 6, 2, new CFMargin(5, 5), new CFMargin(5));

			A4M36x2 = p;
		}

		public PageSpecification(
			String name, XSize size, Int32 rowPerPage, Int32 columnPerPage,
			CFMargin pageMargin, CFMargin labelMargin)
		{
			this.Name = name;
			this.PageSize = size;
			this.RowPerPage = rowPerPage;
			this.ColumnPerPage = columnPerPage;
			this.PageMargin = pageMargin;
			this.LabelMargin = labelMargin;
		}

		// TODO Make all the following readonly property.

		public Int32 LabelsPerPage
		{
			get
			{
				return this.RowPerPage * this.ColumnPerPage;
			}
		}

		public XSize PageSizeAfterMargin
		{
			get
			{
				return new XSize(
					this.PageSize.Width - this.PageMargin.Left - this.PageMargin.Right,
					this.PageSize.Height - this.PageMargin.Top - this.PageMargin.Bottom);
			}
		}

		public XSize LabelSize
		{
			get
			{
				XSize pageSizeAfterMargin = this.PageSizeAfterMargin;
				return new XSize(
					pageSizeAfterMargin.Width / this.ColumnPerPage,
					pageSizeAfterMargin.Height / this.RowPerPage);
			}
		}

		public XSize LabelSizeAfterMargin
		{
			get
			{
				XSize labelSize = this.LabelSize;

				return new XSize(
					labelSize.Width - this.LabelMargin.Left - this.LabelMargin.Right,
					labelSize.Height - this.LabelMargin.Top - this.LabelMargin.Bottom);
			}
		}

		public XPoint PageMarginOffset
		{
			get
			{
				return new XPoint(this.PageMargin.Left, this.PageMargin.Top);
			}
		}

		public XPoint LabelMarginOffset
		{
			get
			{
				return new XPoint(this.LabelMargin.Left, this.LabelMargin.Top);
			}
		}

		public XRect GetLabelRectangle(int r, int c)
		{
			XPoint pageMarginOffset = this.PageMarginOffset;
			XSize labelSize = this.LabelSize;
			XSize labelSizeAfterMargin = this.LabelSizeAfterMargin;

			XPoint labelOffset = new XPoint(
				pageMarginOffset.X + c * labelSize.Width + this.LabelMargin.Left,
				pageMarginOffset.Y + r * labelSize.Height + this.LabelMargin.Top);

			XRect labelRectangle = new XRect(labelOffset, labelSizeAfterMargin);

			return labelRectangle;
		}

		public static PageSpecification Get(String name)
		{
			if ("Oddy A4 4x2".Equals(name))
			{
				return A4Oddy4x2;
			}

			if ("M3 A4 6x2".Equals(name))
			{
				return A4M36x2;
			}

			return A4M36x2;
		}
	}
}
