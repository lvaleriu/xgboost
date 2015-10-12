using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xgboost_dotnet;

namespace xgboost_dotnet_test
{
    public class TestsLowlevel
    {
        public static void LoadMatrixFromTxt()
        {
            var checks = new Checks("Load matric from txt");
            var handle = XgBoostWrapper.CreateDMatrixFromFile("agaricus.txt.train", silent: 0);
            var rowCount = XgBoostWrapper.GetDMatrixRowCount(handle);
            XgBoostWrapper.FreeDMatrix(handle);
            checks.IsEqual(6513, rowCount, "Rowcount check");
            checks.PrintReport();
        }

        public static void LoadMatrixFromDense()
        {
            var checks = new Checks("Load matric from dense");
            var mat = new DenseMatrix(20, 10);
            mat[1, 5] = 1; mat[1, 6] = 2;
            mat[2, 3] = 3; mat[3, 8] = 4;
            mat[4, 1] = 5; mat[4, 7] = 6;
            var handle = XgBoostWrapper.CreateDMatrixFromDenseMatrix(mat.Data, mat.RowCount, mat.ColCount);
            checks.IsEqual(20, XgBoostWrapper.GetDMatrixRowCount(handle), "Rowcount check");
            XgBoostWrapper.FreeDMatrix(handle);
            checks.PrintReport();
        }

        public static void TestBooster()
        {
            var checks = new Checks("Test booster");
            var trainHandle = XgBoostWrapper.CreateDMatrixFromFile("agaricus.txt.train");
            var testHandle = XgBoostWrapper.CreateDMatrixFromFile("agaricus.txt.test");
            var boosterHandle = XgBoostWrapper.CreateBooster(new IntPtr[] { trainHandle, testHandle });
            XgBoostWrapper.BoosterUpdateOneIter(boosterHandle, 1, trainHandle);
            var res1 = XgBoostWrapper.BoosterEvalOneIter(boosterHandle, 1, new IntPtr[] { trainHandle }, new string[] { "train" });
            var rmse1 = double.Parse(res1.Split(':')[1], CultureInfo.InvariantCulture);

            XgBoostWrapper.BoosterUpdateOneIter(boosterHandle, 2, trainHandle);
            var res2 = XgBoostWrapper.BoosterEvalOneIter(boosterHandle, 2, new IntPtr[] { trainHandle }, new string[] { "train" });
            var rmse2 = double.Parse(res2.Split(':')[1], CultureInfo.InvariantCulture);
            checks.IsTrue(rmse1>rmse2, "Train error should go dowm");

            XgBoostWrapper.FreeDMatrix(trainHandle);
            XgBoostWrapper.FreeDMatrix(testHandle);
           // XgBoostWrapper.FreeBooster(boosterHandle);
            checks.PrintReport();
        }
    
    }
}
