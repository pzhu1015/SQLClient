using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;

namespace SQLDAL
{
    public interface IConnectInfo 
    {
        #region 属性
        Image CloseImage { get; }
        Image OpenImage { get; }
        Form ConnectForm { get; }
        string DriverName { get; }
        string DefaultPort { get; }
        string DesignTableScript { get; }
        string OpenTableScript { get; }
        string OpenViewScript { get; }
        string LoadTableScript { get; }
        string LoadViewScript { get; }
        string[] DataTypes { get; }
        #endregion

        #region 数据库存操作
        bool Open();
        bool Create(string name);
        void Drop(string name);
        #endregion

        #region 查询语句格式化解析
        bool Format(string sql, out string formatSql);
        bool Parse(string sql, out List<StatementObj> statements);
        #endregion

        #region 获取驱动器组件
        DbConnection GetConnection(string database);
        DbDataAdapter GetDataAdapter(DbCommand command);
        #endregion

        #region 组装脚本模版
        string GetLoadTableScript(string database);
        string GetLoadViewScript(string database);
        #endregion

        bool DesignTable(string database, string tablename, out DataTable table);
        bool OpenTable(string database, string tablename, long start, long pageSize, out DataTable datatable, out string statement);
        bool DesignView(string database, string viewname, out DataTable table);
        bool OpenView(string dataase, string viewname, long start, long pageSize, out DataTable datatable, out string statement);
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
