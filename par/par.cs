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
            dic.Add("*", new Func<string, string, string>(Mul));
        }

        public Par(string[] args)
        {
            Param = args;

            dic.Add("*", new Func<string,string,string>(Mul));
        }

        public double Calculate(ref double res, ref int i)
        {
            Result=double.Parse(Param[0]);

            return 0;
        }

        public string CallMulFromDic()
        {
            string res = "";

            Func<string, string, string> d;
            if (dic.TryGetValue("*", out d))
                res=d("5", "2");
            else
                return "no result";

            return "5*2=" + res;
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

        #endregion
    }
}
