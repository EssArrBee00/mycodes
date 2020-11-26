using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace atmBO
{
    //for validation to enter system
    public class AdminAccount
    {
        public string userName { set; get; }

        public string password { set; get; }



    }


    public class CustomerAccount
    {

        //used to keep record of total number of accounts in system 

        public static int theCount { set; get; }



        public string status { set; get; }
        public string login { set; get; } //username

        public string PIN { set; get; }   //password

        public string holderName { set; get; }

        public string accountType { set; get; }

        public int accountBalance { set; get; }

        public int accountNumber { set; get; }

        public CustomerAccount()
        {

            ++theCount;
        }

        public static CustomerAccount Copy(CustomerAccount input) {
            CustomerAccount o = new CustomerAccount
            {
                accountNumber = input.accountNumber,
                accountBalance = input.accountBalance,
                accountType = input.accountType,
                holderName = input.holderName,
                login = input.login,
                PIN = input.PIN,
                status = input.status
            };


            return o;

        }

    }


    public class transaction
    {
        public string transactionType { get; set; }
        public int userID { get; set; }
        public string holdersName { get; set; }
        public string date { get; set; }
        public int ammount { get; set; }
    }
}
