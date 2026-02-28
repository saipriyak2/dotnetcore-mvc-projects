using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_OOPS
{
    public class InterestEarningAccount: BankAccount
    {
        public InterestEarningAccount(string name, decimal intialBalance) : base(name, intialBalance)
        {
        }

        public override void PerformMonthEndTransactions()
        {
            if(Balance > 500m)
            {
                decimal interest = Balance * 0.02m;
                MakeDeposit(interest, DateTime.Now, "apply monthly interest");
            }
        }
    }
}
