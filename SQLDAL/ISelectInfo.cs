using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLDAL
{
    public interface ISelectInfo
    {
        string Open();
        void Close();
    }
}
