using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace par
{
    /// <summary>
    /// list of operations
    /// </summary>
    public class Operations
    {
        public string Mul { get; private set; }

        public string Sum { get; private set; }

        public string Div { get; private set; }

        public Operations()
        {
            Mul = "*";
            Sum = "+";
            Div = "/";
        }
    }

}
