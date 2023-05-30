using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PayRoll.Domain.Entities;
using PayRoll.Infrastructure.Implementation;

namespace PayRoll.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Cadre> Cadre => Set<Cadre>();
        public DbSet<Level> Level => Set<Level>();
        public DbSet<Position> Position => Set<Position>();
        public DbSet<SalaryOption> SalaryOption => Set<SalaryOption>();
        public DbSet<Employee> Employee => Set<Employee>();
        public DbSet<PayRollManagement> PayRollManagement => Set<PayRollManagement>();
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}