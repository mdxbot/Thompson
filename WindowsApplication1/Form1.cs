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
            lexer1.output(textBox1.Text);
            MessageBox.Show("" + lexer1.output(textBox1.Text) + "");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            nfa nfa1 = new nfa();
            nfa1.tonfa("a|b|c|d");
            MessageBox.Show("123");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            lexer1.createdfa();
        }
    }
}