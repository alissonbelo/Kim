using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Name)
            .HasMaxLength(250)
            .HasColumnType("varchar(250)")
            .IsRequired();
        
        builder.Property(c => c.Document)
            .HasMaxLength(11)
            .HasColumnType("varchar(11)")
            .IsRequired();
        
        builder.Property(c => c.Phone)
            .HasMaxLength(11)
            .HasColumnType("varchar(11)")
            .IsRequired();
        
        builder.Property(c => c.Email)
            .HasMaxLength(150)
            .HasColumnType("varchar(150)")
            .IsRequired();
    }
}