using System;
using System.IO;
using System.Linq;
using CatLang.Lang.Functions;
using CatLang.Lang.Objects;
using CatLang.Lang.Utils;

namespace CatLang
{
    class Program
    {
        public static List<Part> codetree = new();
        public static List<Variable> globalvars = new();
        
        static void Main(string[] args)
        {
            string filepath = args[0];
            string script = File.ReadAllText(filepath);
            string[] parts = script.Split("func");
            parts = parts.Skip(1).ToArray();
            foreach (var part in parts)
            {
                string funcstr = "func" + part;

                string name = TypeParser.GetPartName(funcstr);
                string[] lines = TypeParser.GetLines(funcstr);

                Part p = new();
                p.Name = name;
                p.Lines = lines.ToList();

                codetree.Add(p);
            }

            Part main = null;
            foreach (Part p in codetree)
            {
                if (p.Name == "start")
                {
                    main = p;
                    break;
                }
            }

            FunctionRunner.RunPart(main);
        }
    }
}