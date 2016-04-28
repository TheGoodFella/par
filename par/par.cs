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

        //you enter the tri, returns you the right function
        Dictionary<string, Func<string,string,string>> dic = new Dictionary<string, Func<string,string,string>>();

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
            dic.Add(op.Mul, Mul);
            dic.Add(op.Sum, Sum);
            dic.Add(op.Div, Div);
        }

        public double Calculate(ref double res, ref int i)
        {
            Result=double.Parse(Param[0]);

            return 0;
        }

        public string CallFuncFromDic(string a, string b, string ope)
        {
            string res = "";

            Func<string, string, string> d;
            if (dic.TryGetValue(ope, out d))
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
