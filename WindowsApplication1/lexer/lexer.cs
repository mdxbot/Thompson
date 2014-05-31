using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace WindowsApplication1
{
    class lexer
    {
        private List<dfa> dfa = new List<dfa>();
        private List<string> content = new List<string>();
        private List<int> type = new List<int>();
        private List<string> errors = new List<string>();

        public void createdfa()
        {
            if (dfa.Count == 0)
            {
                //regex
                string constant = "[0|[1|2|3|4|5|6|7|8|9][0|1|2|3|4|5|6|7|8|9]#][ε|.[0|1|2|3|4|5|6|7|8|9]#[1|2|3|4|5|6|7|8|9]]|PI|E";
                string identifier = "[_|a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z|A|B|C|D|E|F|G|H|I|J|K|L|M|N|O|P|Q|R|S|T|U|V|W|X|Y|Z][_|a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z|A|B|C|D|E|F|G|H|I|J|K|L|M|N|O|P|Q|R|S|T|U|V|W|X|Y|Z|0|1|2|3|4|5|6|7|8|9]#";
                string function = "sin|cos|tg|ctg|^|log|lg|ln";
                string operators = "*|/|+|-|=";
                string startofEx = "?";
                string delimiter = " |\r|\n|\t|,|;|(|)";

                int f1 = 1;
                nfa nfa1 = new nfa();
                dfa dfa1 = new dfa();
                f1 = nfa1.tonfa(constant);
                dfa1.todfa(nfa1, constant, f1);
                dfa1.mindfa();
                dfa.Add(dfa1);
                int f2 = 1;
                nfa nfa2 = new nfa();
                dfa dfa2 = new dfa();
                f2 = nfa2.tonfa(identifier);
                dfa2.todfa(nfa2, identifier, f2);
                dfa2.mindfa();
                dfa.Add(dfa2);
                int f3 = 1;
                nfa nfa3 = new nfa();
                dfa dfa3 = new dfa();
                f3 = nfa3.tonfa(function);
                dfa3.todfa(nfa3, function, f3);
                dfa3.mindfa();
                dfa.Add(dfa3);
                int f4 = 1;
                nfa nfa4 = new nfa();
                dfa dfa4 = new dfa();
                f4 = nfa4.tonfa(operators);
                dfa4.todfa(nfa4, operators, f4);
                dfa4.mindfa();
                dfa.Add(dfa4);
                int f5 = 1;
                nfa nfa5 = new nfa();
                dfa dfa5 = new dfa();
                f5 = nfa5.tonfa(startofEx);
                dfa5.todfa(nfa5, startofEx, f5);
                dfa5.mindfa();
                dfa.Add(dfa5);
                int f6 = 1;
                nfa nfa6 = new nfa();
                dfa dfa6 = new dfa();
                f6 = nfa6.tonfa(delimiter);
                dfa6.todfa(nfa6, delimiter, f6);
                dfa6.mindfa();
                dfa.Add(dfa6);
            }
        }

        public List<string> createstr(string text)
        {
            content.Clear();
            type.Clear();
            errors.Clear();
            List<char> str = new List<char>();
            char[] temp=text.ToCharArray();
            for (int i = 0; i < text.Length; i++)
            {
                str.Add(temp[i]);
            }
            string word = "";
            for (int j = 0; j < str.Count; j++)
            {
                int flag = 0;
                int tag = 0;
                for (int k = 2; k < dfa.Count; k++)
                {
                    if (dfa[k].accept(str[j].ToString()))
                    {
                        tag = k;
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    word = word + str[j].ToString();
                    if(j + 1 == str.Count)
                    {
                        bool acc = false;
                        for (int o = 0; o < 2; o++)
                        {
                            if (dfa[o].accept(word))
                            {
                                acc = true;
                                if (o == 1 && word.Length > 32)
                                {
                                    errors.Add("Error:Invaild identifier length");
                                    break;
                                }
                                else
                                {
                                    content.Add(word);
                                    type.Add(o);
                                    break;
                                }
                            }
                        }
                        if (acc == false)
                        {
                            errors.Add("Error:Invalid word'" + word + "'");
                        }
                        word = "";
                    }
                }
                else if (flag == 1 )
                {
                    if (word != "")
                    {
                        bool acc = false;
                        for (int o = 0;;)
                        {
                            if (dfa[o].accept(word))
                            {
                                acc = true;
                                if (o == 1 && word.Length > 32)
                                {
                                    errors.Add("Error:Invaild identifier length");
                                    break;
                                }
                                else
                                {
                                    content.Add(word);
                                    type.Add(o);
                                    break;
                                }
                            }
                            if (o == 0)
                            {
                                o = 2;
                            }
                            else if (o == 1)
                            {
                                break;
                            }
                            else if (o == 2)
                            {
                                o = 1;
                            }
                        }
                        if (acc == false)
                        {
                            errors.Add("Error:Invalid word'" + word + "'");
                        }
                        word = "";
                    }
                    content.Add(str[j].ToString());
                    type.Add(tag);
                }
            }
            return errors;
        }

        public List<List<string>> output()
        {
            List<List<string>> str = new List<List<string>>();
            List<string> t = new List<string>();
            if (errors.Count == 0)
            {
                content.Add("#");
                str.Add(content);
                foreach (var item in type)
                    t.Add(item.ToString());
                t.Add("#");
                str.Add(t);
            }
            else
            {
                t.Add("Error");
                str.Add(t);
            }
            return str;
        }
    }
}
