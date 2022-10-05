using Microsoft.EntityFrameworkCore;

using VehicleRental.DAL.Data;

namespace VehicleRental.Business.UnitTests.Infrastructure
{
    public class TestBase : IDisposable
    {

        protected readonly masterContext _masterContext;

        public TestBase()
        {

            var options = new DbContextOptionsBuilder<masterContext>()
                .UseInMemoryDatabase(databaseName: "master")
                .Options;

            _masterContext = new masterContext(options);
            _masterContext.Database.EnsureCreated();

            DBContextInitializer.Intialize(_masterContext);
        }

        public void Dispose()
        {
            _masterContext.Database.EnsureDeleted();
            _masterContext.Dispose();
        }

    }
}
