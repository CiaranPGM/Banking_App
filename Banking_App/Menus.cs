using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Banking_App
{
    class Menus
    {
        //Fields
        string userInput, firstName, lastName, address, email;
        int userNumChoice, phoneNum;

        public void MenuScreen()
        {
            Console.Clear();
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.WriteLine("\t\t  |    Welcome to My Banking System\t  |");
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.WriteLine("\t\t  |\t1. Create a new account \t  |");
            Console.WriteLine("\t\t  |\t2. Search for an account \t  |");
            Console.WriteLine("\t\t  |\t3. Deposit \t\t\t  |");
            Console.WriteLine("\t\t  |\t4. Withdraw \t\t\t  |");
            Console.WriteLine("\t\t  |\t5. A/C statement \t\t  |");
            Console.WriteLine("\t\t  |\t6. Delete account \t\t  |");
            Console.WriteLine("\t\t  |\t7. Exit \t\t\t  |");
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.Write("\t\t  |    Enter your Choice (1 - 7): ");

            int cursorPosMenuLeft = Console.CursorLeft;
            int cursorPosMenuTop = Console.CursorTop;
            Console.Write("\t  |");

            Console.WriteLine("\n\t\t  -----------------------------------------");

            Console.SetCursorPosition(cursorPosMenuLeft, cursorPosMenuTop);
            userInput = Console.ReadLine();

            if (int.TryParse(userInput, out userNumChoice))
            {
                switch (Convert.ToInt32(userNumChoice))
                {
                    case 1:
                        CreateAccount();
                        break;
                    case 2:
                        SearchAccount();
                        break;
                    case 3:
                        DepositMoney();
                        break;
                    case 4:
                        WithdrawMoney();
                        break;
                    case 5:
                        DisplayStatement();
                        break;
                    case 6:
                        DeleteAccount();
                        break;
                    case 7:
                        Exit();
                        break;
                    default:
                        Console.WriteLine("\n\t  Incorrect Input...Please enter a number between 1 - 7.");
                        Console.Write("\t\t\t  Press 'enter' to retry");
                        Console.ReadKey();

                        /* I am concerend about the two lines below this comment. Is it dangerous?
                        *  I fear this will give me an error down the track.
                        *  Does it make sense to have a recursive method call right before "break"?
                        *  Is it possible that we may get stuck in this switch statement?
                        */
                        MenuScreen();
                        break;
                }
            }else
            {
                Console.WriteLine("\n\t  Incorrect Input...Please enter a number between 1 - 7.");
                Console.Write("\t\t\t  Press 'enter' to retry");
                Console.ReadKey();
                MenuScreen();
            }
        }

        private void CreateAccount()
        {
            Console.Clear();
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.WriteLine("\t\t  |    \t    CREATE A NEW ACCOUNT\t  |");
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.WriteLine("\t\t  |    \t      ENTER THE DETAILS \t  |");
            Console.Write("\t\t  |  \t\t\t\t\t  |");
            Console.WriteLine("\n\t\t  |  First Name: \t\t\t  |");


            Console.WriteLine("\t\t  |  Last Name: \t\t\t  |");


            Console.WriteLine("\t\t  |  Address: \t\t\t\t  |");


            Console.WriteLine("\t\t  |  Phone: \t\t\t\t  |");


            Console.WriteLine("\t\t  |  Email: \t\t\t\t  |");


            Console.WriteLine("\t\t  -----------------------------------------\n\n");


            firstName = Console.ReadLine();
            lastName = Console.ReadLine();
            address = Console.ReadLine();
            string phoneStringInput = Console.ReadLine();
            if(phoneStringInput.Length == 10 && int.TryParse(phoneStringInput, out phoneNum))
            {
                phoneNum = Convert.ToInt32(phoneStringInput);
            }
            else
            {
                Console.WriteLine("\n\n\n\n\t\t  Phone number incorrect.");
                Console.WriteLine("\t\t  Please use numeric characters at a length no more than 10");
                Console.Write("\t\t\t Press 'enter' to retry.");
                Console.ReadKey();
                CreateAccount();
            }
            //STILL NEED TO DO A FIELD CHECK FOR EMAIL------------------------------
            email = Console.ReadLine();
            /* NEED TO LOOK INTO FILE OUTPUT FOR THE ACCOUNT INFO
             * POSSIBLY USE ANOTHER CLASS 'ACCOUNT.CLASS' TO HANDLE ACCOUNT CREATION
             */
        }

        private void SearchAccount()
        {

        }

        private void DepositMoney()
        {
            throw new NotImplementedException();
        }

        private void WithdrawMoney()
        {
            throw new NotImplementedException();
        }

        private void DisplayStatement()
        {
            throw new NotImplementedException();
        }

        private void DeleteAccount()
        {
            throw new NotImplementedException();
        }

        private void Exit()
        {
            throw new NotImplementedException();
        }
    }
}
