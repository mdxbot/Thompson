using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1
{
    class nfa
    {
        private char[,] list = new char[300, 300];
        private int count;
        private int f0;
        
        public int tonfa(string text)
        {
            char[] str;
            int[] s = new int[10];
            int[] f = new int[10];
            int[] t = new int[10];
            int[] h = new int[10];
            count = 1;//总结点数
            int flag = 0;//记录未完成或
            int n = 0;//未完成左括号个数
            int m = 0;//层级
            s[0] = 0;
            f[0] = 1;
            list[s[0], f[0]] = 'ε';
            int num = text.Length;
            str = text.ToCharArray();
            for (int j = 0; j < num; j++)
            {
                //判断字符
                if ((str[j] >= 'a' && str[j] <= 'z') || (str[j] >= 'A' && str[j] <= 'Z') ||
                    (str[j] >= '0' && str[j] <= '9') || str[j] == '.' || str[j] == '_' ||
                    str[j] == '+' || str[j] == '-' || str[j] == '*' || str[j] == '/' || 
                    str[j] == '\t' || str[j] == '\n' || str[j] == '\r' || str[j] == ' ' ||
                    str[j] == ';' || str[j] == '(' || str[j] == ')' || str[j] == '^' || str[j] == 'ε')
                {
                    list[f[m], f[m] + 1] = str[j];
                    count = count + 1;
                    f[m] = count;
                    if (j + 1 == num)
                    {
                        if (flag == 1)
                        {
                            list[f[m], f[m - 1]] = 'ε';
                            m = m - 1;
                        }
                    }
                }
                else if (str[j] == '|')
                {
                    if (flag >= 1 && t[flag] == 0)
                    {
                        list[f[m], h[flag]] = 'ε';
                        count = count + 1;
                        s[m] = count;
                        list[s[m - 1], s[m]] = 'ε';
                        f[m] = s[m];
                    }
                    else if (flag == 0 || (flag >= 1 && t[flag] != 0))
                    {
                        flag = flag + 1;
                        m = m + 1;
                        f[m] = f[m - 1];
                        count = count + 1;
                        f[m - 1] = count;
                        list[f[m], f[m - 1]] = 'ε';
                        h[flag] = f[m - 1];
                        count = count + 1;
                        s[m] = count;
                        list[s[m - 1], s[m]] = 'ε';
                        f[m] = s[m];
                    }
                }
                else if (str[j] == '#')
                {
                    if (str[j - 1] != ']' && str[j - 1] != '#')
                    {
                        list[f[m] - 1, f[m]] = 'ε';
                        list[f[m], f[m] + 1] = str[j - 1];
                        count = count + 1;
                        f[m] = count;
                        m = m + 1;
                        s[m] = f[m - 1] - 1;
                        f[m] = f[m - 1];
                    }
                    count = count + 1;
                    list[f[m], s[m]] = 'ε';
                    f[m - 1] = count;
                    list[f[m], f[m - 1]] = 'ε';
                    f[m] = count;
                    list[s[m] - 1, f[m]] = 'ε';
                    count = count + 1;
                    f[m] = count;
                    list[f[m] - 1, f[m]] = 'ε';
                    f[m - 1] = f[m];
                    m = m - 1;
                    if (j + 1 == num)
                    {
                        if (flag == 1)
                        {
                            list[f[m], h[flag]] = 'ε';
                        }
                    }
                }
                else if (str[j] == '[')
                {
                    f[m] = f[m] + 1;
                    list[f[m] - 1, f[m]] = 'ε';
                    count = count + 1;
                    n = n + 1;
                    m = m + 1;
                    count = count + 1;
                    s[m] = f[m - 1];
                    f[m] = f[m - 1] + 1;
                    list[f[m - 1], f[m]] = 'ε';
                    if (flag >= 1 && t[flag] == 0)
                    {
                        t[flag] = n;
                    }
                }
                else if (str[j] == ']')
                {
                    if (n < t[flag])
                    {
                        t[flag] = 0;
                    }
                    n = n - 1;
                    if (flag >= 1 && (t[flag] == 0 || j + 1 == num))
                    {
                        if (j + 1 == num && flag > 1)
                        {
                            int sflag = flag;
                            list[f[m], h[flag]] = 'ε';
                            m = m - 1;
                            for (int k = 1; k < sflag; k++)
                            {
                                flag = flag - 1;
                                list[f[m], h[flag]] = 'ε';
                                for (int x = 0; x < m; x++)
                                {
                                    if (f[x] == h[flag] && flag > 1)
                                    {
                                        m = x;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            list[f[m], h[flag]] = 'ε';
                            flag = flag - 1;
                            m = m - 1;
                        }
                    }
                    f[m - 1] = f[m];
                    if ((j + 1 < num && str[j + 1] != '#') || j + 1 == num)
                    {
                        m = m - 1;
                        f[m] = count + 1;
                        count = count + 1;
                        list[f[m + 1], f[m]] = 'ε';
                    }
                }
            }
            f0 = f[0];
            return f0;
        }
       
        public List<int> move(int i, char x)
        {
            List<int> t = new List<int>();
            int max = list.GetLength(1);
            for (int j = 0; j < max; j = j + 1)
            {
                if (list[i, j] == x)
                {
                    t.Add(j);
                }
            }
            return t;
        }

        public List<char> getch(string text)
        {
            List<char> ch = new List<char>();
            char[] str;
            int num = text.Length;
            str = text.ToCharArray();
            for (int j = 0; j < num; j++)
            {
                //判断字符
                if ((str[j] >= 'a' && str[j] <= 'z') || (str[j] >= 'A' && str[j] <= 'Z') ||
                    (str[j] >= '0' && str[j] <= '9') || str[j] == '.' || str[j] == '_' ||
                    str[j] == '+' || str[j] == '-' || str[j] == '*' || str[j] == '/' ||
                    str[j] == '\t' || str[j] == '\n' || str[j] == '\r' || str[j] == ' ' ||
                    str[j] == ';' || str[j] == '(' || str[j] == ')' || str[j] == '^')
                {
                    if (ch.Contains(str[j]) == false)
                    {
                        ch.Add(str[j]);
                    }
                }
            }
            return ch;
        }
    }
}
