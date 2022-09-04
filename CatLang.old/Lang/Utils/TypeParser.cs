using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using CatLang.Lang.Objects;
using CatLang.Lang.Variables;
using CatLang.Lang.Database;
using CatLang.Lang.Functions;

namespace CatLang.Lang.Utils
{
    public static class TypeParser
    {
        public static string GetPartName(string FullScript)
        {
            return FullScript.Split("func")[1].Split("(")[0].Trim();
        }
        public static string[] GetLines(string FullScript)
        {
            string reformatted = FullScript;
            reformatted = reformatted.Substring(reformatted.IndexOf("{") + 1);
            reformatted = reformatted.Substring(0, reformatted.LastIndexOf("}"));
            reformatted = reformatted.Trim().Replace("\n", "").Replace("\t", "");
            reformatted = reformatted.Replace("\r", "").Replace("}", "};");
            string[] lines = reformatted.Split(";");


            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].Trim();
            }
            lines = lines.SkipLast(1).ToArray();
            var list = lines.ToList();
            foreach (string l in lines)
            {
                if (l.StartsWith("//"))
                {
                    list.Remove(l);
                }
            }
            return list.ToArray();
        }
        public static string GetCommandName(string FullLine)
        {
            return FullLine.Split("(")[0].Trim();
        }
        public static List<Variable> GetArguments(string FullLine)
        {
            string argStr = FullLine;
            argStr = argStr.Substring(argStr.IndexOf("(") + 1);
            argStr = argStr.Substring(0, argStr.LastIndexOf(")"));
            
            if (argStr.StartsWith(','))
                argStr = argStr.Substring(1);
            
            if (argStr.EndsWith(','))
                argStr = argStr.Substring(argStr.Length - 2);
            
            if (argStr.Length > 0)
            {
                List<Variable> varlist = new();

                string temp = "";
                bool open = false;

                while (argStr.Length > 0)
                {
                    char c = argStr[0];
                    if (open)
                    {
                        if (c == '"')
                        {
                            open = false;
                        }
                        else
                        {
                            temp += c;
                        }
                    }
                    else
                    {
                        if (c == ',')
                        {
                            Variable a = new();
                            if (IsMethod(temp))
                            {
                                a.Value = FunctionRunner.GetCommand(temp);
                            }
                            else if (IsNumeric(temp))
                            {
                                a.Value = temp;
                            }
                            varlist.Add(a);
                            temp = "";
                        }
                        else if (c == '"')
                        {
                            open = true;
                        }
                        else if (c == '(')
                        {
                            string t2 = "";
                            for (int i = 0; i < argStr.LastIndexOf(")"); i++)
                            {
                                t2 += argStr[i];
                            }
                            argStr = argStr.Substring(t2.Length - 1);
                            temp += t2;
                        }
                        else
                        {
                            temp += c;
                        }
                    }
                    argStr = argStr.Substring(1);
                }
                Variable v = new();
                if (IsMethod(temp))
                {
                    v.Value = FunctionRunner.GetCommand(temp);
                }
                else if (IsNumeric(temp) || IsString('"' + temp + '"'))
                {
                    v.Value = temp;
                }
                else if (IsVariable(temp))
                {
                    v.Value = Program.globalvars.Find(x => x.Name == temp).Value;
                }
                varlist.Add(v);

                return varlist;
            }
            return new();
        }
    
    
        public static bool IsDeclaration(string Line)
        {
            if (Line.Split(" ").Length <= 1)
            {
                return false;
            }
            string keyword = Line.Split(" ")[0].Trim();
            string varname = Line.Split(" ")[1].Trim();

            if (Keywords.DeclarationKeywords.Contains(keyword))
            {
                return true;
            }
            return false;
        }
        public static bool IsMethod(string Line)
        {
            // [A-Za-z0-9]{1,}\([a-zA-Z0-9\t\n .,/<>?;:""'`!@#$%^&*()\[\]{}_+=|\\-]{1,}\)
            // [A-Za-z0-9]{1,}\(.{0,}\)
            // [A-Za-z0-9]{1,}\(.{0,}\)[\s]{0,}[^{]
            // [A-Za-z0-9]{1,}\(.{0,}\)[\s]{0,}[.^]{0,}
            // [A-Za-z0-9]{1,}\(.{0,}\)[\s]{0,}[^{}\[\]()]
            // [A-Za-z0-9]{1,}\(.{0,}\)[\s]{0,}[^{\[(]
            // [A-Za-z0-9]{1,}\(.{0,}\)[^{]
            return new Regex(@"[A-Za-z0-9]{1,}\(.{0,}\)").IsMatch(Line);
        }
        public static bool IsString(string Line)
        {
            return new Regex(@""".{1,}""").IsMatch(Line);
        }
        public static bool IsNumeric(string Line)
        {
            return float.TryParse(Line, out float a);
        }
        public static bool IsVariable(string Line)
        {
            return new Regex(@"[a-zA-Z0-9]{1,}").IsMatch(Line);
        }
        public static bool IsStatement(string Line)
        {
            // [A-Za-z0-9]{1,}\(.{0,}\)[\s]{0,}[{].{0,}
            // [A-Za-z0-9]{1,}\(.{0,}\)[\s]{0,}[{\[(]
            return new Regex(@".{1,}(==|>=|<=|!=|>|<).{1,}").IsMatch(Line);
        }
    
    
        public static string GetStatementString(string Line)
        {
            string reformatted = Line;
            reformatted = reformatted.Trim().Replace("\n", "").Replace("\t", "");
            reformatted = reformatted.Substring(reformatted.IndexOf("(") + 1);
            reformatted = reformatted.Substring(0, reformatted.LastIndexOf(")"));

            if (IsStatement(reformatted))
                return reformatted;
            return "";
        }
        public static Statement GetStatement(string StatementString)
        {
            string st = GetStatementString(StatementString);
            string s1 = "", s2 = "", op = "";
            foreach (var c in Keywords.CompareKeywords)
            {
                if (st.Contains(c))
                {
                    s1 = st.Split(c).First().Trim();
                    s2 = st.Split(c).Last().Trim();
                    op = c;
                    break;
                }
            }

            Console.WriteLine(s1);
            Console.WriteLine(s2);
            
            Statement s = new();
            s.Side1 = new Regex(@"").Matches(st)[0].Value;

            return new();
        }
    }
}
