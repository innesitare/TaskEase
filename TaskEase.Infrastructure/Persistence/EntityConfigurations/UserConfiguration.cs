using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEase.Domain.Users;

namespace TaskEase.Infrastructure.Persistence.EntityConfigurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("BoardUsers")
            .HasKey(u => u.Id);
        
        builder.Property(bt => bt.Id)
            .IsRequired();

        builder.Property(u => u.Name)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(50);
        
        builder.Property(u => u.Email)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(50);

        builder.HasMany(u => u.BoardTasks)
            .WithOne()
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}