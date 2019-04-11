using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDAL
{
    public interface ITableInfo
    {
        void Close();
        void CloseDesign();
        bool Open(Int64 start, Int64 pageSize, out DataTable datatable, out string statement);
        bool Design(out DataTable table);
    }
}
