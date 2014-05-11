using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1
{
    class dfa
    {
        private int[,] list = new int[200, 100];
        private List<int> final = new List<int>();
        private List<char> ch = new List<char>();
        private List<List<int>> status = new List<List<int>>();
        private int s0 = 2;

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
                        list[tag + 1, i] = temp + 1;
                    }
                }
                tag = tag + 1;
            }
            int t = status.Count;
            for (int s = 0; s < t; s++)
            {
                if (status[s].Contains(f0))
                {
                    final.Add(s + 1);
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
                    foreach (var item in nfa1.move(start[0], 'ε'))
                        closure.Add(item);
                    if (closure.Count != 0)
                    {
                        type = 1;
                    }
                }
            }
            if (type >= 1)
            {
                temp1.Clear();
                foreach (var item in closure)
                    temp1.Add(item);
                while (temp1.Count != 0)
                {
                    int e = temp1.Count;
                    for (int b = 0; b < e; b++)
                    {
                        if (nfa1.move(temp1[b], 'ε').Count != 0)
                        {
                            int d = nfa1.move(temp1[b], 'ε').Count;
                            for (int s = 0; s < d; s++)
                            {
                                int h = nfa1.move(temp1[b], 'ε')[s];
                                if (temp2.Contains(h) == false &&
                                    closure.Contains(h) == false)
                                {
                                    temp2.Add(h);
                                    closure.Add(h);
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
            int m = status.Count;
            int flag = 0;
            if (m == 0)
            {
                if (nfa1.move(0, 'ε').Count == 1)
                {
                    status.Add(closure);
                    num = 0;
                }
                else if (nfa1.move(0, 'ε').Count > 1)
                {
                    closure.Add(0);
                    status.Add(closure);
                    num = 0;
                }
            }
            else
            {
                for (int k = 0; k < m; k++)
                {
                    flag = 0;
                    if (closure.Count != 0)
                    {
                        if (closure.Count <= status[k].Count)
                        {
                            for (int a = 0; a < closure.Count; a++)
                            {
                                if (status[k].Contains(closure[a]) == false)
                                {
                                    flag = 1;
                                    break;
                                }
                            }
                            if (flag == 0 && closure.Count == status[k].Count)
                            {
                                num = k;
                                break;
                            }
                            else if (flag == 0 && closure.Count < status[k].Count)
                            {
                                break;
                            }
                        }
                        else
                        {
                            flag = 1;
                        }
                    }
                }
                if (flag == 1)
                {
                    status.Add(closure);
                    num = status.Count - 1;
                }
            }
            temp1.Clear();
            temp2.Clear();
            return num;
        }

        public void mindfa()
        {
            int[] de = new int[200];
            int[] stat = new int[200];
            int[,] nlist = new int[200, 100];
            int m = status.Count;
            int n = ch.Count;
            int count = 0;
            de[0] = 3;
            stat[0] = 1;
            List<int> temp1 = new List<int>();
            for (int i = 1; i < m + 1; i++)
            {
                if (final.Contains(i))
                {
                    de[i] = 2;
                }
                else
                {
                    de[i] = 1;
                }
            }
            for (int i = 1; i < m + 1; i++)
            {
                if (de[i] == 1)
                {
                    s0 = 1;
                }
            }
            while (compare(stat, de) == false)
            {
                de.CopyTo(stat, 0);
                for (int i = 1; i < de[0]; i++)
                {
                    for (int k = 0; k < ch.Count; k++)
                    {
                        int tag = 0;
                        count = 0;
                        for (int j = 1; j < status.Count + 1; j++)
                        {
                            if (de[j] == i)
                            {
                                if (count == 0)
                                {
                                    count = j;
                                }
                                else
                                {
                                    int f = list[j, k];
                                    if ((de[f] != de[list[count, k]] && list[count, k] != 0)
                                        || (list[count, k] == 0 && f != 0))
                                    {
                                        de[j] = de[0];
                                        tag = 1;
                                    }
                                }
                            }
                        }
                        if (tag == 1)
                        {
                            de[0] = de[0] + 1;
                        }
                    }
                }
            }
            for (int i = 1; i < status.Count + 1; i++)
            {
                if (final.Contains(i) && temp1.Contains(de[i]) == false)
                {
                    temp1.Add(de[i]);
                }
                for (int j = 0; j < ch.Count; j++)
                {
                    if (list[i, j] != 0)
                    {
                        int s = de[list[i, j]];
                        if(nlist[de[i], j] != s)
                        {
                            nlist[de[i], j] = s;
                        }
                    }
                }
            }
            list.Initialize();
            for (int i = 0; i < status.Count + 1; i++)
            {
                for (int j = 0; j < ch.Count; j++)
                {
                    list[i, j] = nlist[i, j];
                }
            }
            final.Clear();
            foreach (var item in temp1)
                final.Add(item);
        }

        public bool accept(string text)
        {
            text = "ε" + text + "ε";
            char[] str = text.ToCharArray();
            int l = str.Length;
            int yet = s0;
            for (int i = 0; i < l; i++)
            {
                if (ch.Contains(str[i]))
                {
                    int next = show(yet, str[i]);
                    if (next != 404)
                    {
                        yet = next;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (str[i] != 'ε')
                {
                    yet = 404;
                    break;
                }
            }
            if (final.Contains(yet))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int show(int x, char y)
        {
            if (ch.Contains(y))
            {
                int next = list[x, ch.IndexOf(y)];
                return next;
            }
            else
            {
                return 404;
            }
        }

        private bool compare(int[] array1,int[] array2)
        {
            bool jug = true;
            if (array1.Length == array2.Length)
            {
                for (int i = 0; i < array1.Length; i++)
                {
                    if (array1[i] != array2[i])
                    {
                        jug = false;
                    }
                }
            }
            else
            {
                jug = false;
            }
            return jug;
        }
    }
}
