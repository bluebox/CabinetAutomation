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
using CabinetAutomation.Cix;

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

			this.pageTypeComboBox.SelectedIndex = 0;
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

			Int32 grainType = GrainType.Default;

			this.labelGenerator.edgeBinding = edgeBindingCheckBox.Checked;
			this.labelGenerator.Quantity = quantity;
			this.labelGenerator.barcodeFormat = BarcodeFormat.Default;
			this.labelGenerator.page = PageSpecification.Get(this.pageTypeComboBox.SelectedItem as String);

			String csvFolderName = Path.GetDirectoryName(this.biesseCabinetCsvFilePath);
			String csvFileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.biesseCabinetCsvFilePath);
			String pdfFileName = String.Format("{0}-BarcodeLabels.pdf", csvFileNameWithoutExtension);
			
			this.biesseCncLabelFilePath = Path.Combine(csvFolderName, pdfFileName);
			this.biesseCabinetCsvParser = new CsvParser(biesseCabinetCsvFilePath);

			this.labelGenerator.SaveToPdf(this.biesseCncLabelFilePath, biesseCabinetCsvParser.Parts.Clone());

			String beamSawXmlFileNameFormat = String.Format("BeamSawXml-{0}{1}/{0}-{2}.xml", csvFileNameWithoutExtension, "{0}", "{1}");

			this.beamSawXmlFilePathFormat = Path.Combine(csvFolderName, beamSawXmlFileNameFormat);

			this.beamSawXmlGenerator.parts = biesseCabinetCsvParser.Parts.Clone();
			this.beamSawXmlGenerator.Quantity = quantity;

			beamSawXmlGenerator.Generate(this.beamSawXmlFilePathFormat);

			FinderUpdater fu = new FinderUpdater(csvFolderName);

			fu.FindAndUpdate();

			// this.openPdfButton_Click(this.openPdfFolderButton, null);
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
				foreach (BoardType boardType in this.biesseCabinetCsvParser.Parts.BoardTypes)
				{
					String beamSawXmlFilePath = String.Format(beamSawXmlFilePathFormat, boardType);
					
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
