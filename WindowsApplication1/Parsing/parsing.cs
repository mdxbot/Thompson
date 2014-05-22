using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1
{
    class parsing
    {
        private List<string> grammar = new List<string>();
        private List<string> errors = new List<string>();
        private string[,] table = new string[15,15];

        public List<string> prediction(lexer lexer1)
        {
            errors.Clear();
            Stack<char> ch = new Stack<char>();
            List<string> nstr = new List<string>();
            List<string> cstr = new List<string>();
            int s = lexer1.output().Count;
            if (s > 1)
            {
                foreach (var item in lexer1.output()[1])
                    nstr.Add(item);
                foreach (var item in lexer1.output()[0])
                    cstr.Add(item);
                ch.Push('#');
                ch.Push('a');
                int count = 1;
                while (ch.Peek() != '#')
                {
                    string a = nstr[0];
                    char x = ch.Peek();
                    if (x >= '0' && x <= '9')
                    {
                        if (x.ToString() == a)
                        {
                            ch.Pop();
                            nstr.Remove(nstr[0]);
                            count++;
                        }
                        else
                        {
                            int line = 1;
                            for (int i = 0; i < count; i++)
                            {
                                if (cstr[i] == '\n'.ToString())
                                {
                                    line = line + 1;
                                }
                            }
                            errors.Add("Error:line(" + line + ") syntax error(1)");
                            break;
                        }
                    }
                    else if (x == 'ε')
                    {
                        ch.Pop();
                    }
                    else
                    {
                        int u = 0;
                        for (int i = 0; i < grammar.Count; i++)
                        {
                            if (grammar[i][0] == x)
                            {
                                u = i;
                                break;
                            }
                        }
                        int n = -1;
                        if (a == "#")
                        {
                            n = 10;
                        }
                        else
                        {
                            n = Convert.ToInt16(a, 10);
                        }
                        if (table[u, n] != null)
                        {
                            ch.Pop();
                            for (int i = table[u, n].Length; i > 0; i--)
                            {
                                ch.Push(table[u, n][i - 1]);
                            }
                        }
                        else
                        {
                            int line = 1;
                            for (int i = 0; i < count; i++)
                            {
                                if (cstr[i] == '\n'.ToString())
                                {
                                    line = line + 1;
                                }
                            }
                            errors.Add("Error:line(" + line + ") syntax error(2)");
                            break;
                        }
                    }
                }
            }
            return errors;
        }

        public void createtable()
        {
            optimize();
            table.Initialize();
            List<List<char>> fl = follow();
            for (int i = 0; i < grammar.Count; i++)
            {
                string[] exp = grammar[i].Split(new char[2] { '|', '→' });
                for (int j = 1; j < exp.Length; j++)
                {
                    List<char> ftemp = new List<char>();
                    foreach (var item in first(exp[j][0]))
                        ftemp.Add(item);
                    for (int k = 0; k < ftemp.Count; k++)
                    {
                        if (ftemp[k] != 'ε')
                        {
                            int ch = Convert.ToInt16(ftemp[k].ToString(),10);
                            table[i, ch] = table[i, ch] + exp[j];
                        }
                    }
                    if (ftemp.Contains('ε'))
                    {
                        for (int k = 0; k < fl[i].Count; k++)
                        {
                            int ch = -1;
                            if (fl[i][k] == '#')
                            {
                                ch = 10;
                            }
                            else
                            {
                                ch = Convert.ToInt16(fl[i][k].ToString(), 10);
                            }
                            table[i, ch] = table[i, ch] + exp[j];
                            //table[i, ch] = "ε";
                        }
                    }
                }
            }
        }

        public List<char> first(char ch)
        {
            List<char> f = new List<char>();
            if ((ch >= '0' && ch <= '9') || ch == 'ε')
            {
                f.Add(ch);
            }
            else if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
            {
                int num = -1;
                for (int i = 0; i < grammar.Count; i++)
                {
                    if (grammar[i][0] == ch)
                    {
                        num = i;
                        break;
                    }
                }
                if (num >= 0)
                {
                    string[] exp = grammar[num].Split(new char[2] { '|', '→' });
                    for (int i = 1; i < exp.Length; i++)
                    {
                        for (int j = 0; j < exp[i].Length; j++)
                        {
                            List<char> temp = new List<char>();
                            if (j == 0)
                            {
                                temp.Add('ε');
                            }
                            else
                            {
                                foreach(var item in first(exp[i][j-1]))
                                    temp.Add(item);
                            }
                            if (temp.Contains('ε'))
                            {
                                foreach (var item in first(exp[i][j]))
                                {
                                    if (f.Contains(item) == false)
                                    {
                                        f.Add(item);
                                    }
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            return f;
        }

        public List<List<char>> follow()
        {
            List<List<char>> f = new List<List<char>>();
            for (int i = 0; i < grammar.Count; i++)
            {
                f.Add(new List<char>());
            }
            f[0].Add('#');
            for (int i = 0; i < grammar.Count; i++)
            {
                List<char> temp = new List<char>();
                for (int j = 0; j < grammar.Count; j++)
                {
                    if (j != i)
                    {
                        string[] exp = grammar[j].Split('→');
                        while (exp[1].Contains(grammar[i][0].ToString()))
                        {
                            int index = exp[1].IndexOf(grammar[i][0]);
                            if (index + 1 != exp[1].Length)
                            {
                                if (exp[1][index + 1] != '|')
                                {
                                    foreach (var item in first(exp[1][index + 1]))
                                    {
                                        if (f[i].Contains(item) == false && item != 'ε')
                                        {
                                            f[i].Add(item);
                                        }
                                    }
                                }
                            }
                            exp[1] = exp[1].Remove(index, 1);
                        }
                    }
                }
            }
            for (int i = 0; i < grammar.Count; i++)
            {
                List<char> temp = new List<char>();
                for (int j = 0; j < grammar.Count; j++)
                {
                    if (j != i)
                    {
                        string[] exp = grammar[j].Split('→');
                        while (exp[1].Contains(grammar[i][0].ToString()))
                        {
                            int index = exp[1].IndexOf(grammar[i][0]);
                            if (index + 1 == exp[1].Length)
                            {
                                foreach (var item in f[j])
                                {
                                    if (f[i].Contains(item) == false)
                                    {
                                        f[i].Add(item);
                                    }
                                }
                            }
                            else
                            {
                                if (exp[1][index + 1] == '|')
                                {
                                    foreach (var item in f[j])
                                    {
                                        if (f[i].Contains(item) == false)
                                        {
                                            f[i].Add(item);
                                        }
                                    }
                                }
                                else if (first(exp[1][index + 1]).Contains('ε'))
                                {
                                    foreach (var item in f[j])
                                    {
                                        if (f[i].Contains(item) == false)
                                        {
                                            f[i].Add(item);
                                        }
                                    }
                                }
                            }
                            exp[1] = exp[1].Remove(index, 1);
                        }
                    }
                }
            }
            return f;
        }

        public void optimize()//消除左递归和左因子
        {
            grammar.Clear();
            //grammar.Add("s→icts|ictses|a");
            //grammar.Add("c→b");

            //grammar.Add("l→e0l|ε");
            //grammar.Add("e→e1t|e2t|t");
            //grammar.Add("t→t3f|t4f|t5f|f");
            //grammar.Add("f→6e7|8|9");

            //grammar.Add("a→b4f|ε");//总
            //grammar.Add("b→1c");//赋值语句1
            //grammar.Add("c→3d|5e");//赋值语句2
            //grammar.Add("d→0c|5e");//赋值语句3
            //grammar.Add("e→0c|1c|3d|5e|ε");//赋值语句4
            //grammar.Add("f→0g|1g|2h");//表达式1
            //grammar.Add("g→2h|3i|ε");//表达式2
            //grammar.Add("h→0g|1g|5i");//表达式3
            //grammar.Add("i→0g|1g|2h|5i|ε");//表达式4
            //0：常量，1：变量名，2：函数，3：运算符，4：？，5：分隔符，9：#
            for (int i = 1; i < grammar.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    string[] tempi = grammar[i].Split(new char[2] { '|', '→' });
                    string[] tempj = grammar[j].Split(new char[2] { '|', '→' });
                    List<string> newex = new List<string>();
                    char ch = grammar[j].Substring(0, 1).ToCharArray()[0];
                    for (int k = 1; k < tempi.Length; k++)
                    {
                        if (tempi[k].ToCharArray()[0] == ch)
                        {
                            string str = tempi[k].Substring(1);
                            tempi[k] = "";
                            for (int l = 1; l < tempj.Length; l++)
                            {
                                if (newex.Contains(tempj[l] + str) == false)
                                {
                                    newex.Add(tempj[l] + str);
                                }
                            }
                        }
                    }
                    if (newex.Count != 0)
                    {
                        string changed = "";
                        for (int k = 0; k < tempi.Length; k++)
                        {
                            if (tempi[k] != "")
                            {
                                if (k == 0)
                                {
                                    changed = changed + tempi[0] + "→";
                                }
                                else
                                {
                                    changed = changed + tempi[k] + "|";
                                }
                            }
                        }
                        for (int k = 0; k < newex.Count; k++)
                        {
                            if (k + 1 == newex.Count)
                            {
                                changed = changed + newex[k];
                            }
                            else
                            {
                                changed = changed + newex[k] + "|";
                            }
                        }
                        direct(changed, i);
                    }
                }
            }
            for (int i = 0; i < grammar.Count; i++)
            {
                direct(grammar[i], i);
            }
            picklf();
        }

        public void direct(string str, int i)//消除直接左递归，参数为串和位置
        {
            int flag = 0;//直接左递归标志
            char ch = str.Substring(0, 1).ToCharArray()[0];
            string[] exp = str.Split(new char[2] { '|', '→' });
            for (int j = 1; j < exp.Length; j++)
            {
                char[] temp = exp[j].ToCharArray();
                if (temp.Length > 0)
                {
                    if (temp[0] == ch)
                    {
                        flag = 1;
                        break;
                    }
                }
            }
            if (flag == 1)
            {
                string thenew = ch.ToString().ToUpper() + "→";
                for (int k = 1; k < exp.Length; k++)
                {
                    char[] temp = exp[k].ToCharArray();
                    if (temp.Length > 0)
                    {
                        if (temp[0] == ch)
                        {
                            thenew = thenew + exp[k].Substring(1) + ch.ToString().ToUpper() + "|";
                            exp[k] = "";
                        }
                        else
                        {
                            if (exp[k] == "ε")
                            {
                                exp[k] = ch.ToString().ToUpper();
                            }
                            else
                            {
                                exp[k] = exp[k] + ch.ToString().ToUpper();
                            }
                        }
                    }
                    else
                    {
                        if (exp[k] == "ε")
                        {
                            exp[k] = ch.ToString().ToUpper();
                        }
                        else
                        {
                            exp[k] = exp[k] + ch.ToString().ToUpper();
                        }
                    }
                }
                string changed = exp[0] + "→";
                for (int k = 1; k < exp.Length; k++)
                {
                    if (exp[k] != "")
                    {
                        changed = changed + exp[k];
                        if (k + 1 < exp.Length)
                        {
                            changed = changed + "|";
                        }
                    }
                }
                thenew = thenew + "ε";
                grammar.Remove(grammar[i]);
                if (changed != "")
                {
                    grammar.Insert(i, changed);
                }
                grammar.Insert(i + 1, thenew);
            }
        }

        public void picklf()//提取直接左因子
        {
            char ch = 'Z';
            for (int i = 0; i < grammar.Count; i++)
            {
                bool flag = false;
                int maxlength = 0;
                string temphead = "";
                string[] exp = grammar[i].Split(new char[2] { '|', '→' });
                string changed = exp[0] + "→";
                for (int j = 1; j < exp.Length; j++)
                {
                    if (exp[j].Length > maxlength)
                    {
                        maxlength = exp[j].Length;
                    }
                }
                while (findlf(exp) != 404)
                {
                    List<int> current = new List<int>();
                    List<int> temp = new List<int>();
                    int x = findlf(exp);
                    flag = true;
                    for (int m = 1; m < exp.Length; m++)
                    {
                        if (exp[m].Length > 0)
                        {
                            if (exp[m].Substring(0, 1) == exp[x].Substring(0, 1))
                            {
                                if (current.Contains(m) == false)
                                {
                                    current.Add(m);
                                }
                            }
                        }
                    }
                    int length=0;
                    int count = current.Count;
                    List<int> newtemp = new List<int>();
                    for (int t = 0; t < maxlength; t++)
                    {
                        for (int j = 0; j < count; j++)
                        {
                            for (int k = j + 1; k < count; k++)
                            {
                                if (exp[current[j]].Length >= t + 1 && exp[current[k]].Length >= t + 1)
                                {
                                    if (exp[current[k]][t] == exp[current[j]][t])
                                    {
                                        length = t;
                                    }
                                    else
                                    {
                                        if (newtemp.Contains(current[k]) == false)
                                        {
                                            newtemp.Add(current[k]);
                                        }
                                    }
                                }
                                else if (exp[current[j]].Length >= t + 1 && exp[current[k]].Length < t + 1)
                                {
                                    if (newtemp.Contains(current[k]) == false)
                                    {
                                        newtemp.Add(current[k]);
                                    }
                                }
                            }
                        }
                        if (current.Count > (newtemp.Count+temp.Count + 1))
                        {
                            foreach(var item in newtemp)
                            {
                                if (temp.Contains(item) == false)
                                {
                                    temp.Add(item);
                                }
                            }
                            newtemp.Clear();
                        }
                        else
                        {
                            length = t - 1;
                            break;
                        }
                    }
                    for (int j = 0; j < temp.Count; j++)
                    {
                        current.Remove(temp[j]);
                    }
                    temphead = exp[current[0]].Substring(0, length + 1);
                    int l = temphead.Length;
                    string remain = ch.ToString() + "→";
                    for (int j = 0; j < current.Count; j++)
                    {
                        if (exp[current[j]].Substring(l) != "")
                        {
                            if (j + 1 == count)
                            {
                                remain = remain + exp[current[j]].Substring(l);
                            }
                            else
                            {
                                remain = remain + exp[current[j]].Substring(l) + "|";
                            }
                        }
                        else
                        {
                            if (j + 1 == count)
                            {
                                remain = remain + "ε";
                            }
                            else
                            {
                                remain = remain + "ε|";
                            }
                        }
                        if (j == 0)
                        {
                            exp[current[j]] = temphead + ch.ToString();
                            ch--;
                        }
                        else
                        {
                            exp[current[j]] = "";
                        }
                    }
                    grammar.Insert(i + 1, remain);
                }
                if (flag)
                {
                    for (int k = 1; k < exp.Length; k++)
                    {
                        if (exp[k] != "")
                        {
                            changed = changed + exp[k];
                            changed = changed + "|";
                        }
                    }
                    changed = changed.Substring(0, changed.Length - 1);
                    grammar.Remove(grammar[i]);
                    grammar.Insert(i, changed);
                }
            }
        }

        public int findlf(string[] exp)
        {
            int flag = 404;
            for (int i = 1; i < exp.Length; i++)
            {
                for (int j = i + 1; j < exp.Length; j++)
                {
                    if (exp[i] != "" && exp[j] != "")
                    {
                        if (exp[i].Length >= 1 && exp[j].Length >= 1)
                        {
                            if (exp[j].Substring(0, 1) == exp[i].Substring(0, 1))
                            {
                                flag = i;
                                break;
                            }
                        }
                    }
                }
                if (flag != 404)
                {
                    break;
                }
            }
            return flag;
        }
    }
}
