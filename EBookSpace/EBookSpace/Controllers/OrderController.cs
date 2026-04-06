using EBookSpace.Models.DTOs.UI;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;

namespace EBookSpace.Controllers
{
    public class OrderController : Controller
    {
        [BindProperty]
        public PaymentDTO _paymentdto { get; set; }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IntiateOrder()
        {

            string key = "rzp_test_us_STdrDMVckIg5bT";
            string secret = "GAFi2OKE7T7tr3sZ0dDCmEtd";

            Random _random = new Random();
            string TransactionId = _random.Next(0, 10000).ToString();

            //RazorpayClient client = new RazorpayClient(key, secret);

            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("amount", Convert.ToDecimal(_paymentdto.TotalPrice) * 100);
            input.Add("currency", "USD");
            input.Add("receipt", TransactionId);

            RazorpayClient client = new RazorpayClient(key, secret);
            Razorpay.Api.Order order = client.Order.Create(input);

            ViewBag.orderid = order["id"].ToString();
            return View("Payment", _paymentdto);
        }

        //public IActionResult Payment(string razorpayPaymentId, string razorpayOrderId, string razorpaySignature)
        //{
        //    Dictionary<string, object> attributes = new Dictionary<string, object>();
        //    attributes.Add("razorpay_payment_id", razorpayPaymentId);
        //    attributes.Add("razorpay_order_id", razorpayOrderId);
        //    attributes.Add("razorpay_signature", razorpaySignature);
        //    Utils.verifyPaymentSignature(attributes);

        //    PaymentDTO paymentDTO = new _paymentdto();
        //    paymentDTO.TransactionId = razorpayPaymentId;
        //    paymentDTO.OrderId = razorpayOrderId;

        //    return View("PaymentSuccess", _paymentdto);
        //}
        public IActionResult Payment(string razorpayPaymentId, string razorpayOrderId, string razorpaySignature)
        {
            // 1. Verify payment signature
            Dictionary<string, string> attributes = new Dictionary<string, string>();
            attributes.Add("razorpay_payment_id", razorpayPaymentId);
            attributes.Add("razorpay_order_id", razorpayOrderId);
            attributes.Add("razorpay_signature", razorpaySignature);

            Utils.verifyPaymentSignature(attributes);

            // 2. Prepare DTO
            PaymentDTO paymentDTO = new PaymentDTO();
            paymentDTO.TransactionId = razorpayPaymentId;
            paymentDTO.OrderId = Convert.ToInt32(razorpayOrderId);

            // 3. Return View
            return View("PaymentSuccess", paymentDTO);
        }
    }
}
