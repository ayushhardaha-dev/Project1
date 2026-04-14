-- Step 1 bootstrap for PostgreSQL + pgvector.
-- Run this once per database.
CREATE EXTENSION IF NOT EXISTS vector;

CREATE TABLE IF NOT EXISTS knowledge_documents (
    id uuid PRIMARY KEY,
    file_name varchar(512) NOT NULL,
    content_type varchar(200) NOT NULL,
    size_in_bytes bigint NOT NULL,
    uploaded_by varchar(200) NOT NULL,
    processing_status varchar(30) NOT NULL,
    created_utc timestamp with time zone NOT NULL,
    updated_utc timestamp with time zone NULL
);

CREATE TABLE IF NOT EXISTS document_chunks (
    id uuid PRIMARY KEY,
    document_id uuid NOT NULL REFERENCES knowledge_documents(id) ON DELETE CASCADE,
    chunk_index integer NOT NULL,
    content text NOT NULL,
    source_locator varchar(200) NOT NULL,
    embedding_vector vector(1536) NOT NULL,
    created_utc timestamp with time zone NOT NULL,
    updated_utc timestamp with time zone NULL
);

-- IVFFlat index for cosine similarity search.
-- Tune lists based on corpus size (e.g. sqrt(rows)).
CREATE INDEX IF NOT EXISTS ix_document_chunks_embedding_cosine
ON document_chunks
USING ivfflat (embedding_vector vector_cosine_ops)
WITH (lists = 100);

CREATE INDEX IF NOT EXISTS ix_document_chunks_document_id
ON document_chunks(document_id);
