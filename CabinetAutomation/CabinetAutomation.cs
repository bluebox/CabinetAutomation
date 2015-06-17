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

			this.deliveryDateTimePicker.Value = DateTime.Today.AddDays(7).Date;
			this.codeFormatComboBox.SelectedIndex = 0;
		}

		private void submitButton_Click(object sender, EventArgs e)
		{
			if (this.biesseCabinetCsvFilePath == null)
			{
				MessageBox.Show("Please select input file.");

				return;
			}

			Int32 quantity = 1;

			try
			{
				quantity = Int32.Parse(quantityTextBox.Text.Trim());
			}
			catch (FormatException)
			{
				MessageBox.Show("Please select a valid quantity.");
			}

			this.labelGenerator.Quantity = quantity;
			this.labelGenerator.DueDate = this.deliveryDateTimePicker.Value;
			this.labelGenerator.BarcodeFormat = BarcodeFormat.Parse(this.codeFormatComboBox.SelectedItem as String);

			String folder = Path.GetDirectoryName(this.biesseCabinetCsvFilePath);
			String pdfFileName = String.Format("BarcodeLabels.pdf");
			
			this.biesseCncLabelFilePath = Path.Combine(folder, pdfFileName);
			this.biesseCabinetCsvParser = new CsvParser(biesseCabinetCsvFilePath);

			this.labelGenerator.SaveToPdf(this.biesseCncLabelFilePath, biesseCabinetCsvParser.Parts);

			String beamSawXmlFileNameFormat = "BeamSaw-{0}.xml";

			this.beamSawXmlFilePathFormat = Path.Combine(folder, beamSawXmlFileNameFormat);

			beamSawXmlGenerator.Generate(quantity, biesseCabinetCsvParser.Parts, this.beamSawXmlFilePathFormat);

			this.openPdfButton_Click(this.openPdfFolderButton, null);
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
