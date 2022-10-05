using Microsoft.EntityFrameworkCore;

namespace VehicleRental.Business.Tests
{
    public class SearchLogicTests
    {
        [SetUp]
        public void Setup()
        {
            var dbOption = new DbContextOptionsBuilder<masterContext>()
                .useinmemory
        }

        [Test]
        public void Test1()
        {
            SearchLogic = new SearchLogic();

            Assert.Pass();
        }
    }
}