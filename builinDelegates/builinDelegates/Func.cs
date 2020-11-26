using System;
using System.Collections.Generic;
using System.Text;

/*
Func is a generic delegate that encapsulates a method that can accept parameters 
and return some value.

There are 17 Func delegates defined in the class library. 
The first delegate cannot accept parameters and  can only return value
Other Func delegate can take upto 16 parameters and one return value. 
The last parameter is always the return type.

 */


namespace builinDelegates
{
    class Func
    {
        public static int sum(int a,int b)
        {
            Console.WriteLine($"this funtion sums two values {a+b}");
            return a + b;

        }




        static void Main(string[] args)
        {
            Console.Write("introducing Func delegate\n");

            Func<int, int, int> delgateFunc = sum;
            delgateFunc(2, 3);


        }
    }
}
