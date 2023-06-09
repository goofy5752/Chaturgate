namespace Chaturgate.Data.Seeder.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(ChaturgateDbContext dbContext, IServiceProvider serviceProvider);
    }
}
