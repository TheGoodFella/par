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

        /// <summary>
        /// dictionary for the commands settings, not returs string
        /// </summary>
        Dictionary<string, Action> dicVoid = new Dictionary<string, Action>();

        public string[] Param { get; set; }

        public double Result { get; set; }

        private bool onlyResult = false;

        private bool onlyAllResult = false;

        private bool sumAll = false;

        private List<double> allRes = new List<double>();

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
            dicSin.Add("?", Help);
            dicSin.Add(string.Empty, Empty);

            //dictionary void
            dicVoid.Add("-ores", Res);
            dicVoid.Add("-sumall", SumAll);
            dicVoid.Add("-allores", ResAll);
        }

        #region Void methods
        private void Res()
        {
            onlyResult = true;
        }

        private void SumAll()
        {
            sumAll = true;
        }

        private void ResAll()
        {
            onlyAllResult = true;
        }
        #endregion

        #region single methods
        private string Empty()
        {
            return "invalid command, type ? for help";
        }

        private string Help()
        {
            string r = "\n" +
		       "https://github.com/TheGoodFella/par\n\n"+
                       "?\t\t\tshow help\n\n" +
                       "NUMERIC PARAMETERS:\n" +
                       "<num><operator><num>\twithout spaces!, return the mathematical result\n" +
                       "OPTIONAL COMMANDS OBLIGATORILY BEFORE THE CALC YOU WANT IT TO BE APPLIED:\n" +
                       "-ores\t\t\tshow only the result, without the syntax a+b=result\n" +
                       "-sumall\t\t\tsum all the results of the operations written after this command\n" +
                       "available operations:\n* --> Multiplication\n+ --> Sum\n/ --> division\nSubtraction --> invert the number sign: instead of 8-3 use 8+-3\n"+
                       "OPTIONAL COMMANDS IN ANY PLACE\n"+
                       "-allores\t\tthis command when -sumall, only show the sumall result without syntax: sumall=result";

            return r;
        }
        #endregion

        public int IndexParam(string s, string q)
        {
            for (int i = 0; i < Param.Length; i++)
                if (Param[i] == s) return i;
            return -1;
        }

        public string ManageCmd()
        {
            StringBuilder sb = new StringBuilder();

            if (Param.Length == 0)
                sb.Append(SingleCommands(""));

            for (int i = 0; i < Param.Length; i++)
            {
                //check if single parameter function commands are available:
                string re = SingleCommands(Param[i]);
                if (re != string.Empty)
                    sb.Append(re);
                else
                    //check if void functions command (set up command) are available
                    VoidCommands(Param[i]);

                //and calculate the operation 
                //I scroll the array of operation and compare to the input string, if match calculate it
                for (int o = 0; o < op.Count; o++)
                {
                    if (Param[i].Contains(op[o]))
                    {
                        //get the index of the mathematical symbol
                        int index = Param[i].IndexOf(op[o]);

                        //get the first number:
                        string a = Param[i].Substring(0, index);
                        //get the second number:
                        string b = Param[i].Substring(index + 1, Param[i].Length - 1 - index);
                        //get the mathematical symbol
                        string ope = Param[i].Substring(index, 1);
                        //return the mathematical result
                        sb.Append(Calculate(a, b, ope) + "\n");
                    }
                }
            }
            if (sumAll)
            {
                double all = 0;

                for (int i = 0; i < allRes.Count; i++)
                    all += allRes[i];
                if (onlyAllResult)
                    sb.Append(all + "\n");
                else
                    sb.Append("Sum all=" + all + "\n");
                onlyAllResult = false;
            }
            return sb.ToString();
        }

        public void VoidCommands(string cmd)
        {
            Action action;
            if (dicVoid.TryGetValue(cmd, out action))
                action();
        }

        public string SingleCommands(string cmd)
        {
            Func<string> funcSin;
            if (dicSin.TryGetValue(cmd, out funcSin))
                return funcSin();

            return string.Empty;
        }

        public string Calculate(string a, string b, string ope)
        {
            string res = "";

            Func<string, string, string> d;
            if (dicTri.TryGetValue(ope, out d))
                res = d(a, b);
            else
                return "no result";
            string final = "";

            //if sumall is active, store all the final result for other purpose like sum them all, etc
            if (sumAll)
                allRes.Add(double.Parse(res));

            if (onlyResult)
                final = res;
            else
                final = a + ope + b + "=" + res;
            onlyResult = false;
            return final;
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
