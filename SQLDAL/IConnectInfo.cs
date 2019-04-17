using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;

namespace SQLDAL
{
    public interface IConnectInfo 
    {
        bool Open();
        bool Close();
        bool Refresh();
        bool Create(string name);
        void Drop(string name);
        DatabaseInfo AddDataBaseInfo(string name);
        Form GetConnectForm();
        DbConnection GetConnection(string database);
        DatabaseInfo GetDatabaseInfo();

        Image CloseImage { get; }
        Image OpenImage { get; }
    }
}
