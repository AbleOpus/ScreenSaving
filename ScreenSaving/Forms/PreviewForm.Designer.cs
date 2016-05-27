namespace ScreenSaving.Forms
{
    partial class PreviewForm
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
            this.panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel.Location = new System.Drawing.Point(64, 12);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(152, 112);
            this.panel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(267, 85);
            this.label1.TabIndex = 1;
            this.label1.Text = "This is what the preview will look like in the screensaver settings dialog. The o" +
    "fficial preview window is 152x112.";
            // 
            // PreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 232);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PreviewForm";
            this.Text = "Preview Debugger";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label label1;
    }
}