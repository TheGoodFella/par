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
    public class Operations : List<string>
    {
        //the indices of the operations ist:
        public int Mul { get; private set; }

        public int Sum { get; private set; }

        public int Div { get; private set; }

        public int Pow { get; private set; }

        public Operations()
        {
            Add("*"); Mul = 0;
            Add("+"); Sum = 1;
            Add("/"); Div = 2;
            Add("^");Pow = 3;
        }
    }

}
