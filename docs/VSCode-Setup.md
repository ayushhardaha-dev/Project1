# Use this repository in Visual Studio Code

If your goal is to work only in **VS Code** (not Visual Studio), follow these exact steps.

## 1) Install prerequisites

- **.NET SDK 8.x**
- **PostgreSQL 15+**
- **pgvector** extension in PostgreSQL
- **Visual Studio Code**

Recommended VS Code extensions (already declared in `.vscode/extensions.json`):

- `ms-dotnettools.csharp`
- `ms-azuretools.vscode-docker` (optional)
- `mtxr.sqltools` (optional for DB browsing)

## 2) Open the repository as a workspace

From your terminal at repo root:

```bash
code Project1.code-workspace
```

If you prefer, `code .` also works.

## 3) Restore and build

Open the VS Code terminal and run:

```bash
dotnet restore src/Backend/KnowledgeAgent.sln
dotnet build src/Backend/KnowledgeAgent.sln
```

Or use tasks:

1. `Ctrl/Cmd + Shift + P`
2. Run **Tasks: Run Build Task**
3. Select `dotnet: build backend`

## 4) Setup the PostgreSQL database

Create a database (example):

```sql
CREATE DATABASE knowledge_agent;
```

Run bootstrap script:

```bash
psql "Host=localhost;Port=5432;Database=knowledge_agent;Username=postgres;Password=postgres" -f sql/001_init_pgvector.sql
```

## 5) What works right now in Step 1

Current repo state is **architecture foundation** (Domain/Application/Infrastructure + SQL). There is not yet a runnable Web API host project.

So in VS Code today you can:

- edit domain and infrastructure code
- compile the solution
- inspect and run SQL bootstrap script

## 6) What comes next (Step 2)

Once we add `KnowledgeAgent.Api` (Presentation layer), VS Code debugging (`F5`) can be enabled with `.vscode/launch.json`.
