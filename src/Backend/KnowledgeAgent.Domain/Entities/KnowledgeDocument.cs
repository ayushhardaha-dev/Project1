using KnowledgeAgent.Domain.Common;

namespace KnowledgeAgent.Domain.Entities;

public sealed class KnowledgeDocument : BaseEntity
{
    private readonly List<DocumentChunk> _chunks = new();

    private KnowledgeDocument() { }

    public KnowledgeDocument(string fileName, string contentType, long sizeInBytes, string uploadedBy)
    {
        FileName = fileName;
        ContentType = contentType;
        SizeInBytes = sizeInBytes;
        UploadedBy = uploadedBy;
        ProcessingStatus = DocumentProcessingStatus.Uploaded;
    }

    public string FileName { get; private set; } = string.Empty;
    public string ContentType { get; private set; } = string.Empty;
    public long SizeInBytes { get; private set; }
    public string UploadedBy { get; private set; } = string.Empty;
    public DocumentProcessingStatus ProcessingStatus { get; private set; }
    public IReadOnlyCollection<DocumentChunk> Chunks => _chunks;

    public void MarkProcessing() => ProcessingStatus = DocumentProcessingStatus.Processing;

    public void MarkReady() => ProcessingStatus = DocumentProcessingStatus.Ready;

    public void MarkFailed() => ProcessingStatus = DocumentProcessingStatus.Failed;

    public DocumentChunk AddChunk(int chunkIndex, string content, string sourceLocator)
    {
        var chunk = new DocumentChunk(Id, chunkIndex, content, sourceLocator);
        _chunks.Add(chunk);
        return chunk;
    }
}

public enum DocumentProcessingStatus
{
    Uploaded = 0,
    Processing = 1,
    Ready = 2,
    Failed = 3
}
