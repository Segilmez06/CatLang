using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatLang.Lang.Database
{
    public static class Keywords
    {
        public static string[] ReservedKeywords = { "var", "str", "num", "func", "if", "else", "while", "for", "return", "start" };
        public static string[] DeclarationKeywords = { "var", "str", "num", "func"};
        public static string[] CompareKeywords = { "==", ">=", "<=", "!=", ">", "<", "&&", "||" };
    }
}
