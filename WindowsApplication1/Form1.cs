using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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
            nfa nfa1 = new nfa();
            dfa dfa1 = new dfa();
            nfa1.getch(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //List<List<int>> status = new List<List<int>>();
            //char[,] nfa = new char[10, 10];
            //nfa[0, 1] = '¦Å';
            //nfa[0, 4] = '¦Å';
            //nfa[1, 2] = 'a';
            //nfa[4, 5] = 'b';
            //nfa[2, 3] = '¦Å';
            //nfa[5, 3] = '¦Å';
            //List<int> start = new List<int>();
            //start.Add(0);
            //MessageBox.Show(smove(start, '¦Å', status, nfa).ToString());
            //MessageBox.Show(smove(status[0], 'a', status, nfa).ToString());
            //MessageBox.Show(smove(status[1], 'a', status, nfa).ToString());
            //MessageBox.Show(smove(status[2], 'a', status, nfa).ToString());
            //MessageBox.Show(smove(status[3], 'a', status, nfa).ToString());
            //label1.Text = status[2][0].ToString();
        }
    }
}