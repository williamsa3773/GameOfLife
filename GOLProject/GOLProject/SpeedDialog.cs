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
    public partial class SpeedDialog : Form
    {
        public SpeedDialog()
        {
            InitializeComponent();
        }

        public int Number
        {
            get { return (int)numericUpDownNumber.Value; }
            set { numericUpDownNumber.Value = value; }
        }
    }
}
