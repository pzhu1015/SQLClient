using SQLParser;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDAL
{
    public enum SqlStatementType
    {
        eTable,
        eMsg
    }

    public interface IDatabaseInfo
    {
        
        bool Open();
        bool Close();
        void Refresh();

        void RefreshTable();
        void RefreshView();
        void RefreshSelect();

        SelectInfo CreateSelect(string name, string script);
        TableInfo CreateTable(string name, string script);
        ViewInfo CreateView(string name, string script);

        void AlterSelect(string name, string script);
        void AlterTable(string name, string script);
        void AlterView(string name, string script);

        void DropTable(string name);
        void DropView(string name);
        void DropSelect(string name);

        TableInfo AddTableInfo(string name);
        ViewInfo AddViewInfo(string name);
        SelectInfo AddSelectInfo(string name);

        DataTable LoadTable();
        DataTable LoadView();
        DataTable LoadSelect();

        TableInfo GetTableInfo();
        ViewInfo GetViewInfo();

        SqlStatementType GetStatementType(string statement, string first_word);

        bool ExecueNonQuery(string sql, out int count, out string error, out Int64 cost);

        bool ExecuteQuery(string sql, out DataTable table, out int count, out string error, out Int64 cost);
    }
}
