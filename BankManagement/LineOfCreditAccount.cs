using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_OOPS
{
    public class LineOfCreditAccount: BankAccount
    {
        public LineOfCreditAccount(string name,decimal intialBalance,decimal creditLimit):base(name,intialBalance,-creditLimit)
        {
                
        }

        public override void PerformMonthEndTransactions()
        {
            if(Balance < 0)
            {
                decimal interest = -Balance * 0.07m;
                MakeWithdrawal(interest, DateTime.Now, "Charge monthly interest");
            }
        }

        protected override transaction? CheckWithdrawalLimit(bool isOverdrawn) =>
        isOverdrawn
            ? new transaction(-20, DateTime.Now, "Apply overdraft fee")
            : default;

    }
}
