namespace WinFormsDemo.Forms
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
            this.checkBoxShowBounds = new System.Windows.Forms.CheckBox();
            this.checkBoxIdScreen = new System.Windows.Forms.CheckBox();
            this.buttonPickForeColor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBoxShowBounds
            // 
            this.checkBoxShowBounds.AutoSize = true;
            this.checkBoxShowBounds.Location = new System.Drawing.Point(12, 12);
            this.checkBoxShowBounds.Name = "checkBoxShowBounds";
            this.checkBoxShowBounds.Size = new System.Drawing.Size(129, 17);
            this.checkBoxShowBounds.TabIndex = 2;
            this.checkBoxShowBounds.Text = "Show Screen Bounds";
            this.checkBoxShowBounds.UseVisualStyleBackColor = true;
            // 
            // checkBoxIdScreen
            // 
            this.checkBoxIdScreen.AutoSize = true;
            this.checkBoxIdScreen.Location = new System.Drawing.Point(12, 35);
            this.checkBoxIdScreen.Name = "checkBoxIdScreen";
            this.checkBoxIdScreen.Size = new System.Drawing.Size(97, 17);
            this.checkBoxIdScreen.TabIndex = 3;
            this.checkBoxIdScreen.Text = "Identify Screen";
            this.checkBoxIdScreen.UseVisualStyleBackColor = true;
            // 
            // buttonPickForeColor
            // 
            this.buttonPickForeColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPickForeColor.Location = new System.Drawing.Point(12, 58);
            this.buttonPickForeColor.Name = "buttonPickForeColor";
            this.buttonPickForeColor.Size = new System.Drawing.Size(260, 23);
            this.buttonPickForeColor.TabIndex = 4;
            this.buttonPickForeColor.Text = "Pick Foreground Color...";
            this.buttonPickForeColor.UseVisualStyleBackColor = true;
            this.buttonPickForeColor.Click += new System.EventHandler(this.buttonPickForeColor_Click);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 95);
            this.Controls.Add(this.buttonPickForeColor);
            this.Controls.Add(this.checkBoxIdScreen);
            this.Controls.Add(this.checkBoxShowBounds);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WinFormsDemo Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxShowBounds;
        private System.Windows.Forms.CheckBox checkBoxIdScreen;
        private System.Windows.Forms.Button buttonPickForeColor;
    }
}