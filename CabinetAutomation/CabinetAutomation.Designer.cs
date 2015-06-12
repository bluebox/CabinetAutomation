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
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.customerNameTextBox = new System.Windows.Forms.TextBox();
			this.customerMobileTextBox = new System.Windows.Forms.TextBox();
			this.deliveryDateTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.submitButton = new System.Windows.Forms.Button();
			this.openPdfButton = new System.Windows.Forms.Button();
			this.openXmlButton = new System.Windows.Forms.Button();
			this.openPdfFolderButton = new System.Windows.Forms.Button();
			this.openXmlFolderButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.browseButton = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Customer Name";
			// 
			// panel1
			// 
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
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.deliveryDateTextBox);
			this.panel1.Controls.Add(this.customerMobileTextBox);
			this.panel1.Controls.Add(this.customerNameTextBox);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(61, 118);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(369, 213);
			this.panel1.TabIndex = 1;
			// 
			// customerNameTextBox
			// 
			this.customerNameTextBox.Location = new System.Drawing.Point(128, 4);
			this.customerNameTextBox.Name = "customerNameTextBox";
			this.customerNameTextBox.Size = new System.Drawing.Size(238, 20);
			this.customerNameTextBox.TabIndex = 1;
			this.customerNameTextBox.Text = "Mithun Dhali";
			// 
			// customerMobileTextBox
			// 
			this.customerMobileTextBox.Location = new System.Drawing.Point(128, 31);
			this.customerMobileTextBox.Name = "customerMobileTextBox";
			this.customerMobileTextBox.Size = new System.Drawing.Size(238, 20);
			this.customerMobileTextBox.TabIndex = 2;
			this.customerMobileTextBox.Text = "9833568580";
			// 
			// deliveryDateTextBox
			// 
			this.deliveryDateTextBox.Location = new System.Drawing.Point(128, 58);
			this.deliveryDateTextBox.Name = "deliveryDateTextBox";
			this.deliveryDateTextBox.Size = new System.Drawing.Size(238, 20);
			this.deliveryDateTextBox.TabIndex = 3;
			this.deliveryDateTextBox.Text = "28-Jun-2015";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 34);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(85, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Customer Mobile";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 61);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(71, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Delivery Date";
			// 
			// submitButton
			// 
			this.submitButton.Location = new System.Drawing.Point(128, 111);
			this.submitButton.Name = "submitButton";
			this.submitButton.Size = new System.Drawing.Size(75, 23);
			this.submitButton.TabIndex = 6;
			this.submitButton.Text = "Submit";
			this.submitButton.UseVisualStyleBackColor = true;
			this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
			// 
			// openPdfButton
			// 
			this.openPdfButton.Location = new System.Drawing.Point(128, 156);
			this.openPdfButton.Name = "openPdfButton";
			this.openPdfButton.Size = new System.Drawing.Size(75, 23);
			this.openPdfButton.TabIndex = 7;
			this.openPdfButton.Text = "Open";
			this.openPdfButton.UseVisualStyleBackColor = true;
			this.openPdfButton.Click += new System.EventHandler(this.openPdfButton_Click);
			// 
			// openXmlButton
			// 
			this.openXmlButton.Location = new System.Drawing.Point(128, 185);
			this.openXmlButton.Name = "openXmlButton";
			this.openXmlButton.Size = new System.Drawing.Size(75, 23);
			this.openXmlButton.TabIndex = 8;
			this.openXmlButton.Text = "Open";
			this.openXmlButton.UseVisualStyleBackColor = true;
			this.openXmlButton.Click += new System.EventHandler(this.openXmlButton_Click);
			// 
			// openPdfFolderButton
			// 
			this.openPdfFolderButton.Location = new System.Drawing.Point(210, 156);
			this.openPdfFolderButton.Name = "openPdfFolderButton";
			this.openPdfFolderButton.Size = new System.Drawing.Size(75, 23);
			this.openPdfFolderButton.TabIndex = 9;
			this.openPdfFolderButton.Text = "Open Folder";
			this.openPdfFolderButton.UseVisualStyleBackColor = true;
			this.openPdfFolderButton.Click += new System.EventHandler(this.openPdfFolderButton_Click);
			// 
			// openXmlFolderButton
			// 
			this.openXmlFolderButton.Location = new System.Drawing.Point(210, 184);
			this.openXmlFolderButton.Name = "openXmlFolderButton";
			this.openXmlFolderButton.Size = new System.Drawing.Size(75, 23);
			this.openXmlFolderButton.TabIndex = 10;
			this.openXmlFolderButton.Text = "Open Folder";
			this.openXmlFolderButton.UseVisualStyleBackColor = true;
			this.openXmlFolderButton.Click += new System.EventHandler(this.openXmlFolderButton_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(5, 161);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(57, 13);
			this.label4.TabIndex = 11;
			this.label4.Text = "Label PDF";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(3, 190);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(59, 13);
			this.label5.TabIndex = 12;
			this.label5.Text = "Beam XML";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(5, 89);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(52, 13);
			this.label6.TabIndex = 13;
			this.label6.Text = "Input Csv";
			// 
			// browseButton
			// 
			this.browseButton.Location = new System.Drawing.Point(128, 84);
			this.browseButton.Name = "browseButton";
			this.browseButton.Size = new System.Drawing.Size(75, 23);
			this.browseButton.TabIndex = 14;
			this.browseButton.Text = "Browse";
			this.browseButton.UseVisualStyleBackColor = true;
			this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog";
			this.openFileDialog1.Filter = "CSV File (*.csv)|*.csv";
			this.openFileDialog1.InitialDirectory = ".";
			this.openFileDialog1.RestoreDirectory = true;
			this.openFileDialog1.Title = "Open Biesse Cabinet output (CSV)";
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

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button openPdfButton;
		private System.Windows.Forms.Button submitButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox deliveryDateTextBox;
		private System.Windows.Forms.TextBox customerMobileTextBox;
		private System.Windows.Forms.TextBox customerNameTextBox;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button openXmlFolderButton;
		private System.Windows.Forms.Button openPdfFolderButton;
		private System.Windows.Forms.Button openXmlButton;
		private System.Windows.Forms.Button browseButton;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
	}
}

