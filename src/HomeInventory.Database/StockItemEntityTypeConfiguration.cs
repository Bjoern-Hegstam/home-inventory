using HomeInventory.Database.Common;
using HomeInventory.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeInventory.Database;

public class StockItemEntityTypeConfiguration : IEntityTypeConfiguration<StockItem>
{
    public void Configure(EntityTypeBuilder<StockItem> builder)
    {
        builder.ToTable("StockItem");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .HasConversion(new IdentifierConverter<StockItemId>());

        builder.Property(x => x.Sku)
            .IsRequired()
            .HasConversion<string>(x => x.Value, x => new Sku(x));

        builder.HasIndex(x => x.Id)
            .IsUnique();
    }
}