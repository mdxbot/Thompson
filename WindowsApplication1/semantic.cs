using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace WindowsApplication1
{
    class semantic
    {
        private List<string> error = new List<string>();
        private List<string> cstr = new List<string>();

        public List<string> calculate(parsing p1, lexer l1)
        {
            cstr = l1.output()[0];
            error.Clear();
            List<List<string>> quad = new List<List<string>>();//四元式
            List<string> id = new List<string>();//符号表
            List<string> value = new List<string>();
            quad = p1.output();
            for (int i = 0; i < quad[0].Count; i++)
            {
                double x = 0, y = 0;
                string xstr = "", ystr = "";
                int[] flag = { 0, 0 };

                if (quad[0][i][0] == '0')
                {
                    x = Convert.ToDouble(quad[0][i].Substring(2));
                }
                else if (quad[0][i][0] == '1')
                {
                    flag[0] = 1;
                    xstr = quad[0][i];
                }
                else if (quad[0][i][0] == '#')
                {
                    flag[0] = -1;
                }
                if (quad[1][i][0] == '0')
                {
                    y = Convert.ToDouble(quad[1][i].Substring(2));
                }
                else if (quad[1][i][0] == '1')
                {
                    flag[1] = 1;
                    ystr = quad[1][i];
                }
                else if (quad[1][i][0] == '#')
                {
                    flag[1] = -1;
                }

                if (flag[0] == 0)
                {
                    double temp = 0;
                    switch (quad[2][i])
                    {
                        case "+":
                            if (flag[1] == 0)
                            {
                                id.Add(quad[3][i].Substring(2));
                                temp = x + y;
                                value.Add("0|" + temp);
                            }
                            else if (flag[1] == 1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(ystr))
                                {
                                    int num = id.IndexOf(ystr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = x + Convert.ToDouble(value[num].Substring(2));
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|" + x + "+" + ystr.Substring(2));
                                    }
                                }
                                else
                                {
                                    value.Add("1|" + x + "+" + ystr.Substring(2));
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "-":
                            if (flag[1] == 0)
                            {
                                id.Add(quad[3][i].Substring(2));
                                temp = x - y;
                                value.Add("0|" + temp);
                            }
                            else if (flag[1] == 1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(ystr))
                                {
                                    int num = id.IndexOf(ystr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = x - Convert.ToDouble(value[num].Substring(2));
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|" + x + "-" + ystr.Substring(2));
                                    }
                                }
                                else
                                {
                                    value.Add("1|" + x + "-" + ystr.Substring(2));
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "*":
                            if (flag[1] == 0)
                            {
                                id.Add(quad[3][i].Substring(2));
                                temp = x * y;
                                value.Add("0|" + temp);
                            }
                            else if (flag[1] == 1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(ystr))
                                {
                                    int num = id.IndexOf(ystr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = x * Convert.ToDouble(value[num].Substring(2));
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|" + x + "*" + ystr.Substring(2));
                                    }
                                }
                                else
                                {
                                    value.Add("1|" + x + "*" + ystr.Substring(2));
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "/":
                            if (flag[1] == 0)
                            {
                                id.Add(quad[3][i].Substring(2));
                                temp = x / y;
                                value.Add("0|" + temp);
                            }
                            else if (flag[1] == 1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(ystr))
                                {
                                    int num = id.IndexOf(ystr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = x / Convert.ToDouble(value[num].Substring(2));
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|" + x + "/" + ystr.Substring(2));
                                    }
                                }
                                else
                                {
                                    value.Add("1|" + x + "/" + ystr.Substring(2));
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "^":
                            if (flag[1] == 0)
                            {
                                id.Add(quad[3][i].Substring(2));
                                temp = Math.Pow(x, y);
                                value.Add("0|" + temp);
                            }
                            else if (flag[1] == 1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(ystr))
                                {
                                    int num = id.IndexOf(ystr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = Math.Pow(x, Convert.ToDouble(value[num].Substring(2)));
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|" + x + "^" + ystr.Substring(2));
                                    }
                                }
                                else
                                {
                                    value.Add("1|" + x + "^" + ystr.Substring(2));
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "=":
                            err(quad[4][i]);
                            break;
                        case "sin":
                            if (flag[1] == -1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                temp = Math.Sin(x);
                                value.Add("0|" + temp);
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "cos":
                            if (flag[1] == -1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                temp = Math.Cos(x);
                                value.Add("0|" + temp);
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "tg":
                            if (flag[1] == -1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                temp = Math.Tan(x);
                                value.Add("0|" + temp);
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "ctg":
                            if (flag[1] == -1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                temp = 1 / Math.Tan(x);
                                value.Add("0|" + temp);
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "log":
                            id.Add(quad[3][i].Substring(2));
                            if (flag[1] == -1)
                            {
                                temp = Math.Log(x, 2);
                                value.Add("0|" + temp);
                            }
                            else if (flag[1] == 0)
                            {
                                temp = Math.Log(y, x);
                                value.Add("0|" + temp);
                            }
                            else if (flag[1] == 1)
                            {
                                if (id.Contains(ystr))
                                {
                                    int num = id.IndexOf(ystr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = Math.Log(Convert.ToDouble(value[num].Substring(2)), x);
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|log(" + x + "," + ystr + ")");
                                    }
                                }
                                else
                                {
                                    value.Add("1|log(" + x + "," + ystr + ")");
                                }
                            }
                            break;
                        case "ln":
                            if (flag[1] == -1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                temp = Math.Log(x);
                                value.Add("0|" + temp);
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "lg":
                            if (flag[1] == -1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                temp = Math.Log(x, 2);
                                value.Add("0|" + temp);
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        default:
                            err(quad[4][i]);
                            break;
                    }
                }
                else if (flag[0] == 1)
                {
                    double temp = 0;
                    switch (quad[2][i])
                    {
                        case "+":
                            if (flag[1] == 0)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr))
                                {
                                    int num = id.IndexOf(xstr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = Convert.ToDouble(value[num].Substring(2)) + y;
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "+" + y);
                                    }
                                }
                                else
                                {
                                    value.Add("1|" + xstr.Substring(2) + "+" + y);
                                }
                            }
                            else if (flag[1] == 1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr) && id.Contains(ystr))
                                {
                                    int num1 = id.IndexOf(xstr);
                                    int num2 = id.IndexOf(ystr);
                                    if (value[num1][0] == '0' && value[num2][0] == '0')
                                    {
                                        temp = Convert.ToDouble(value[num1].Substring(2)) + Convert.ToDouble(value[num2].Substring(2));
                                        value.Add("0|" + temp);
                                    }
                                    else if (value[num1][0] == '0')
                                    {
                                        value.Add("1|" + value[num1].Substring(2) + "+" + ystr.Substring(2));
                                    }
                                    else if (value[num2][0] == '0')
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "+" + value[num2].Substring(2));
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "+" + ystr.Substring(2));
                                    }
                                }
                                else if (id.Contains(xstr))
                                {
                                    int num1 = id.IndexOf(xstr);
                                    if (value[num1][0] == '0')
                                    {
                                        value.Add("1|" + value[num1].Substring(2) + "+" + ystr.Substring(2));
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "+" + ystr.Substring(2));
                                    }
                                }
                                else if(id.Contains(ystr))
                                {
                                    int num2 = id.IndexOf(ystr);
                                    if (value[num2][0] == '0')
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "+" + value[num2].Substring(2));
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "+" + ystr.Substring(2));
                                    }
                                }
                                else
                                {
                                    value.Add("1|" + xstr.Substring(2) + "+" + ystr.Substring(2));
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "-":
                            if (flag[1] == 0)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr))
                                {
                                    int num = id.IndexOf(xstr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = Convert.ToDouble(value[num].Substring(2)) - y;
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "-" + y);
                                    }
                                }
                                else
                                {
                                    value.Add("1|" + xstr.Substring(2) + "-" + y);
                                }
                            }
                            else if (flag[1] == 1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr) && id.Contains(ystr))
                                {
                                    int num1 = id.IndexOf(xstr);
                                    int num2 = id.IndexOf(ystr);
                                    if (value[num1][0] == '0' && value[num2][0] == '0')
                                    {
                                        temp = Convert.ToDouble(value[num1].Substring(2)) - Convert.ToDouble(value[num2].Substring(2));
                                        value.Add("0|" + temp);
                                    }
                                    else if (value[num1][0] == '0')
                                    {
                                        value.Add("1|" + value[num1].Substring(2) + "-" + ystr.Substring(2));
                                    }
                                    else if (value[num2][0] == '0')
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "-" + value[num2].Substring(2));
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "-" + ystr.Substring(2));
                                    }
                                }
                                else if (id.Contains(xstr))
                                {
                                    int num1 = id.IndexOf(xstr);
                                    if (value[num1][0] == '0')
                                    {
                                        value.Add("1|" + value[num1].Substring(2) + "-" + ystr.Substring(2));
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "-" + ystr.Substring(2));
                                    }
                                }
                                else if (id.Contains(ystr))
                                {
                                    int num2 = id.IndexOf(ystr);
                                    if (value[num2][0] == '0')
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "-" + value[num2].Substring(2));
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "-" + ystr.Substring(2));
                                    }
                                }
                                else
                                {
                                    value.Add("1|" + xstr.Substring(2) + "-" + ystr.Substring(2));
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "*":
                            if (flag[1] == 0)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr))
                                {
                                    int num = id.IndexOf(xstr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = Convert.ToDouble(value[num].Substring(2)) * y;
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "*" + y);
                                    }
                                }
                                else
                                {
                                    value.Add("1|" + xstr.Substring(2) + "*" + y);
                                }
                            }
                            else if (flag[1] == 1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr) && id.Contains(ystr))
                                {
                                    int num1 = id.IndexOf(xstr);
                                    int num2 = id.IndexOf(ystr);
                                    if (value[num1][0] == '0' && value[num2][0] == '0')
                                    {
                                        temp = Convert.ToDouble(value[num1].Substring(2)) * Convert.ToDouble(value[num2].Substring(2));
                                        value.Add("0|" + temp);
                                    }
                                    else if (value[num1][0] == '0')
                                    {
                                        value.Add("1|" + value[num1].Substring(2) + "*" + ystr.Substring(2));
                                    }
                                    else if (value[num2][0] == '0')
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "*" + value[num2].Substring(2));
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "*" + ystr.Substring(2));
                                    }
                                }
                                else if (id.Contains(xstr))
                                {
                                    int num1 = id.IndexOf(xstr);
                                    if (value[num1][0] == '0')
                                    {
                                        value.Add("1|" + value[num1].Substring(2) + "*" + ystr.Substring(2));
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "*" + ystr.Substring(2));
                                    }
                                }
                                else if (id.Contains(ystr))
                                {
                                    int num2 = id.IndexOf(ystr);
                                    if (value[num2][0] == '0')
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "*" + value[num2].Substring(2));
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "*" + ystr.Substring(2));
                                    }
                                }
                                else
                                {
                                    value.Add("1|" + xstr.Substring(2) + "*" + ystr.Substring(2));
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "/":
                            if (flag[1] == 0)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr))
                                {
                                    int num = id.IndexOf(xstr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = Convert.ToDouble(xstr.Substring(2)) / y;
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "/" + y);
                                    }
                                }
                                else
                                {
                                    value.Add("1|" + xstr.Substring(2) + "/" + y);
                                }
                            }
                            else if (flag[1] == 1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr) && id.Contains(ystr))
                                {
                                    int num1 = id.IndexOf(xstr);
                                    int num2 = id.IndexOf(ystr);
                                    if (value[num1][0] == '0' && value[num2][0] == '0')
                                    {
                                        temp = Convert.ToDouble(value[num1].Substring(2)) / Convert.ToDouble(value[num2].Substring(2));
                                        value.Add("0|" + temp);
                                    }
                                    else if (value[num1][0] == '0')
                                    {
                                        value.Add("1|" + value[num1].Substring(2) + "/" + ystr.Substring(2));
                                    }
                                    else if (value[num2][0] == '0')
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "/" + value[num2].Substring(2));
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "/" + ystr.Substring(2));
                                    }
                                }
                                else if (id.Contains(xstr))
                                {
                                    int num1 = id.IndexOf(xstr);
                                    if (value[num1][0] == '0')
                                    {
                                        value.Add("1|" + value[num1].Substring(2) + "/" + ystr.Substring(2));
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "/" + ystr.Substring(2));
                                    }
                                }
                                else if (id.Contains(ystr))
                                {
                                    int num2 = id.IndexOf(ystr);
                                    if (value[num2][0] == '0')
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "/" + value[num2].Substring(2));
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "/" + ystr.Substring(2));
                                    }
                                }
                                else
                                {
                                    value.Add("1|" + xstr.Substring(2) + "/" + ystr.Substring(2));
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "^":
                            if (flag[1] == 0)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr))
                                {
                                    int num = id.IndexOf(xstr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = Math.Pow(Convert.ToDouble(value[num].Substring(2)), y);
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "^" + y);
                                    }
                                }
                                else
                                {
                                    value.Add("1|" + xstr.Substring(2) + "^" + y);
                                }
                            }
                            else if (flag[1] == 1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr) && id.Contains(ystr))
                                {
                                    int num1 = id.IndexOf(xstr);
                                    int num2 = id.IndexOf(ystr);
                                    if (value[num1][0] == '0' && value[num2][0] == '0')
                                    {
                                        temp = Math.Pow(Convert.ToDouble(value[num1].Substring(2)), Convert.ToDouble(value[num2].Substring(2)));
                                        value.Add("0|" + temp);
                                    }
                                    else if (value[num1][0] == '0')
                                    {
                                        value.Add("1|" + value[num1].Substring(2) + "^" + ystr.Substring(2));
                                    }
                                    else if (value[num2][0] == '0')
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "^" + value[num2].Substring(2));
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "^" + ystr.Substring(2));
                                    }
                                }
                                else if (id.Contains(xstr))
                                {
                                    int num1 = id.IndexOf(xstr);
                                    if (value[num1][0] == '0')
                                    {
                                        value.Add("1|" + value[num1].Substring(2) + "^" + ystr.Substring(2));
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "^" + ystr.Substring(2));
                                    }
                                }
                                else if (id.Contains(ystr))
                                {
                                    int num2 = id.IndexOf(ystr);
                                    if (value[num2][0] == '0')
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "^" + value[num2].Substring(2));
                                    }
                                    else
                                    {
                                        value.Add("1|" + xstr.Substring(2) + "^" + ystr.Substring(2));
                                    }
                                }
                                else
                                {
                                    value.Add("1|" + xstr.Substring(2) + "^" + ystr.Substring(2));
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "=":
                            if (id.Contains(xstr))
                            {
                                int index = id.IndexOf(xstr);
                                if (flag[1] == 0)
                                {
                                    value.RemoveAt(index);
                                    value.Insert(index, "0|" + y);
                                }
                                else if (flag[1] == 1)
                                {
                                    if (id.Contains(ystr))
                                    {
                                        int num = id.IndexOf(ystr);
                                        if (value[num][0] == '0')
                                        {
                                            value.RemoveAt(index);
                                            value.Insert(index, "0|" + Convert.ToDouble(value[num].Substring(2)));
                                        }
                                        else
                                        {
                                            value.RemoveAt(index);
                                            value.Insert(index, "1|" + ystr.Substring(2));
                                        }
                                    }
                                    else
                                    {
                                        value.RemoveAt(index);
                                        value.Insert(index, "1|" + ystr.Substring(2));
                                    }
                                }
                                else
                                {
                                    err(quad[4][i]);
                                }
                            }
                            else
                            {
                                id.Add(xstr.Substring(2));
                                if (flag[1] == 0)
                                {
                                    value.Add("0|" + y);
                                }
                                else if (flag[1] == 1)
                                {
                                    if (id.Contains(ystr))
                                    {
                                        int num = id.IndexOf(ystr);
                                        if (value[num][0] == '0')
                                        {
                                            value.Add("0|" + Convert.ToDouble(value[num].Substring(2)));
                                        }
                                        else
                                        {
                                            value.Add("1|" + ystr.Substring(2));
                                        }
                                    }
                                    else
                                    {
                                        value.Add("1|" + ystr.Substring(2));
                                    }
                                }
                                else
                                {
                                    err(quad[4][i]);
                                }
                            }
                            break;
                        case "sin":
                            if (flag[1] == -1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr))
                                {
                                    int num = id.IndexOf(xstr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = Math.Sin(Convert.ToDouble(value[num].Substring(2)));
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|sin(" + xstr.Substring(2) + ")");
                                    }
                                }
                                else
                                {
                                    value.Add("1|sin(" + xstr.Substring(2) + ")");
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "cos":
                            if (flag[1] == -1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr))
                                {
                                    int num = id.IndexOf(xstr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = Math.Cos(Convert.ToDouble(value[num].Substring(2)));
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|cos(" + xstr.Substring(2) + ")");
                                    }
                                }
                                else
                                {
                                    value.Add("1|cos(" + xstr.Substring(2) + ")");
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "tg":
                            if (flag[1] == -1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr))
                                {
                                    int num = id.IndexOf(xstr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = Math.Tan(Convert.ToDouble(value[num].Substring(2)));
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|tg(" + xstr.Substring(2) + ")");
                                    }
                                }
                                else
                                {
                                    value.Add("1|tg(" + xstr.Substring(2) + ")");
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "ctg":
                            if (flag[1] == -1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr))
                                {
                                    int num = id.IndexOf(xstr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = 1 / Math.Tan(Convert.ToDouble(value[num].Substring(2)));
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|ctg(" + xstr.Substring(2) + ")");
                                    }
                                }
                                else
                                {
                                    value.Add("1|ctg(" + xstr.Substring(2) + ")");
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "log":
                            id.Add(quad[3][i].Substring(2));
                            if (flag[1] == -1)
                            {
                                if (id.Contains(xstr))
                                {
                                    int num = id.IndexOf(xstr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = Math.Log(Convert.ToDouble(value[num].Substring(2)), 2);
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|log(" + xstr.Substring(2) + ")");
                                    }
                                }
                                else
                                {
                                    value.Add("1|log(" + xstr.Substring(2) + ")");
                                }
                            }
                            else if (flag[1] == 0)
                            {
                                if (id.Contains(xstr))
                                {
                                    int num = id.IndexOf(xstr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = Math.Log(y, Convert.ToDouble(value[num].Substring(2)));
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|log(" + xstr.Substring(2) + "," + y + ")");
                                    }
                                }
                                else
                                {
                                    value.Add("1|log(" + xstr.Substring(2) + "," + y + ")");
                                }
                            }
                            else if (flag[1] == 1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr) && id.Contains(ystr))
                                {
                                    int num1 = id.IndexOf(xstr);
                                    int num2 = id.IndexOf(ystr);
                                    if (value[num1][0] == '0' && value[num2][0] == '0')
                                    {
                                        temp = Math.Log(Convert.ToDouble(value[num2].Substring(2)), Convert.ToDouble(value[num1].Substring(2)));
                                        value.Add("0|" + temp);
                                    }
                                    else if (value[num1][0] == '0')
                                    {
                                        value.Add("1|log(" + value[num1].Substring(2) + "," + ystr.Substring(2) + ")");
                                    }
                                    else if (value[num2][0] == '0')
                                    {
                                        value.Add("1|log(" + xstr.Substring(2) + "," + value[num2].Substring(2) + ")");
                                    }
                                    else
                                    {
                                        value.Add("1|log(" + xstr.Substring(2) + "," + ystr.Substring(2) + ")");
                                    }
                                }
                                else if (id.Contains(xstr))
                                {
                                    int num1 = id.IndexOf(xstr);
                                    if (value[num1][0] == '0')
                                    {
                                        value.Add("1|log(" + value[num1].Substring(2) + "," + ystr.Substring(2) + ")");
                                    }
                                    else
                                    {
                                        value.Add("1|log(" + xstr.Substring(2) + "," + ystr.Substring(2) + ")");
                                    }
                                }
                                else if (id.Contains(ystr))
                                {
                                    int num2 = id.IndexOf(ystr);
                                    if (value[num2][0] == '0')
                                    {
                                        value.Add("1|log(" + xstr.Substring(2) + "," + value[num2].Substring(2) + ")");
                                    }
                                    else
                                    {
                                        value.Add("1|log(" + xstr.Substring(2) + "," + ystr.Substring(2) + ")");
                                    }
                                }
                                else
                                {
                                    value.Add("1|log(" + xstr.Substring(2) + "," + ystr.Substring(2) + ")");
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "ln":
                            if (flag[1] == -1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr))
                                {
                                    int num = id.IndexOf(xstr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = Math.Log(Convert.ToDouble(value[num].Substring(2)));
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|tg(" + xstr.Substring(2) + ")");
                                    }
                                }
                                else
                                {
                                    value.Add("1|tg(" + xstr.Substring(2) + ")");
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        case "lg":
                            if (flag[1] == -1)
                            {
                                id.Add(quad[3][i].Substring(2));
                                if (id.Contains(xstr))
                                {
                                    int num = id.IndexOf(xstr);
                                    if (value[num][0] == '0')
                                    {
                                        temp = Math.Log(Convert.ToDouble(value[num].Substring(2)), 10);
                                        value.Add("0|" + temp);
                                    }
                                    else
                                    {
                                        value.Add("1|tg(" + xstr.Substring(2) + ")");
                                    }
                                }
                                else
                                {
                                    value.Add("1|tg(" + xstr.Substring(2) + ")");
                                }
                            }
                            else
                            {
                                err(quad[4][i]);
                            }
                            break;
                        default:
                            err(quad[4][i]);
                            break;
                    }
                }
            }
            return error;
        }

        public void err(string count)
        {
            int c = Convert.ToInt32(count);
            int line = 1;
            for (int i = 0; i < c; i++)
            {
                if (cstr[i] == '\n'.ToString())
                {
                    line = line + 1;
                }
            }
            if (error.Contains("Error:line(" + line + ") syntax error(4)") == false)
            {
                error.Add("Error:line(" + line + ") syntax error(4)");
            }
        }
    }
}
