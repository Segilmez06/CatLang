using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CatLang.Lang.Objects;
using CatLang.Lang.Functions;

namespace CatLang.Lang.Utils
{
    public static class FunctionRunner
    {
        public static void RunPart(Part Function)
        {
            foreach (string Line in Function.Lines)
            {
                if (Line.Trim().StartsWith("//"))
                {
                    continue;
                }
                
                if(TypeParser.IsDeclaration(Line))
                    {
                    string vartype = Line.Split(' ')[0].ToLower();
                    string varname = Line.Split(' ')[1];

                    Variable v = new();
                    v.Type = vartype;
                    v.Name = varname;

                    Program.globalvars.Add(v);
                }
                else if (TypeParser.IsStatement(Line))
                {
                    Statement s = TypeParser.GetStatement(Line);
                }
                else if (TypeParser.IsMethod(Line))
                {
                    GetCommand(Line);
                }
                else
                {
                    string[] equationParts = Line.Split('=');

                    string mainpart = equationParts[0].Trim(), sourcepart = equationParts[1].Trim();

                    Variable dest = new();

                    if (TypeParser.IsDeclaration(mainpart))
                    {
                        string vartype = mainpart.Split(' ')[0].ToLower();
                        string varname = mainpart.Split(' ')[1];

                        dest.Type = vartype;
                        dest.Name = varname;

                        Program.globalvars.Add(dest);
                    }
                    else
                    {
                        string varname = mainpart.Trim();
                        dest = Program.globalvars.Find(x => x.Name == varname);
                    }

                    if (TypeParser.IsMethod(sourcepart))
                    {
                        dest.Value = GetCommand(sourcepart);
                    }
                    else
                    {
                        if (TypeParser.IsString(sourcepart))
                        {
                            dest.Value = sourcepart.Substring(1, sourcepart.Length - 2);
                        }
                        else if (TypeParser.IsNumeric(sourcepart))
                        {
                            dest.Value = sourcepart;
                        }
                        else if (TypeParser.IsVariable(sourcepart))
                        {
                            dest.Value = Program.globalvars.Find(x => x.Name == sourcepart);
                        }
                    }
                }

                //if (equationParts.Length == 1) // One-sided method
                //{
                //    string cmd = equationParts[0];
                //    if (TypeParser.IsMethod(cmd))
                //    {
                //        GetCommand(cmd);
                //    }
                //    else if (TypeParser.IsDeclaration(cmd))
                //    {
                //        string vartype = cmd.Split(' ')[0].ToLower();
                //        string varname = cmd.Split(' ')[1];

                //        Variable v = new();
                //        v.Type = vartype;
                //        v.Name = varname;

                //        Program.globalvars.Add(v);
                //    }
                    
                //}
                //else if (equationParts.Length > 1) // Two-sided equation - aka Assignment => This is probably piece of shit
                //{
                //    string mainpart = equationParts[0].Trim(), sourcepart = equationParts[1].Trim();

                //    Variable dest = new();
                        
                //    if (TypeParser.IsDeclaration(mainpart))
                //    {
                //        string vartype = mainpart.Split(' ')[0].ToLower();
                //        string varname = mainpart.Split(' ')[1];

                //        dest.Type = vartype;
                //        dest.Name = varname;

                //        Program.globalvars.Add(dest);
                //    }
                //    else
                //    {
                //        string varname = mainpart.Trim();
                //        dest = Program.globalvars.Find(x => x.Name == varname);
                //    }

                //    if (TypeParser.IsMethod(sourcepart))
                //    {
                //        dest.Value = GetCommand(sourcepart);
                //    }
                //    else
                //    {
                //        if (TypeParser.IsString(sourcepart))
                //        {
                //            dest.Value = sourcepart.Substring(1,sourcepart.Length - 2);
                //        }
                //        else if (TypeParser.IsNumeric(sourcepart))
                //        {
                //            dest.Value = sourcepart;
                //        }
                //        else if (TypeParser.IsVariable(sourcepart))
                //        {
                //            dest.Value = Program.globalvars.Find(x => x.Name == sourcepart);
                //        }
                //    }
                    
                //}
            }
            
            #region Old Code
            //foreach (var line in lines)
            //{
            //    if (line.Contains('(') && line.EndsWith(')') && !line.Contains("=")) //Normal method calling
            //    {
            //        Command c = new();
            //        c.Name = TypeParser.GetCommandName(line);
            //        c.Arguments = TypeParser.GetArguments(line);
            //        p.Commands.Add(c);
            //    }
            //    else if (line.Contains('(') && line.EndsWith(')') && line.Contains("=")) //Assigning with result
            //    {
            //        string toAssign = line.Split("=")[0].Trim();
            //        string value = line.Split("=")[1].Trim().Replace("(", "").Replace(")", "").Trim();

            //        var returnedValue = typeof(InternalFunctions).GetMethod(value).Invoke(new InternalFunctions(), null);

            //        string[] assignitems = toAssign.Split(" ");
            //        if (assignitems.Length == 1) //Already declared
            //        {
            //            foreach (Variable v in globalvars)
            //            {
            //                if (assignitems[0] == v.Name)
            //                {
            //                    v.Value = returnedValue;
            //                }
            //            }
            //        }
            //        else //New
            //        {
            //            Variable v = new Variable();

            //            string vartype = assignitems[0].Trim().ToLower();
            //            string varname = assignitems[1].Trim();

            //            v.Name = varname;
            //            v.Type = vartype;

            //            v.Value = returnedValue;
            //            globalvars.Add(v);
            //        }
            //    }
            //    else if (line.Contains("=")) //Assigning value
            //    {
            //        string toAssign = line.Split("=")[0].Trim();
            //        string value = line.Split("=")[1].Trim();

            //        string[] assignitems = toAssign.Split(" ");
            //        if (assignitems.Length == 1) //Already declared
            //        {
            //            int val;
            //            if (int.TryParse(value, out val))
            //            {
            //                foreach (Variable v in globalvars)
            //                {
            //                    if (assignitems[0] == v.Name && v.Type == "num")
            //                    {
            //                        v.Value = val;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                if (value.StartsWith('"') && value.EndsWith('"'))
            //                {
            //                    foreach (Variable v in globalvars)
            //                    {
            //                        if (assignitems[0] == v.Name && v.Type == "str")
            //                        {
            //                            v.Value = value.Substring(1, value.Length - 3);
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    foreach (Variable a in globalvars)
            //                    {
            //                        if (assignitems[0] == a.Name)
            //                        {
            //                            foreach (Variable v in globalvars)
            //                            {
            //                                if (assignitems[1] == v.Name && v.Type == a.Type)
            //                                {
            //                                    a.Value = v.Value;
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        else //New
            //        {
            //            Variable v = new Variable();

            //            string vartype = assignitems[0].Trim().ToLower();
            //            string varname = assignitems[1].Trim();

            //            v.Name = varname;
            //            v.Type = vartype;

            //            int val;
            //            if (int.TryParse(value, out val))
            //            {
            //                if (v.Type == "str")
            //                {
            //                    v.Value = val.ToString();
            //                }
            //                else if (v.Type == "num")
            //                {
            //                    v.Value = val;
            //                }
            //            }
            //            else
            //            {
            //                if (value.StartsWith('"') && value.EndsWith('"'))
            //                {
            //                    if (v.Type == "str")
            //                    {
            //                        v.Value = value.Substring(1, value.Length - 2);
            //                    }
            //                }
            //                else
            //                {
            //                    foreach (Variable source in globalvars)
            //                    {
            //                        if (assignitems[1] == source.Name && source.Type == v.Type)
            //                        {
            //                            v.Value = source.Value;
            //                            break;
            //                        }
            //                    }
            //                }
            //            }
            //            globalvars.Add(v);
            //        }
            //    }
            //    else //New object
            //    {
            //        string toAssign = line.Split("=")[0].Trim();
            //        string value = line.Split("=")[1].Trim();

            //        string[] assignitems = toAssign.Split(" ");

            //        Variable v = new Variable();

            //        string vartype = assignitems[0].Trim();
            //        string varname = assignitems[1].Trim();

            //        v.Name = varname;
            //        v.Type = vartype;

            //        int val;
            //        if (int.TryParse(value, out val))
            //        {
            //            if (v.Type == "str")
            //            {
            //                v.Value = val.ToString();
            //            }
            //            else if (v.Type == "num")
            //            {
            //                v.Value = val;
            //            }
            //        }
            //        else
            //        {
            //            if (value.StartsWith('"') && value.EndsWith('"'))
            //            {
            //                if (v.Type == "str")
            //                {
            //                    v.Value = value.Substring(1, value.Length - 3);
            //                }
            //            }
            //            else
            //            {
            //                foreach (Variable source in globalvars)
            //                {
            //                    if (assignitems[1] == source.Name && source.Type == v.Type)
            //                    {
            //                        v.Value = source.Value;
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //        globalvars.Add(v);
            //    }

            //}


            //foreach (Command cmd in Function.Commands)
            //{
            //    typeof(InternalFunctions).GetMethod(cmd.Name).Invoke(new InternalFunctions(), new[] { cmd.Arguments });
            //}
            #endregion
        }
        public static object GetCommand(string CommandString)
        {
            string cmdName = CommandString.Split('(')[0];
            return typeof(InternalFunctions).GetMethod(cmdName).Invoke(new InternalFunctions(), new[] { TypeParser.GetArguments(CommandString) });
        }
    }
}