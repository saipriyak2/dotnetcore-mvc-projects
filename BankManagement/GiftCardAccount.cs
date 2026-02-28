using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_OOPS
{
    public class GiftCardAccount: BankAccount
    {
        private readonly decimal _monthlyDeposit = 0m;



        public GiftCardAccount(string name, decimal intialBalance, decimal monthlyDeposit = 0) : base(name, intialBalance)
        => _monthlyDeposit = monthlyDeposit;


        public override void PerformMonthEndTransactions()
        {
            if (_monthlyDeposit != 0)
            {
                MakeDeposit(_monthlyDeposit, DateTime.Now, "Add monthly deposit");
            }
        }
    }
}
