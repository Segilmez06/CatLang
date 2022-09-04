using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CatLang.Lang.Objects;

namespace CatLang.Lang.Functions
{
    public class InternalFunctions
    {
        public void print(List<Variable> Arguments)
        {
            foreach (Variable t in Arguments)
            {
                Console.Write(t.Value.ToString());
            }
        }
        public void printf(List<Variable> Arguments)
        {
            print(Arguments);
        }
        public void println(List<Variable> Arguments)
        {
            foreach (Variable t in Arguments)
            {
                Console.WriteLine(t.Value.ToString());
            }
        }
        public void echo(List<Variable> Arguments)
        {
            print(Arguments);
        }

        
        public string getInput(List<Variable> Arguments)
        {
            return Console.ReadLine();
        }

        public float add(List<Variable> Arguments)
        {
            float total = 0;
            foreach (Variable v in Arguments)
            {
                total += (float)v.Value;
            }
            return total;
        }
    }
}
