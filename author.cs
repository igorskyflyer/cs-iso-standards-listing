using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace seminarski_is
{
    public partial class author : Form
    {
        public author()
        {
            InitializeComponent();
        }

        private void authors_Load(object sender, EventArgs e)
        {
            label1.Parent = pictureBox1;
        }
    }
}
