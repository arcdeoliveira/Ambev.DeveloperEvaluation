using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class AdressConfiguration : IEntityTypeConfiguration<Adress>
    {
        public void Configure(EntityTypeBuilder<Adress> builder)
        {
            builder.ToTable("Adresses");  

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Street)
                .HasColumnName("Street")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Number)
                .HasColumnName("Number")
                .HasMaxLength(int.MaxValue)
                .IsRequired();  

            builder.Property(x => x.Complement)
                .HasConversion<string>()
                .HasColumnName("Complement")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasOne(builder => builder.Afiliate)
                .WithOne(builder => builder.Adress)
                .HasForeignKey<Adress>(builder => builder.AfiliateId)   
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
