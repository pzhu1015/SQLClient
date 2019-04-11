using Helper;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SQLDAL
{
    public class SelectInfo : ISelectInfo
    {
        protected string name;
        protected string message;
        protected bool isOpen = false;
        protected DatabaseInfo databaseInfo;
        protected TreeNode node;
        protected ListViewItem item;
        protected object openSelectPage;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public DatabaseInfo DatabaseInfo
        {
            get
            {
                return databaseInfo;
            }

            set
            {
                databaseInfo = value;
            }
        }

        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }

        public bool IsOpen
        {
            get
            {
                return isOpen;
            }

            set
            {
                isOpen = value;
            }
        }

        public TreeNode Node
        {
            get
            {
                return node;
            }

            set
            {
                node = value;
            }
        }

        public ListViewItem Item
        {
            get
            {
                return item;
            }

            set
            {
                item = value;
            }
        }

        public object OpenSelectPage
        {
            get
            {
                return openSelectPage;
            }

            set
            {
                openSelectPage = value;
            }
        }

        public void Close()
        {
            this.isOpen = false;
        }

        public string Open()
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DataSet ds = new DataSet();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = $"SELECT CONTENTS FROM TB_SELECT WHERE NAME='{this.name}' AND CONNECT='{this.databaseInfo.ConnectInfo.Name}' AND DATABASE='{this.databaseInfo.Name}';";
                    DbDataAdapter da = new SQLiteDataAdapter(command as SQLiteCommand);
                    da.Fill(ds);
                    this.isOpen = true;
                    return ds.Tables[0].Rows[0][0].ToString();
                }
            }
            catch (Exception ex)
            {
                this.message = ex.Message;
                LogHelper.Error(ex);
                return "";
            }
        }

        public bool Update(string contents)
        {
            try
            {
                using (DbConnection connection = new SQLiteConnection(ConnectInfo.LocalConnectionString))
                {
                    connection.Open();
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = $"UPDATE TB_SELECT SET CONTENTS='{contents}' WHERE NAME='{this.name}' AND CONNECT='{this.databaseInfo.ConnectInfo.Name}' AND DATABASE='{this.databaseInfo.Name}';";
                    command.ExecuteNonQuery();
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


    }
}
