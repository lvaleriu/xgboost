using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xgboost_dotnet;

namespace xgboost_dotnet_test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestsLowlevel.LoadMatrixFromTxt();
            TestsLowlevel.LoadMatrixFromDense();
            TestsLowlevel.TestBooster();
            Console.Write("Press any key");
            Console.ReadKey();
        }
    }
}
