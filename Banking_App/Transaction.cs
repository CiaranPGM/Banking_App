using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Banking_App
{
    class Transaction
    {
        //Fields
        string type, dateAndTime;
        int amount, accNumber;
        

        public Transaction()
        {

        }

        public Transaction(int accNumber, int amount, string type, string dateAndTime)
        {
            this.accNumber = accNumber;
            this.amount = amount;
            this.type = type;
            this.dateAndTime = dateAndTime;
        }

        public void Add(string transPath)
        {
            DirectoryInfo d = new DirectoryInfo(@transPath + "/" + accNumber);
            FileInfo[] files = d.GetFiles("*.txt");
            int count = 0;
            string[] lines = { type, dateAndTime, accNumber.ToString(), amount.ToString()};
            foreach(FileInfo file in files)
            {
                count++;
            }
            string path = Path.GetFullPath(transPath + "/" + accNumber + "/" + type + ++count + ".txt");
            System.IO.File.WriteAllLines(path, lines);
        }
    }
}
