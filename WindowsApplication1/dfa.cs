using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1
{
    class dfa
    {
        private char[,] list = new char[50, 50];
        private List<int> final;
        private List<char> ch;
        private List<List<int>> status;

        public void todfa(nfa nfa1, string text)
        {
            ch = nfa1.getch(text);
            int n = ch.Count;
            int unmark = 1;
            //char[,] dfa = new char[50, 50];
            if (status.Count == 0)
            {
                List<int> start = new List<int>();
                start.Add(0);
                nfa1.smove(start, 'ε');
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
                        temp = nfa1.smove(status[tag], ch[i]);
                        if (temp == mem + 1)
                        {
                            unmark++;
                        }
                        list[tag, temp] = ch[i];
                        tag = temp;
                    }
                }
            }
            int t = status.Count;
            for (int s = 0; s < t; s++)
            {
                if (status[s].Contains(nfa1.f0))
                {
                    final.Add(s);
                }
            }
        }
    }
}
