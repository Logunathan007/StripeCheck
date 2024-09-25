using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StripeCheck.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeCheck.Persistence.DBContexts
{
    public class DbConnection : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbConnection(DbContextOptions options) : base(options)
        {
            Console.WriteLine("Logunathan");
        }
        public DbSet<ReferralCode> ReferralCodes { get; set; }
    }
}
