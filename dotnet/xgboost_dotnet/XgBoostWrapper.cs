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
        public static extern int XGDMatrixFree(IntPtr handle);

        [DllImport(DLL_NAME)]
        public static extern IntPtr XGDMatrixCreateFromFile(string fname, int silent);

        //output = 0 when success, -1 when failure happens, return number of rows..
        [DllImport(DLL_NAME)]
        static extern int XGDMatrixNumRow(IntPtr handle, ref ulong output);

        [DllImport(DLL_NAME)]
        static extern IntPtr XGDMatrixCreateFromMat(float[] data, ulong nrow, ulong ncol, float missing);

        public static int GetRowCount(IntPtr handle)
        {
            var error = 0ul;
            var res = XgBoostWrapper.XGDMatrixNumRow(handle, ref error);
            return res;
        }

        public static IntPtr CreateFromMatrix(float[] data, int rowCount, int colCount, float missing = float.MinValue)
        {
            var res = XgBoostWrapper.XGDMatrixCreateFromMat(data, (ulong)rowCount, (ulong)colCount, missing);
            return res;
        }

    }
}
