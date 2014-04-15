using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1
{
    class nfa
    {
        private char[,] list;
        private int f0;
        private int count;
        private List<char> ch;
        private List<List<int>> status;
        public char[,] todfa()
        {
            int n = ch.Count;
            int unmark = 1;
            char[,] dfa = new char[50,50];
            if (status.Count == 0)
            {
                List<int> start = new List<int>();
                start.Add(0);
                smove(start, 'ε');
            }
            else
            {
                int tag = 0;
                while (unmark > 0)
                {
                    unmark--;
                    for (int i = 0; i < n; i++)
                    {
                        int temp;
                        int mem = status.Count;
                        temp = smove(status[tag], ch[i]);
                        if (temp == mem+1)
                        {
                            unmark++;
                        }
                        dfa[tag, temp] = ch[i];
                        tag = temp;
                    }
                }
            }
            return dfa;
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
    }
}
