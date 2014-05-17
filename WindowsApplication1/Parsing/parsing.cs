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
            //grammar.Add("s→icts|ictses|a");
            //grammar.Add("c→b");

            //grammar.Add("a→1b2|aba|c");
            //grammar.Add("b→bc|3");
            //grammar.Add("c→bac|b1|ε");

            //grammar.Add("<l>→<e>;<l>|ε");
            //grammar.Add("<e>→<e>+<t>|<e>-<t>|<t>");
            //grammar.Add("<t>→<t>*<f>|<t>/<f>|<t>mod<f>|<f>");
            //grammar.Add("<f>→(<e>)|id|num");
            grammar.Clear();
            grammar.Add("a→dbd4dc");//总
            grammar.Add("b→bdbd|1d3ded|ε");//赋值语句
            grammar.Add("c→cd3dcd|f|0|1");//表达式
            grammar.Add("d→5d|ε");//分隔符
            grammar.Add("e→cd3d0|0");//赋值语句右边
            grammar.Add("f→2dfd|2d0d|2d0d0d|2d1d|ε");//函数
            //0：常量，1：变量名，2：函数，3：运算符，4：？，5：分隔符
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
                            if (k + 1 < exp.Length)
                            {
                                changed = changed + "|";
                            }
                        }
                    }
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
