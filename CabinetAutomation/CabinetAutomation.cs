using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CabinetAutomation
{
	public partial class CabinetAutomation : Form
	{
		private String inputFilePath = null;
		private String labelFilePath = null;
		private String xmlFilePath = null;
		private LabelGenerator generator = new LabelGenerator();

		public CabinetAutomation()
		{
			InitializeComponent();
		}

		private void submitButton_Click(object sender, EventArgs e)
		{
			if (this.inputFilePath == null)
			{
				MessageBox.Show("Please select input file.");

				return;
			}

			if (this.customerNameTextBox.Text.Length < 4)
			{
				MessageBox.Show("Please enter customer name.");

				return;
			}

			if (this.customerMobileTextBox.Text.Length < 10)
			{
				MessageBox.Show("Please enter customer mobile");

				return;
			}

			if (this.deliveryDateTextBox.Text.Length < 5)
			{
				MessageBox.Show("Please enter delivery date.");

				return;
			}

			this.generator.customerName = this.customerNameTextBox.Text;
			this.generator.customerMobile = this.customerMobileTextBox.Text;
			this.generator.dueDate = this.deliveryDateTextBox.Text;

			String folder = Path.GetDirectoryName(this.inputFilePath);
			String pdfFileName = String.Format("{0}-{1}.pdf", this.generator.customerName, this.generator.customerMobile);
			
			this.labelFilePath = Path.Combine(folder, pdfFileName);

			CsvParser parser = new CsvParser(inputFilePath);

			this.generator.SaveToPdf(this.labelFilePath, parser.Parts);
		}

		private void openPdfButton_Click(object sender, EventArgs e)
		{
			if (this.labelFilePath != null)
			{
				System.Diagnostics.Process.Start(labelFilePath);
			}
		}

		private void openPdfFolderButton_Click(object sender, EventArgs e)
		{
			if (this.labelFilePath != null)
			{
				// String folder = Path.GetDirectoryName(this.labelFilePath);

				System.Diagnostics.Process.Start("explorer", "/select," + this.labelFilePath);
			}
		}

		private void openXmlButton_Click(object sender, EventArgs e)
		{
			if (this.xmlFilePath != null)
			{
				System.Diagnostics.Process.Start(xmlFilePath);
			}
		}

		private void openXmlFolderButton_Click(object sender, EventArgs e)
		{
			if (this.xmlFilePath != null)
			{
				String folder = Path.GetDirectoryName(this.xmlFilePath);

				System.Diagnostics.Process.Start(folder);
			}
		}

		private void browseButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == this.openFileDialog1.ShowDialog())
			{
				this.inputFilePath = this.openFileDialog1.FileName;
			}
		}
	}
}
