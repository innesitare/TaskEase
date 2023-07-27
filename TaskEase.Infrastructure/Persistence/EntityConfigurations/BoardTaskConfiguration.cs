using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEase.Domain.BoardTasks;

namespace TaskEase.Infrastructure.Persistence.EntityConfigurations;

public sealed class BoardTaskConfiguration : IEntityTypeConfiguration<BoardTask>
{
    public void Configure(EntityTypeBuilder<BoardTask> builder)
    {
        builder.ToTable("BoardTasks")
            .HasKey(bt => bt.Id);

        builder.Property(bt => bt.Id)
            .IsRequired();

        builder.Property(bt => bt.Title)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(bt => bt.Description)
            .IsRequired()
            .IsUnicode(false)
            .HasMaxLength(500);

        builder.Property(bt => bt.Status)
            .IsRequired();
    }
}