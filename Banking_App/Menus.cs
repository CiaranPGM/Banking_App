﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace Banking_App
{
    class Menus
    {
        //Fields
        string userInput, firstName, lastName, address, email;
        int userNumChoice, phoneNum, accNumber;

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
                        Console.WriteLine("\n\n");
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
            if(email.Contains('@') && (email.Contains("gmail.com") || email.Contains("outlook.com") || email.Contains("uts.edu.au") || email.Contains("hotmail.com")))
            {
                Console.Write("\n\n\n\t\t   Is this information correct (y/n)?");
                string userInput = Console.ReadLine();
                if(userInput == "y")
                {
                    Account newAcc = new Account(firstName, lastName, address, phoneNum, email);
                    newAcc.CreateAccount();
                    Console.WriteLine("\n\t   Account Created! details will be provided via email");
                    Console.Write("\t\t   Account number is: " + newAcc.getAccountNumber());
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
            string input = Console.ReadLine();
            if (int.TryParse(input, out accNumber))
            {
                Account acc = new Account();
                int i = acc.SearchAccount(accNumber);
                if (i == 1)
                {
                    SearchAccount();
                }
                else
                {
                    MenuScreen();
                }
            }
            else
            {
                Console.WriteLine("\n\t Incorrect input...Please enter a 6 digit account number. E.g '100000'");
                Console.Write("\t\t Check another account (y/n)?");
                string userInput = Console.ReadLine();
                if (userInput == "y")
                    SearchAccount();
                else
                    MenuScreen();
            }
        }

        private void DepositMoney()
        {
            Console.Clear();
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.WriteLine("\t\t  |    \t         DEPOSIT     \t\t  |");
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.WriteLine("\t\t  |    \t      ENTER THE DETAILS \t  |");
            Console.Write("\t\t  |  \t\t\t\t\t  |");

            Console.Write("\n\t\t  |    Account Number: ");
            int cursorPosAccNumLeft = Console.CursorLeft;
            int cursorPosAccNumTop = Console.CursorTop;
            Console.WriteLine("\t\t\t  |");

            Console.WriteLine("\t\t  |  \t\t\t\t\t  |");

            Console.Write("\t\t  |    Amount: $");
            int cursorPosAmountLeft = Console.CursorLeft;
            int cursorPosAmountTop = Console.CursorTop;
            Console.WriteLine("\t\t\t  |");

            Console.WriteLine("\t\t  -----------------------------------------");

            Console.SetCursorPosition(cursorPosAccNumLeft, cursorPosAccNumTop);
            string accNumInput = Console.ReadLine();
            int accNum;

            if (accNumInput.Length < 10 && accNumInput.ToString().Length > 5 && (int.TryParse(accNumInput, out accNum)))
            {
                Account acc = new Account();
                int value = acc.CheckAccount(accNum);
                if (value == 0)
                {
                    Console.Write("\n\n\n\t\t   Account found! Enter the amount...");
                    Console.SetCursorPosition(cursorPosAmountLeft, cursorPosAmountTop);
                    string amountInput = Console.ReadLine();
                    int amount;
                    if (int.TryParse(amountInput, out amount)) {
                        acc.Deposit(amount, accNum);
                        Console.Write("\n\n\n\t\t   Success! Press enter.");
                        Console.ReadKey();
                        MenuScreen();
                    }
                }
                else
                {
                    DepositMoney();
                }
            }
            else
            {
                Console.WriteLine("\n\n\n\t\t   Account not found! Account must be atleast 6 digits.");
                Console.Write("\t\t   Retry (y/n)?");
                string input = Console.ReadLine();
                if (input == "y")
                    DepositMoney();
                else
                    MenuScreen();
            }
        }

        private void WithdrawMoney()
        {
            Console.Clear();
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.WriteLine("\t\t  |    \t         WITHDRAW     \t\t  |");
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.WriteLine("\t\t  |    \t      ENTER THE DETAILS \t  |");
            Console.Write("\t\t  |  \t\t\t\t\t  |");

            Console.Write("\n\t\t  |    Account Number: ");
            int cursorPosAccNumLeft = Console.CursorLeft;
            int cursorPosAccNumTop = Console.CursorTop;
            Console.WriteLine("\t\t\t  |");

            Console.WriteLine("\t\t  |  \t\t\t\t\t  |");

            Console.Write("\t\t  |    Amount: $");
            int cursorPosAmountLeft = Console.CursorLeft;
            int cursorPosAmountTop = Console.CursorTop;
            Console.WriteLine("\t\t\t  |");

            Console.WriteLine("\t\t  -----------------------------------------");

            Console.SetCursorPosition(cursorPosAccNumLeft, cursorPosAccNumTop);
            string accNumInput = Console.ReadLine();
            int accNum;

            if (accNumInput.Length < 10 && accNumInput.ToString().Length > 5 && (int.TryParse(accNumInput, out accNum)))
            {
                Account acc = new Account();
                int value = acc.CheckAccount(accNum);
                if (value == 0)
                {
                    Console.Write("\n\n\n\t\t   Account found! Enter the amount...");
                    Console.SetCursorPosition(cursorPosAmountLeft, cursorPosAmountTop);
                    string amountInput = Console.ReadLine();
                    int amount;
                    if (int.TryParse(amountInput, out amount))
                    {
                        int outcome = acc.Withdraw(amount, accNum);
                        if(outcome == 0)
                        { 
                            Console.Write("\n\n\n\t\t   Success! Press enter.");
                            Console.ReadKey();
                            MenuScreen();
                        }
                        else
                        {
                            Console.Write("\n\n\n\t\t   The amount you entered was too high. Press 'enter'.");
                            Console.ReadKey();
                            WithdrawMoney();
                        }
                    }
                }
                else
                {
                    WithdrawMoney();
                }
            }
            else
            {
                Console.WriteLine("\n\n\n\t\t   Account not found! Account must be atleast 6 digits.");
                Console.Write("\t\t   Retry (y/n)?");
                string input = Console.ReadLine();
                if (input == "y")
                    WithdrawMoney();
                else
                    MenuScreen();
            }
        }

        private void DisplayStatement()
        {
            Console.Clear();
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.WriteLine("\t\t  |    \t      STATEMENT\t\t\t  |");
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.WriteLine("\t\t  |    \t      ENTER THE DETAILS \t  |");
            Console.Write("\t\t  |  \t\t\t\t\t  |");

            Console.Write("\n\t\t  |    Account Number: ");
            int cursorPosAccNumLeft = Console.CursorLeft;
            int cursorPosAccNumTop = Console.CursorTop;
            Console.WriteLine("\t\t\t  |");

            Console.WriteLine("\t\t  -----------------------------------------");

            Console.SetCursorPosition(cursorPosAccNumLeft, cursorPosAccNumTop);
            string accNumInput = Console.ReadLine();
            int accNum;

            if (accNumInput.Length < 10 && accNumInput.ToString().Length > 5 && (int.TryParse(accNumInput, out accNum)))
            {
                Account acc = new Account();
                int value = acc.CheckAccount(accNum);
                if (value == 0)
                {
                    Console.WriteLine("\n\n\t\t   Account found! Statement is displayed below...");
                    acc.GenerateStatement(accNum);
                    MenuScreen();

                }
                else
                {
                    Console.WriteLine("\n\t Incorrect input...Please enter a 6 digit account number. E.g '100000'");
                    Console.Write("\t\t Check another account (y/n)?");
                    string userInput = Console.ReadLine();
                    if (userInput == "y")
                        DisplayStatement();
                    else
                        MenuScreen();
                }
            }
            else
            {
                Console.WriteLine("\n\n\n\t\t   Account not found! Account must be atleast 6 digits.");
                Console.Write("\t\t   Retry (y/n)?");
                string input = Console.ReadLine();
                if (input == "y")
                    DisplayStatement();
                else
                    MenuScreen();
            }
        }

        private void DeleteAccount()
        {
            Console.Clear();
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.WriteLine("\t\t  |    \t      DELETE AN ACCOUNT\t\t  |");
            Console.WriteLine("\t\t  -----------------------------------------");
            Console.WriteLine("\t\t  |    \t      ENTER THE DETAILS \t  |");
            Console.Write("\t\t  |  \t\t\t\t\t  |");

            Console.Write("\n\t\t  |    Account Number: ");
            int cursorPosAccNumLeft = Console.CursorLeft;
            int cursorPosAccNumTop = Console.CursorTop;
            Console.WriteLine("\t\t\t  |");

            Console.WriteLine("\t\t  -----------------------------------------");

            Console.SetCursorPosition(cursorPosAccNumLeft, cursorPosAccNumTop);
            string accNumInput = Console.ReadLine();
            int accNum;

            if (accNumInput.Length < 10 && accNumInput.ToString().Length > 5 && (int.TryParse(accNumInput, out accNum)))
            {
                Account acc = new Account();
                int value = acc.CheckAccount(accNum);
                if (value == 0)
                {
                    Console.WriteLine("\n\n\t\t   Account found! Details displayed below...");
                    acc.SearchAccountDetails(accNum);
                    MenuScreen();
                }
                else { DeleteAccount(); }
            }
            else
            {
                Console.WriteLine("\n\n\n\t\t   Account not found! Account must be atleast 6 digits.");
                Console.Write("\t\t   Retry (y/n)?");
                string input = Console.ReadLine();
                if (input == "y")
                    DeleteAccount();
                else
                    MenuScreen();
            }
        }

        private void Exit()
        {
            Environment.Exit(0);
        }
    }
}
