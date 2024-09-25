using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeCheck.Application
{
    public class PaymentRequest
    {
        public int Amount { get; set; }
        public string Referral { get; set; }
    }
}
