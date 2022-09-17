using CatLang.Lang.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatLang.Lang
{
    public class Calls
    {
        public object set(object[] Arguments)
        {
            //try
            //{
            //    string argstr = ((string)Arguments).Trim();
            //    string tag = argstr.Split(" ")[0];
            //    object val = argstr.Substring(tag.Length).Trim();
            //    Variable var = Program.vars.Find(x => x.Name == tag);
            //    if (var != null)
            //    {
            //        var.Value = val;
            //        return new CallOut(0);
            //    }
            //    Program.vars.Add(new Variable(tag, val));
            //}
            //catch (Exception e)
            //{
            //    return new CallOut(1, e);
            //}
            return new CallOut(0);
        }

        public object write(object[] Arguments)
        {
            try
            {
                string msg = Arguments[0].ToString();
                if (Arguments[0].GetType() == typeof(Variable))
                {
                    Variable v = (Variable)Arguments[0];
                    object str = v.Value;
                    msg = str.ToString();
                    //msg = ((Variable)Arguments[0]).Value.ToString();
                }
                Console.WriteLine(msg);
            }
            catch (Exception e)
            {
                return new CallOut(1, e);
            }
            return new CallOut(0);
        }

        public object read(object[] Arguments)
        {
            try
            {
                if (Arguments[1] != null)
                {
                    Console.Write(Arguments[1].ToString());
                }
                Variable v = (Variable)Arguments[0];
                v.Value = Console.ReadLine();
            }
            catch (Exception e)
            {
                return new CallOut(1, e);
            }
            return new CallOut(0);
        }

        public object exit(object[] Arguments)
        {
            try
            {
                Environment.Exit((int)Arguments[0]);
            }
            catch (Exception e)
            {
                return new CallOut(1, e);
            }
            return new CallOut(0);
        }

        public object clear(object[] Arguments)
        {
            try
            {
                Console.Clear();
            }
            catch (Exception e)
            {
                return new CallOut(1, e);
            }
            return new CallOut(0);
        }
    }

    public class CallOut
    {
        public int Code { get; set; }
        public object Output { get; set; }
        public CallOut()
        {
            Code = 0;
            Output = "";
        }
        public CallOut(int ExitCode)
        {
            Code = ExitCode;
            Output = "";
        }
        public CallOut(int ExitCode, object CallOutput)
        {
            Code = ExitCode;
            Output = CallOutput;
        }
    }
}
