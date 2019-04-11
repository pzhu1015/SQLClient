using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AccessDAL
{
    public partial class AccessConnectForm : Form
    {
        private string connectionString;
        public AccessConnectForm()
        {
            InitializeComponent();
        }

        public string ConnectionString
        {
            get
            {
                return connectionString;
            }

            set
            {
                connectionString = value;
            }
        }
    }
}
