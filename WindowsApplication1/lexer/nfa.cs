using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1
{
    class nfa
    {
        private char[,] list = new char[500, 500];
        private int count;
        private int f0;
        
        public int tonfa(string text)
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
                    (str[j] >= '0' && str[j] <= '9') || str[j] == '.' || str[j] == '_' ||
                    str[j] == '+' || str[j] == '-' || str[j] == '*' || str[j] == '/' || 
                    str[j] == '\t' || str[j] == '\n' || str[j] == '\r' || str[j] == ' ' ||
                    str[j] == ';' || str[j] == '(' || str[j] == ')' || str[j] == '^')
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
                    if (str[j - 1] != ']' && str[j - 1] != '*')
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
                }
                else if (str[j] == ']')
                {
                    n = n - 1;
                    if (flag == 1)
                    {
                        flag = flag - 1;
                        list[f[m], f[m - 1]] = 'ε';
                        m = m - 1;
                    }
                    f[m - 1] = f[m];
                    if (j + 1 < num && str[j + 1] != '*')
                    {
                        m = m - 1;
                    }
                }
            }
            for (int j = 2; j < 500; j++)//去掉无用空转移
            {
                int sum = 0;
                int pre = 0;
                for (int i = 1; i < 500; i++)
                {
                    if (sum < 2 && list[i, j] == 'ε')
                    {
                        sum = sum + 1;
                        if (sum == 1)
                        {
                            pre = i;
                        }
                    }
                    else if (sum == 2)
                    {
                        break;
                    }
                }
                if (sum == 1 && pre != -1)
                {
                    for (int k = 2; k < 500; k++)
                    {
                        if (list[pre, k] != list[0,0])
                        {
                            sum = sum + 1;
                        }
                    }
                    if (sum == 2)
                    {
                        list[pre, j] = list[0, 0];
                        for (int p = 0; p < 500; p++)
                        {
                            list[p, j] = list[p, pre];
                            list[p, pre] = list[0, 0];
                        }
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
