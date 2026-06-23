namespace ExxamenKval
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmbClients = new ComboBox();
            cmbTours = new ComboBox();
            numAdults = new NumericUpDown();
            numChildren = new NumericUpDown();
            dtpDeparture = new DateTimePicker();
            txtTotalPrice = new TextBox();
            btnSave = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)numAdults).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numChildren).BeginInit();
            SuspendLayout();
            // 
            // cmbClients
            // 
            cmbClients.FormattingEnabled = true;
            cmbClients.Location = new Point(54, 191);
            cmbClients.Name = "cmbClients";
            cmbClients.Size = new Size(391, 28);
            cmbClients.TabIndex = 0;
            // 
            // cmbTours
            // 
            cmbTours.FormattingEnabled = true;
            cmbTours.Location = new Point(54, 262);
            cmbTours.Name = "cmbTours";
            cmbTours.Size = new Size(391, 28);
            cmbTours.TabIndex = 1;
            // 
            // numAdults
            // 
            numAdults.Location = new Point(533, 191);
            numAdults.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numAdults.Name = "numAdults";
            numAdults.Size = new Size(150, 27);
            numAdults.TabIndex = 2;
            // 
            // numChildren
            // 
            numChildren.Location = new Point(533, 263);
            numChildren.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numChildren.Name = "numChildren";
            numChildren.Size = new Size(150, 27);
            numChildren.TabIndex = 3;
            // 
            // dtpDeparture
            // 
            dtpDeparture.Format = DateTimePickerFormat.Short;
            dtpDeparture.Location = new Point(54, 337);
            dtpDeparture.Name = "dtpDeparture";
            dtpDeparture.Size = new Size(250, 27);
            dtpDeparture.TabIndex = 4;
            // 
            // txtTotalPrice
            // 
            txtTotalPrice.Location = new Point(54, 413);
            txtTotalPrice.Name = "txtTotalPrice";
            txtTotalPrice.ReadOnly = true;
            txtTotalPrice.Size = new Size(250, 27);
            txtTotalPrice.TabIndex = 5;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(429, 413);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(210, 107);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ButtonHighlight;
            label1.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(288, 54);
            label1.Name = "label1";
            label1.Size = new Size(218, 38);
            label1.TabIndex = 7;
            label1.Text = "ОТМЫВ ДЕНЕГ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.CadetBlue;
            ClientSize = new Size(730, 681);
            Controls.Add(label1);
            Controls.Add(btnSave);
            Controls.Add(txtTotalPrice);
            Controls.Add(dtpDeparture);
            Controls.Add(numChildren);
            Controls.Add(numAdults);
            Controls.Add(cmbTours);
            Controls.Add(cmbClients);
            ForeColor = SystemColors.ControlDarkDark;
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)numAdults).EndInit();
            ((System.ComponentModel.ISupportInitialize)numChildren).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cmbClients;
        private ComboBox cmbTours;
        private NumericUpDown numAdults;
        private NumericUpDown numChildren;
        private DateTimePicker dtpDeparture;
        private TextBox txtTotalPrice;
        private Button btnSave;
        private Label label1;
    }
}
