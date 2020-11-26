using atmBLL;
using atmBO;
using System;
using System.Collections.Generic;
using System.Text;


namespace atmView
{
    public class ATM_VIEW
    {
        public void LoginScreen()
        {

            Console.WriteLine("===============================   ATM MACHINE   ==============================");
            Console.WriteLine("-----==================== WELCOME TO THE ATM SYSTEM ======================-----");
            Console.WriteLine("\n\n\n");
            Console.Write("PLEASE ENTER YOUR USER NAME: ");
            string Name = Console.ReadLine(); //username to login
            // check from the file if the user exists



            Console.Write("PLEASE ENTER YOUR PASSWORD: ");
            string pass = Console.ReadLine(); //password to login
            int count = 0;
            // check if password is entered wrong 3 times in a row
            // if so disable that account
            //int countentry = 0;
            try
            {
                if (Name == "admin" && pass == "admin" || Name == "ADMIN" && pass == "ADMIN") 
                {
                    AdminAccount ad = new AdminAccount { userName = Name, password = pass };
                    adminMenu();
                }
                else
                {
                    ATM_DLL obj = new ATM_DLL();
                    bool valid = obj.AccountPresent(Name, pass);
                    if (count == 3)
                    {
                        Console.WriteLine("YOU HAVE REACHED THE LIMIT OF LOGIN ATTEMPTS \n YOUR ACCOUNT IS BEING DISABLES NOW");
                        obj.disableAccount(Name, pass);
                    }

                    if (valid)
                    {

                        userMenu(Name, pass);

                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("SORRY THIS ACCOUNT DOES NOT EXISTS OR YOU HAVE ENTERED WRONG CREDENTIALS");
                        count++;
                        LoginScreen();
                    }


                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine($"{e.GetType()} says {e.Message}");
                LoginScreen();

            }


        }
        public void adminMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("1----Create New Account.\n2----Delete Existing Account.\n3----Update Account Information.\n4----Search for Account.\n5----View Reports\n6----Exit");
            Console.WriteLine("Please select one of the above options ");
            int choice = System.Convert.ToInt32(Console.ReadLine());
            if (choice == 1 || choice == 2 || choice == 3 || choice == 4 || choice == 5 || choice == 6)
            {
                if (choice == 1) CreateNewAccount();
                if (choice == 2) deleteAccount();
                if (choice == 3) updateAccount();
                if (choice == 4) searchAccount();
                if (choice == 5) viewReport();
                if (choice == 6) exit();
            }
            else
            {
                adminMenu();
            }
        }

        public void userMenu(string name, string pass)
        {
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("1----Withdraw Cash\n2----Cash Transfer\n3----Deposit Cash\n4----Display Balance\n5----Exit\n");
            Console.Write("Please select one of the above options ");
            int choice = System.Convert.ToInt32(Console.ReadLine());
            if (choice == 1 || choice == 2 || choice == 3 || choice == 4 || choice == 5)
            {
                if (choice == 1) withdrawCash(name, pass);
                if (choice == 2) transferCash(name, pass);
                if (choice == 3) depositCash(name, pass);
                if (choice == 4) displayBalance(name, pass);
                if (choice == 5) exit();
            }
            else
            {
                userMenu(name, pass);
            }


        }

