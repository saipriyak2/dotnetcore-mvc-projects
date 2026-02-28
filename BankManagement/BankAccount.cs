using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_OOPS
{
    public class BankAccount
    {
        public string Number { get; }
        public string Owner{ get; set; }

        public decimal Balance
        {
            get
            {
                decimal Balance = 0;
                foreach(var item in _allTransactions)
                {
                    Balance += item.Amount;
                }

                return Balance;
            }
        }

        private static int s_accountNumberSeed = 1234567890;

        private readonly decimal _minimumBalance;

        public BankAccount(string name, decimal intialBalance) : this(name, intialBalance, 0) { }

        public BankAccount(string name,decimal intialBalance,decimal minimumBalance)
        {
            Number = s_accountNumberSeed.ToString();
            s_accountNumberSeed++;

            Owner = name;
            _minimumBalance = minimumBalance;

            if(intialBalance > 0 )
            {
                MakeDeposit(intialBalance, DateTime.Now, "Intial Balance");
            }
        }

        private readonly List<transaction> _allTransactions = new(); 

        public void MakeDeposit(decimal amount,DateTime date,string note)
        {
            if(amount < 0 )
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new transaction(amount, date, note);
            _allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount,DateTime date,string note)
        {
            if(amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount withdrawal must be positive");

            }

            transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
            transaction? withdrawal = new(-amount, date, note);
            _allTransactions.Add(withdrawal);
            if(overdraftTransaction != null)
            {
                _allTransactions.Add(overdraftTransaction);
            }
        }

        protected virtual transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if(isOverdrawn)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            else
            {
                return default;
            }
        }

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal Balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach(var item in _allTransactions)
            {
                Balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{Balance}\t{item.Notes}");
            }

            return report.ToString();
        }

        public virtual void PerformMonthEndTransactions()
        {

        }


    }
}
