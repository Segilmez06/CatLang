using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatLang.Lang.Objects
{
    public abstract class VariableBase<VarType>
    {
        public string? Name { get; set; }
        public VarType Value { get; set; }
    }
}
