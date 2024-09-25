using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeCheck.Application
{
    public class PaymentResponse
    {
        public string ReferralCode { get; set; }
        public string Msg { get; set; }
        public string Status { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
