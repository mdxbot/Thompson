using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication1
{
    public partial class Editer : Form
    {
        lexer lexer1 = new lexer();
        parsing sa = new parsing();
        public Editer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            semantic s1 = new semantic();
            textBox2.Clear();
            List<string> str = new List<string>();
            List<string> temp = new List<string>();
            temp = lexer1.createstr(textBox1.Text);
            foreach (var item in temp)
                str.Add(item);
            temp.Clear();
            temp = sa.prediction(lexer1);
            foreach (var item in temp)
                str.Add(item);
            temp.Clear();
            temp = s1.calculate(sa,lexer1);
            foreach (var item in temp)
                str.Add(item);
            temp.Clear();
            for (int i = 0; i < str.Count; i++)
            {
                textBox2.Text = textBox2.Text + str[i] + "\r\n";
            }
            if (textBox2.Text == "")
            {
                textBox2.Text = s1.output();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<char> first = new List<char>();
            //first = sa.first('b');
            //sa.follow();
            int i = 1;
        }

        private void Editer_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            lexer1.createdfa();
            sa.createtable();
        }
    }
}