namespace GOLProject
{
    partial class SettingsDialog
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.numericUpDownLength = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownWidth = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSpeed = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLife = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.checkBoxBoundary = new System.Windows.Forms.CheckBox();
            this.checkBoxGrid = new System.Windows.Forms.CheckBox();
            this.buttonGridColor = new System.Windows.Forms.Button();
            this.buttonCellColor = new System.Windows.Forms.Button();
            this.buttonBackColor = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLife)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Set Boundary True/False";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Set Show Grid to True/False";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Set the board Max Length";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(81, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Set the boards Max Width";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(83, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Set Speed in Milliseconds";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 238);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Set Initial Cells Alive Percentages";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(118, 264);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Change Grid Color";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(120, 290);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Change Cell Color";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(79, 316);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Change Background Color";
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(114, 405);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(195, 405);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // numericUpDownLength
            // 
            this.numericUpDownLength.Location = new System.Drawing.Point(217, 158);
            this.numericUpDownLength.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownLength.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownLength.Name = "numericUpDownLength";
            this.numericUpDownLength.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownLength.TabIndex = 12;
            this.numericUpDownLength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDownWidth
            // 
            this.numericUpDownWidth.Location = new System.Drawing.Point(217, 184);
            this.numericUpDownWidth.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownWidth.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownWidth.Name = "numericUpDownWidth";
            this.numericUpDownWidth.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownWidth.TabIndex = 13;
            this.numericUpDownWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDownSpeed
            // 
            this.numericUpDownSpeed.Location = new System.Drawing.Point(217, 210);
            this.numericUpDownSpeed.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownSpeed.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSpeed.Name = "numericUpDownSpeed";
            this.numericUpDownSpeed.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownSpeed.TabIndex = 14;
            this.numericUpDownSpeed.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownLife
            // 
            this.numericUpDownLife.Location = new System.Drawing.Point(217, 236);
            this.numericUpDownLife.Name = "numericUpDownLife";
            this.numericUpDownLife.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownLife.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(101, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(182, 44);
            this.label7.TabIndex = 18;
            this.label7.Text = "Change the settings for the game. Settings will be saved when you exit game.";
            // 
            // checkBoxBoundary
            // 
            this.checkBoxBoundary.AutoSize = true;
            this.checkBoxBoundary.Location = new System.Drawing.Point(217, 108);
            this.checkBoxBoundary.Name = "checkBoxBoundary";
            this.checkBoxBoundary.Size = new System.Drawing.Size(78, 17);
            this.checkBoxBoundary.TabIndex = 19;
            this.checkBoxBoundary.Text = "True/False";
            this.checkBoxBoundary.UseVisualStyleBackColor = true;
            // 
            // checkBoxGrid
            // 
            this.checkBoxGrid.AutoSize = true;
            this.checkBoxGrid.Location = new System.Drawing.Point(217, 134);
            this.checkBoxGrid.Name = "checkBoxGrid";
            this.checkBoxGrid.Size = new System.Drawing.Size(78, 17);
            this.checkBoxGrid.TabIndex = 20;
            this.checkBoxGrid.Text = "True/False";
            this.checkBoxGrid.UseVisualStyleBackColor = true;
            // 
            // buttonGridColor
            // 
            this.buttonGridColor.Location = new System.Drawing.Point(217, 259);
            this.buttonGridColor.Name = "buttonGridColor";
            this.buttonGridColor.Size = new System.Drawing.Size(75, 23);
            this.buttonGridColor.TabIndex = 21;
            this.buttonGridColor.Text = "Change Color";
            this.buttonGridColor.UseVisualStyleBackColor = true;
            this.buttonGridColor.Click += new System.EventHandler(this.UpdateGridColor_Click);
            // 
            // buttonCellColor
            // 
            this.buttonCellColor.Location = new System.Drawing.Point(217, 285);
            this.buttonCellColor.Name = "buttonCellColor";
            this.buttonCellColor.Size = new System.Drawing.Size(75, 23);
            this.buttonCellColor.TabIndex = 22;
            this.buttonCellColor.Text = "Change Color";
            this.buttonCellColor.UseVisualStyleBackColor = true;
            this.buttonCellColor.Click += new System.EventHandler(this.UpdateCellColor_Click);
            // 
            // buttonBackColor
            // 
            this.buttonBackColor.Location = new System.Drawing.Point(217, 311);
            this.buttonBackColor.Name = "buttonBackColor";
            this.buttonBackColor.Size = new System.Drawing.Size(75, 23);
            this.buttonBackColor.TabIndex = 23;
            this.buttonBackColor.Text = "Change Color";
            this.buttonBackColor.UseVisualStyleBackColor = true;
            this.buttonBackColor.Click += new System.EventHandler(this.UpdateBackColor_Click);
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(384, 461);
            this.Controls.Add(this.buttonBackColor);
            this.Controls.Add(this.buttonCellColor);
            this.Controls.Add(this.buttonGridColor);
            this.Controls.Add(this.checkBoxGrid);
            this.Controls.Add(this.checkBoxBoundary);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.numericUpDownLife);
            this.Controls.Add(this.numericUpDownSpeed);
            this.Controls.Add(this.numericUpDownWidth);
            this.Controls.Add(this.numericUpDownLength);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SettingsDialog";
            this.Text = "SettingsDialog";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLife)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.NumericUpDown numericUpDownLength;
        private System.Windows.Forms.NumericUpDown numericUpDownWidth;
        private System.Windows.Forms.NumericUpDown numericUpDownSpeed;
        private System.Windows.Forms.NumericUpDown numericUpDownLife;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox checkBoxBoundary;
        private System.Windows.Forms.CheckBox checkBoxGrid;
        private System.Windows.Forms.Button buttonGridColor;
        private System.Windows.Forms.Button buttonCellColor;
        private System.Windows.Forms.Button buttonBackColor;
    }
}