namespace Samplenator
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtFilename = new System.Windows.Forms.TextBox();
            this.plotDgrDistr = new OxyPlot.WindowsForms.PlotView();
            this.plotCumDgrDistr = new OxyPlot.WindowsForms.PlotView();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.comboMethod = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1082, 16);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "Load File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtFilename
            // 
            this.txtFilename.Enabled = false;
            this.txtFilename.Location = new System.Drawing.Point(14, 16);
            this.txtFilename.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.Size = new System.Drawing.Size(1061, 27);
            this.txtFilename.TabIndex = 1;
            // 
            // plotDgrDistr
            // 
            this.plotDgrDistr.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.plotDgrDistr.Location = new System.Drawing.Point(14, 51);
            this.plotDgrDistr.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.plotDgrDistr.Name = "plotDgrDistr";
            this.plotDgrDistr.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotDgrDistr.Size = new System.Drawing.Size(635, 407);
            this.plotDgrDistr.TabIndex = 2;
            this.plotDgrDistr.Text = "plotDgrDistribution";
            this.plotDgrDistr.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotDgrDistr.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotDgrDistr.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // plotCumDgrDistr
            // 
            this.plotCumDgrDistr.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.plotCumDgrDistr.Location = new System.Drawing.Point(656, 51);
            this.plotCumDgrDistr.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.plotCumDgrDistr.Name = "plotCumDgrDistr";
            this.plotCumDgrDistr.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotCumDgrDistr.Size = new System.Drawing.Size(605, 407);
            this.plotCumDgrDistr.TabIndex = 3;
            this.plotCumDgrDistr.Text = "plotDgrDistribution";
            this.plotCumDgrDistr.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotCumDgrDistr.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotCumDgrDistr.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(14, 466);
            this.txtOutput.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(635, 320);
            this.txtOutput.TabIndex = 4;
            // 
            // comboMethod
            // 
            this.comboMethod.FormattingEnabled = true;
            this.comboMethod.Location = new System.Drawing.Point(787, 467);
            this.comboMethod.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboMethod.Name = "comboMethod";
            this.comboMethod.Size = new System.Drawing.Size(472, 28);
            this.comboMethod.TabIndex = 5;
            this.comboMethod.SelectedIndexChanged += new System.EventHandler(this.comboMethod_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(655, 469);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "sampling method";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Location = new System.Drawing.Point(662, 509);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.Size = new System.Drawing.Size(597, 277);
            this.propertyGrid.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 804);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboMethod);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.plotCumDgrDistr);
            this.Controls.Add(this.plotDgrDistr);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Samplenator 3000";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button button1;
        private TextBox txtFilename;
        private OxyPlot.WindowsForms.PlotView plotDgrDistr;
        private OxyPlot.WindowsForms.PlotView plotCumDgrDistr;
        private TextBox txtOutput;
        private ComboBox comboMethod;
        private Label label1;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private PropertyGrid propertyGrid;
    }
}