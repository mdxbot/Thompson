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
            List<string> str = new List<string>();
            str.Add("<e>¡ú<e>+<t>|<t>");
            str.Add("<t>¡ú<t>*<f>|<f>");
            str.Add("<f>¡ú(<e>)|-<f>|a");
            sa.optimize(str);
        }

        private void Editer_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
            //lexer1.createdfa();
        }
    }
}