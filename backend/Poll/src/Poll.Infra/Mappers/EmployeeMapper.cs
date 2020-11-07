using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poll.Domain.Entities;

namespace Poll.Infra.Mappers
{
    public class EmployeeMapper : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> mapper)
        {
            mapper.ToTable("Employee");

            mapper.Property(e => e.Id)
                .HasAnnotation("Relational:ColumnName", "EmployeeId")
                .ValueGeneratedOnAdd();

            mapper.HasKey(e => e.Id);

            mapper.Property(e => e.Name)
                .HasMaxLength(150)
                .IsRequired();
         
            mapper.Property(e => e.Password)
              .IsRequired();          

            mapper.HasIndex(e =>  e.Id)
                 .IsUnique();
        }
    }
}
