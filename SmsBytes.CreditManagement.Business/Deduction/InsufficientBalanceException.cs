using System;

namespace SmsBytes.CreditManagement.Business.Deduction
{
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException() : base("You don't have sufficient balance to perform this operation")
        {
        }
    }
}
