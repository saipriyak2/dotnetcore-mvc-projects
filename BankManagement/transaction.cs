using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement_OOPS
{
    public class transaction
    {
        public decimal Amount{ get;}
        public DateTime Date { get;}
        public string Notes { get; }


        public transaction(decimal amount, DateTime date, string notes)
        {
            Amount = amount;
            Date = date;
            Notes = notes;
        }
    }


}
