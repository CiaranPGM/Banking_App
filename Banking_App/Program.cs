using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Login log = new Login();
            log.LoginScreen();

            MainMenu mm = new MainMenu();
            mm.MenuScreen();
        }
    }
}
