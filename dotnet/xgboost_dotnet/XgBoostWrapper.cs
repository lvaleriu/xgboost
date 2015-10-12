using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace xgboost_dotnet
{
    public static class XgBoostWrapper
    {
        public const string DLL_NAME = "xgboost_wrapper.dll";

        //return 0 when success, -1 when failure happens
        [DllImport(DLL_NAME)]
        static extern int XGDMatrixFree(IntPtr handle);

        [DllImport(DLL_NAME)]
        static extern IntPtr XGDMatrixCreateFromFile(string fname, int silent);

        //output = 0 when success, -1 when failure happens, return number of rows..
        [DllImport(DLL_NAME)]
        static extern int XGDMatrixNumRow(IntPtr handle, ref ulong output);

        [DllImport(DLL_NAME)]
        static extern IntPtr XGDMatrixCreateFromMat(float[] data, ulong nrow, ulong ncol, float missing);

        [DllImport(DLL_NAME)]
        static extern IntPtr XGBoosterCreate(IntPtr[] dmats, ulong len);

        [DllImport(DLL_NAME)]
        static extern void XGBoosterFree(IntPtr handle);

        [DllImport(DLL_NAME)]
        static extern void XGBoosterSetParam(IntPtr handle, string name, string value);

        [DllImport(DLL_NAME)]
        static extern void XGBoosterUpdateOneIter(IntPtr handle, int iter, IntPtr dtrain);
        
        [DllImport(DLL_NAME)]
        static extern string XGBoosterEvalOneIter(IntPtr handle, int iter, IntPtr[] dmats, string[] evnames, ulong len);

        public static IntPtr CreateDMatrixFromFile(string path, int silent = 1)
        {
            var res = XGDMatrixCreateFromFile(path, silent);
            return res;
        }

        public static IntPtr CreateDMatrixFromDenseMatrix(float[] data, int rowCount, int colCount, float missing = float.MinValue)
        {
            var res = XgBoostWrapper.XGDMatrixCreateFromMat(data, (ulong)rowCount, (ulong)colCount, missing);
            return res;
        }

        public static int GetDMatrixRowCount(IntPtr handle)
        {
            var error = 0ul;
            var res = XgBoostWrapper.XGDMatrixNumRow(handle, ref error);
            return res;
        }
        
        public static void FreeDMatrix(IntPtr handle) 
        {
            XGDMatrixFree(handle);
        }

        public static IntPtr CreateBooster(IntPtr[] dmats)
        {
            var res = XGBoosterCreate(dmats, (ulong)dmats.Length);
            return res;
        }

        public static void FreeBooster(IntPtr handle)
        {
            XGBoosterFree(handle);
        }

        public static void BoosterUpdateOneIter(IntPtr handle, int iter, IntPtr dtrain)
        {
            XGBoosterUpdateOneIter(handle, iter, dtrain);
        }

        public static string BoosterEvalOneIter(IntPtr handle, int iter, IntPtr[] dmats, string[] evalNames)
        {
            if (dmats.Length != evalNames.Length) throw new ArgumentException();
            var res = XGBoosterEvalOneIter(handle, iter, dmats, evalNames, (ulong)dmats.Length);
            return res;
        }

        public static void BoosterSetParam(IntPtr handle, string name, string value)
        {
            XGBoosterSetParam(handle, name, value);
        }

    }
}
