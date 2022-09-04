using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatLang.Lang.Objects
{
    public class Part
    {
        public string Name { get; set; }
        public List<string> Lines { get; set; }
        public List<Command> Commands{ get; set; }

        public Part()
        {
            Commands = new List<Command>();
        }
    }
}
