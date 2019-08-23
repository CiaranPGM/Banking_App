using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Banking_App
{
    class Account
    {
        //Fields
        string firstName, lastName, address, email, accountsPath, txtPath, transPath;
        int phoneNum, accNumber, balance;


        public Account()
        {
            accountsPath = Path.GetFullPath("../../Database/Accounts");
            transPath = Path.GetFullPath("../../Database/Transactions");
        }

        public Account(string firstName, string lastName, string address, int phoneNum, string email, int accNumber)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.phoneNum = phoneNum;
            this.email = email;
            this.accNumber = accNumber;
            this.balance = 1000;
            accountsPath = Path.GetFullPath("../../Database/Accounts");
            transPath = Path.GetFullPath("../../Database/Transactions");
            txtPath = Path.GetFullPath(accountsPath + "/" + accNumber + ".txt");
        }

        //This method creates the .txt file for the account, holding all the information.
        //This also creates a directory in /Transactions with the accNumber as the title, this will hold all the transaction history files.
        public void CreateAccount()
        {
            //THIS CODE WILL BE USED TO CHECK IF THE ACCOUNT ALREADY EXISTS, AND IF IT DOES INCREMENT THE ACCOUNT NUMBER BY 1
            //foreach (var file in Directory.EnumerateFiles(@accountsPath, "*.txt"))
            //{
            //    if (file)
            //}

            string[] lines = { firstName, lastName, balance.ToString(), address, phoneNum.ToString(), email};
            System.IO.File.WriteAllLines(txtPath, lines);

            DirectoryInfo di = Directory.CreateDirectory(@transPath + "/" + accNumber);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            //if (Directory.Exists(transPath + "/" + accNumber))
            //{
            //    di.Delete();
            //    Directory.CreateDirectory(@transPath + "/" + accNumber);
            //}

        }

        public int SearchAccount(int accountNum)
        {
            if (accountNum.ToString().Length < 10 && accountNum.ToString().Length > 5 && (int.TryParse(accountNum.ToString(), out accountNum)))
            {
                string[] files = Directory.GetFiles(accountsPath);
                for (int it = 0; it < files.Length; it++)
                {
                    if (files[it].Contains(accountNum.ToString()))
                    {
                        string[] file = File.ReadAllLines(accountsPath + "/" + accountNum + ".txt");
                        Console.WriteLine("\t\tAccount found!");
                        Console.WriteLine("\t\t  -----------------------------------------");
                        Console.WriteLine("\t\t  |    \t    ACCOUNT DETAILS\t  |");
                        Console.WriteLine("\t\t  -----------------------------------------");
                        Console.WriteLine("\t\t  |  \t\t\t\t\t  |");
                        Console.Write("\n\t\t  |  Account no: " + accountNum);


                        Console.WriteLine("\t\t  -----------------------------------------\n\n");

                        Console.ReadKey();
                        return 0;
                    }
                }
                Console.WriteLine("\t\tAccount could not be found.");
                Console.Write("\t\t  Check another account? (y/n)?");
                string input = Console.ReadLine();
                if (input == "y")
                    return 1;
                else
                    return 0;
            }
            else
            {
                Console.WriteLine("\n\t Incorrect input...Please enter a 6 digit account number. E.g '100000'");
                Console.Write("\t\t Check another account (y/n)?");
                string input = Console.ReadLine();
                if (input == "y")
                    return 1;
                else
                    return 0;
            }
        }


        private int getAccountNumber()
        {
            return accNumber;
        }
    }
}
