using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using IdeaManager.Data.Db;

namespace IdeaManager.Data.Db
{
    public class IdeaDbContextFactory : IDesignTimeDbContextFactory<IdeaDbContext>
    {
        public IdeaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdeaDbContext>();
            optionsBuilder.UseSqlite("Data Source=ideamanager.db");

            return new IdeaDbContext(optionsBuilder.Options);
        }
    }
}