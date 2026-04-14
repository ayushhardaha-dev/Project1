# Step 1 - Domain Models + PostgreSQL pgvector setup

This baseline adds the first vertical slice for the Enterprise RAG Knowledge Agent backend:

- Domain entities (`KnowledgeDocument`, `DocumentChunk`) for ingestion and retrieval.
- Infrastructure `DbContext` + EF Core mappings for PostgreSQL.
- `pgvector` column mapping to `vector(1536)` for `text-embedding-ada-002`.
- SQL bootstrap script for extension, tables, and cosine ANN index.

## Similarity query shape (top 3 chunks)

```sql
SELECT c.id,
       c.document_id,
       c.chunk_index,
       c.content,
       c.source_locator,
       1 - (c.embedding_vector <=> @query_embedding) AS cosine_similarity
FROM document_chunks c
ORDER BY c.embedding_vector <=> @query_embedding
LIMIT 3;
```

> `<=>` is cosine distance when used with `vector_cosine_ops`.
