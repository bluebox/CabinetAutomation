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
			this.grainComboBox.SelectedIndex = 0;
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

			Int32 grainType = GrainType.Parse(grainComboBox.Text);

			this.labelGenerator.edgeBinding = edgeBindingCheckBox.Checked;
			this.labelGenerator.Quantity = quantity;
			this.labelGenerator.DueDate = this.deliveryDateTimePicker.Value;
			this.labelGenerator.barcodeFormat = BarcodeFormat.Parse(this.codeFormatComboBox.SelectedItem as String);

			String csvFolderName = Path.GetDirectoryName(this.biesseCabinetCsvFilePath);
			String csvFileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.biesseCabinetCsvFilePath);
			String pdfFileName = String.Format("{0}-BarcodeLabels.pdf", csvFileNameWithoutExtension);
			
			this.biesseCncLabelFilePath = Path.Combine(csvFolderName, pdfFileName);
			this.biesseCabinetCsvParser = new CsvParser(biesseCabinetCsvFilePath);

			this.labelGenerator.SaveToPdf(this.biesseCncLabelFilePath, biesseCabinetCsvParser.Parts);

			String beamSawXmlFileNameFormat = String.Format("{0}-BeamSaw-{1}.xml", csvFileNameWithoutExtension, "{0}");

			this.beamSawXmlFilePathFormat = Path.Combine(csvFolderName, beamSawXmlFileNameFormat);

			beamSawXmlGenerator.Generate(quantity, biesseCabinetCsvParser.Parts, grainType, this.beamSawXmlFilePathFormat);

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
