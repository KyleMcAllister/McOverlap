
using McOverlapCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace McOverlap
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainUI());
            }
            catch(Exception ex)
            {
                MessageBox.Show("It happnened somwhere");
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
