using KnowledgeAgent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pgvector;

namespace KnowledgeAgent.Infrastructure.Persistence.Configurations;

public sealed class DocumentChunkConfiguration : IEntityTypeConfiguration<DocumentChunk>
{
    public void Configure(EntityTypeBuilder<DocumentChunk> builder)
    {
        builder.ToTable("document_chunks");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Content)
            .HasColumnType("text")
            .IsRequired();

        builder.Property(x => x.SourceLocator)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.EmbeddingVector)
            .HasConversion(
                value => new Vector(value),
                value => value.ToArray())
            .HasColumnType("vector(1536)")
            .IsRequired();

        builder.HasIndex(x => x.DocumentId);

        builder.HasIndex(x => x.ChunkIndex);
    }
}
