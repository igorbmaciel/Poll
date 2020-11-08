using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poll.Domain.Entities;

namespace Poll.Infra.Mappers
{
    public class VoteMapper : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> mapper)
        {
            mapper.ToTable("Vote");

            mapper.Property(e => e.Id)
                .HasAnnotation("Relational:ColumnName", "VoteId")
                .ValueGeneratedOnAdd();

            mapper.HasKey(e => e.Id);

            mapper.Property(e => e.EmployeeId)
                .IsRequired();

            mapper.Property(e => e.TaskId)
              .IsRequired();

            mapper.Property(e => e.Comment)
                 .HasMaxLength(4000)
                 .IsUnicode(false)
                 .IsRequired()
                 .HasAnnotation("Relational:ColumnName", "Comment");

            mapper.Property(e => e.Date)
               .HasColumnType("datetime")
               .HasDefaultValueSql("(getutcdate())")
               .HasAnnotation("Relational:ColumnName", "Date");

            mapper.HasOne(d => d.Employee)
               .WithMany(p => p.VoteList)
               .HasForeignKey(d => d.EmployeeId)
               .HasConstraintName("FK_Vote_Employee");

            mapper.HasOne(d => d.Tasks)
               .WithMany(p => p.VoteList)
               .HasForeignKey(d => d.TaskId)
               .HasConstraintName("FK_Vote_Tasks");

            mapper.HasIndex(e => e.Id)
                 .IsUnique();
        }
    }
}
