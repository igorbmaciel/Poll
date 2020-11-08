using Microsoft.EntityFrameworkCore;
using Poll.Domain.Entities;
using Poll.Infra.Mappers;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;

namespace Poll.Infra.Context
{
    public class PollContext : TnfDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public PollContext(DbContextOptions<PollContext> options, ITnfSession session)
           : base(options, session)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyTableNameToLowerConventions(modelBuilder);

            modelBuilder.ApplyConfiguration(new EmployeeMapper());
            modelBuilder.ApplyConfiguration(new TasksMapper());
            modelBuilder.ApplyConfiguration(new VoteMapper());

            base.OnModelCreating(modelBuilder);
        }

        private static void ApplyTableNameToLowerConventions(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.Relational().TableName.ToLower();

                foreach (var property in entity.GetProperties())
                    property.Relational().ColumnName = property.Relational().ColumnName.ToLower();

                foreach (var key in entity.GetKeys())
                    key.Relational().Name = key.Relational().Name.ToLower();

                foreach (var key in entity.GetForeignKeys())
                    key.Relational().Name = key.Relational().Name.ToLower();

                foreach (var index in entity.GetIndexes())
                    index.Relational().Name = index.Relational().Name.ToLower();
            }
        }
    }
}
