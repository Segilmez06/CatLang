using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatLang.Lang
{
    public class Variable
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Variable()
        {

        }
        public Variable(string Tag)
        {
            Name = Tag;
        }
        public Variable(string Tag, object Data)
        {
            Name = Tag;
            Value = Data;
        }

    }
}
