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
            //Arrange
            var testingContext = new SecureFlightDatabaseTestContext();
            testingContext.CreateDatabase();
            var repository = new BaseRepository<Airport>(testingContext);
            
            //TODO: Add test code here
            //Act
            
            
            //Assert
            
            testingContext.DisposeDatabase();
        }
    }
}
