using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using atmBO;
using System.Threading.Channels;

namespace atmDAL
{
    public class ATM_DAL
    {
        List<string> readDatafromfile(string fileName)//reads from file in form of string
        {
            List<string> list = new List<string>();


            string filePath = Path.Combine(Environment.CurrentDirectory,
                fileName);
            StreamReader sr = new StreamReader(filePath);
            string line = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                list.Add(line);
            }
            sr.Close();
            return list;



        }
        string Read(string filename)
        {
            string path = Path.Combine(Environment.CurrentDirectory, filename);
            StreamReader sr = new StreamReader(path);
            string text = sr.ReadToEnd();
            sr.Close();
            return text;
        }
        public List<CustomerAccount> readCustomers()
        {


            List<String> stringList = readDatafromfile("AccountData.txt");
            List<CustomerAccount> empList = new List<CustomerAccount>();


            foreach (string s in stringList)
            {
                if (s.Length > 1)
                {
                    string[] data = s.Split(",");
                    CustomerAccount e = new CustomerAccount();
                    e.accountNumber = System.Convert.ToInt32(data[0]);
                    e.login = data[1];
                    e.PIN = data[2];
                    e.holderName = data[3];
                    e.accountBalance = System.Convert.ToInt32(data[4]);
                    e.status = data[5];
                    e.accountType = data[6];
                    empList.Add(e);
                }
            }

            return empList;



        }
        public List<CustomerAccount> readACC()
        {
            List<CustomerAccount> list = new List<CustomerAccount>();
            string text = Read("AccountData.txt");
            string[] arr = text.Split("\n");
            foreach (string s in arr)
            {
                if (s.Length > 1)
                {
                    string[] data = s.Split(",");
                    CustomerAccount e = new CustomerAccount();
                    e.accountNumber = System.Convert.ToInt32(data[0]);
                    e.login = data[1];
                    e.PIN = data[2];
                    e.holderName = data[3];
                    e.accountBalance = System.Convert.ToInt32(data[4]);
                    e.status = data[5];
                    e.accountType = data[6];

                    list.Add(e);
                }
            }
            return list;
        }
        public void SaveAccount(List<CustomerAccount> temp) //list to string to write into file
        {
            string text = "";
            for (int i = 0; i < temp.Count; i++)
            {
                text = text + $"{temp[i].accountNumber},{temp[i].login},{temp[i].PIN},{temp[i].holderName},{temp[i].accountBalance},{temp[i].status},{temp[i].accountType}\n";
            }

            Savelist(text, "AccountData.txt");
        }
        public void SaveAccounts(CustomerAccount temp)
        {
            string text = $"{temp.accountNumber},{temp.login},{temp.PIN},{temp.holderName},{temp.accountBalance},{temp.status},{temp.accountType}";
            Save(text, "AccountData.txt");

        }


        public void Savelist(string text, string fileName)//writes strings into file in append mode
        {
            string filePath = Path.Combine(Environment.CurrentDirectory,
                fileName);
            StreamWriter sw = new StreamWriter(filePath, append: false);
            sw.WriteLine(text);
            sw.Close();
        }



        void Save(string text, string fileName)//writes strings into file in append mode
        {
            string filePath = Path.Combine(Environment.CurrentDirectory,
                fileName);
            StreamWriter sw = new StreamWriter(filePath, append: true);
            sw.WriteLine(text);
            sw.Close();
        }
        
        public void writeReports(transaction t)
        {
            string fileName = "transactions.txt";
            writeReports(t, fileName);

        }

        public void writeReports(transaction t,string fileName)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory,
                fileName);
            StreamWriter sw = new StreamWriter(filePath, append: true);

            string text = DateTime.Today.ToString("MM/dd/yyyy");
            sw.Write($"{t.holdersName},{t.transactionType},{t.userID},{t.ammount},{text}\n");
            sw.Close();
            
        }

        public List<transaction> readReports()
        {
            string reportPath = Path.Combine(Environment.CurrentDirectory, "transactions.txt");
            if (File.Exists(reportPath))
            {
                List<transaction> rep = new List<transaction>();
                string[] arr = File.ReadAllLines(reportPath);

                foreach(string s in arr)
                {
                    string[] data = s.Split(",");
                    transaction t = new transaction();
                    t.holdersName = data[0];
                    t.transactionType = data[1];
                    t.userID = System.Convert.ToInt32(data[2]);
                    t.ammount = System.Convert.ToInt32(data[3]);
                    t.date = (data[4]);

                    rep.Add(t);

                }
                return rep;
               
            }

            return null;
        }




    }
}

