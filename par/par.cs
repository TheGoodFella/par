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
        Dictionary<string, Func<string, string, string>> dicTri = new Dictionary<string, Func<string, string, string>>();

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

        private bool mulAll = false;

        private List<double> allRes = new List<double>();

        /// <summary>
        /// the string that will be returned at the end of the program
        /// </summary>
        public StringBuilder Sb { get; set; }

        public Par()
        {
            Init();
        }

        public Par(string[] args)
        {
            Param = args;
            Init();
        }

        private void Init()
        {
            //dictionary triplet
            dicTri.Add(op[op.Mul], Mul);
            dicTri.Add(op[op.Sum], Sum);
            dicTri.Add(op[op.Div], Div);
            dicTri.Add(op[op.Pow], Pow);

            //dictionary single
            dicSin.Add("?", Help);
            dicSin.Add(string.Empty, Empty);

            //dictionary void
            dicVoid.Add("-ores", Res);
            dicVoid.Add("-sumall", SumAll);
            dicVoid.Add("-allores", ResAll);
            dicVoid.Add("-mulall", MulAll);

            Sb = new StringBuilder();
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

        private void MulAll()
        {
            mulAll = true;
        }
        #endregion

        #region single methods
        private string Empty()
        {
            return "invalid command, type ? for help";
        }

        private string Help()
        {
            string r =  "\n" +
                        "https://github.com/TheGoodFella/par\n\n"           +
                        "? \tshow help\n\n"                              +
                        "\nNUMERIC PARAMETERS:\n"                             +
                        "\t<num><operator><num>    without spaces!, return the mathematical result\n" +
                        "\nOPTIONAL COMMANDS OBLIGATORILY BEFORE THE CALC YOU WANT IT TO BE APPLIED:\n"   +
                        "\t-ores                   show only the result, without the syntax a+b=result\n" +
                        "\n*ALL COMMANDs (one at a time) (operate with the result of the operation written after it):\n"     +
                        "\t-sumall                 sum all the results of the operations written after this command\n"                   +
                        "\t-mulall                 multiplies all the results of the operations written after this command\n"            +
                        "\nAVAILABLE OPERATIONS:\n" +
                        "\t*           --> Multiplication\n"+
                        "\t+           --> Sum\n"+
                        "\t/           --> division\n"+
                        "\t^           --> Power\n"+
                        "\tsubtraction --> invert the number sign: instead of 8-3 use 8+-3\n" +
                        "\nOPTIONAL COMMANDS IN ANY PLACE\n"                  +
                        "\t-allores                this command when a *all command is present, only show the result\n"                  +
                        "\nThis program doesn't have code for check the correct input format";

            return r;
        }
        #endregion

        public int IndexParam(string s, string q)
        {
            for (int i = 0; i < Param.Length; i++)
                if (Param[i] == s) return i;
            return -1;
        }

        /// <summary>
        /// Main method, all starts with this and finish with this
        /// </summary>
        /// <returns>the text this class has generated starting from the command stored in Param</returns>
        public string ManageCmd()
        {
            //the lenght of parameters Param is zero?
            IsZero();

            for (int i = 0; i < Param.Length; i++)
            {
                //check if single parameter function commands are available:
                if (!IsSingle(i))
                    //check if void functions command (set up command) are available
                    VoidCommands(Param[i]);

                //essential try catch because the string can be an operation (like 8*9), a command (like -help) or something else, only few times is just a number
                try
                {
                    IsaN(i);
                }
                catch (FormatException) { }

                //and calculate the operation 
                //I scroll the array of operation and compare to the input string, if match calculate it
                for (int o = 0; o < op.Count; o++)
                {
                    IsOperation(i, o, op);
                }
            }

            LastAppend();
            return Sb.ToString();
        }

        #region Methods call by ManageCmd

        /// <summary>
        /// check if Param is empty, and append to Sb if true
        /// </summary>
        private void IsZero()
        {
            if (Param.Length == 0)
                Append(SingleCommands(""), true);
        }

        /// <summary>
        /// Check if is a command to a method with no parameter and a string return, and append to Sb if true
        /// </summary>
        /// <param name="index">current index of Param</param>
        /// <returns></returns>
        private bool IsSingle(int index)
        {
            string re = SingleCommands(Param[index]);
            if (re != string.Empty)
            {
                Append(re, true);
                return true;
            }
            return false;
        }

        /// <summary>
        /// check if the string is a number, if true append, and add the number to the allRes list
        /// </summary>
        /// <param name="index">current index of Param</param>
        private void IsaN(int index)
        {
            if (!double.IsNaN(double.Parse(Param[index])))
            {
                Append(Param[index], true);
                AddRes(Param[index]);
            }
        }

        /// <summary>
        /// Check if the string is an operation (two number and an operator in the middle), if true append and add the result to the allRes list
        /// </summary>
        /// <param name="index">current index of Param</param>
        /// <param name="indexO">current index of operations</param>
        /// <param name="op">operations list</param>
        private void IsOperation(int index, int indexO, Operations op)
        {
            if (Param[index].Contains(op[indexO]))
            {
                //get the index of the mathematical symbol
                int iop = Param[index].IndexOf(op[indexO]);

                //get the first number:
                string a = Param[index].Substring(0, iop);
                //get the second number:
                string b = Param[index].Substring(iop + 1, Param[index].Length - 1 - iop);
                //get the mathematical symbol
                string ope = Param[index].Substring(iop, 1);
                //return the mathematical result
                Append(Calculate(a, b, ope), true);
            }
        }

        #endregion
        

        /// <summary>
        /// Unique method to append text to StringBuilder Sb
        /// </summary>
        /// <param name="s">string to append</param>
        /// <param name="addNewLine">true: add new line, false: do not</param>
        private void Append(string s, bool addNewLine)
        {
            Sb.Append(s);
            if (addNewLine)
                Sb.Append("\n");
        }



        /// <summary>
        /// Append to the Sb the commands that need to be executed at last (like -sumall) 
        /// </summary>
        private void LastAppend()
        {
            if (sumAll || mulAll)
            {
                double all = 0;
                if (sumAll) all = 0;
                if (mulAll) all = 1;

                if (sumAll && mulAll)
                    Append("Warning: use only one -*all command at a time", true);

                for (int i = 0; i < allRes.Count; i++)
                {

                    string o = "";
                    if (sumAll)
                    {
                        all += allRes[i];
                        o = "+";
                    }
                    if (mulAll)
                    {
                        all *= allRes[i];
                        o = "*";
                    }
                    if (!onlyAllResult) //if sum all the result, append all the result to Sb without the new line
                    {
                        Append(allRes[i].ToString() + o, false);
                    }

                }
                if (onlyAllResult)
                    Append(all.ToString(), true);
                else
                {
                    Sb.Remove(Sb.Length - 1, 1); //remove the last + before append =
                    Append("=" + all, true);
                }
                onlyAllResult = false;
            }
        }

        /// <summary>
        /// Execute the void function, basically option to the execution program (like -sumall)
        /// </summary>
        /// <param name="cmd">string to be checked if is a void command</param>
        public void VoidCommands(string cmd)
        {
            Action action;
            if (dicVoid.TryGetValue(cmd, out action))
                action();
        }

        /// <summary>
        /// Execute the single function (single because there is only one string returned and zero parameters)
        /// </summary>
        /// <param name="cmd">string to be checked</param>
        /// <returns>output of the command</returns>
        public string SingleCommands(string cmd)
        {
            Func<string> funcSin;
            if (dicSin.TryGetValue(cmd, out funcSin))
                return funcSin();

            return string.Empty;
        }

        /// <summary>
        /// Execute an operation between two numbers, recognize the type of operation from the mathematical sign
        /// </summary>
        /// <param name="a">the first number</param>
        /// <param name="b">the second number</param>
        /// <param name="ope">the operator in the form of string (like: * , / , +)</param>
        /// <returns>return only the result (returns only 8 if the input was 6+2), or the complete operations (return 6+2=8 if the input was 6+2)</returns>
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
            AddRes(res);

            if (onlyResult)
                final = res;
            else
                final = a + ope + b + "=" + res;
            onlyResult = false;
            return final;
        }

        private void AddRes(string s)
        {
            if (sumAll || mulAll)
                allRes.Add(double.Parse(s));
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

        private string Pow(string a, string b)
        {
            return Math.Pow(double.Parse(a), double.Parse(b)).ToString();
        }

        #endregion
    }
}
