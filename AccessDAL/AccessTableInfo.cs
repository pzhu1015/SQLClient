using System;

using Helper;
using SQLDAL;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Collections.Generic;

namespace AccessDAL
{
    public class AccessTableInfo : TableInfo
    {
        public override bool Design(out DataTable table)
        {
            throw new NotImplementedException();
        }

        public override bool Open(long start, long pageSize, out DataTable datatable, out string statement)
        {
            throw new NotImplementedException();
        }
    }
}
