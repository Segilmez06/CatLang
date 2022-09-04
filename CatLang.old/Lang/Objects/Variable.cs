using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CatLang.Lang.Utils;

namespace CatLang.Lang.Objects
{
    public class Variable
    {
        public string? Name { get; set; }
        public string Type { get; set; }
        object val;
        public object Value { 
            get
            {
                return val;
            } 
            set 
            {
                string v = value.ToString();
                if (TypeParser.IsNumeric(v))
                {
                    val = float.Parse(v.ToString().Replace(".", ","));
                    Type = "num";
                }
                else
                {
                    val = value;
                    Type = "str";
                }
            } 
        }
    }
}
