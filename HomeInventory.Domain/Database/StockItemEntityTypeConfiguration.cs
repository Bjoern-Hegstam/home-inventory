using HomeInventory.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeInventory.Domain.Database;

public class StockItemEntityTypeConfiguration : IEntityTypeConfiguration<StockItem>
{
    public void Configure(EntityTypeBuilder<StockItem> builder)
    {
        builder.ToTable("StockItem");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.StockItemId)
            .IsRequired()
            .HasConversion(new IdentifierConverter<StockItemId>());

        builder.Property(x => x.Sku)
            .IsRequired()
            .HasConversion<string>(x => x.Value, x => new Sku(x));

        builder.HasIndex(x => x.StockItemId)
            .IsUnique();
    }
}