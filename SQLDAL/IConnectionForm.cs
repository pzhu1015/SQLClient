using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLDAL
{
    public interface IConnectionForm
    {
        string User { get; set; }
        string ConnectionString { get; set; }
        string Password { get; set; }
        bool LoadConnectionInfo(ConnectInfo info);
    }
}
