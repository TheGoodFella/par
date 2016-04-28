using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using par;

namespace cose
{
    class Program
    {
        static void Main(string[] args)
        {
            Par p = new Par();

            if (args[0] == "--help")
            {
                Console.Write("available operations:\n*\n+\n/");
                return;
            }
            Console.Write(p.CallFuncFromDic(args[0], args[2], args[1]));
        }
    }
}
