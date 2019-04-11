using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDAL
{
    public interface IViewInfo
    {
        void Close();
        bool Open(Int64 start, Int64 pageSize, out DataTable datatable, out string statement);
        void Design();
    }
}
