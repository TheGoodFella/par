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
            Par p = new Par();
            Console.Write(p.CallFuncFromDic(args[0], args[2], args[1]));
        }
    }
}
