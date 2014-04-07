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
            char[,] nfa = new char[100,100];
            char[] str;
            int[] s = new int[10];
            int[] f = new int[10];
            int count=2;//总结点数
            int flag=0;//记录未完成或
            int n = 0;//未完成左括号个数
            int m=0;//起点终点计数
            s[0] = 0;
            f[0] = 1;
            nfa[s[0],f[0]] = 'ε';
            int num = textBox1.Text.Length;
            str=textBox1.Text.ToCharArray();
            for (int j = 0; j < num; j++)
            {
                //判断字符
                if ((str[j] >= 'a' && str[j] <= 'z') || (str[j] >= 'A' && str[j] <= 'Z') ||
                    (str[j] >= '0' && str[j] <= '9'))
                {
                    nfa[f[m],f[m] + 1] = str[j];
                    count = count + 1;
                    f[m] = count; 
                    if (j + 1 == num)
                    {
                        if (flag == 1)
                        {
                            nfa[f[m],f[m - 1]] = 'ε';
                        }
                    }
                }
                else if (str[j] == '|')
                {
                    if(flag == 1)
                    {
                        nfa[f[m],f[m - 1]] = 'ε';
                        count = count + 1;
                        s[m] = count;
                        nfa[s[m - 1],s[m]] = 'ε';
                    }
                    else if(flag == 0)
                    {
                        flag = 1;
                        m = m + 1;
                        f[m] = f[m - 1];
                        count = count + 1;
                        f[m - 1] = count;
                        nfa[f[m],f[m - 1]] = 'ε';
                        count = count + 1;
                        s[m] = count;
                        nfa[s[m - 1],s[m]] = 'ε';
                    }
                }
                else if (str[j] == '*')
                {
                    
                }
                else if (str[j] == '(')
                {
                    n = n + 1;
                }
                else if (str[j] == ')')
                {
                    n = n - 1;
                }
            }
            MessageBox.Show("" + count + " " + f[0] + "");
        }
    }
}