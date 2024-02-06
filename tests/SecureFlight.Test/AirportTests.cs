using System;
using System.Threading.Tasks;
using SecureFlight.Core.Entities;
using SecureFlight.Infrastructure.Repositories;
using Xunit;

namespace SecureFlight.Test
{
    public class AirportTests
    {
        [Fact]
        public async Task Update_Succeeds()
        {
            var testingContext = new SecureFlightDatabaseTestContext();
            testingContext.CreateDatabase();
            //Your test here:
            
            testingContext.DisposeDatabase();
        }
    }
}
