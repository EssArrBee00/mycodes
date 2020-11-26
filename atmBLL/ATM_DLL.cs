using atmBO;
using atmDAL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace atmBLL
{
    public class ATM_DLL
    {
        public void saveObject(CustomerAccount newAccount)
        {
            ATM_DAL savedata = new ATM_DAL();
            newAccount.PIN = encryptData(newAccount.PIN);
            savedata.SaveAccounts(newAccount);

        }

        public void saveList(List<CustomerAccount> newList)
        {

            ATM_DAL savedata = new ATM_DAL();
           savedata.SaveAccount(newList);

        }
        public string encryptData(string input)
        {
            String o = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '0') o = o + '9';
                if (input[i] == '1') o = o + '8';
                if (input[i] == '2') o = o + '7';
                if (input[i] == '3') o = o + '6';
                if (input[i] == '4') o = o + '5';
                if (input[i] == '5') o = o + '4';
                if (input[i] == '6') o = o + '3';
                if (input[i] == '7') o = o + '2';
                if (input[i] == '8') o = o + '1';
                if (input[i] == '9') o = o + '0';
                if (input[i] >= 'A' && input[i] <= 'Z')
                {
                    o = o+ (input[i] + 25 - (2 * (input[i] - 'A')));
                }
                if (input[i] >= 'a' && input[i] <= 'z')
                {
                    o = o+(input[i] + 25 - (2 * (input[i] - 'a')));
                }

            }
            return o;
        }
        public string decryptData(string input)
        {
            String o = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '0') o = o + '9';
                if (input[i] == '1') o = o + '8';
                if (input[i] == '2') o = o + '7';
                if (input[i] == '3') o = o + '6';
                if (input[i] == '4') o = o + '5';
                if (input[i] == '5') o = o + '4';
                if (input[i] == '6') o = o + '3';
                if (input[i] == '7') o = o + '2';
                if (input[i] == '8') o = o + '1';
                if (input[i] == '9') o = o + '0';
                if (input[i] >= 'A' && input[i] <= 'Z')
                {
                    o = o + (input[i] + 25 - (2 * (input[i] - 'A')));
                }
                if (input[i] >= 'a' && input[i] <= 'z')
                {
                    o = o + (input[i] + 25 - (2 * (input[i] - 'a')));
                }

            }
            return o;
        }

         void Receipt(string operationname = null, int ammount = 0,CustomerAccount x=null)
        {
            DateTime TodayDate = DateTime.Today;
            Console.WriteLine($"Account # {x.accountNumber}");
            Console.WriteLine($"Date : {TodayDate}");
            Console.WriteLine($"Ammount {operationname} amount");
            Console.WriteLine($"Balance : {x.accountBalance}");
        }

        public void deleteAccounts(int AccountNumber)
        {
            ATM_DAL obj = new ATM_DAL();
            List<CustomerAccount> temp = obj.readCustomers();

            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].accountNumber == AccountNumber)
                {
                    Console.WriteLine($"do you want to delete account with holder name {temp[i].holderName}:");
                    string choice = Console.ReadLine();
                    if (choice == "yes")
                    {
                        Console.WriteLine(temp[i].accountNumber);
                        temp.RemoveAt(i);
                       
                    }

                }

            }
            
            obj.SaveAccount(temp);
            Console.WriteLine("account successfully deleted\n");



        }

        public void deleteAccount(int AccountNumber)
        {
            ATM_DAL obj = new ATM_DAL();
            List<CustomerAccount> temp = obj.readCustomers();

            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].accountNumber == AccountNumber)
                {
                    
                    {
                       
                        temp.RemoveAt(i);

                    }

                }

            }
            obj.SaveAccount(temp);
           
        }

        public CustomerAccount showOldData(int AccountNumber)
        {
            CustomerAccount abc = new CustomerAccount();
            ATM_DAL obj = new ATM_DAL();
            
            List<CustomerAccount> temp = obj.readCustomers();

            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].accountNumber == AccountNumber)
                {

                  abc= CustomerAccount.Copy(temp[i]);
                }

            }

            return abc;



          
        }

        public List<CustomerAccount> searching(string holderName = "", string accType = "", int startingbalance = 0, string accStatus = "", int accNum = 0)
        {
            List<CustomerAccount> searchedData = new List<CustomerAccount>();
            ATM_DAL obj = new ATM_DAL();
            _ = new List<CustomerAccount>();
            List<CustomerAccount> fileList = obj.readCustomers();
            foreach (CustomerAccount c in fileList )
            {
                if ((accNum == c.accountNumber || accNum == 0) &&
                   (holderName == c.holderName || holderName == "") &&
                   (accType == c.accountType || accType == "") &&
                   (startingbalance == c.accountBalance || startingbalance == 0) &&
                   (accStatus.ToLower() == c.status || accStatus == ""))

                { 
                    
                    searchedData.Add(c);
                 }


            }

            return searchedData;



        }


        public void showonConsole(List<CustomerAccount> temp)
        {
            Console.WriteLine("==== SEARCH RESULTS ======");
            Console.WriteLine(format: "{0,-15} {1,-15} {2,-15} {3,-15} {4,-15} ",
                           "Account NO",
                           "Holders Name",
                           "Type",
                           "Balance",
                           "Status");
            foreach (CustomerAccount c in temp)
            {

                Console.WriteLine(format: "{0,-15} {1,-15} {2,-15} {3,-15} {4,-15} ",
                    c.accountNumber,
                    c.holderName,
                    c.accountType,
                    c.accountBalance,
                    c.status);

            }
        }


        public List<CustomerAccount> searchByAmmount(int min, int max)
        {
            List<CustomerAccount> fileData = new List<CustomerAccount>();
            List<CustomerAccount> output = new List<CustomerAccount>();
            ATM_DAL obj = new ATM_DAL();
            fileData = obj.readCustomers();

            for (int i = 0; i < fileData.Count; i++)
            {
                if (fileData[i].accountBalance >= min && fileData[i].accountBalance <= max)
                {
                    output.Add(fileData[i]);
                }

            }

            return output;





        }


        public bool AccountPresent(string login, string pass)
        {
            pass = decryptData(pass);
            List<CustomerAccount> fileData = new List<CustomerAccount>();
            ATM_DAL obj = new ATM_DAL();
            fileData = obj.readCustomers();
            for (int i = 0; i < fileData.Count; i++)
            {
                if (fileData[i].login == login && fileData[i].PIN == pass)
                {
                    return true;
                }
            }



            return false;
        }

        public CustomerAccount AccountPresent(int number)
        {
            CustomerAccount ret = new CustomerAccount();

            List<CustomerAccount> fileData = new List<CustomerAccount>();
            ATM_DAL obj = new ATM_DAL();
            fileData = obj.readCustomers();
            for (int i = 0; i < fileData.Count; i++)
            {
                if (fileData[i].accountNumber == number)
                {
                    ret = fileData[i];
                    //maybe shallow copy
                }
            }
            return ret;
        }



        public void fastCash(int num, string name, string pass)
        {
          
            List<CustomerAccount> fileData = new List<CustomerAccount>();
            ATM_DAL obj = new ATM_DAL();
            fileData = obj.readACC();
            for (int i = 0; i < fileData.Count; i++)
            {
              
                if (fileData[i].login == name&& fileData[i].PIN==encryptData(pass))
                {
                    transaction t = new transaction();
                    DateTime date = new DateTime();
                    date = DateTime.Today;
                    t.date = date.Date.ToString("dd/MM/yyyy");
                    t.holdersName = name;
                    t.userID = fileData[i].accountNumber;
                    t.transactionType = "widthdraw";
                    int a=0;
                    if (num == 1)
                    {
                        if (fileData[i].accountBalance > 500)
                        {
                            fileData[i].accountBalance = fileData[i].accountBalance - 500;
                            t.ammount = 500;
                            a = 500;
                        }

                    }
                    if (num == 2)
                    {
                        if (fileData[i].accountBalance > 1000)
                        {
                            fileData[i].accountBalance = fileData[i].accountBalance - 1000;
                            t.ammount = 1000;
                            a = 1000;
                        }
                    }
                    if (num == 3)
                    {
                        if (fileData[i].accountBalance > 2000)
                        {
                            fileData[i].accountBalance = fileData[i].accountBalance - 2000;
                            t.ammount = 2000;
                            a = 2000;
                        }

                    }
                    if (num == 4)
                    {
                        if (fileData[i].accountBalance > 5000)
                        {
                            fileData[i].accountBalance = fileData[i].accountBalance - 5000;
                            t.ammount = 5000;
                            a = 5000;
                        }
                    }
                    if (num == 5)
                    {
                        if (fileData[i].accountBalance > 10000)
                        {
                            fileData[i].accountBalance = fileData[i].accountBalance - 10000;
                            t.ammount = 10000;
                            a = 10000;
                        }
                    }
                    if (num == 6)
                    {
                        if (fileData[i].accountBalance > 15000)
                        {
                            fileData[i].accountBalance = fileData[i].accountBalance - 15000;
                            t.ammount = 15000;
                            a = 15000;
                        }
                    }
                    if (num == 7)
                    {
                        if (fileData[i].accountBalance > 20000)
                        {
                            fileData[i].accountBalance = fileData[i].accountBalance - 20000;
                            t.ammount = 20000;
                            a = 20000;
                        }
                    }

                    obj.writeReports(t);
                    Console.WriteLine("DO YOU WANT RECIEPT IF YES PRESS 1 ELSE PRESS ANY KEY TO CONTINUE");
                   
                    string x = Console.ReadLine();
                    if(x=="1")
                    Receipt("widthdraw", a, fileData[i]);
                    
                }
            }
            obj.SaveAccount(fileData);
           
            
        }


        public void NormalCash(int ammount, string name, string pass)
        {
            List<CustomerAccount> fileData = new List<CustomerAccount>();
            ATM_DAL obj = new ATM_DAL();
            fileData = obj.readCustomers();
            for (int i = 0; i < fileData.Count; i++)
            {
                if (fileData[i].login == name && fileData[i].PIN == encryptData(pass))
                {
                    if (fileData[i].accountBalance > ammount)
                    {
                        fileData[i].accountBalance = fileData[i].accountBalance - ammount;
                        transaction t = new transaction();
                        DateTime date = new DateTime();
                        date = DateTime.Today;
                        t.date = date.Date.ToString("dd/MM/yyyy");
                        t.holdersName = name;
                        t.userID = fileData[i].accountNumber;
                        t.ammount = ammount;
                        t.transactionType = "widthdraw";


                        Console.WriteLine("DO YOU WANT RECIEPT IF YES PRESS 1 ELSE PRESS ANY KEY TO CONTINUE");

                        string x = Console.ReadLine();
                        if (x == "1")
                            Receipt("widthdraw", ammount, fileData[i]);
                        obj.writeReports(t);
                    }
                   
                }
            }
            obj.SaveAccount(fileData);

        }

        public void CashDeposit(int amount, string name, string pass)
        {

            List<CustomerAccount> fileData = new List<CustomerAccount>();
            ATM_DAL obj = new ATM_DAL();
            fileData = obj.readCustomers();

            for (int i = 0; i < fileData.Count; i++)
            {

                if (fileData[i].login == name && fileData[i].PIN ==encryptData( pass))
                {
                    fileData[i].accountBalance = fileData[i].accountBalance + amount;
                    Console.WriteLine("Ammount deposited successfully");
                    transaction t = new transaction();
                    DateTime date = new DateTime();
                    date = DateTime.Today;
                    t.date = date.Date.ToString("dd/MM/yyyy");
                    t.holdersName = name;
                    t.userID = fileData[i].accountNumber;
                    t.ammount = amount;
                    t.transactionType = "Deposit";

                    Console.WriteLine("DO YOU WANT RECIEPT IF YES PRESS 1 ELSE PRESS ANY KEY TO CONTINUE");

                    string x = Console.ReadLine();
                    if (x == "1")
                        Receipt("Deposit", amount, fileData[i]);


                    obj.writeReports(t);
                }
            }

            obj.SaveAccount(fileData);
        }


        public void showBalance(string name, string pass)
        {

            List<CustomerAccount> fileData = new List<CustomerAccount>();
            ATM_DAL obj = new ATM_DAL();
            fileData = obj.readCustomers();
            for (int i = 0; i < fileData.Count; i++)
            {

                if (fileData[i].login == name && fileData[i].PIN ==encryptData(pass))
                {
                    DateTime now = DateTime.Now;

                    Console.WriteLine($"Account #{fileData[i].accountNumber}\nDate: {DateTime.Now.ToString("dd/MM/yyyy")}\nBalance: {fileData[i].accountBalance}");
                }
            }

        }

        public void transferCash(int amount, string user1, string pass, int user2)
        {
            List<CustomerAccount> fileData = new List<CustomerAccount>();
            ATM_DAL obj = new ATM_DAL();
            fileData = obj.readACC();
            CustomerAccount acc1 = new CustomerAccount();
            CustomerAccount acc2 = new CustomerAccount();
            for (int i = 0; i < fileData.Count; i++)
            {
                if (fileData[i].accountNumber == user2)
                {
                    acc2 = fileData[i];
                   
                }
                if( fileData[i].login == user1 && fileData[i].PIN == encryptData(pass))
                {
                    acc1 = fileData[i];
                }
            }
            if (acc1.accountBalance >= amount)
            {
                
                acc2.accountBalance = acc2.accountBalance + amount;
                acc1.accountBalance = acc1.accountBalance - amount;
                transaction t = new transaction();
                DateTime date = new DateTime();
                date = DateTime.Today;
                t.date = date.Date.ToString("dd/MM/yyyy");
                t.holdersName = user1;
                t.userID = acc1.accountNumber;
                t.ammount = amount;
                t.transactionType = "transfer";



                Console.WriteLine("DO YOU WANT RECIEPT IF YES PRESS 1 ELSE PRESS ANY KEY TO CONTINUE");

                string x = Console.ReadLine();
                if (x == "1")
                    Receipt("transfer", amount, acc1);
                obj.writeReports(t);

            }

            for (int j = 0; j < fileData.Count; j++)
            {
                if (fileData[j].accountNumber == acc1.accountNumber)
                {
                    fileData[j] = CustomerAccount.Copy(acc1);
                }
                if (fileData[j].accountNumber == acc2.accountNumber)
                {
                    fileData[j] = CustomerAccount.Copy(acc2);
                }
               
            }
            obj.SaveAccount(fileData);



        }

        public List<transaction> searchByDate(DateTime d1, DateTime d2)
        {
            List<transaction> fileData = new List<transaction>();
            List<transaction> outData = new List<transaction>();
            ATM_DAL obj = new ATM_DAL();
            fileData = obj.readReports();
            for (int i = 0; i < fileData.Count; i++)
            {
                DateTime d = DateTime.Parse(fileData[i].date);
               
                if (d >= d1 && d <= d2)
                {
                   
                    outData.Add(fileData[i]);
                }
            }
            return outData;
        }

        public void showReportD(List<transaction> t)
        {
            Console.WriteLine("==== SEARCH RESULTS ======");
            Console.WriteLine(format: "{0,-15} {1,-15} {2,-15} {3,-15} {4,-15} ",
                           "TRANSACTION TYPE",
                           "USER ID",
                           "HOLDER NAME",
                           "AMMOUNT",
                           "DATE");
            foreach (transaction c in t)
            {

                Console.WriteLine(format: "{0,-15} {1,-15} {2,-15} {3,-15} {4,-15} ",
                    c.transactionType,
                    c.userID,
                    c.holdersName,
                    c.ammount,
                    c.date);

            }

        }

        public void disableAccount(string name,string pass)
        {
            pass = decryptData(pass);
            List<CustomerAccount> fileData = new List<CustomerAccount>();
            ATM_DAL obj = new ATM_DAL();
            fileData = obj.readCustomers();
            for (int i = 0; i < fileData.Count; i++)
            {
                if (fileData[i].login == name && fileData[i].PIN == pass)
                {
                    fileData[i].status = "disable";
                }
            }

        }
    }
}

