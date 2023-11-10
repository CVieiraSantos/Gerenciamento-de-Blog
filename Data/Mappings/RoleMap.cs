using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");

            builder.HasKey(x=> x.Id);

            builder.Property(x=> x.Id)
            .UseIdentityColumn()
            .ValueGeneratedOnAdd();

            builder.Property(x=> x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(20);

            builder.Property(x=> x.Slug)
            .IsRequired()
            .HasColumnName("Slug")
            .HasColumnType("NVARCHAR")            
            .HasMaxLength(50);

            builder.HasIndex(x=> x.Slug, "IX_Role_Slug")
            .IsUnique();
            
        }
    }
}