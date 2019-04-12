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
        void Create(string name);
        void Drop(string name);
        DatabaseInfo AddDataBaseInfo(string name);
        Form GetConnectForm();
        //Image CloseImage();
        //Image OpenImage();
        DbConnection GetConnection(string database);
        DatabaseInfo GetDatabaseInfo();

        Image CloseImage { get; }
        Image OpenImage { get; }
    }
}
