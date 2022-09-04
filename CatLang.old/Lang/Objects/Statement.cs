using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatLang.Lang.Objects
{
    public class Statement
    {
        public string Side1 { get; set; }
        public string Side2 { get; set; }
        public string Operator { get; set; }
        public bool Result { get; set; }
    }
}
