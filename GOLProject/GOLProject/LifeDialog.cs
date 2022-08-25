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
    public partial class LifeDialog : Form
    {
        public LifeDialog()
        {
            InitializeComponent();
        }

        public int Life
        {
            get { return (int)numericUpDownLife.Value; }
            set { numericUpDownLife.Value = value; }
        }
    }
}
