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
            char[,] nfa = new char[30, 30];
            char[] str;
            int[] s = new int[10];
            int[] f = new int[10];
            int count = 1;//�ܽ����
            int flag = 0;//��¼δ��ɻ�
            int n = 0;//δ��������Ÿ���
            int m = 0;//�㼶
            s[0] = 0;
            f[0] = 1;
            nfa[s[0],f[0]] = '��';
            int num = textBox1.Text.Length;
            str = textBox1.Text.ToCharArray();
            for (int j = 0; j < num; j++)
            {
                //�ж��ַ�
                if ((str[j] >= 'a' && str[j] <= 'z') || (str[j] >= 'A' && str[j] <= 'Z') ||
                    (str[j] >= '0' && str[j] <= '9'))
                {
                    nfa[f[m], f[m] + 1] = str[j];
                    count = count + 1;
                    f[m] = count; 
                    if (j + 1 == num)
                    {
                        if (flag == 1)
                        {
                            nfa[f[m], f[m - 1]] = '��';
                            m = m - 1;
                        }
                    }
                }
                else if (str[j] == '|')
                {
                    if(flag == 1)
                    {
                        nfa[f[m], f[m - 1]] = '��';
                        count = count + 1;
                        s[m] = count;
                        nfa[s[m - 1], s[m]] = '��';
                    }
                    else if(flag == 0)
                    {
                        flag = 1;
                        m = m + 1;
                        f[m] = f[m - 1];
                        count = count + 1;
                        f[m - 1] = count;
                        nfa[f[m], f[m - 1]] = '��';
                        count = count + 1;
                        s[m] = count;
                        nfa[s[m - 1], s[m]] = '��';
                        f[m] = s[m];
                    }
                }
                else if (str[j] == '*')
                {
                    if (str[j - 1] != ')' && str[j - 1] != '*')
                    {
                        nfa[f[m] - 1, f[m]] = '��';
                        nfa[f[m], f[m] + 1] = str[j - 1];
                        count = count + 1;
                        f[m] = count;
                        m = m + 1;
                        s[m] = f[m - 1] - 2;
                        f[m] = f[m - 1];
                    }
                    count = count + 1;
                    nfa[f[m], s[m] + 1] = '��';
                    f[m] = count;
                    nfa[s[m], f[m]] = '��';
                    nfa[f[m] - 1, f[m]] = '��';
                    count = count + 1;
                    f[m] = count;
                    nfa[f[m] - 1, f[m]] = '��';
                    f[m - 1] = f[m];
                    m = m - 1;
                }
                else if (str[j] == '(')
                {
                    f[m] = f[m] + 1;
                    nfa[f[m] - 1, f[m]] = '��';
                    count = count + 1;  
                    n = n + 1;
                    m = m + 1;
                    count=count+1;
                    s[m] = f[m - 1] - 1;
                    f[m] = f[m - 1] + 1;
                    nfa[f[m - 1], f[m]] = '��';
                }
                else if (str[j] == ')')
                {
                    n = n - 1;
                    if (flag == 1)
                    {
                        flag = flag - 1;
                        nfa[f[m], f[m - 1]] = '��';
                        m = m - 1;
                    }
                    f[m - 1] = count + 1;
                    count = count + 1;
                    nfa[f[m], f[m - 1]] = '��';
                    f[m] = f[m - 1];
                    if (str[j + 1] != '*')
                    {
                        m = m - 1;
                    }
                }
            }
            MessageBox.Show("" + count + " " + f[0] + " " + nfa[3, 4] + " " + nfa[6, 7] + "");
        }
    }
}