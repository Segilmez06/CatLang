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
        public List<Command> Commands { get; set; }

        /// <summary>
        /// Code part that can be invoked.
        /// </summary>
        public Part()
        {
            Commands = new List<Command>();
        }

        /// <summary>
        /// Add command to list.
        /// </summary>
        /// <param name="command"></param>
        public void AddCmd(Command command)
        {
            Commands.Add(command);
        }

        /// <summary>
        /// Remove command from list.
        /// </summary>
        /// <param name="command"></param>
        public void RemoveCmd(Command command)
        {
            Commands.Remove(command);
        }
    }
}
