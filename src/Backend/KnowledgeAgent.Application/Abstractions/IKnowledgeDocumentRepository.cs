using KnowledgeAgent.Domain.Entities;

namespace KnowledgeAgent.Application.Abstractions;

public interface IKnowledgeDocumentRepository
{
    Task<KnowledgeDocument?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(KnowledgeDocument document, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
