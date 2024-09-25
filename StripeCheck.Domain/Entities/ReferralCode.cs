using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeCheck.Domain.Entities
{
    public class ReferralCode
    {
        [Key]
        public Guid Id { get; set; }
        public string Referral {  get; set; }
        public string AccountId { get; set; }
    }
}
