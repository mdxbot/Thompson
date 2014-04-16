using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1
{
    class nfa
    {
        private char[,] list = new char[100, 100];
        public int f0;
        private int count;
        private List<List<int>> status;
        
        public void tonfa(string text)
        {
            char[] str;
            int[] s = new int[10];
            int[] f = new int[10];
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
                    (str[j] >= '0' && str[j] <= '9'))
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
                    if (flag == 1)
                    {
                        list[f[m], f[m - 1]] = 'ε';
                        count = count + 1;
                        s[m] = count;
                        list[s[m - 1], s[m]] = 'ε';
                    }
                    else if (flag == 0)
                    {
                        flag = 1;
                        m = m + 1;
                        f[m] = f[m - 1];
                        count = count + 1;
                        f[m - 1] = count;
                        list[f[m], f[m - 1]] = 'ε';
                        count = count + 1;
                        s[m] = count;
                        list[s[m - 1], s[m]] = 'ε';
                        f[m] = s[m];
                    }
                }
                else if (str[j] == '*')
                {
                    if (str[j - 1] != ')' && str[j - 1] != '*')
                    {
                        list[f[m] - 1, f[m]] = 'ε';
                        list[f[m], f[m] + 1] = str[j - 1];
                        count = count + 1;
                        f[m] = count;
                        m = m + 1;
                        s[m] = f[m - 1] - 2;
                        f[m] = f[m - 1];
                    }
                    count = count + 1;
                    list[f[m], s[m] + 1] = 'ε';
                    f[m] = count;
                    list[s[m], f[m]] = 'ε';
                    list[f[m] - 1, f[m]] = 'ε';
                    count = count + 1;
                    f[m] = count;
                    list[f[m] - 1, f[m]] = 'ε';
                    f[m - 1] = f[m];
                    m = m - 1;
                }
                else if (str[j] == '(')
                {
                    f[m] = f[m] + 1;
                    list[f[m] - 1, f[m]] = 'ε';
                    count = count + 1;
                    n = n + 1;
                    m = m + 1;
                    count = count + 1;
                    s[m] = f[m - 1] - 1;
                    f[m] = f[m - 1] + 1;
                    list[f[m - 1], f[m]] = 'ε';
                }
                else if (str[j] == ')')
                {
                    n = n - 1;
                    if (flag == 1)
                    {
                        flag = flag - 1;
                        list[f[m], f[m - 1]] = 'ε';
                        m = m - 1;
                    }
                    f[m - 1] = count + 1;
                    count = count + 1;
                    list[f[m], f[m - 1]] = 'ε';
                    f[m] = f[m - 1];
                    if (str[j + 1] != '*')
                    {
                        m = m - 1;
                    }
                }
            }
            f0 = f[0];
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

        public int smove(List<int> start, char r)
        {
            int num = 404;
            int l1 = 0, l2 = 0;
            int n = start.Count;
            List<int> temp1 = new List<int>();
            List<int> temp2 = new List<int>();
            List<int> closure = new List<int>();
            for (int i = 0; i < n; i++)
            {
                temp1 = move(start[i], r);
                if (temp1.Count == 0)
                {
                    temp2 = move(start[i], 'ε');
                }
                else
                {
                    l1 = temp1.Count;
                    for (int j = 0; j < l1; j++)
                    {
                        temp2 = move(temp1[j], 'ε');
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
                    (str[j] >= '0' && str[j] <= '9'))
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
