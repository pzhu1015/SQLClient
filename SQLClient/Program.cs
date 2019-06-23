using System;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using Helper;
using SQLDAL;

namespace SQLClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            LogHelper.Initialize($@"{Application.StartupPath}\log4net.config");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 0)
            {
                Application.Run(new LoginForm());
            }
            else
            {
                string userId = "";
                string passWd = "";
                int i = 0;
                while (i < args.Length)
                {
                    if (args[i] == "-user")
                    {
                        userId = args[i + 1];
                        i++;
                    }
                    else if (args[i] == "-password")
                    {
                        passWd = args[i + 1];
                        i++;
                    }
                    else
                    {
                        Application.Exit();
                    }
                    i++;
                }
                string error;
                bool rslt = ConnectInfo.Login(userId, passWd, out error);
                if (rslt)
                {
                    BonusSkins.Register();
                    SkinManager.EnableFormSkins();
                    UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
                    SqlClientForm form = new SqlClientForm();
                    form.LoginUser = userId;
                    form.Password = passWd;
                    Application.Run(form);
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}
