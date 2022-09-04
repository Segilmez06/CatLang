using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatLang.Lang
{
    public class InternalCalls
    {
        public object set(object Arguments)
        {
            try
            {
                string argstr = ((string)Arguments).Trim();
                string tag = argstr.Split(" ")[0];
                object val = argstr.Substring(tag.Length).Trim();
                Variable var = Program.vars.Find(x => x.Name == tag);
                if (var != null)
                {
                    var.Value = val;
                    return new CallOut(0);
                }
                Program.vars.Add(new Variable(tag, val));
            }
            catch (Exception e)
            {
                return new CallOut(1, e);
            }
            return new CallOut(0);
        }
        
        public object write(object Arguments)
        {
            try
            {
                string varname = (string)Arguments;
                Variable v = Program.vars.Find(x => x.Name == varname);
                if (v != null)
                {
                    object val = v.Value;
                    Console.WriteLine(val);
                    return new CallOut(0);
                }
                return new CallOut(1, new NullReferenceException());
            }
            catch (Exception e)
            {
                return new CallOut(1, e);
            }
            return new CallOut(0);
        }

        public object read(object Arguments)
        {
            try
            {
                string data = Console.ReadLine();
                string varname = (string)Arguments;
                Variable v = Program.vars.Find(x => x.Name == varname);
                if (v != null)
                {
                    v.Value = data;
                }
                else
                {
                    Program.vars.Add(new Variable(varname, data));
                }
            }
            catch (Exception e)
            {
                return new CallOut(1, e);
            }
            return new CallOut(0);
        }

        public object exit(object Arguments)
        {
            try
            {
                int code = 0;
                if (((string)Arguments).Length < 0)
                {
                    code = Convert.ToInt32(Arguments);
                }
                Environment.Exit(code);
            }
            catch (Exception e)
            {
                return new CallOut(1, e);
            }
            return new CallOut(0);
        }

        public object clear(object Arguments)
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
            
        }
        public CallOut(int ExitCode)
        {
            Code = ExitCode;
        }
        public CallOut(int ExitCode, object CallOutput)
        {
            Code = ExitCode;
            Output = CallOutput;
        }
    }
}
