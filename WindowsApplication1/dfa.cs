﻿using System;
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
            temp1.Clear();
            temp2.Clear();
            return num;
        }

        public void mindfa()
        {
            int[] de = new int[50];
            int[] stat = new int[50];
            int[,] nlist = new int[30, 30];
            int m = status.Count;
            int n = ch.Count;
            int count = 0;
            int sum = 2;
            de[0] = 3;
            //List<List<int>> stat = new List<List<int>>();
            //List<int> temp1 = new List<int>();
            //List<List<int>> temp2 = new List<List<int>>();
            //temp2.Add(final);
            for (int i = 1; i < m + 1; i++)
            {
                if (final.Contains(i))
                {
                    de[i] = 1;
                }
                else
                {
                    de[i] = 2;
                }
            }
            while (compare(stat, de) == false)
            {
                de.CopyTo(stat, 0);
                for (int i = sum; i < de[0]; i++)
                {
                    int g = 0;
                    if (g == 0)
                    {
                        sum = de[0];
                        g = 1;
                    }
                    for (int k = 0; k < ch.Count; k++)
                    {
                        int tag = 0;
                        count = 0;
                        for (int j = 1; j < 50; j++)
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
                                    if (de[f] != de[list[count, k]])
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
            //for (int i = 0; i < stat.Count; i++)
            //{
            //    for (int j = 0; j < ch.Count; j++)
            //    {
            //        if (list[stat[i][0], j] != 0)
            //        {
            //            int s = de[list[stat[i][0], j]];
            //            nlist[i + 1, j] = s + 1;
            //        }
            //    }
            //}
            //list.Initialize();
            //nlist.CopyTo(list, 0);
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