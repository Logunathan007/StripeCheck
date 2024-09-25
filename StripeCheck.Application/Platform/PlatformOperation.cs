using Stripe;
using StripeCheck.Application.DBOperation;
using StripeCheck.Application.ServiceProvider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StripeCheck.Application.Platform
{
    public interface IPlatformOperation
    {
        public PaymentResponse PayToPlatform(int amount, string Referral);
    }
    public class PlatformOperation : IPlatformOperation
    {
        //Connected Account and their Referal Code
        private readonly ISPOperation _ISPOperation;
        private readonly DBOperations _DBOperations;

        public PlatformOperation(ISPOperation _ISPOperation, DBOperations _DBOperations)
        {
            this._ISPOperation = _ISPOperation;
            this._DBOperations = _DBOperations;
        }

        public PaymentResponse PayToPlatform(int amount, string Referral)
        {
            StripeConfiguration.ApiKey = SKey.Key;
            PaymentResponse paymentResponse = new PaymentResponse();
            paymentResponse.ReferralCode = Referral;

            var paymentIntentOptions = new PaymentIntentCreateOptions
            {
                Amount = amount * 100,  
                Currency = "usd",
                PaymentMethod = "pm_card_visa",  
                Confirm = true,  
                ReturnUrl = "https://github.com",
            };

            try
            {
                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = paymentIntentService.Create(paymentIntentOptions);

                var options = new TransferCreateOptions
                {
                    Amount = (long)(((double)amount * .8) * 100),
                    Currency = "usd",
                    Destination = _DBOperations.getDataByReferral(Referral).AccountId,
                    SourceTransaction = paymentIntent.LatestChargeId
                };
                var service = new TransferService();
                service.Create(options);
                paymentResponse.Status = "Success";
                paymentResponse.Msg = "Payment sended successfully ";
                return paymentResponse;
            }
            catch (Exception ex)
            {
                paymentResponse.Status = "Failed";
                paymentResponse.Msg = $"Stripe Error: {ex.Message}";
                return paymentResponse;
            }
        }
    }
}