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
        int accNumber = 100000;

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

            Console.Write("\n\t\t  |  First Name: ");
            int cursorPosFirstNameLeft = Console.CursorLeft;
            int cursorPosFirstNameTop = Console.CursorTop;
            Console.Write("\t\t\t  |");

            Console.Write("\n\t\t  |  Last Name: ");
            int cursorPosLastNameLeft = Console.CursorLeft;
            int cursorPosLastNameTop = Console.CursorTop;
            Console.Write("\t\t\t  |");


            Console.Write("\n\t\t  |  Address: ");
            int cursorPosAddressLeft = Console.CursorLeft;
            int cursorPosAddressTop = Console.CursorTop;
            Console.Write("\t\t\t\t  |");

            Console.Write("\n\t\t  |  Phone: ");
            int cursorPosPhoneLeft = Console.CursorLeft;
            int cursorPosPhoneTop = Console.CursorTop;
            Console.Write("\t\t\t\t  |");


            Console.Write("\n\t\t  |  Email: ");
            int cursorPosEmailLeft = Console.CursorLeft;
            int cursorPosEmailTop = Console.CursorTop;
            Console.WriteLine("\t\t\t\t  |");


            Console.WriteLine("\t\t  -----------------------------------------\n\n");

            Console.SetCursorPosition(cursorPosFirstNameLeft, cursorPosFirstNameTop);
            firstName = Console.ReadLine();

            Console.SetCursorPosition(cursorPosLastNameLeft, cursorPosLastNameTop);
            lastName = Console.ReadLine();

            Console.SetCursorPosition(cursorPosAddressLeft, cursorPosAddressTop);
            address = Console.ReadLine();

            Console.SetCursorPosition(cursorPosPhoneLeft, cursorPosPhoneTop);
            string phoneStringInput = Console.ReadLine();
            if(phoneStringInput.Length == 10 && int.TryParse(phoneStringInput, out phoneNum))
            {
                phoneNum = Convert.ToInt32(phoneStringInput);
            }
            else
            {
                Console.WriteLine("\n\n\n\t\t\t  Phone number incorrect.");
                Console.WriteLine("\t   Please use numeric characters at a length no more than 10");
                Console.Write("\t\t\t Press 'enter' to retry.");
                Console.ReadKey();
                CreateAccount();
            }

            Console.SetCursorPosition(cursorPosEmailLeft, cursorPosEmailTop);
            email = Console.ReadLine();
            if(email.Contains('@') && (email.Contains("gmail.com") || email.Contains("outlook.com") || email.Contains("uts.edu.au")))
            {
                Console.Write("\n\n\n\t\t   Is this information correct (y/n)?");
                string userInput = Console.ReadLine();
                if(userInput == "y")
                {
                    accNumber += 1;
                    Account newAcc = new Account(firstName, lastName, address, phoneNum, email, accNumber);
                    newAcc.CreateAccount();
                    Console.WriteLine("\n\t   Account Created! details will be provided via email");
                    Console.Write("\t\t   Account number is: " + accNumber);
                    Console.Write("\n\t\t Press 'enter' to return to the menu");
                    Console.ReadKey();
                    MenuScreen();
                }else if (userInput == "n")
                {
                    CreateAccount();
                }
            }
            else
            {
                Console.WriteLine("\n\n\n\t\t\t  Email incorrect.");
                Console.WriteLine("\t  Please enter a vaild email with '@' and a domain. E.g. gmail.com.");
                Console.Write("\t\t\t Press 'enter' to retry.");
                Console.ReadKey();
                CreateAccount();
            }

            /* NEED TO LOOK INTO FILE OUTPUT FOR THE ACCOUNT INFO
             * POSSIBLY USE ANOTHER CLASS 'ACCOUNT.CLASS' TO HANDLE ACCOUNT CREATION
             */
        }

        private void SearchAccount()
        {
            Console.Clear();
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.WriteLine("\t\t  |    \t      SEARCH AN ACCOUNT\t\t  |");
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.WriteLine("\t\t  |    \t      ENTER THE DETAILS \t  |");
            Console.Write("\t\t  |  \t\t\t\t\t  |");

            Console.Write("\n\t\t  |    Account Number: ");
            int cursorPosAccNumLeft = Console.CursorLeft;
            int cursorPosAccNumTop = Console.CursorTop;
            Console.WriteLine("\t\t\t  |");

            Console.WriteLine("\t\t  -----------------------------------------");

            Console.SetCursorPosition(cursorPosAccNumLeft, cursorPosAccNumTop);
            int accNumInput = Convert.ToInt32(Console.ReadLine());

            Account acc = new Account();
            int i = acc.SearchAccount(accNumInput);
            if (i == 1)
            {
                SearchAccount();
            }
            else
            {
                MenuScreen();
            }
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
