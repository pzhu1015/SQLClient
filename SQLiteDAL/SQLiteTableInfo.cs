using System;

using Helper;
using SQLDAL;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Collections.Generic;

namespace SQLiteDAL
{
    public class SQLiteTableInfo : TableInfo
    {
        /// <summary>
        /// Table Headers: FIELDNAME, FIELDTYPE, FIELDLENGTH, FIELDSCALE, FIELDISNULL, FIELDDEFAULT, FIELDPRIMARYKE, FIELDCOMMENTS
        /// </summary>
        /// <returns></returns>
        public override bool Design(out DataTable table)
        {
            table = new DataTable("Design");
            table.Columns.Add("FIELDNAME");
            table.Columns.Add("FIELDTYPE");
            table.Columns.Add("FIELDLENGTH");
            table.Columns.Add("FIELDSCALE");
            table.Columns.Add("FIELDISNULL");
            table.Columns.Add("FIELDPRIMARYKEY");
            table.Columns.Add("FIELDDEFAULT");
            table.Columns.Add("FIELDCOMMENTS");
            string field_name = "", field_type = "", field_length = "", field_precision = "", field_isnull = "", field_primarykey = "";
            try
            {
                using (DbConnection connection = this.databaseInfo.ConnectInfo.GetConnection(this.databaseInfo.Name))
                {
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = string.Format(this.databaseInfo.ConnectInfo.DesignTable, this.name);
                    DbDataAdapter da = new SQLiteDataAdapter(command as SQLiteCommand);
                    da.Fill(ds);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        field_name = dr[1].ToString();
                        string str = dr[2].ToString();
                        int first_bracket = str.IndexOf("(");
                        int last_bracket = str.IndexOf(")");
                        if (first_bracket > 0 && last_bracket > 0)
                        {
                            field_type = str.Substring(0, first_bracket);
                            int comma = str.IndexOf(",");
                            if (comma > 0)
                            {
                                field_length = str.Substring(first_bracket + 1, comma - first_bracket - 1);
                                field_precision = str.Substring(comma + 1, last_bracket - comma - 1);
                            }
                            else
                            {
                                field_length = str.Substring(first_bracket + 1, last_bracket - first_bracket - 1);
                                field_precision = "0";
                            }
                        }
                        else
                        {
                            field_type = str;
                            field_length = "0";
                            field_precision = "0";
                        }
                        field_isnull = dr[3].ToString() == "0" ? "False" : "True";
                        field_primarykey = dr[5].ToString() != "0" ? "True" : "False";
                        object field_default = dr[4];
                        table.Rows.Add(new object[] { field_name, field_type, field_length, field_precision, field_isnull, field_primarykey, field_default });
                    }
                    this.isDesign = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return false;
            }
        }

        public override bool Open(Int64 start, Int64 pageSize, out DataTable datatable, out string statement)
        {
            try
            {
                using (DbConnection connection = this.databaseInfo.ConnectInfo.GetConnection(this.databaseInfo.Name))
                {
                    statement = string.Format(this.databaseInfo.ConnectInfo.OpenTable, this.name, pageSize, start);
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = statement;
                    DbDataAdapter da = new SQLiteDataAdapter(command as SQLiteCommand);
                    da.Fill(ds);
                    this.isOpen = true;
                    datatable = ds.Tables[0];
                    return true;
                }
            }
            catch(Exception ex)
            {
                datatable = null;
                statement = "";
                this.message = ex.Message;
                LogHelper.Error(ex);;
                return false;
            }
        }
    }
}
