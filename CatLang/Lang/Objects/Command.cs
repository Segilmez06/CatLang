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
        public List<object> Arguments { get; set; }

        /// <summary>
        /// A line of program code.
        /// </summary>
        public Command()
        {

        }

        /// <summary>
        /// A line of program code with arguments.
        /// </summary>
        /// <param name="CmdName"></param>
        /// <param name="CmdArguments"></param>
        public Command(string CmdName, object[] CmdArguments)
        {
            Name = CmdName;
            Arguments = CmdArguments.ToList();
        }
    }
}
