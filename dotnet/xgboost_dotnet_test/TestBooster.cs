using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xgboost_dotnet;

namespace xgboost_dotnet_test
{
    public class TestBooster
    {
        public static void TestBoosterBasic()
        {
            var checks = new Checks("Test booster basic");
            var trainMatrix = new DMatrix("agaricus.txt.train");
            var testMatrix = new DMatrix("agaricus.txt.test");


        }
    }
}
