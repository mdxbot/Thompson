using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1
{
    class dfa
    {
        private int[,] list = new int[50, 50];
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
                    if (temp != 404)
                    {
                        list[tag, i] = temp;
                    }
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
            int l1 = 0;
            int n = start.Count;
            int type = 0;
            List<int> temp1 = new List<int>();
            List<int> temp2 = new List<int>();
            List<int> closure = new List<int>();
            for (int i = 0; i < n; i++)
            {
                if (r != 'ε')
                {
                    int g = nfa1.move(start[i], r).Count;
                    if (g != 0)
                    {
                        type = 1;
                        foreach (var item in nfa1.move(start[i], r))
                            temp1.Add(item);
                        if (temp1.Count != 0)
                        {
                            l1 = temp1.Count;
                            for (int j = 0; j < l1; j++)
                            {
                                if (closure.Contains(temp1[j]) == false)
                                {
                                    closure.Add(temp1[j]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    type = 1;
                }
            }
            for (int i = 0; i < n; i++)
            {
                if (type == 1)
                {
                    temp1.Clear();
                    foreach (var item in nfa1.move(start[i], 'ε'))
                        temp1.Add(item);
                    int count = temp1.Count;
                    while (temp1.Count != 0)
                    {
                        int e = temp1.Count;
                        for (int t = 0; t < e; t++)
                        {
                            if (closure.Contains(temp1[t]) == false)
                            {
                                closure.Add(temp1[t]);
                            }
                        }
                        temp1.Clear();
                        foreach (var item in closure)
                            temp1.Add(item);
                        int c = closure.Count;
                        for (int b = 0; b < c; b++)
                        {
                            if (nfa1.move(temp1[b], 'ε').Count != 0)
                            {
                                int d = nfa1.move(temp1[b], 'ε').Count;
                                for (int s = 0; s < d; s++)
                                {
                                    if (temp2.Contains(nfa1.move(temp1[b], 'ε')[s]) == false &&
                                        closure.Contains(nfa1.move(temp1[b], 'ε')[s]) == false)
                                    {
                                        temp2.Add(nfa1.move(temp1[b], 'ε')[s]);
                                        closure.Add(nfa1.move(temp1[b], 'ε')[s]);
                                    }
                                }
                            }
                        }
                        temp1.Clear();
                        foreach (var item in temp2)
                            temp1.Add(item);
                        temp2.Clear();
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
                        for (int a = 0; a < closure.Count; a++)
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

        public void mindfa()
        {
            List<List<int>> stat = new List<List<int>>();
            List<int> temp1=new List<int>();

        }

        public int show(int x, char y)
        {
            if (ch.Contains(y))
            {
                return list[x, ch.IndexOf(y)];
            }
            else
            {
                return 404;
            }
        }
    }
}
