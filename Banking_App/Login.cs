using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_App
{
    class Login
    {
        //Fields
        private string userName, password, loginPath;
        private string[] loginInfo;

        public Login()
        {
            loginPath = Path.GetFullPath("../../Database/Login/login.txt");
            loginInfo = System.IO.File.ReadAllLines(@loginPath);
        }

        public void LoginScreen()
        {
            Console.Clear();
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.WriteLine("\t\t  |    WELCOME TO SIMPLE BANKING SYSTEM\t  |");
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.Write("\t\t  |\tEnter Username: ");

            int cursorPosUserNameLeft = Console.CursorLeft;
            int cursorPosUserNameTop = Console.CursorTop;
            Console.Write("\t\t  |");

            Console.Write("\n\t\t  |\tEnter Password: ");

            int cursorPosPwdLeft = Console.CursorLeft;
            int cursorPosPwdTop = Console.CursorTop;
            Console.Write("\t\t  |");

            Console.WriteLine("\n\t\t  -----------------------------------------");

            Console.SetCursorPosition(cursorPosUserNameLeft, cursorPosUserNameTop);
            userName = Console.ReadLine();

            Console.SetCursorPosition(cursorPosPwdLeft, cursorPosPwdTop);

            //Converting password characters into '*'
            string passChar = "*";
            ConsoleKeyInfo keyInfo;
            StringBuilder sb = new StringBuilder();

            do
            {
                keyInfo = Console.ReadKey(true);
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    sb.Append(keyInfo.KeyChar);
                    password = sb.ToString();
                    Console.Write(passChar);
                }else if(keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    sb.Remove(password.Length - 1, 1);
                    password = sb.ToString();
                    Console.Write("\b \b");
                }else if(keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }
            } while (true);

            //Checking the credentials of the userName and password input.
            if(userName == loginInfo[0] && password == loginInfo[1])
            {
                Console.WriteLine("\n\n\t\t\t     Valid Credentials!");
                Console.Write("\t\t\t Press 'enter' to continue.");
                Console.ReadKey();

                Menus mm = new Menus();
                mm.MenuScreen();
            }
            else
            {
                Console.WriteLine("\n\n\t\t\t     Incorrent Credentials!");
                Console.Write("\t\t\t     Press 'enter' to retry.");
                Console.ReadKey();
                LoginScreen();
            }
        }
    }
}
