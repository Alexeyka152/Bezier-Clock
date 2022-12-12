using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Часы_безье
{
    public partial class Form1 : Form
    {
        BasierClockControl clkctl;

        public Form1()
        {
            clkctl = new Часы_безье.BasierClockControl();
            clkctl.Parent = this;
            clkctl.Time = DateTime.Now;
            clkctl.Dock = DockStyle.Fill;
            clkctl.BackColor = Color.Lime;
            clkctl.ForeColor = Color.Blue;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            clkctl.Time = DateTime.Now;
        }
    }
}
