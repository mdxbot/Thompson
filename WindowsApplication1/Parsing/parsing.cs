using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1
{
    class parsing
    {
        private List<string> grammar = new List<string>();

        public void optimize()//消除左递归和左因子
        {
            //grammar.Add("<s>→i<c>t<s>|i<c>t<s>e<s>|a");
            //grammar.Add("<c>→b");

            //grammar.Add("<e>→<e>+<t>|<t>");
            //grammar.Add("<t>→<t>*<f>|<f>");
            //grammar.Add("<f>→(<e>)|-<f>|a");

            grammar.Add("<l>→<e>;<l>|ε");
            grammar.Add("<e>→<e>+<t>|<e>-<t>|<t>");
            grammar.Add("<t>→<t>*<f>|<t>/<f>|<t>mod<f>|<f>");
            grammar.Add("<f>→(<e>)|id|num");
            for (int i = 1; i < grammar.Count; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    string[] tempi = grammar[i].Split(new char[2] { '|', '→' });
                    string[] tempj = grammar[j].Split(new char[2] { '|', '→' });
                    List<string> newex = new List<string>();
                    char ch = grammar[j].Substring(1, 1).ToCharArray()[0];
                    for (int k = 1; k < tempi.Length; k++)
                    {
                        if (tempi[k].ToCharArray()[0] == '<' && tempi[k].ToCharArray()[1] == ch)
                        {
                            string str = tempi[k].Substring(3);
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
            char ch = str.Substring(1, 1).ToCharArray()[0];
            string[] exp = str.Split(new char[2] { '|', '→' });
            for (int j = 1; j < exp.Length; j++)
            {
                char[] temp = exp[j].ToCharArray();
                if (temp.Length > 3)
                {
                    if (temp[1] == ch && temp[0] == '<')
                    {
                        flag = 1;
                        break;
                    }
                }
            }
            if (flag == 1)
            {
                string thenew = "<" + ch.ToString().ToUpper() + ">→";
                for (int k = 1; k < exp.Length; k++)
                {
                    char[] temp = exp[k].ToCharArray();
                    if (temp.Length > 3)
                    {
                        if (temp[1] == ch && temp[0] == '<')
                        {
                            thenew = thenew + exp[k].Substring(3) + "<" + ch.ToString().ToUpper() + ">" + "|";
                            exp[k] = "";
                        }
                        else
                        {
                            if (exp[k] == "ε")
                            {
                                exp[k] = "<" + ch.ToString().ToUpper() + ">";
                            }
                            else
                            {
                                exp[k] = exp[k] + "<" + ch.ToString().ToUpper() + ">";
                            }
                        }
                    }
                    else
                    {
                        if (exp[k] == "ε")
                        {
                            exp[k] = "<" + ch.ToString().ToUpper() + ">";
                        }
                        else
                        {
                            exp[k] = exp[k] + "<" + ch.ToString().ToUpper() + ">";
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
                string[] exp = grammar[i].Split(new char[2] { '|', '→' });
                string temphead = "";
                string changed = exp[0] + "→";
                List<int> current = new List<int>();
                List<int> temp = new List<int>();
                for (int j = 1; j < exp.Length; j++)
                {
                    if (exp[j].Length > maxlength)
                    {
                        maxlength = exp[j].Length;
                    }
                }
                while (findlf(exp) != 404)
                {
                    int x = findlf(exp);
                    flag = true;
                    for (int m = 1; m < exp.Length; m++)
                    {
                        int f = 1;
                        if (exp[x][0] == '<')
                        {
                            f = 2;
                        }
                        if (exp[m].Substring(0, f) == exp[x].Substring(0, f))
                        {
                            current.Add(m);
                        }
                    }
                    int count = current.Count;
                    for (int t = 0; t < maxlength; t++)
                    {
                        for (int j = 0; j < count; j++)
                        {
                            for (int k = j + 1; k < count; k++)
                            {
                                if (exp[current[j]].Length >= t + 1 && exp[current[k]].Length >= t + 1)
                                {
                                    if (current.Count > temp.Count + 1)
                                    {
                                        if (exp[current[k]][t] == exp[current[j]][t])
                                        {
                                            temphead = temphead + exp[current[k]][t].ToString();
                                        }
                                        else
                                        {
                                            temp.Add(current[k]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    int l = temphead.Length;
                    string remain = "<" + ch.ToString() + ">→";
                    for (int j = 0; j < count; j++)
                    {
                        if (exp[current[j]].Substring(l) != "")
                        {
                            remain = remain + exp[current[j]].Substring(l) + "|";
                        }
                        if (j == 0)
                        {
                            exp[current[j]] = temphead + "<" + ch.ToString() + ">";
                            ch--;
                        }
                        else
                        {
                            exp[current[j]] = "";
                        }
                    }
                    remain = remain + "ε";
                    grammar.Insert(i + 1, remain);
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
                }
                if (flag)
                {
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
                        int x = 1;
                        if (exp[i][0] == '<')
                        {
                            x = 2;
                        }
                        if (exp[i].Length >= x && exp[j].Length >= x)
                        {
                            if (exp[j].Substring(0, x) == exp[i].Substring(0, x))
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
