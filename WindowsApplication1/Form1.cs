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
            int f0 = 1;
            nfa nfa1 = new nfa();
            dfa dfa1 = new dfa();
            f0 = nfa1.tonfa(textBox1.Text);
            dfa1.todfa(nfa1, textBox1.Text, f0);
            dfa1.mindfa();
            MessageBox.Show("" + dfa1.show(0, 'a') + "");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //List<List<int>> status = new List<List<int>>();
            //char[,] nfa = new char[10, 10];
            //nfa[0, 1] = '��';
            //nfa[0, 4] = '��';
            //nfa[1, 2] = 'a';
            //nfa[4, 5] = 'b';
            //nfa[2, 3] = '��';
            //nfa[5, 3] = '��';
            //List<int> start = new List<int>();
            //start.Add(0);
            //MessageBox.Show(smove(start, '��', status, nfa).ToString());
            //MessageBox.Show(smove(status[0], 'a', status, nfa).ToString());
            //MessageBox.Show(smove(status[1], 'a', status, nfa).ToString());
            //MessageBox.Show(smove(status[2], 'a', status, nfa).ToString());
            //MessageBox.Show(smove(status[3], 'a', status, nfa).ToString());
            //label1.Text = status[2][0].ToString();
        }
    }
}