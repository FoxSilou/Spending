namespace Spending.Services.Tests
{
    using Spending.Infrastructure.Data;
    using Spending.Infrastructure.Repositories;
    using Xunit;

    public class SpendingServiceTests
    {
        [Fact]
        public void Get_IdExists_ReturnsSpending()
        {
            //// Arrange
            //Data.Spending spending;
            //using (var context = new Helpers(nameof(Get_IdExists_ReturnsSpending)))
            //{
            //    spending = context.CreateSpending();
            //}

            //// Act
            //Models.Spending result;
            //using (var context = Helpers.CreateContext(nameof(Get_IdExists_ReturnsSpending)))
            //{
            //    SpendingService service = GetService(context);
            //    result = service.Get(spending.Id);
            //}

            //// Assert
            //result.ToDb().Should().BeEquivalentTo(spending);
        }

        private SpendingRepository GetService(ApplicationDbContext context)
        {
            return new SpendingRepository(context);
        }
    }
}
