using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace par
{
    

    
    public class Par
    {
        Operations op = new Operations();

        /// <summary>
        /// Dictionary for the math commands, like  2 * 6 for example. You enter the tri, returns you the right function
        /// </summary>
        Dictionary<string, Func<string,string,string>> dicTri = new Dictionary<string, Func<string,string,string>>();

        /// <summary>
        /// dictionary for the single commands, like --help
        /// </summary>
        Dictionary<string, Func<string>> dicSin = new Dictionary<string, Func<string>>();

        public string[] Param { get; set; }

        public double Result { get; set; }

        public Par() 
        {
            StoreDictionary();
        }

        public Par(string[] args)
        {
            Param = args;

            StoreDictionary();
        }

        private void StoreDictionary()
        {
            //dictionary triplet
            dicTri.Add(op[op.Mul], Mul);
            dicTri.Add(op[op.Sum], Sum);
            dicTri.Add(op[op.Div], Div);

            //dictionary single
            dicSin.Add("--help", Help);
        }

        private string Help()
        {
            return "available operations:\n* --> Multiplication\n+ --> Sum\n/ --> division";
        }
        
        public int IndexParam(string s, string q)
        {
            for (int i = 0; i < Param.Length; i++)
                if (Param[i] == s) return i;
            return -1;
        }

        public string ManageCmd()
        {
            for (int i = 0; i < Param.Length; i++)
            {
                for (int o = 0; o < op.Count; o++)
                {
                    if (Param[i].Contains(op[o]))
                    {
                        int index = Param[i].IndexOf(op[o]);

                        string a = Param[i].Substring(0, index);
                        index = Param[i].IndexOf(op[o]);
                        string b = Param[i].Substring(index + 1, Param[i].Length-1 - index);
                        string ope = Param[i].Substring(index,1);
                        return Calculate(a, b, ope);
                    }
                }
            }
            if (Param.Length == 1)
            {
                Func<string> funcSin;
                if (dicSin.TryGetValue(Param[0], out funcSin))
                    return funcSin();
                else
                    return "error";
            }

            //if (Param.Length == 3)
            //{
            //    Func<string, string, string> funcTri;
            //    if (dicTri.TryGetValue(Param[1], out funcTri))
            //        return funcTri(Param[0], Param[2]);
            //    else
            //        return "no result";
            //}

            return "null";
        }

        public string Calculate(string a, string b, string ope)
        {
            string res = "";

            Func<string, string, string> d;
            if (dicTri.TryGetValue(ope, out d))
                res=d(a, b);
            else
                return "no result";

            return a + ope + b + "=" + res;
        }

        private void SendOp(double a, string o, double b)
        {

        }

        #region math operations

        private string Sum(string a, string b)
        {
            return (double.Parse(a) + double.Parse(b)).ToString();
        }

        private string Mul(string a, string b)
        {
            return (double.Parse(a) * double.Parse(b)).ToString();
        }

        private string Div(string a, string b)
        {
            return (double.Parse(a) / double.Parse(b)).ToString();
        }

        #endregion
    }
}
