using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityColumn();

            builder.Property(x=> x.Title)
            .HasColumnName("Title")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(50)
            .IsRequired();    

            builder.Property(x=> x.Summary)
            .HasColumnName("Summary")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(300)
            .IsRequired();  

            builder.Property(x=> x.Body)
            .HasColumnName("Body")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(300)
            .IsRequired(); 

            builder.Property(x=> x.Slug)
            .HasColumnName("Slug")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(250)
            .IsRequired();    
               
            
            builder.Property(x=> x.CreateDate)
            .HasColumnName("CreateDate")
            .HasColumnType("SMALLDATETIME")
            .HasDefaultValue(DateTime.Now.ToUniversalTime());


            builder.Property(x=> x.LastUpdateDate)
            .HasColumnName("LastUpdateDate")
            .HasColumnType("SMALLDATETIME");

            builder.HasIndex(x=> x.Slug, "IX_Post_Slug")
            .IsUnique();

            builder.HasOne(x=> x.Category)
            .WithMany(x=> x.Posts)
            .HasForeignKey("CategoryId")
            .HasConstraintName("FK_Post_Category")
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x=>x.Author)
            .WithMany(x=> x.Posts)
            .HasForeignKey("UserId")
            .HasConstraintName("FK_Post_User")
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x=> x.Tags)
            .WithMany(x=> x.Posts)
            .UsingEntity<Dictionary<string, object>>(
                "PostTag",
                post => post.HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("PostId")
                    .HasConstraintName("FK_PostTag_PostId")
                    .OnDelete(DeleteBehavior.Cascade),

                tag => tag.HasOne<Post>()
                    .WithMany()
                    .HasForeignKey("TagId")
                    .HasConstraintName("FK_PostTag_TagId")
                    .OnDelete(DeleteBehavior.Cascade));            
        }
    }
}