using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using WebAPINET8.Models;

namespace WebAPINET8.Database
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
			try
			{
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if(dbCreator != null)
                {
                    if (!dbCreator.CanConnect())
                        dbCreator.Create();
                    if(!dbCreator.HasTables())
                        dbCreator.CreateTables();
                }
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
            }
        }
    }
}
