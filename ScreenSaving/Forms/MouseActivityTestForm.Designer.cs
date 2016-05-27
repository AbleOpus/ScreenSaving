namespace ScreenSaving.Forms
{
    partial class MouseActivityTestForm
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
            if (disposing)
            {
                detector.Dispose();
                timerUpdate.Dispose();
                thresholdPen.Dispose();
                distancePen.Dispose();
            }
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
            this.numberBoxMoveThresh = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numberBoxMoveThresh)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(271, 403);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Distance To Move (threshold)";
            // 
            // numberBoxMoveThresh
            // 
            this.numberBoxMoveThresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numberBoxMoveThresh.Location = new System.Drawing.Point(449, 400);
            this.numberBoxMoveThresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.numberBoxMoveThresh.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numberBoxMoveThresh.Name = "numberBoxMoveThresh";
            this.numberBoxMoveThresh.Size = new System.Drawing.Size(112, 25);
            this.numberBoxMoveThresh.TabIndex = 1;
            this.numberBoxMoveThresh.ValueChanged += new System.EventHandler(this.numberBoxMoveThresh_ValueChanged);
            // 
            // MouseActivityTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 442);
            this.Controls.Add(this.numberBoxMoveThresh);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MouseActivityTestForm";
            this.Text = "Mouse Activity Test";
            ((System.ComponentModel.ISupportInitialize)(this.numberBoxMoveThresh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numberBoxMoveThresh;
    }
}