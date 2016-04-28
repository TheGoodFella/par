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

            string toPrint = "";
            if (args.Length > 0)
            {
                if (args[1] == "+")
                    toPrint = Sum(args[0], args[2]);
                if (args[1] == "*")
                    toPrint = Mul(args[0], args[2]);




                for (int i = 0; i < args.Length; i++)
                    Console.Write(args[i] + " ");

            }

            Console.Write(" = ");
            Console.Write(toPrint);
        }

        static string Sum(string a, string b)
        {
            return (double.Parse(a) + double.Parse(b)).ToString();
        }

        static string Mul(string a ,string b)
        {
            return (double.Parse(a) * double.Parse(b)).ToString();
        }
    }
}
