using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace par
{
    

    
    public class Par
    {
        //you enter the tri, returns you the right function
        Dictionary<Operations, Func<double>> dic = new Dictionary<Operations, Func<double>>();

        public string[] Param { get; set; }

        public double Result { get; set; }

        public Par() { }

        public Par(string[] args)
        {
            Param = args;


        }

        public double Calculate(ref double res, ref int i)
        {
            Result=double.Parse(Param[0]);

            return 0;
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
