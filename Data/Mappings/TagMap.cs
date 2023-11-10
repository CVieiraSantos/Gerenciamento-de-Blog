using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class TagMap : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tag");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

            builder.Property(x => x.Slug)
            .IsRequired()
            .HasColumnName("Slug")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50);

            builder.HasIndex(x=> x.Slug, "IX_Tag_Slug")
            .IsUnique();
        }
    }
}