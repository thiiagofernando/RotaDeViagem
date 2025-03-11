using Microsoft.EntityFrameworkCore;
using RotaDeViagemApi.Data;

namespace RotaDeViagemTest
{
    public abstract class TestBase
    {
        protected RotaDbContext GetDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<RotaDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            return new RotaDbContext(options);
        }
    }
}
