using KnowledgeAgent.Domain.Common;

namespace KnowledgeAgent.Domain.Entities;

public sealed class DocumentChunk : BaseEntity
{
    private DocumentChunk() { }

    public DocumentChunk(Guid documentId, int chunkIndex, string content, string sourceLocator)
    {
        DocumentId = documentId;
        ChunkIndex = chunkIndex;
        Content = content;
        SourceLocator = sourceLocator;
    }

    public Guid DocumentId { get; private set; }
    public int ChunkIndex { get; private set; }
    public string Content { get; private set; } = string.Empty;
    public string SourceLocator { get; private set; } = string.Empty;

    // OpenAI text-embedding-ada-002 uses 1536 dimensions.
    public float[] EmbeddingVector { get; private set; } = Array.Empty<float>();

    public KnowledgeDocument? Document { get; private set; }

    public void SetEmbedding(float[] embedding)
    {
        EmbeddingVector = embedding;
        Touch();
    }
}
