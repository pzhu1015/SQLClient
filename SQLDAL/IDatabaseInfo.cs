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

        bool LoadTable(bool isFirst);
        bool LoadView(bool isFirst);
        bool LoadSelect(bool isFirst);
        
        void CloseTable();
        void CloseDesignTable();
        bool OpenTable(Int64 start, Int64 pageSize, out DataTable datatable, out string statement);
        bool DesignTable(out DataTable table);
    }
}
