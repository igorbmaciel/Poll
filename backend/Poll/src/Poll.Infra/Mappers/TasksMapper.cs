using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poll.Domain.Entities;

namespace Poll.Infra.Mappers
{
    public class TasksMapper : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> mapper)
        {
            mapper.ToTable("Tasks");

            mapper.Property(e => e.Id)
                .HasAnnotation("Relational:ColumnName", "TasksId")
                .ValueGeneratedOnAdd();

            mapper.HasKey(e => e.Id);

            mapper.Property(e => e.Name)
                .IsRequired();

            mapper.HasIndex(e => e.Id)
                 .IsUnique();
        }
    }
}
