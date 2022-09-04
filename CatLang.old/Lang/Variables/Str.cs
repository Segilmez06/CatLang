using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CatLang.Lang.Objects;

namespace CatLang.Lang.Variables
{
    public class Str : VariableBase<string>
    {
        public readonly string Type = "str";

        public Str()
        {

        }

        public Str(string NewValue)
        {
            Value = NewValue;
        }
    }
}
