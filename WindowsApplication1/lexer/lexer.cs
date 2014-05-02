using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1
{
    class lexer
    {
        private List<dfa> dfa = new List<dfa>();

        public void createdfa()
        {
            //regex
            string constant = "[0|[1|2|3|4|5|6|7|8|9][0|1|2|3|4|5|6|7|8|9]#][ε|.[0|1|2|3|4|5|6|7|8|9]#[1|2|3|4|5|6|7|8|9]]|PI|E";
            string identifier = "[_|a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z|A|B|C|D|E|F|G|H|I|J|K|L|M|N|O|P|Q|R|S|T|U|V|W|X|Y|Z][_|a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z|A|B|C|D|E|F|G|H|I|J|K|L|M|N|O|P|Q|R|S|T|U|V|W|X|Y|Z|0|1|2|3|4|5|6|7|8|9]#";
            string function = "sin|cos|tg|ctg|^|log|lg|ln";
            string operators = "*|/|+|-";
            string delimiter = " |\r\n|\t|;|(|)";

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
            f5 = nfa5.tonfa(delimiter);
            dfa5.todfa(nfa5, delimiter, f5);
            dfa5.mindfa();
            dfa.Add(dfa5);
        }

        public bool output(string text)
        {
            List<string> content = new List<string>();
            List<int> type = new List<int>();
            return dfa[0].accept(text);
        }
    }
}
