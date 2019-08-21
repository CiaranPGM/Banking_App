using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking_App
{
    class Login
    {
        //Fields
        private string userName, password;

        public void LoginScreen()
        {
            Console.Clear();
            Console.WriteLine("\t\t-----------------------------------------");
            Console.WriteLine("\t\t|\tWelcome to Simple Banking System\t|");
            Console.WriteLine("\t\t-----------------------------------------");
            Console.Write("\t\t|\tEnter Username: ");

            int CursorPosUserNameLeft = Console.CursorLeft;
            int CursorPosUserNameTop = Console.CursorTop;
            Console.Write("\t\t|");

            Console.Write("\n\t\t|\tEnter Password: ");

            int CursorPosPwdLeft = Console.CursorLeft;
            int CursorPosPwdTop = Console.CursorTop;
            Console.Write("\t\t|");

            Console.WriteLine("\n\t\t-----------------------------------------");

            Console.SetCursorPosition(CursorPosUserNameLeft, CursorPosUserNameTop);
            userName = Console.ReadLine();

            Console.SetCursorPosition(CursorPosPwdLeft, CursorPosPwdTop);
            //password = Console.ReadLine();

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

            //THIS NEEDS TO BE CHECKED WITH THE LOGIN.TXT FILE INSTEAD OF THIS----------------------------
            if(userName == "Ciaran" && password == "uts_123")
            {
                Console.Write("\n\n\tValid Credentials! Press 'enter' to continue.");
                Console.ReadKey();
            }
            else
            {
                Console.Write("\n\n\tIncorrent Credentials! Press 'enter' to retry.");
                Console.ReadKey();
                LoginScreen();
            }
            //REMOVE THIS BLOCK OF CODE AFTER FIGURING OUT THE CHECK WITH LOGIN.TXT-----------------------
        }
    }
}
