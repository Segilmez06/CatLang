using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatParser.Objects
{
    public class Variable
    {
        public string? Name { get; set; }
        public string Type { 
            get {
                return val.GetType().ToString();
            }
        }
        object val;
        public object Value { 
            get
            {
                return val;
            } 
            set 
            {
                string v = value.ToString();
                if (float.TryParse(v, out float a))
                {
                    val = v;
                }
                else
                {
                    val = value;
                }
            } 
        }
    }
}
