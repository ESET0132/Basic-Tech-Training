using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance_demo
{
    internal class SavingsAccount : Account
    {
        public double InterestRate { get; set; }

        public SavingsAccount(string accountNumber, string holderName, double balance = 0.0, double interestRate = 0.05)
            : base(accountNumber, holderName, balance)
        {
            InterestRate = interestRate;
        }

        public void ApplyInterest()
        {
            double interest = Balance * InterestRate;
            Balance += interest;
            Console.WriteLine($"Interest of ₹{interest} applied. New Balance: ₹{Balance}");
        }
    }
}
