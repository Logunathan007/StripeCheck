using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using StripeCheck.Persistence.DBContexts;
using StripeCheck.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeCheck.Application.DBOperation
{

    public class DBOperations
    {
        private readonly DbConnection _DBObject;
        public DBOperations(DbConnection _db)
        {
            _DBObject = _db;
        }
        public void addData(string referral,string accountId)
        {
            ReferralCode referralCode = new ReferralCode()
            {
                Referral = referral,
                AccountId = accountId
            };
            _DBObject.ReferralCodes.Add(referralCode);
            _DBObject.SaveChanges();
        }
        public List<string> getData()
        {
            return _DBObject.ReferralCodes
                    .Select(rc => rc.Referral)
                    .ToList();
        }
        public ReferralCode getDataByReferral(string referral)
        {
            return _DBObject.ReferralCodes.FirstOrDefault(obj => obj.Referral == referral);
        }
    }
}
