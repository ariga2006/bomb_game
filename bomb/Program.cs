using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bomb
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

<<<<<<< HEAD
            Console.WriteLine("テスト2");
=======
            Console.WriteLine("テスト");
>>>>>>> 6eb5fd9 (Revert "ぼむ")
        }
    }
   
}
