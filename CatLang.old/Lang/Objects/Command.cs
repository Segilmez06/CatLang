using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatLang.Lang.Objects
{
    public class Command
    {
        public string Name { get; set; }
        public List<Variable> Arguments { get; set; }
    }
}
