using System;
using System.Collections.Generic;
using System.Data;

namespace SQLDAL
{

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

        bool Format(string sql, out string formatSql);
        bool Parse(string sql, out List<StatementObj> statements);
        bool ExecueNonQuery(string sql, out int count, out string error, out Int64 cost);

        bool ExecuteQuery(string sql, out DataTable table, out int count, out string error, out Int64 cost);
    }

    public enum SqlType
    {
        eTable,
        eMsg
    }


    public class StatementObj
    {
        public SqlType SqlType;
        public string SqlText;
    }
}