        void withdrawCash(string name, string pass)
        {
            Console.WriteLine("1) Fast Cash \n2) Normal Cash\nPlease select a mode of withdrawal:");
            int choice = System.Convert.ToInt32(System.Console.ReadLine());
            
            if (choice == 1) //fastcash
                {

                    //check if money is valid and available in account
                    Console.WriteLine("1----500\n2----1000\n3----2000\n4----5000\n5----10000\n6----15000\n7----20000");
                    //pass ammount to function that checks validity and subtracts from initial ammount
                    int ammountChoice = System.Convert.ToInt32(Console.ReadLine());
                if (ammountChoice == 1 || ammountChoice == 2 || ammountChoice == 3 || ammountChoice == 4 || ammountChoice == 5)
                {
                    Console.Write("are you sure you want to withdraw money press 1/0 for yes or no respectively:");
                    int decision = System.Convert.ToInt32(Console.ReadLine());
                    if (decision == 1)
                    {
                        //call function
                        ATM_DLL obj = new ATM_DLL();
                        obj.fastCash(ammountChoice, name, pass);

                        Console.WriteLine("Cash Successfully Withdrawn!");
                    }
                    else
                    {
                        //call function to exit or cancel request
                        Console.WriteLine("you entered 0 for cancelling the request");
                        Console.WriteLine("CANCELLING YOUR REQUEST...");
                        userMenu(name, pass);

                    }
                }
                else
                {
                    Console.WriteLine("YOU HAVE ENETERED WRONG INPUT");
                    withdrawCash(name, pass);
                }
                
                }
            if (choice == 2) //normal
            {
                Console.Write("please enter how much you want to withdraw:");
                int ammount = System.Convert.ToInt32(Console.ReadLine());
                ATM_DLL obj = new ATM_DLL();
                obj.NormalCash(ammount, name, pass);

                Console.WriteLine("Cash Successfully Withdrawn!");
               

            }

            Console.WriteLine("PRESS 1 TO GO TO USER MENU OR ANY OTHER KEY TO EXIT");
            string x = Console.ReadLine();
            if (x == "1")
                userMenu(name, pass);
            else
            {
                exit();
            }


        }
        void transferCash(string name, string pass)
        {
            Console.Write("please enter the ammount in multiple of 500:");
            int ammount = System.Convert.ToInt32(Console.ReadLine());
            if (ammount % 500 == 0)
            {

                //check validity and proceed transfer by showing account owner name
                Console.WriteLine("Enter the account number you want to transfer money");
                int acc = System.Convert.ToInt32(Console.ReadLine());
                ATM_DLL obj = new ATM_DLL();
                CustomerAccount x = obj.AccountPresent(acc);
                Console.Write($"You wish to deposit Rs {ammount} in account held by {x.holderName}.\n If this information is correct please re-enter the account number:");
                int acc2 = System.Convert.ToInt32(Console.ReadLine());
                if (acc == acc2)
                {
                    obj.transferCash(ammount, name, pass, x.accountNumber);
                }
                else
                {
                    Console.WriteLine("your account number does not matches");
                    transferCash(name, pass);

                }

            }
            else
            {

                Console.WriteLine("sorry the ammount is not in multiples of 500");
                //call function to exit
                Console.WriteLine("PLEASE MAKE YOUR REQUEST AGAIN");
                transferCash(name, pass);

            }


            Console.WriteLine("PRESS 1 TO GO TO USER MENU OR ANY OTHER KEY TO EXIT");
            string z = Console.ReadLine();
            if (z == "1")
                userMenu(name, pass);
            else
            {
                exit();
            }


        }
        void depositCash(string name, string pass)
        {
            try
            {
                Console.WriteLine("ENETR THE AMMOUNT YOU WANT TO DEPOSIT");
                int ammount = System.Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("ARE YOU SURE  PRESS 0/1 FOR NO AND YES RESPECTIVELY");
                int decision = System.Convert.ToInt32(Console.ReadLine());
                if (decision == 1)
                {
                    //proceed with request
                    ATM_DLL obj = new ATM_DLL();
                    obj.CashDeposit(ammount, name, pass);
                    Console.WriteLine("CASH DEPOSITED SUCCESSFULLY");
                    Console.WriteLine("PRESS 1 TO GO TO USER MENU OR ANY OTHER KEY TO EXIT");
                    string x = Console.ReadLine();
                    if (x == "1")
                        userMenu(name, pass);
                    else
                    {
                        userMenu(name, pass);
                    }

                }
                else
                {
                    //false stop request
                    Console.WriteLine("your request to deposit ammount has been cancelled");
                    Console.WriteLine("PRESS 1 TO GO TO USER MENU OR ANY OTHER KEY TO EXIT");
                    string x = Console.ReadLine();
                    if (x == "1")
                        userMenu(name, pass);
                    else
                    {
                        exit();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.GetType()} says {e.Message}");
            }

            

        }
        void displayBalance(string name, string pass)
        {
            //call function to display
            ATM_DLL obj = new ATM_DLL();
            obj.showBalance(name, pass);

            Console.WriteLine("PRESS 1 TO GO TO USER MENU AND PRESS 0 TO EXIT");
            string x = Console.ReadLine();
            if (x == "1")
            {
                userMenu(name, pass);
            }
            else
            {
                exit();
            }

        }
        void exit()
        {
            Console.WriteLine("your program is now exiting");
          
        }

        void CreateNewAccount()
        {
            string again = "yes";
            while (again == "yes")
            {
                //used to create account;
                Console.Clear();
                Console.WriteLine("enter the data of the user you want to add");
                Console.Write("LOGIN OF USER (five digits):");
                string login = Console.ReadLine();
                Console.Write("PIN CODE :");
                string pinCode = Console.ReadLine();
                while (pinCode.Length != 5)
                {
                    Console.WriteLine("PLEASE ENTER FIVE DIGIT PIN CODE");
                    pinCode = Console.ReadLine();
                }
                Console.Write("NAME OF USER :");
                string Name = Console.ReadLine();
                Console.Write("ACCOUNT TYPE (SAVINGS/CURRENT)");
                string type = Console.ReadLine();
                while (type != "savings" && type != "SAVINGS" && type != "CURRENT" && type != "current")
                {
                    Console.WriteLine("you have entered wrong input please try again");

                    type = Console.ReadLine();


                }
                Console.Write("ACCOUNT BALANCE :");
                int balance = System.Convert.ToInt32(Console.ReadLine());
                Console.Write("STATUS(active,disable) :");
                string status = Console.ReadLine();
                while (status != "ACTIVE" && status != "DISABLE" && status != "active" && status != "disable")
                {
                    Console.WriteLine("you have entered wrong input please try again");
                    status = Console.ReadLine();

                }



                CustomerAccount newAccount = new CustomerAccount { accountNumber = CustomerAccount.theCount, login = login, PIN = pinCode, holderName = Name, accountType = type, accountBalance = balance, status = status };
                // now pass this object to DAL for further proceeding
                ATM_DLL temp = new ATM_DLL();
                temp.saveObject(newAccount);


                Console.WriteLine($"Account Successfully Created – the account number assigned is:{newAccount.accountNumber}");
                Console.WriteLine("do you want to enter again enter yes/no");
                again = Console.ReadLine();
                if (again == "no"|| again=="NO")
                {
                    Console.Clear();
                    adminMenu();
                   
                }
            }

        }
        void deleteAccount()
        {
            Console.Write("Enter the account number to which you want to delete: ");
            int number = System.Convert.ToInt32(Console.ReadLine());
            ATM_DLL del = new ATM_DLL();
            del.deleteAccounts(number);

            Console.WriteLine("PRESS 1 TO GO TO ADMIN MENU OR ANY OTHER KEY TO EXIT");
            string x = Console.ReadLine();
            if (x == "1")
                adminMenu();
            else
            {
                exit();
            }

        }
        void updateAccount()
        {
            Console.WriteLine("Enter the account number you want to update");
            int number = System.Convert.ToInt32(Console.ReadLine());
            ATM_DLL old = new ATM_DLL();
            CustomerAccount temp=old.showOldData(number); //overload operator to make deep copy
            Console.WriteLine($"Account number:{temp.accountNumber}\nAccount login:{temp.login}\nAccount PIN:{old.decryptData(temp.PIN)}\nAccount holdername:{temp.holderName}\nAccount Balance:{temp.accountBalance}\nAccount status:{temp.status}\nAccount type:{temp.accountType}\n");
            Console.WriteLine("Please enter in the fields you wish to update (leave blank otherwise):");
            CustomerAccount updatedData = new CustomerAccount();
            string input;
            updatedData.accountNumber = temp.accountNumber;
            Console.Write("login:");
            input = Console.ReadLine();
            if (input == "")
            {
                updatedData.login = temp.login;
            }
            else
            {
                updatedData.login = input;
            }
            Console.Write("PIN:");
            input = Console.ReadLine();
            if (input == "")
            {
                updatedData.PIN = temp.PIN;
            }
            else
            {
                updatedData.PIN = input;
            }
            Console.Write("holder Name:");
            input = Console.ReadLine();
            if (input == "")
            {
                updatedData.holderName = temp.holderName;
            }
            else
            {
                updatedData.holderName = input;
            }
            Console.Write("Account balance:");
            input = Console.ReadLine();
            var num = input;
            if (input == "")
            {
                updatedData.accountBalance = temp.accountBalance;
            }
            else
            {
                int a = 0;
                if (int.TryParse(input, out a))
                    updatedData.accountBalance = a;
            }
            Console.Write("Account status:");
            input = Console.ReadLine();
            if (input == "")
            {
                updatedData.status = temp.status;
            }
            else
            {
                updatedData.status = input;
            }
            Console.Write("Account type:");
            input = Console.ReadLine();
            if (input == "")
            {
                updatedData.accountType = temp.accountType;
            }
            else
            {
                updatedData.accountType = input;
            }
            old.deleteAccount(number);
            old.saveObject(updatedData);
            Console.WriteLine("Your account has been successfully been updated.");
            Console.WriteLine("PRESS 1 TO GO TO ADMIN MENU OR ANY OTHER KEY TO EXIT");
            string x = Console.ReadLine();
            if(x=="1")
            adminMenu();
            else
            {
                exit();
            }


        }
        void searchAccount()
        {

            string holderName;
            string accType;
            int startingbalance;
            string accStatus;
            int accNum;
            Console.WriteLine("SEARCH MENU:");
            Console.WriteLine("please enter following info to search account");
            Console.WriteLine("incase of no info leave that blank");
            Console.Write("ACCOUNT ID:");
            try
            {
                accNum = System.Convert.ToInt32(Console.ReadLine());
            }
            catch(Exception )
            {
                accNum = 0;
            }

           
            Console.Write("holder Name:");
            holderName = Console.ReadLine();

            try
            {
                Console.Write("Account balance:");
                startingbalance = System.Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception)
            {
                startingbalance = 0;
            }
          
          
            Console.Write("Account status:");
            accStatus = Console.ReadLine();
       
            
            Console.Write("Account type:");
            accType = Console.ReadLine();
         
           

            ATM_DLL obj = new ATM_DLL();
            List<CustomerAccount> x = obj.searching(holderName,accType,startingbalance,accStatus,accNum);
            obj.showonConsole(x);

            Console.WriteLine("PRESS 1 TO GO TO ADMIN MENU AND ANYOTHER TO EXIT");
            string z= Console.ReadLine();
            if (z == "1")
                adminMenu();
            else
            {
                exit();
            }

        }
        void viewReport()
        {
            Console.WriteLine("1---Accounts By Amount\n2-- - Accounts By Date");
            Console.Write("choose your option:");
            int choice = System.Convert.ToInt32(Console.ReadLine());
            if (choice == 1)
            {
                Console.WriteLine("Enter the minimum amount:");
                int minimum = System.Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter the maximum amount:");
                int maximun = System.Convert.ToInt32(Console.ReadLine());
                ATM_DLL obj = new ATM_DLL();
                List<CustomerAccount> x = obj.searchByAmmount(minimum, maximun);
                obj.showonConsole(x);

            }
            if (choice == 2)
            {

                Console.Write("Enter the starting date (format MM/dd/yyyy):");
                DateTime d1= DateTime.Parse(Console.ReadLine());
                Console.Write("Enter the ending date(format MM/dd/yyyy):");
                DateTime d2 = DateTime.Parse(Console.ReadLine());
                ATM_DLL obj = new ATM_DLL();
                List<transaction> x = obj.searchByDate(d1, d2);
                obj.showReportD(x);

            }

            Console.WriteLine("PRESS 1 TO GO TO ADMIN MENU ANYOTHER KEY TO EXIT");
            string z = Console.ReadLine();
            if (z == "1")
                adminMenu();
            else
            {
                exit();
            }
        }
        
    }

}

