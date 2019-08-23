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

        public Account(string firstName, string lastName, string address, int phoneNum, string email)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.phoneNum = phoneNum;
            this.email = email;
            this.balance = 1000;
            accountsPath = Path.GetFullPath("../../Database/Accounts");
            transPath = Path.GetFullPath("../../Database/Transactions");
        }

        //This method creates the .txt file for the account, holding all the information.
        //This also creates a directory in /Transactions with the accNumber as the title, this will hold all the transaction history files.
        public void CreateAccount()
        {
            //Checks all the accounts currently in the database, and assigned the accNumber 100001 + n (n being number of accounts)
            DirectoryInfo d = new DirectoryInfo(@accountsPath);
            FileInfo[] files = d.GetFiles("*.txt");
            string str = "";
            foreach(FileInfo file in files)
            {
                str += ", " + file.Name;
            }
            string[] seperateStrings = { "txt", ",", ".", " "};
            string[] accNums = str.Split(seperateStrings, System.StringSplitOptions.RemoveEmptyEntries);
            accNumber = Convert.ToInt32(accNums[accNums.Length - 1]);
            accNumber++;

            //Creates the filepath for the file
            txtPath = Path.GetFullPath(accountsPath + "/" + accNumber + ".txt");


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
                        Console.WriteLine("\n\n\t\tAccount found!");
                        Console.WriteLine("\t\t  -----------------------------------------");
                        Console.WriteLine("\t\t  |    \t    ACCOUNT DETAILS\t\t  |");
                        Console.WriteLine("\t\t  -----------------------------------------");
                        Console.WriteLine("\t\t  |  \t\t\t\t\t  |");
                        Console.WriteLine("\t\t  |  Account no: " + accountNum + "\t\t\t  |");
                        Console.WriteLine("\t\t  |  Account Balance: $" + file[2] + "\t\t  |");
                        Console.WriteLine("\t\t  |  First Name: " + file[0] + "\t\t\t  |");
                        Console.WriteLine("\t\t  |  Last Name: " + file[1] + "\t\t  |");
                        Console.WriteLine("\t\t  |  Address: " + file[3] + "\t\t  |");
                        Console.WriteLine("\t\t  |  Phone: " + file[4] + "\t\t\t  |");
                        Console.WriteLine("\t\t  |  Email: " + file[5] + "\t\t  |");


                        Console.Write("\t\t  |  \t\t\t\t\t  |");
                        Console.WriteLine("\n\t\t  -----------------------------------------");

                        Console.Write("\n\t\t\tCheck another account (y/n)?");
                        string awnser = Console.ReadLine();
                        if (awnser == "y")
                            return 1;
                        else
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

        public int CheckAccount(int accNum)
        {
            DirectoryInfo d = new DirectoryInfo(@accountsPath);
            FileInfo[] files = d.GetFiles("*.txt");
            string str = "";
            foreach (FileInfo file in files)
            {
                str += ", " + file.Name;
            }
            string[] seperateStrings = { "txt", ",", ".", " " };
            string[] accNums = str.Split(seperateStrings, System.StringSplitOptions.RemoveEmptyEntries);
            
            foreach(string s in accNums)
            {
                if(s == accNum.ToString())
                {
                    return 0;
                }
            }

            Console.WriteLine("Couldn't find the account. Retry??");
            Console.ReadKey();
            return 1;
        }

        public int Deposit(int amount, int accNum)
        {
            Transaction trans = new Transaction(accNumber, amount, "Deposit", DateTime.Now.ToString("DD/mm/yyy h:mm tt"));
            txtPath = Path.GetFullPath(accountsPath + "/" + accNum + ".txt");
            
            string[] lines = File.ReadAllLines(txtPath);
            int balance = Convert.ToInt32(lines[2]);
            balance += amount;
            lines[2] = balance.ToString();
            System.IO.File.WriteAllLines(txtPath, lines);
            return 0;
        }


        public int getAccountNumber()
        {
            return accNumber;
        }
    }
}
