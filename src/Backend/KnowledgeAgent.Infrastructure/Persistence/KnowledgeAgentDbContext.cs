using KnowledgeAgent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Pgvector;

namespace KnowledgeAgent.Infrastructure.Persistence;

public sealed class KnowledgeAgentDbContext : DbContext
{
    public KnowledgeAgentDbContext(DbContextOptions<KnowledgeAgentDbContext> options)
        : base(options)
    {
    }

    public DbSet<KnowledgeDocument> Documents => Set<KnowledgeDocument>();
    public DbSet<DocumentChunk> Chunks => Set<DocumentChunk>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("vector");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KnowledgeAgentDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public static Vector ToVector(float[] embedding)
    {
        if (embedding.Length != 1536)
        {
            throw new ArgumentException("Embedding length must be 1536 for text-embedding-ada-002.", nameof(embedding));
        }

        return new Vector(embedding);
    }
}
