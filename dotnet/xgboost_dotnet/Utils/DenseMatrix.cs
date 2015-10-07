using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xgboost_dotnet
{
    public class DenseMatrix
    {
        float[] _data;
        int _colCount;
        int _rowCount;

        public DenseMatrix(int rowCount, int colCount)
        {
            _colCount = colCount;
            _rowCount = rowCount;
            _data = new float[rowCount * colCount];
        }

        public float this[int row, int col] 
        {
            get
            {
                return _data[row * _colCount + col];
            }
            set
            {
                _data[row * _colCount] = value;
            }
        }

        public float[] Data
        {
            get { return _data; }
        }

        public int RowCount
        {
            get { return _rowCount; }
        }

        public int ColCount
        {
            get { return _colCount; }
        }
    }
}
