using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBookSpace.Models.DTOs.UI
{
    public class PaymentDTO
    {
        public int OrderId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }

        public double TotalPrice { get; set; }
        [NotMapped]
        public string TransactionId { get; set; }
      
      
    }
}
