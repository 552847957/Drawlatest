using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LJJSCAD
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm mf = new MainForm();

            //SuiZuanForm szf = new SuiZuanForm();
            
           Application.Run(mf);
            //Application.Run(szf);
            //Application.Run(new tesFORMMOBAN());
        }
    }
}