using Stripe;
using StripeCheck.Application.DBOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeCheck.Application.Admin
{
    public interface IAdminOperation
    {
        public PaymentResponse OnBoardServiceProvider(string FirstName, string LastName, string Email, string Referral);
    }
    public class AdminOperation : IAdminOperation
    {
        private readonly DBOperations _DBOperations;
        public AdminOperation(DBOperations _DBOperations)
        {
            this._DBOperations = _DBOperations;
        }
        public PaymentResponse OnBoardServiceProvider(string FirstName,string LastName,string Email,string Referral)
        {
            StripeConfiguration.ApiKey = SKey.Key;
            PaymentResponse res = new PaymentResponse();
            
            var accountOptions = new AccountCreateOptions
            {
                Type = "custom",
                Country = "US",
                Email = Email,
                BusinessType = "individual",
                TosAcceptance = new AccountTosAcceptanceOptions
                {
                    Date = DateTime.UtcNow,
                    Ip = "8.8.8.8",
                },
                Capabilities = new AccountCapabilitiesOptions
                {
                    CardPayments = new AccountCapabilitiesCardPaymentsOptions { Requested = true },
                    Transfers = new AccountCapabilitiesTransfersOptions { Requested = true },
                },
                BusinessProfile = new AccountBusinessProfileOptions
                {
                    Mcc = "5734",
                    Url = "https://github.com",
                },
                Individual = new AccountIndividualOptions
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    SsnLast4 = "1234",
                    IdNumber = "123451234",
                    Dob = new DobOptions
                    {
                        Day = 15,
                        Month = 7,
                        Year = 1990
                    },
                    Address = new AddressOptions
                    {
                        Line1 = "1234 Main Street",
                        City = "San Francisco",
                        State = "CA",
                        Country = "US",
                        PostalCode = "94111"
                    },
                    Email = Email,
                    Phone = "0000000000",
                    Verification = new AccountIndividualVerificationOptions
                    {
                        Document = new AccountIndividualVerificationDocumentOptions
                        {
                            Front = "file_identity_document_success"
                        }
                    }
                }
            };
            try
            {
                var accountService = new AccountService();
                var account = accountService.Create(accountOptions);

                var externalAccountService = new ExternalAccountService();
                var externalAccountOptions = new ExternalAccountCreateOptions
                {
                    ExternalAccount = "btok_us_verified",
                    DefaultForCurrency = true,
                };
                var externalAccount = externalAccountService.Create(account.Id, externalAccountOptions);

                res.Status = "Success";
                res.Msg = "Service Provider is OnBoarded";
                res.ReferralCode = Referral;
                _DBOperations.addData(Referral, account.Id);
                return res;
            }
            catch (Exception ex)
            {
                res.Status = "Failed";
                res.Msg = $"Error: {ex.Message}";
                res.ReferralCode = Referral;
                return res;
            }
        }
    }
}
