using MAWService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAWService.Persistance.FluentApi;

internal class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ExternalId).ValueGeneratedOnAdd();
        builder.HasIndex(x => x.ExternalId);
    }
}