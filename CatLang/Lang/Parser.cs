using CatLang.Lang.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatLang.Lang
{
    public static class Parser
    {
        public static object[] GetArgumentsFromLine(string ArgumentString, List<Variable> VariableList)
        {
            List<object> Arguments = new List<object>();
            //object[] args = new object[] {};
            string argString = ArgumentString.Trim() + " ";
            string value = "", type = "";
            while (argString.Length > 0)
            {
                char c = argString[0];
                
                if (type.Length > 0)
                {
                    if (c == ' ' && type != "str" && type != "skip")
                    {
                        if (type == "dbl")
                        {
                            if (double.TryParse(value, out double val))
                            {
                                Arguments.Add(val);
                            }
                        }
                        else
                        {
                            Variable v = VariableList.Find(x => x.Tag == value);
                            if (v == null)
                            {
                                v = new Variable();
                                v.Tag = value;
                                v.Value = null;
                                VariableList.Add(v);
                            }

                            Arguments.Add(v);
                        }
                        type = "";
                        value = "";

                    }
                    else if (c == '"' && type == "str")
                    {
                        type = "";
                        Arguments.Add(value);
                    }
                    else
                    {
                        if (type != "skip")
                        {
                            if (int.TryParse(c.ToString(), out int ch))
                            {
                                value += c;
                            }
                            else if (type == "dbl")
                            {
                                type = "skip";
                                value = "";
                            }
                            else
                            {
                                value += c;
                            }
                        }
                        else if (c == ' ')
                        {
                            type = "";
                        }
                    }
                }
                else
                {
                    if (c == '$')
                    {
                        type = "var";
                    }
                    else if (c == '"')
                    {
                        type = "str";
                    }
                    else
                    {
                        if (int.TryParse(c.ToString(), out int ch))
                        {
                            type = "dbl";
                            value += c;
                        }
                    }
                }
                argString = argString.Substring(1);
                if (argString.Length == 0 && type == "dbl")
                {
                    double num = 0;
                    double.TryParse(value, out num);
                    Arguments.Add(num);
                }
            }

            for (int i = 0; i < Arguments.Count; i++)
            {
                object arg = Arguments[i];
                if (arg.GetType() == typeof(double))
                {
                    if (int.TryParse(arg.ToString(), out int num))
                    {
                        Arguments[i] = num;
                    }
                }
            }

            return Arguments.ToArray();
        }
    }
}
