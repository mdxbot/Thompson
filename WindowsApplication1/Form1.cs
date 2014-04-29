using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lexer lexer1 = new lexer();
            lexer1.createdfa();
            MessageBox.Show("" + 123 + "");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}