using CatLang.Lang.Objects;
using CatLang.LogSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CatLang.Lang
{
    public class CatLang
    {
        string[] Lines = new string[] { };
        string FilePath = "";

        bool IsRunning = false;
        bool IsDebugMode = false;
        bool ConsoleSession = false;

        string Prompt = "> ";

        ConsoleColor DefaultForeColor = Console.ForegroundColor;
        ConsoleColor PromptColor = ConsoleColor.Green;
        ConsoleColor ErrorColor = ConsoleColor.Red;
        ConsoleColor DebugColor = ConsoleColor.Yellow;

        Logger Log = new();

        List<Variable> Variables = new();
        List<Part> Parts = new();

        /// <summary>
        /// Initialize language
        /// </summary>
        /// <param name="Args"></param>
        public CatLang(string[] Args)
        {
            IsRunning = true;
            //Log.Write("CatLang started");
            ConsoleSession = (Args.Length == 0);
            //Log.Write($"Console mode: {ConsoleSession}");

#if DEBUG
                IsDebugMode = true;
#endif

            Variable v = new Variable();
            v.Tag = ".out";
            v.Value = "";
            v.Type = "str";
            Variables.Add(v);

            Variable e = new Variable();
            e.Tag = ".exitcode";
            e.Value = 0;
            e.Type = "int";
            Variables.Add(e);

            if (ConsoleSession)
            {
                while (IsRunning)
                {
                    if ((int)Variables.Find(x => x.Tag == ".exitcode").Value != 0)
                    {
                        Console.ForegroundColor = ErrorColor;
                    }
                    else
                    {
                        Console.ForegroundColor = PromptColor;
                    }
                    Console.Write(Prompt);
                    Console.ForegroundColor = DefaultForeColor;
                    string line = Console.ReadLine();
                    //Log.Write($"Running '{line}'");
                    ProcessLine(line);
                }
            }
            else
            {
                string FilePath = Path.GetFullPath(Args[0]);
                if (!File.Exists(FilePath))
                {
                    ExitProcess(1, (new FileNotFoundException()).Message);
                }
                string _text = File.ReadAllText(FilePath).Trim();
                if (_text.Length == 0)
                {
                    ExitProcess(1, (new NullReferenceException()).Message);
                }
                Lines = File.ReadAllLines(FilePath);
                foreach (string line in Lines)
                {
                    ProcessLine(line);
                }
            }
        }

        public bool ReadPart = false;
        public Part CurrentPart = new Part();

        /// <summary>
        /// Run a command.
        /// </summary>
        /// <param name="cmd"></param>
        public void ProcessCommand(Command cmd)
        {
            string name = cmd.Name;
            object[] args = cmd.Arguments.ToArray();

            MethodInfo method = typeof(Calls).GetMethod(name);
            CallOut cmdout = new(0);
            if (method != null)
            {
                cmdout = (CallOut)method.Invoke(new Calls(), new object[] { args });
            }
            else
            {
                cmdout = new(1, new NullReferenceException().Message);
            }
            Variables.Find(x => x.Tag == ".out").Value = cmdout.Output;
            Variables.Find(x => x.Tag == ".exitcode").Value = cmdout.Code;
        }

        /// <summary>
        /// Process a line of command.
        /// </summary>
        /// <param name="Line"></param>
        public void ProcessLine(string Line)
        {
            string line = Line.Trim();
            if (line.Length != 0 && line[0] != '#')
            {
                string cmd = line.Split(":")[0];
                object[] args = new string[] { };
                if (line != cmd)
                {
                    string argString = line.Substring(cmd.Length + 1);
                    args = Parser.GetArgumentsFromLine(argString, Variables);
                }
                if (IsDebugMode) PrintArgumentInfo(args);

                Command command = new Command(cmd, args);
                if (ReadPart)
                {
                    CurrentPart.AddCmd(command);
                }
                else
                {
                    ProcessCommand(command);
                }
            }
        }

        
        /// <summary>
        /// Process a part.
        /// </summary>
        /// <param name="TargetPart"></param>
        public void ProcessPart(Part TargetPart)
        {
            foreach (Command cmd in TargetPart.Commands)
            {
                ProcessCommand(cmd);
            }
        }

        /// <summary>
        /// Exit current process with exit code and message
        /// </summary>
        /// <param name="ExitCode"></param>
        /// <param name="Message"></param>
        private void ExitProcess(int ExitCode = 0, string Message = "")
        {
            string ExitState = ExitCode == 0 ? "Info" : "Error";
            if (Message.Length > 0)
            {
                Console.WriteLine($"{ExitState}: {Message}");
            }
            Log.Close();
            Environment.Exit(ExitCode);
        }


        /// <summary>
        /// Prints values of elements in given array
        /// </summary>
        /// <param name="args"></param>
        private void PrintArgumentInfo(object[] args)
        {
            Console.ForegroundColor = DebugColor;
            Console.WriteLine($"{args.Length} arguments");
            foreach (object arg in args)
            {
                object val = "";
                if (arg.GetType() == typeof(Variable))
                {
                    Variable v = (Variable)arg;
                    val = v.Tag;
                    if (string.IsNullOrEmpty((string)val))
                    {
                        val = "Not assigned";
                    }
                }
                else
                {
                    val = arg.ToString();
                }
                Console.WriteLine($"'{val}' is a/an {arg.GetType()}");
            }
            Console.ForegroundColor = DefaultForeColor;
        }
    }
}
