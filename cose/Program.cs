﻿using System;
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
            Par p = new Par(args);

            Console.Write(p.ManageCmd());
        }
    }
}
