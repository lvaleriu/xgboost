using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xgboost_dotnet
{
    public class DMatrix
    {
        public IntPtr Handle;
        public DMatrix(string srcPath, int silent = 1)
        {
            XgBoostWrapper.CreateDMatrixFromFile(srcPath, silent);
        }
    }
}
