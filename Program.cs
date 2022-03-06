using JustCli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Check
{
    internal class Program
    {
        static int Main(string[] args)
        {
            return CommandLineParser.Default.ParseAndExecuteCommand(args);
        }
    }
}
