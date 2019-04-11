using System;

using SQLDAL;
using System.Data;
using System.Data.Common;
using Helper;
using System.Data.OleDb;

namespace AccessDAL
{
    public class AccessViewInfo : ViewInfo
    {
        public override void Close()
        {
            this.isOpen = false;
        }

        public override void Design()
        {
        }

        public override bool Open(Int64 start, Int64 pageSize, out DataTable datatable, out string statement)
        {
            throw new NotImplementedException();
        }
    }
}
