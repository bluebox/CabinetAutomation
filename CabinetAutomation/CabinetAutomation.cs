using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CabinetAutomation.BiesseCabinet;
using CabinetAutomation.BiesseCNC;
using CabinetAutomation.BiesseBeamSaw;

namespace CabinetAutomation
{
	public partial class CabinetAutomation : Form
	{
		private String biesseCabinetCsvFilePath = null;
		private String biesseCncLabelFilePath = null;
		private String beamSawXmlFilePathFormat = null;
		private CsvParser biesseCabinetCsvParser = null;
		private LabelGenerator labelGenerator = new LabelGenerator();
		private XmlGenerator beamSawXmlGenerator = new XmlGenerator();

		public CabinetAutomation()
		{
			InitializeComponent();
		}

		private void submitButton_Click(object sender, EventArgs e)
		{
			if (this.biesseCabinetCsvFilePath == null)
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

			this.labelGenerator.customerName = this.customerNameTextBox.Text;
			this.labelGenerator.customerMobile = this.customerMobileTextBox.Text;
			this.labelGenerator.dueDate = this.deliveryDateTextBox.Text;

			String folder = Path.GetDirectoryName(this.biesseCabinetCsvFilePath);
			String pdfFileName = String.Format("{0}-{1}.pdf", this.labelGenerator.customerName, this.labelGenerator.customerMobile);
			
			this.biesseCncLabelFilePath = Path.Combine(folder, pdfFileName);
			this.biesseCabinetCsvParser = new CsvParser(biesseCabinetCsvFilePath);

			this.labelGenerator.SaveToPdf(this.biesseCncLabelFilePath, biesseCabinetCsvParser.Parts);

			String beamSawXmlFileNameFormat = String.Format("{0}-{1}-{2}.xml", 
				this.labelGenerator.customerName, this.labelGenerator.customerMobile, "{0}");

			this.beamSawXmlFilePathFormat = Path.Combine(folder, beamSawXmlFileNameFormat);

			
			beamSawXmlGenerator.Generate(biesseCabinetCsvParser.Parts, this.beamSawXmlFilePathFormat);

			this.openPdfButton_Click(this.openPdfButton, null);
			this.openXmlButton_Click(this.openXmlButton, null);
		}

		private void openPdfButton_Click(object sender, EventArgs e)
		{
			if (this.biesseCncLabelFilePath != null)
			{
				System.Diagnostics.Process.Start(biesseCncLabelFilePath);
			}
		}

		private void openPdfFolderButton_Click(object sender, EventArgs e)
		{
			if (this.biesseCncLabelFilePath != null)
			{
				// String folder = Path.GetDirectoryName(this.labelFilePath);

				System.Diagnostics.Process.Start("explorer", "/select," + this.biesseCncLabelFilePath);
			}
		}

		private void openXmlButton_Click(object sender, EventArgs e)
		{
			if (this.beamSawXmlFilePathFormat != null)
			{
				HashSet<Decimal> thicknessValues = this.biesseCabinetCsvParser.Parts.ThicknessValues;

				foreach (Decimal thickness in thicknessValues)
				{
					String beamSawXmlFilePath = String.Format(beamSawXmlFilePathFormat, thickness);
					System.Diagnostics.Process.Start(beamSawXmlFilePath);
				}
			}
		}

		private void openXmlFolderButton_Click(object sender, EventArgs e)
		{
			if (this.beamSawXmlFilePathFormat != null)
			{
				String folder = Path.GetDirectoryName(this.beamSawXmlFilePathFormat);

				System.Diagnostics.Process.Start(folder);
			}
		}

		private void browseButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == this.openFileDialog1.ShowDialog())
			{
				this.biesseCabinetCsvFilePath = this.openFileDialog1.FileName;
			}
		}
	}
}
