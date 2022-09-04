using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CatLang.Lang.Objects;

namespace CatLang.Lang.Variables
{
    public class Num : VariableBase<float>
    {
        public readonly string Type = "num";

        public Num()
        {
            
        }
        
        public Num(float NewValue)
        {
            Value = NewValue;
        }
    }
}
