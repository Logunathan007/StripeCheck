using Stripe;
using StripeCheck.Application.DBOperation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeCheck.Application.ServiceProvider
{
    public interface ISPOperation
    {
        public PaymentResponse PayToServiceProvider(int amount, string Referral);
    }
    public class SPOperation : ISPOperation
    {

        private readonly DBOperations _DBOperations;
        public SPOperation(DBOperations _DBOperations)
        {
            this._DBOperations = _DBOperations;
        }
        public PaymentResponse PayToServiceProvider(int amount,string Referral)
        {
            StripeConfiguration.ApiKey = SKey.Key;
            PaymentResponse paymentResponse = new PaymentResponse();
            paymentResponse.ReferralCode = Referral;

            var options = new PaymentIntentCreateOptions
            {
                Amount = amount * 100,
                Currency = "usd",
                PaymentMethod = "pm_card_visa",
                Confirm = true,
                ApplicationFeeAmount = (long)(((double)amount * .2) * 100),
                ReturnUrl = "https://github.com",
            };
            var service = new PaymentIntentService();
            var requestOptions = new RequestOptions
            {
                StripeAccount = _DBOperations.getDataByReferral(Referral).AccountId
            };
            try
            {
                PaymentIntent paymentIntent = service.Create(options, requestOptions);
                Console.WriteLine(paymentIntent);
                paymentResponse.Status = "Success";
                paymentResponse.Msg = "Payment sended successfully ";
                return paymentResponse;
            }
            catch (StripeException e)
            {
                paymentResponse.Status = "Failed";
                paymentResponse.Msg = "Error creating PaymentIntent: " + e.StripeError.Message;
                return paymentResponse;
            }
        }
    }
}