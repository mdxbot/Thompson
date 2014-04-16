using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1
{
    class dfa
    {
        private char[,] list = new char[50, 50];
        private List<int> final = new List<int>();
        private List<char> ch = new List<char>();
        private List<List<int>> status = new List<List<int>>();

        public void todfa(nfa nfa1, string text, int f0)
        {
            ch = nfa1.getch(text);
            int n = ch.Count;
            int unmark = 1;
            if (status.Count == 0)
            {
                List<int> start = new List<int>();
                start.Add(0);
                smove(start, 'ε', nfa1);
            }
            int tag = 0;
            int temp = 0;
            while (unmark > 0)
            {
                unmark--;
                for (int i = 0; i < n; i++)
                {
                    int mem = status.Count;
                    temp = smove(status[tag], ch[i], nfa1);
                    if (temp == mem)
                    {
                        unmark++;
                    }
                    list[tag, temp] = ch[i];
                }
                tag = tag + 1;
            }
            int t = status.Count;
            for (int s = 0; s < t; s++)
            {
                if (status[s].Contains(f0))
                {
                    final.Add(s);
                }
            }
        }

        public int smove(List<int> start, char r, nfa nfa1)
        {
            int num = 404;
            int l1 = 0, l2 = 0;
            int n = start.Count;
            List<int> temp1 = new List<int>();
            List<int> temp2 = new List<int>();
            List<int> closure = new List<int>();
            for (int i = 0; i < n; i++)
            {
                temp1 = nfa1.move(start[i], r);
                if (temp1.Count == 0)
                {
                    temp2 = nfa1.move(start[i], 'ε');
                }
                else
                {
                    l1 = temp1.Count;
                    for (int j = 0; j < l1; j++)
                    {
                        temp2 = nfa1.move(temp1[j], 'ε');
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

        public char show(int x, int y)
        {
            return list[x, y];
        }
    }
}
