using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/********************************************************
 * Settings Form class
 * by Bryce Graham
 * Holds the interface for the game's settigns
*********************************************************/

namespace BryceGrahamProject7a
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void debugCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (debugCheckBox.Checked)
            {
                Settings.DebugMode = true;
            }
            else Settings.DebugMode = false;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
