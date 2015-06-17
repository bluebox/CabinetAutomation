namespace CabinetAutomation
{
	partial class CabinetAutomation
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.deliveryDateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.browseButton = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.openXmlFolderButton = new System.Windows.Forms.Button();
			this.openPdfFolderButton = new System.Windows.Forms.Button();
			this.openXmlButton = new System.Windows.Forms.Button();
			this.openPdfButton = new System.Windows.Forms.Button();
			this.submitButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.label1 = new System.Windows.Forms.Label();
			this.quantityTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.codeFormatComboBox = new System.Windows.Forms.ComboBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.codeFormatComboBox);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.quantityTextBox);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.deliveryDateTimePicker);
			this.panel1.Controls.Add(this.browseButton);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.openXmlFolderButton);
			this.panel1.Controls.Add(this.openPdfFolderButton);
			this.panel1.Controls.Add(this.openXmlButton);
			this.panel1.Controls.Add(this.openPdfButton);
			this.panel1.Controls.Add(this.submitButton);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Location = new System.Drawing.Point(60, 62);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(369, 275);
			this.panel1.TabIndex = 1;
			// 
			// deliveryDateTimePicker
			// 
			this.deliveryDateTimePicker.Location = new System.Drawing.Point(128, 6);
			this.deliveryDateTimePicker.Name = "deliveryDateTimePicker";
			this.deliveryDateTimePicker.Size = new System.Drawing.Size(200, 20);
			this.deliveryDateTimePicker.TabIndex = 2;
			// 
			// browseButton
			// 
			this.browseButton.Location = new System.Drawing.Point(128, 87);
			this.browseButton.Name = "browseButton";
			this.browseButton.Size = new System.Drawing.Size(75, 23);
			this.browseButton.TabIndex = 14;
			this.browseButton.Text = "Browse";
			this.browseButton.UseVisualStyleBackColor = true;
			this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(5, 37);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(46, 13);
			this.label6.TabIndex = 13;
			this.label6.Text = "Quantity";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 211);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(59, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Beam XML";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(5, 182);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(57, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Label PDF";
			// 
			// openXmlFolderButton
			// 
			this.openXmlFolderButton.Location = new System.Drawing.Point(210, 205);
			this.openXmlFolderButton.Name = "openXmlFolderButton";
			this.openXmlFolderButton.Size = new System.Drawing.Size(75, 23);
			this.openXmlFolderButton.TabIndex = 10;
			this.openXmlFolderButton.Text = "Open Folder";
			this.openXmlFolderButton.UseVisualStyleBackColor = true;
			this.openXmlFolderButton.Click += new System.EventHandler(this.openXmlFolderButton_Click);
			// 
			// openPdfFolderButton
			// 
			this.openPdfFolderButton.Location = new System.Drawing.Point(210, 177);
			this.openPdfFolderButton.Name = "openPdfFolderButton";
			this.openPdfFolderButton.Size = new System.Drawing.Size(75, 23);
			this.openPdfFolderButton.TabIndex = 9;
			this.openPdfFolderButton.Text = "Open Folder";
			this.openPdfFolderButton.UseVisualStyleBackColor = true;
			this.openPdfFolderButton.Click += new System.EventHandler(this.openPdfFolderButton_Click);
			// 
			// openXmlButton
			// 
			this.openXmlButton.Location = new System.Drawing.Point(128, 206);
			this.openXmlButton.Name = "openXmlButton";
			this.openXmlButton.Size = new System.Drawing.Size(75, 23);
			this.openXmlButton.TabIndex = 8;
			this.openXmlButton.Text = "Open";
			this.openXmlButton.UseVisualStyleBackColor = true;
			this.openXmlButton.Click += new System.EventHandler(this.openXmlButton_Click);
			// 
			// openPdfButton
			// 
			this.openPdfButton.Location = new System.Drawing.Point(128, 177);
			this.openPdfButton.Name = "openPdfButton";
			this.openPdfButton.Size = new System.Drawing.Size(75, 23);
			this.openPdfButton.TabIndex = 7;
			this.openPdfButton.Text = "Open";
			this.openPdfButton.UseVisualStyleBackColor = true;
			this.openPdfButton.Click += new System.EventHandler(this.openPdfButton_Click);
			// 
			// submitButton
			// 
			this.submitButton.Location = new System.Drawing.Point(128, 116);
			this.submitButton.Name = "submitButton";
			this.submitButton.Size = new System.Drawing.Size(75, 23);
			this.submitButton.TabIndex = 6;
			this.submitButton.Text = "Submit";
			this.submitButton.UseVisualStyleBackColor = true;
			this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Delivery Date";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog";
			this.openFileDialog1.Filter = "CSV File (*.csv)|*.csv";
			this.openFileDialog1.InitialDirectory = ".";
			this.openFileDialog1.RestoreDirectory = true;
			this.openFileDialog1.Title = "Open Biesse Cabinet output (CSV)";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 92);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 15;
			this.label1.Text = "Input CSV";
			// 
			// quantityTextBox
			// 
			this.quantityTextBox.Location = new System.Drawing.Point(128, 34);
			this.quantityTextBox.Name = "quantityTextBox";
			this.quantityTextBox.Size = new System.Drawing.Size(200, 20);
			this.quantityTextBox.TabIndex = 16;
			this.quantityTextBox.Text = "1";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 63);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 13);
			this.label2.TabIndex = 17;
			this.label2.Text = "Code format";
			// 
			// codeFormatComboBox
			// 
			this.codeFormatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.codeFormatComboBox.FormattingEnabled = true;
			this.codeFormatComboBox.Items.AddRange(new object[] {
            "Filename",
            "Folder - Filename"});
			this.codeFormatComboBox.Location = new System.Drawing.Point(128, 60);
			this.codeFormatComboBox.Name = "codeFormatComboBox";
			this.codeFormatComboBox.Size = new System.Drawing.Size(200, 21);
			this.codeFormatComboBox.TabIndex = 18;
			// 
			// CabinetAutomation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(558, 497);
			this.Controls.Add(this.panel1);
			this.Name = "CabinetAutomation";
			this.Text = "HINSHISU Cabinet Automation";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button openPdfButton;
		private System.Windows.Forms.Button submitButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button openXmlFolderButton;
		private System.Windows.Forms.Button openPdfFolderButton;
		private System.Windows.Forms.Button openXmlButton;
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.DateTimePicker deliveryDateTimePicker;
		private System.Windows.Forms.ComboBox codeFormatComboBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox quantityTextBox;
		private System.Windows.Forms.Label label1;
	}
}

