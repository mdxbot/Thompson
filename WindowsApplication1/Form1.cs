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
            char[][] nfa=null;
            char[] str;
            List<int> s = new List<int>();
            List<int> f = new List<int>();
            //int count=0;//�ܽ����
            int flag=0;//��¼δ��ɻ�
            int n = 0;//δ��������Ÿ���
            int m=0;//����յ����
            s[0] = 0;
            f[0] = 1;
            nfa[s[0]][f[0]] = '��';
            int num = textBox1.Text.Length;
            str=textBox1.Text.ToCharArray();
            for (int j = 0; j < num; j++)
            {
                //�ж��ַ�
                if ((str[j] >= 'a' && str[j] <= 'z') || (str[j] >= 'A' && str[j] <= 'Z') ||
                    (str[j] >= '0' && str[j] <= '9'))
                {
                    if (j + 1 == num)
                    {
                        if (flag == 1)
                        {
                            //nfa[s[m]][s[m + 2]] = '��';
                            nfa[f[m] + 2][f[m]] = '��';
                            f[m] = f[m + 2] + 1;
                            flag = flag - 1;
                        }
                        else
                        {
                            nfa[f[m]][f[m] + 1] = str[j];
                            f[m] = f[m] + 1;
                        }
                    }
                    else
                    {
                        nfa[f[m]][f[m] + 1] = str[j];
                        f[m] = f[m] + 1;
                    }
                }
                else if (str[j] == '|')
                {
                    if(flag > 0)
                    {

                    }
                    else if(flag == 0)
                    {

                    }
                    flag = flag + 1;
                }
                else if (str[j] == '*')
                {
                    flag = 3;
                }
                else if (str[j] == '(')
                {
                    n = n + 1;
                }
                else if (str[j] == ')')
                {
                    n = n - 1;
                }
                //if (flag == 0)//Ϊ(
                //{

                //}
                //else if (flag == 1)//Ϊ�ַ�
                //{
                //    nfa[f[m]][f[m] + 1] = str[j];
                //    f[m] = f[m] + 1;
                //}
                //else if (flag == 2)//Ϊ|
                //{
                //    nfa[s[m]][s[m] + 1] = '��';
                //    s[m + 1] = s[m] + 1;
                //    nfa[f[m] + 1][f[m] + 2] = '��';
                //    f[m + 1] = f[m] + 1;
                //    f[m] = f[m] + 2;
                //    nfa[s[m]][f[m] + 1] = '��';
                //    s[m + 2] = f[m] + 1;
                //    //nfa[f[m + 2]][f[m]] = '��';
                //}
                //else if (flag == 3)//Ϊ*
                //{

                //}
            }
        }
    }
}