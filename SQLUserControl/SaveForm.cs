using System.Windows.Forms;

namespace SQLUserControl
{
    public partial class SaveForm : DevExpress.XtraEditors.XtraForm
    {
        private string saveName;

        public SaveForm()
        {
            InitializeComponent();
        }

        public string SaveName
        {
            get
            {
                return saveName;
            }

            set
            {
                saveName = value;
            }
        }

        private void SaveSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.saveName = this.txtName.Text;
        }
    }
}