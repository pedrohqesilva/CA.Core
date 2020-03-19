using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context
{
    public abstract class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(GetDbName());
        }

        protected abstract string GetDbName();
    }
}