using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOLProject
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            InitializeComponent();
        }
        public bool Boundary
        {
            get { return checkBoxBoundary.Checked; }
            set { checkBoxBoundary.Checked = value; }
        }
        public bool Grid
        {
            get {  return checkBoxGrid.Checked; }
            set { checkBoxGrid.Checked = value; }
        }
        public int X
        {
            get { return (int)numericUpDownLength.Value; }
            set { numericUpDownLength.Value = value; }

        }
        public int Y
        {
            get { return (int)numericUpDownWidth.Value; }
            set { numericUpDownWidth.Value = value; }
        }
        public int Speed
        {
            get { return (int)numericUpDownSpeed.Value; }
            set { numericUpDownSpeed.Value = value; }
        }
        public int Life
        {
            get { return (int)numericUpDownLife.Value; }
            set { numericUpDownLife.Value = value; }

        }
        public void GridColor()
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = Properties.Settings.Default.GridColor;
            
            if(DialogResult.OK == dlg.ShowDialog())
            {
                Properties.Settings.Default.GridColor = dlg.Color;  
            }
        }
        public void CellColor()
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = Properties.Settings.Default.CellColor;

            if (DialogResult.OK == dlg.ShowDialog())
            {
                Properties.Settings.Default.CellColor = dlg.Color;
            }
        }
        public void BackGroundColor()
        {
            ColorDialog dlg = new ColorDialog();
            dlg.Color = Properties.Settings.Default.BackGroundColor;
            
            if(DialogResult.OK == dlg.ShowDialog())
            {
                Properties.Settings.Default.BackGroundColor = dlg.Color;  
            }
        }

        private void UpdateGridColor_Click(object sender, EventArgs e)
        {
            GridColor();
        }

        private void UpdateCellColor_Click(object sender, EventArgs e)
        {
            CellColor();
        }

        private void UpdateBackColor_Click(object sender, EventArgs e)
        {
            BackGroundColor();
        }
    }
}
