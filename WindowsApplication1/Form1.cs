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
            int count = 1;//总结点数
            int flag = 0;//记录未完成或
            int n = 0;//未完成左括号个数
            int m = 0;//层级
            s[0] = 0;
            f[0] = 1;
            nfa[s[0],f[0]] = 'ε';
            int num = textBox1.Text.Length;
            str = textBox1.Text.ToCharArray();
            for (int j = 0; j < num; j++)
            {
                //判断字符
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
                            nfa[f[m], f[m - 1]] = 'ε';
                            m = m - 1;
                        }
                    }
                }
                else if (str[j] == '|')
                {
                    if (flag == 1)
                    {
                        nfa[f[m], f[m - 1]] = 'ε';
                        count = count + 1;
                        s[m] = count;
                        nfa[s[m - 1], s[m]] = 'ε';
                    }
                    else if (flag == 0)
                    {
                        flag = 1;
                        m = m + 1;
                        f[m] = f[m - 1];
                        count = count + 1;
                        f[m - 1] = count;
                        nfa[f[m], f[m - 1]] = 'ε';
                        count = count + 1;
                        s[m] = count;
                        nfa[s[m - 1], s[m]] = 'ε';
                        f[m] = s[m];
                    }
                }
                else if (str[j] == '*')
                {
                    if (str[j - 1] != ')' && str[j - 1] != '*')
                    {
                        nfa[f[m] - 1, f[m]] = 'ε';
                        nfa[f[m], f[m] + 1] = str[j - 1];
                        count = count + 1;
                        f[m] = count;
                        m = m + 1;
                        s[m] = f[m - 1] - 2;
                        f[m] = f[m - 1];
                    }
                    count = count + 1;
                    nfa[f[m], s[m] + 1] = 'ε';
                    f[m] = count;
                    nfa[s[m], f[m]] = 'ε';
                    nfa[f[m] - 1, f[m]] = 'ε';
                    count = count + 1;
                    f[m] = count;
                    nfa[f[m] - 1, f[m]] = 'ε';
                    f[m - 1] = f[m];
                    m = m - 1;
                }
                else if (str[j] == '(')
                {
                    f[m] = f[m] + 1;
                    nfa[f[m] - 1, f[m]] = 'ε';
                    count = count + 1;  
                    n = n + 1;
                    m = m + 1;
                    count=count+1;
                    s[m] = f[m - 1] - 1;
                    f[m] = f[m - 1] + 1;
                    nfa[f[m - 1], f[m]] = 'ε';
                }
                else if (str[j] == ')')
                {
                    n = n - 1;
                    if (flag == 1)
                    {
                        flag = flag - 1;
                        nfa[f[m], f[m - 1]] = 'ε';
                        m = m - 1;
                    }
                    f[m - 1] = count + 1;
                    count = count + 1;
                    nfa[f[m], f[m - 1]] = 'ε';
                    f[m] = f[m - 1];
                    if (str[j + 1] != '*')
                    {
                        m = m - 1;
                    }
                }
            }
            //MessageBox.Show("" + count + " " + f[0] + " " + nfa[3, 4] + " " + nfa[6, 7] + "");
        }
        List<int> move(int i, char x, char[,] array1)
        {
            List<int> t = new List<int>();
            int max=array1.GetLength(1);
            for (int j = 0; j < max; j = j + 1)
            {
                if (array1[i, j] == x)
                {
                    t.Add(j);
                }
            }
            return t;
        }
        int smove(List<int> start, char r, List<List<int>> status, char[,] array1)
        {
            int num = 404;
            int l1 = 0, l2 = 0;
            int n = start.Count;
            List<int> temp1 = new List<int>();
            List<int> temp2 = new List<int>();
            List<int>  closure=new List<int>();
            for (int i = 0; i < n; i++)
            {
                temp1 = move(start[i], r, array1);
                if (temp1.Count == 0)
                {
                    temp2 = move(start[i], 'ε', array1);
                }
                else
                {
                    l1 = temp1.Count;
                    for (int j = 0; j < l1; j++)
                    {
                        temp2 = move(temp1[j], 'ε', array1);
                        if (closure.Contains(temp1[j]) == false)
                        {
                            closure.Add(temp1[j]);
                        }
                    }
                }
                if (temp2.Count != 0)
                {
                    l2 = temp2.Count;
                    for (int b = 0; b < l2; b++)
                    {
                        if (closure.Contains(temp2[b]) == false)
                        {
                            closure.Add(temp2[b]);
                        }
                    }
                }
            }
            int m = status.Count;
            int flag = 0;
            if (m == 0)
            {
                closure.Add(0);
                status.Add(closure);
                num = 0;
            }
            else
            {
                for (int k = 0; k < m; k++)
                {
                    flag = 0;
                    if (closure.Count == status[k].Count)
                    {
                        for (int a = 0; a < m; a++)
                        {
                            if (status[k].Contains(closure[a]) == false)
                            {
                                flag = 1;
                                break;
                            }
                        }
                        if (flag == 0)
                        {
                            num = k;
                            break;
                        }
                    }
                    else if (closure.Count != 0)
                    {
                        flag = 1;
                    }
                }
                if (flag == 1)
                {
                    status.Add(closure);
                    num = status.Count - 1;
                }
            }
            return num;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<List<int>> status=new List<List<int>>();
            char[,] nfa = new char[10, 10];
            nfa[0, 1] = 'ε';
            nfa[0, 4] = 'ε';
            nfa[1, 2] = 'a';
            nfa[4, 5] = 'b';
            nfa[2, 3] = 'ε';
            nfa[5, 3] = 'ε';
            List<int> start=new List<int>();
            start.Add(0);
            MessageBox.Show(smove(start, 'ε', status, nfa).ToString());
            MessageBox.Show(smove(status[0], 'a', status, nfa).ToString());
            MessageBox.Show(smove(status[1], 'a', status, nfa).ToString());
            MessageBox.Show(smove(status[2], 'a', status, nfa).ToString());
            MessageBox.Show(smove(status[3], 'a', status, nfa).ToString());
            label1.Text = status[2][0].ToString();
        }
    }
}