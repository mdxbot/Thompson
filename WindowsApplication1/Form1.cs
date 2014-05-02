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
        lexer lexer1 = new lexer();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            List<string> str = new List<string>();
            str = lexer1.createstr(textBox1.Text);
            lexer1.output();
            for (int i = 0; i < str.Count; i++)
            {
                textBox2.Text = textBox2.Text + str[i] + "\r\n";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            lexer1.createdfa();
        }
    }
}