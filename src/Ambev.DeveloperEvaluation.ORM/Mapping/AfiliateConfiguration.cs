using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class AfiliateConfiguration : IEntityTypeConfiguration<Affiliate>
    {
        void IEntityTypeConfiguration<Affiliate>.Configure(EntityTypeBuilder<Affiliate> builder)
        {
            builder.ToTable("Afiliates");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasMaxLength(100);

            builder.Property(x => x.Cnpj)
                .HasColumnName("Cnpj")
                .HasMaxLength(14)
                .IsFixedLength()
                .IsRequired();
        }
    }
}
