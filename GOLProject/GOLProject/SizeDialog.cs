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
    public partial class SizeDialog : Form
    {
        public SizeDialog()
        {
            InitializeComponent();
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
    }
}
