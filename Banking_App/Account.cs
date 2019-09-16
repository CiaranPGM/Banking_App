using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;

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
            if (files.Length > 0)
            {
                foreach (FileInfo file in files)
                {
                    str += ", " + file.Name;
                }
                string[] seperateStrings = { "txt", ",", ".", " " };
                string[] accNums = str.Split(seperateStrings, System.StringSplitOptions.RemoveEmptyEntries);
                accNumber = Convert.ToInt32(accNums[accNums.Length - 1]);
                accNumber++;
            }
            else
            {
                accNumber = 100001;
            }

            //Creates the filepath for the file
            txtPath = Path.GetFullPath(accountsPath + "/" + accNumber + ".txt");


            string[] lines = { firstName, lastName, balance.ToString(), address, phoneNum.ToString(), email};
            System.IO.File.WriteAllLines(txtPath, lines);

            DirectoryInfo di = Directory.CreateDirectory(@transPath + "/" + accNumber);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }

            //THIS NEXT BLOCK IS COMMENTED OUT BECAUSE I RAN INTO ISSUES RIGHT BEFORE SUBMISSION
            //THE EMAIL FUNCTIONALITY CAN BE VIEWED IN A/C STATEMENT

            //Emailing the account details
            //MailMessage mail = new MailMessage();
            //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            //mail.From = new MailAddress("bankingsystem19@gmail.com");
            //mail.To.Add(lines[5]);
            //mail.Subject = "Banking Statement";
            //mail.Body = "Statement is attatched";

            //System.Net.Mail.Attachment attachment;
            //attachment = new System.Net.Mail.Attachment(txtPath);
            //mail.Attachments.Add(attachment);

            //SmtpServer.Port = 587;
            //SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            //SmtpServer.Credentials = new System.Net.NetworkCredential("bankingsystem19@gmail.com", "I_Love_UTS_123!@#");
            //SmtpServer.EnableSsl = true;

            //SmtpServer.Send(mail);
            //SmtpServer.Dispose();

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
                Console.WriteLine("\n\n\t\tAccount could not be found.");
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

            Console.Write("\n\n\n\t\tCouldn't find the account. Press 'enter' to retry.");
            Console.ReadLine();
            return 1;
        }

        public void Deposit(int amount, int accNum)
        {
            txtPath = Path.GetFullPath(accountsPath + "/" + accNum + ".txt");
            
            string[] lines = File.ReadAllLines(txtPath);
            int balance = Convert.ToInt32(lines[2]);
            balance += amount;
            lines[2] = balance.ToString();
            System.IO.File.WriteAllLines(txtPath, lines);
            Transaction trans = new Transaction(accNum, amount, "Deposit", DateTime.Now.ToString("dd/MM/yyy h:mm tt"));
            trans.Add(transPath);
        }

        public int Withdraw(int amount, int accNum)
        {
            txtPath = Path.GetFullPath(accountsPath + "/" + accNum + ".txt");

            string[] lines = File.ReadAllLines(txtPath);
            int balance = Convert.ToInt32(lines[2]);
            if (balance < amount)
            {
                return 1;
            }
            else
            {
                balance -= amount;
                lines[2] = balance.ToString();
                System.IO.File.WriteAllLines(txtPath, lines);
                Transaction trans = new Transaction(accNum, amount, "Withdraw", DateTime.Now.ToString("dd/MM/yyy h:mm tt"));
                trans.Add(transPath);
                return 0;
            }
        }

        public int GenerateStatement(int accNum)
        {
            txtPath = Path.GetFullPath(accountsPath + "/" + accNum + ".txt");

            string[] lines = File.ReadAllLines(txtPath);
            string[] statement =
            {
                "\t\t  -----------------------------------------",
                "\t\t  |    \t    SIMPLE BANKING SYSTEM\t  |",
                "\t\t  -----------------------------------------",
                "\t\t  |  Account Statement\t\t\t  |",
                "\t\t  |  \t\t\t\t\t  |",
                "\t\t  |  Account no: " + accNum + "\t\t\t  |",
                "\t\t  |  Account Balance: $" + lines[2] + "\t\t  |",
                "\t\t  |  First Name: " + lines[0] + "\t\t\t  |",
                "\t\t  |  Last Name: " + lines[1] + "\t\t  |",
                "\t\t  |  Address: " + lines[3] + "\t\t  |",
                "\t\t  |  Phone: 0" + lines[4] + "\t\t\t  |",
                "\t\t  |  Email: " + lines[5] + "\t\t  |",
                "\t\t  -----------------------------------------"

            };

            foreach(string str in statement)
            {
                Console.WriteLine(str);
            }


            DirectoryInfo d = new DirectoryInfo(@transPath + "/" + accNum);
            FileInfo[] files = d.GetFiles("*.txt");
            int count = 0;
            foreach (FileInfo file in files)
            {
                if (count <= 4) { 
                    string[] fileLines = File.ReadAllLines(transPath + "/" + accNum + "/" + file.ToString());
                    Console.WriteLine("\t\t  -----------------------------------------");
                    Console.WriteLine("\t\t  |  \t\t  " + fileLines[0] + "\t\t  |");
                    Console.WriteLine("\t\t  -----------------------------------------");
                    Console.WriteLine("\t\t  |  " + fileLines[1] + "\t\t  |");
                    Console.WriteLine("\t\t  |  Account Number: " + fileLines[2] + "\t\t  |");
                    Console.WriteLine("\t\t  |  Amount: $" + fileLines[3] + "\t\t\t  |");
                    Console.WriteLine("\t\t  -----------------------------------------");
                    count++;
                }

            }



            Console.Write("\n\t\t\tEmail Statement (y/n)?");
            string awnser = Console.ReadLine();
            if (awnser == "y") {
                //Send email
                string path1 = Path.GetFullPath("../..");
                System.IO.File.WriteAllLines(@path1 + "/statement.txt", statement);



                //Send the email
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("bankingsystem19@gmail.com");
                mail.To.Add(lines[5]);
                mail.Subject = "Banking Statement";
                mail.Body = "Statement is attatched";

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(path1 + "/statement.txt");
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Credentials = new System.Net.NetworkCredential("bankingsystem19@gmail.com", "I_Love_UTS_123!@#");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                Console.Write("\n\t\tEmail sent successfully!...");
                Console.Write("\n\t\t   Press 'enter'.");
                Console.ReadKey();
                return 1;
            }
            else
                return 0;
        }

        public int SearchAccountDetails(int accountNum)
        {
            if (accountNum.ToString().Length < 10 && accountNum.ToString().Length > 5 && (int.TryParse(accountNum.ToString(), out accountNum)))
            {
                string[] files = Directory.GetFiles(accountsPath);
                for (int it = 0; it < files.Length; it++)
                {
                    if (files[it].Contains(accountNum.ToString()))
                    {
                        string[] file = File.ReadAllLines(accountsPath + "/" + accountNum + ".txt");
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

                        Console.Write("\n\t\t\tDelete account (y/n)?");
                        string awnser = Console.ReadLine();
                        if (awnser == "y")
                        {
                            File.Delete(accountsPath + "/" + accountNum + ".txt");
                            Directory.Delete(transPath + "/" + accountNum, true);
                            Console.Write("\n\t\t\tAccount Deleted...");
                            Console.ReadLine();
                            return 1;
                        }
                        else
                            return 0;
                    }
                }
                Console.WriteLine("\n\n\t\tAccount could not be found.");
                Console.Write("\t\t  Check another account? (y/n)?");
                string input = Console.ReadLine();
                if (input == "y")
                    return 1;
                else
                    return 0;
            }
            return 0;
        }

        public int getAccountNumber()
        {
            return accNumber;
        }
    }
}
