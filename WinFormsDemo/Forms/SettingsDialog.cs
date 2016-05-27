using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinFormsDemo.Forms
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();
            checkBoxIdScreen.Checked = Settings.Default.ShowScreenID;
            checkBoxShowBounds.Checked = Settings.Default.ShowScreenBounds;
        }

        private void buttonPickForeColor_Click(object sender, EventArgs e)
        {
            using (var dialogColor = new ColorDialog())
            {
                dialogColor.Color = Settings.Default.ForeColor;
                dialogColor.FullOpen = true;

                if (dialogColor.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default.ForeColor = dialogColor.Color;
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Settings.Default.ShowScreenID = checkBoxIdScreen.Checked;
            Settings.Default.ShowScreenBounds = checkBoxShowBounds.Checked;
            Settings.Default.Save();
        }
    }
}
