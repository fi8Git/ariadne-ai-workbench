# 06 - Local-First Storage

# Ariadne AI Workbench

**Status:** Draft v0.1  
**Project type:** Open-source local-first application  
**Primary platform:** .NET 10 MAUI Blazor Hybrid  
**Future platform:** Blazor Web application, if useful and/or commercializable  
**Repository name:** `ariadne-ai-workbench`  
**Solution name:** `Ariadne.sln`  
**Root namespace:** `Ariadne`  
**Related documents:** `00-project-brief.md`, `01-product-vision.md`, `02-functional-specification.md`, `03-technical-architecture.md`, `04-domain-model.md`, `05-methodology-workflow.md`

---

## 1. Purpose of this document

This document defines the **local-first storage strategy** for **Ariadne AI Workbench**.

It explains how Ariadne should store projects, datasets, profiling results, fundamental analysis answers, decision logs and reports on the user's machine, while keeping the architecture ready for a future Blazor Web and/or commercial version.

This document is intended to guide:

- implementation of `Ariadne.Infrastructure.Local`;
- application service interfaces in `Ariadne.Application`;
- project persistence and project loading;
- CSV import and dataset file storage;
- report generation and export;
- testing strategy for local persistence;
- Codex implementation tasks;
- future migration to web/cloud storage.

This document is **not** the domain model. Domain concepts belong in:

```text
04-domain-model.md
```

This document is **not** the UI specification. UI behavior belongs in:

```text
07-ui-ux-guidelines.md
```

---

## 2. Storage vision

Ariadne should be a **local-first methodology workbench**.

This means:

```text
User data stays local by default.
Projects can be opened offline.
Datasets are not uploaded by default.
The storage format is transparent enough to be inspected and backed up.
The application can later be adapted to a web/cloud backend without rewriting the domain model.
```

The storage system must support Ariadne's core product promise:

```text
Make AI/ML work easier to understand, document, reproduce and trust.
```

The storage system must therefore preserve:

- imported dataset references;
- immutable original files, when copied into the project;
- dataset versions;
- column profiles;
- user-reviewed variable definitions;
- fundamental analysis answers;
- methodological decisions;
- generated reports;
- project metadata;
- future preprocessing, modelling and evaluation runs.

---

## 3. Methodological alignment

The local-first storage model exists to support Ariadne's methodology, not merely to persist UI state.

The methodology behind Ariadne assumes that a dataset is only an **observed sample** of a broader population. The user must therefore document what the data represents, what its limitations are, what decisions were made, and what uncertainty remains.

The storage design must make the following ideas persistent:

```text
A dataset is not the phenomenon.
A dataset is a sample.
Profiles are computed observations.
Variable definitions are user-reviewed interpretations.
Context matters.
Unknowns matter.
Decisions need rationale.
Reports must be reproducible.
Models come after understanding.
```

This is why Ariadne stores both **technical facts** and **human reasoning**:

| Storage item | Why it matters |
|---|---|
| Original dataset file | Preserves what was actually imported. |
| Dataset hash | Confirms whether the file changed. |
| Import options | Makes parsing reproducible. |
| Column profile | Stores computed observations about variables. |
| Variable definition | Captures user interpretation and review. |
| Fundamental analysis answer | Captures context: What, Who, When, Where, How, Why. |
| Decision log entry | Explains why a choice was made. |
| Report snapshot | Produces a defensible project artifact. |

---

## 4. Local-first principles

Ariadne storage must follow these principles.

### 4.1 User ownership

The user owns their projects and datasets.

Ariadne must not require:

- an account;
- cloud sync;
- telemetry;
- API keys;
- remote storage;
- an internet connection.

### 4.2 Explicit persistence

The user should understand where project data lives.

Ariadne should support two storage modes:

```text
Managed workspace
Portable project folder
```

The managed workspace is convenient for the MVP. The portable project folder supports open-source transparency, backup and later collaboration workflows.

### 4.3 Separation of metadata and files

Ariadne should not store large raw datasets directly inside SQLite tables.

The recommended approach is:

```text
SQLite:
  Project metadata
  Workflow state
  Profiling results
  User answers
  Decision log
  Report metadata

File system:
  Original datasets
  Derived datasets
  Generated reports
  Exports
  Attachments
  Backups
  Temporary files
```

### 4.4 Immutability of raw imports

Original dataset files must be treated as immutable.

If a dataset is transformed, cleaned, split or preprocessed later, Ariadne must create a new dataset version rather than modifying the original import.

### 4.5 Reproducibility

A project should contain enough information to understand:

- which dataset was imported;
- how it was parsed;
- when it was imported;
- what profile was computed;
- which variables were reviewed;
- what assumptions were documented;
- what decisions were taken;
- which report was generated.

### 4.6 Portability

A project should eventually be movable between machines.

This implies:

- paths stored inside project storage should be relative to the project root;
- absolute paths should be limited to the application catalog;
- project folders should contain their own local database;
- generated reports should live inside the project folder;
- future `.ariadnepkg` export should be possible without rewriting storage.

### 4.7 Privacy by default

Ariadne must not leak dataset content.

By default:

- no dataset should be sent over the network;
- no telemetry should include project names, file names, column names or sample values;
- reports should not include raw rows unless the user explicitly asks;
- logs should avoid sensitive dataset values;
- preview caches should be purgeable.

### 4.8 Storage is infrastructure

Storage implementation belongs in `Ariadne.Infrastructure.Local`.

The following projects must not directly depend on SQLite or platform file APIs:

```text
Ariadne.Domain
Ariadne.Application
Ariadne.Analytics
Ariadne.SharedUi
```

---

## 5. Storage architecture overview

Ariadne should use a hybrid local storage model:

```text
Application catalog database
+
Self-contained project folders
```

### 5.1 Application catalog

The application catalog is a small local database stored under the application data directory.

It stores:

- recently opened projects;
- default workspace path;
- application preferences;
- project path pointers;
- last-opened timestamps;
- global non-sensitive settings.

It must not store:

- raw datasets;
- full dataset previews;
- fundamental analysis content;
- decision logs;
- project reports;
- user-sensitive dataset values.

### 5.2 Project folder

Each Ariadne project should be represented by a self-contained folder.

It stores:

- project manifest;
- project SQLite database;
- original dataset files;
- preview caches;
- derived files;
- reports;
- exports;
- attachments;
- backups.

### 5.3 Why this split

This split gives Ariadne three important properties:

| Property | Benefit |
|---|---|
| Fast application startup | The catalog can list projects without opening every project database. |
| Project portability | A project folder can be copied or zipped. |
| Future web compatibility | Application services can later target PostgreSQL/object storage instead of SQLite/files. |

---

## 6. Storage responsibilities by project

### 6.1 `Ariadne.Domain`

Must contain:

- entities;
- value objects;
- enums;
- invariants;
- domain behavior.

Must not contain:

- SQLite attributes;
- EF Core attributes;
- file paths;
- platform APIs;
- serialization concerns;
- DTOs for persistence.

### 6.2 `Ariadne.Application`

Must define storage abstractions:

```csharp
public interface IProjectCatalogStore
{
    Task<IReadOnlyList<ProjectCatalogItemDto>> ListRecentProjectsAsync(CancellationToken cancellationToken);
    Task RegisterProjectAsync(ProjectCatalogRegistrationDto registration, CancellationToken cancellationToken);
    Task RemoveProjectAsync(ProjectId projectId, CancellationToken cancellationToken);
}

public interface IProjectRepository
{
    Task<ProjectSnapshotDto?> LoadAsync(ProjectId projectId, CancellationToken cancellationToken);
    Task SaveAsync(ProjectSnapshotDto snapshot, CancellationToken cancellationToken);
}

public interface IDatasetFileStore
{
    Task<StoredDatasetFileDto> StoreOriginalAsync(StoreDatasetFileRequest request, CancellationToken cancellationToken);
    Task<Stream> OpenReadAsync(ProjectId projectId, DatasetVersionId versionId, CancellationToken cancellationToken);
}

public interface IReportStore
{
    Task<StoredReportDto> SaveMarkdownReportAsync(SaveReportRequest request, CancellationToken cancellationToken);
}
```

The exact names may evolve, but the principle must remain:

```text
Application defines ports.
Infrastructure.Local implements adapters.
```

### 6.3 `Ariadne.Analytics`

Must not know where data is stored.

It may receive:

- streams;
- parsed rows;
- column metadata;
- profiling requests;
- analysis options.

It returns:

- profiling results;
- statistics;
- warnings;
- diagnostics.

It must not open files directly by path unless that path is passed through an application-level abstraction for a specific reason.

### 6.4 `Ariadne.Infrastructure.Local`

Must implement:

- SQLite catalog storage;
- project SQLite storage;
- project folder creation;
- dataset file copying;
- hashing;
- report file writing;
- backup creation;
- temporary staging folders;
- local migration execution;
- storage diagnostics.

### 6.5 `Ariadne.SharedUi`

Must use application services only.

It must not:

- reference SQLite packages;
- call `File.ReadAllText` directly;
- build project paths;
- write report files;
- read dataset files directly.

### 6.6 `Ariadne.Maui`

Must provide platform bootstrapping:

- app data directory provider;
- file picker integration;
- save/open dialogs where needed;
- platform-specific service registrations;
- local workspace initialization.

The MAUI project is an application host, not the owner of storage logic.

---

## 7. Recommended local directory layout

### 7.1 Application data root

The MAUI host should expose a platform-specific application data root through an abstraction such as:

```csharp
public interface IAppDataDirectoryProvider
{
    string GetAppDataDirectory();
    string GetCacheDirectory();
}
```

A typical managed application layout should be:

```text
<AppData>/Ariadne/
  catalog/
    ariadne.catalog.sqlite
    ariadne.catalog.sqlite-wal
    ariadne.catalog.sqlite-shm
  settings/
    settings.json
  workspaces/
    default/
      projects/
  cache/
    previews/
    thumbnails/
    reports/
  temp/
    imports/
    exports/
  logs/
    ariadne.log
```

The exact operating-system path must not be hardcoded.

### 7.2 Default workspace

For the MVP, Ariadne can create a default managed workspace:

```text
<AppData>/Ariadne/workspaces/default/projects/
```

Each new project can be created under this folder unless the user selects a custom location.

### 7.3 Portable project folder

A project folder should look like this:

```text
<workspace>/ariadne-projects/<project-slug>-<project-short-id>/
  ariadne.project.json
  .ariadne/
    project.sqlite
    project.sqlite-wal
    project.sqlite-shm
    manifest.json
    backups/
      2026-01-15T103000Z/
        project.sqlite
        manifest.json
    locks/
      project.lock
  data/
    original/
      <dataset-version-id>/
        source.csv
        import.manifest.json
    preview/
      <dataset-version-id>/
        preview.json
    derived/
      .gitkeep
  reports/
    markdown/
      2026-01-15T104500Z-project-report.md
  exports/
    .gitkeep
  attachments/
    .gitkeep
```

### 7.4 Folder naming

Project folder names should be stable, readable and safe:

```text
<sanitized-project-name>-<short-project-id>
```

Example:

```text
customer-churn-analysis-a1b2c3d4
```

Rules:

- lowercase preferred;
- replace spaces with `-`;
- remove or replace unsafe file-system characters;
- include a short ID to avoid name collisions;
- never rely on folder name as the project identifier.

### 7.5 Project manifest

The project root should contain a human-readable manifest:

```json
{
  "format": "ariadne.project",
  "formatVersion": 1,
  "projectId": "6e381af0-7f25-49f8-92a3-7ff2e5526b21",
  "name": "Customer Churn Analysis",
  "createdAtUtc": "2026-01-15T10:30:00Z",
  "projectDatabase": ".ariadne/project.sqlite",
  "application": "Ariadne AI Workbench",
  "minimumSupportedVersion": "0.1.0"
}
```

The manifest is not the full project state. It is a discovery and compatibility file.

The SQLite project database remains the source of truth for structured project state.

---

## 8. Storage modes

### 8.1 Managed workspace mode

In managed workspace mode, Ariadne chooses the storage location automatically under the application data directory.

Advantages:

- simple onboarding;
- no file-system decisions for the user;
- reliable write permissions;
- easier MVP implementation.

Limitations:

- less visible to the user;
- less convenient for manual backups;
- less natural for Git-based documentation workflows.

### 8.2 Portable project mode

In portable project mode, the user selects a folder.

Advantages:

- easier backup;
- easier sharing;
- easier future `.ariadnepkg` export;
- easier use in a Git repository, provided raw data is ignored;
- more transparent open-source workflow.

Limitations:

- file permissions may vary by platform;
- user may move or delete folders;
- catalog pointers may become stale;
- mobile platforms may have restrictions.

### 8.3 MVP recommendation

The MVP should implement managed workspace mode first, but its internal layout must already look like a portable project folder.

This avoids rework later.

```text
MVP behavior:
  Create projects under the managed default workspace.

Architecture requirement:
  Store each project as a self-contained folder.
```

---

## 9. SQLite strategy

### 9.1 Why SQLite

SQLite is appropriate for Ariadne's open-source local-first MVP because it is:

- embedded;
- local;
- lightweight;
- cross-platform;
- suitable for structured project metadata;
- easy to back up as a file;
- compatible with a repository pattern;
- sufficient for single-user desktop workflows.

### 9.2 Why not store everything in SQLite

SQLite should not be used as a blob dump for large raw datasets in the MVP.

Reasons:

- raw CSV files can be large;
- users may want to inspect the original file;
- file-based datasets are easier to hash and preserve;
- project folders remain transparent;
- future object storage migration is easier;
- avoiding large blobs keeps database backups faster.

### 9.3 Catalog database

The catalog database should be located under:

```text
<AppData>/Ariadne/catalog/ariadne.catalog.sqlite
```

It should contain tables such as:

```text
CatalogProjects
CatalogSettings
CatalogStorageEvents
```

### 9.4 Project database

Each project should contain its own database:

```text
<project-root>/.ariadne/project.sqlite
```

It should contain the full structured state for that project.

### 9.5 EF Core usage

Ariadne may use EF Core with the SQLite provider inside `Ariadne.Infrastructure.Local`.

Rules:

```text
EF Core DbContext belongs only to Infrastructure.Local.
Domain entities must not be EF entities by default.
Persistence models should map to domain snapshots or DTOs.
Application services should depend on interfaces, not DbContext.
```

### 9.6 SQLite pragmas

When opening a local project database, Infrastructure.Local may configure SQLite pragmas where appropriate:

```sql
PRAGMA foreign_keys = ON;
PRAGMA journal_mode = WAL;
PRAGMA busy_timeout = 5000;
```

Notes:

- `foreign_keys = ON` helps preserve integrity;
- WAL can improve reliability and concurrency for local reads/writes;
- `busy_timeout` helps avoid immediate failure if the DB is temporarily locked.

These settings should be tested on supported platforms.

---

## 10. Catalog database schema

The catalog database is intentionally small.

### 10.1 `CatalogProjects`

Purpose: list known projects without opening every project database.

Suggested columns:

| Column | Type | Required | Notes |
|---|---:|---:|---|
| `ProjectId` | TEXT | Yes | Domain project ID. |
| `Name` | TEXT | Yes | Display name. |
| `ProjectRootPath` | TEXT | Yes | Absolute path is allowed here. |
| `ManifestRelativePath` | TEXT | Yes | Usually `ariadne.project.json`. |
| `ProjectDatabaseRelativePath` | TEXT | Yes | Usually `.ariadne/project.sqlite`. |
| `CreatedAtUtc` | TEXT | Yes | ISO 8601 UTC. |
| `LastOpenedAtUtc` | TEXT | No | Updated when opened. |
| `LastKnownAppVersion` | TEXT | No | Useful for migration diagnostics. |
| `IsPinned` | INTEGER | Yes | 0/1. |
| `IsMissing` | INTEGER | Yes | Set when folder cannot be found. |

### 10.2 `CatalogSettings`

Purpose: store non-sensitive app-level settings.

Suggested columns:

| Column | Type | Required | Notes |
|---|---:|---:|---|
| `Key` | TEXT | Yes | Unique setting name. |
| `Value` | TEXT | No | JSON or string value. |
| `UpdatedAtUtc` | TEXT | Yes | Last update. |

Possible keys:

```text
DefaultWorkspacePath
ThemePreference
LastOpenedProjectId
PreviewRowLimit
ReportDefaultFormat
```

### 10.3 `CatalogStorageEvents`

Purpose: store non-sensitive storage diagnostics.

Suggested columns:

| Column | Type | Required | Notes |
|---|---:|---:|---|
| `StorageEventId` | TEXT | Yes | Unique ID. |
| `EventType` | TEXT | Yes | Example: `ProjectCreated`, `ProjectMissing`. |
| `ProjectId` | TEXT | No | Optional. |
| `Message` | TEXT | No | Must not include raw data values. |
| `CreatedAtUtc` | TEXT | Yes | Timestamp. |

---

## 11. Project database schema

The project database stores the structured state of one Ariadne project.

The following schema is a conceptual target. Codex should implement it incrementally according to the roadmap.

### 11.1 `ProjectInfo`

Purpose: store the core project metadata.

Suggested columns:

| Column | Type | Required | Notes |
|---|---:|---:|---|
| `ProjectId` | TEXT | Yes | Primary key. |
| `Name` | TEXT | Yes | Display name. |
| `Slug` | TEXT | Yes | Safe folder-friendly name. |
| `Description` | TEXT | No | User-provided. |
| `CreatedAtUtc` | TEXT | Yes | UTC. |
| `UpdatedAtUtc` | TEXT | Yes | UTC. |
| `MethodologyVersion` | TEXT | Yes | Example: `0.1`. |
| `StorageSchemaVersion` | INTEGER | Yes | Current project DB schema. |

Invariant:

```text
ProjectInfo.ProjectId must match ariadne.project.json projectId.
```

### 11.2 `WorkflowSteps`

Purpose: persist methodology progress.

Suggested columns:

| Column | Type | Required | Notes |
|---|---:|---:|---|
| `WorkflowStepId` | TEXT | Yes | Unique ID. |
| `ProjectId` | TEXT | Yes | FK. |
| `StepKey` | TEXT | Yes | Example: `Dataset`, `Understand`, `Report`. |
| `Status` | TEXT | Yes | `NotStarted`, `InProgress`, `Completed`, `Blocked`. |
| `CompletionRatio` | REAL | No | 0 to 1. |
| `UpdatedAtUtc` | TEXT | Yes | UTC. |

### 11.3 `Datasets`

Purpose: represent datasets known by the project.

Suggested columns:

| Column | Type | Required | Notes |
|---|---:|---:|---|
| `DatasetId` | TEXT | Yes | Primary key. |
| `ProjectId` | TEXT | Yes | FK. |
| `DisplayName` | TEXT | Yes | User-facing name. |
| `Description` | TEXT | No | User-provided. |
| `CreatedAtUtc` | TEXT | Yes | UTC. |
| `UpdatedAtUtc` | TEXT | Yes | UTC. |
| `CurrentVersionId` | TEXT | No | FK to current dataset version. |

### 11.4 `DatasetVersions`

Purpose: preserve imported and derived dataset versions.

Suggested columns:

| Column | Type | Required | Notes |
|---|---:|---:|---|
| `DatasetVersionId` | TEXT | Yes | Primary key. |
| `DatasetId` | TEXT | Yes | FK. |
| `VersionNumber` | INTEGER | Yes | Starts at 1. |
| `Kind` | TEXT | Yes | `Original`, `Imported`, `Derived`, `Prepared`, future. |
| `StoredRelativePath` | TEXT | Yes | Relative to project root. |
| `OriginalFileName` | TEXT | Yes | Sanitized display. |
| `ContentSha256` | TEXT | Yes | File integrity. |
| `FileSizeBytes` | INTEGER | Yes | Original file size. |
| `DetectedFormat` | TEXT | Yes | Example: `Csv`. |
| `RowCount` | INTEGER | No | Filled after profiling/import. |
| `ColumnCount` | INTEGER | No | Filled after profiling/import. |
| `ImportStatus` | TEXT | Yes | `Pending`, `Imported`, `Profiled`, `Failed`. |
| `CreatedAtUtc` | TEXT | Yes | UTC. |

### 11.5 `DatasetImportOptions`

Purpose: make dataset parsing reproducible.

Suggested columns:

| Column | Type | Required | Notes |
|---|---:|---:|---|
| `DatasetImportOptionsId` | TEXT | Yes | Primary key. |
| `DatasetVersionId` | TEXT | Yes | FK. |
| `Format` | TEXT | Yes | `Csv` in MVP. |
| `Encoding` | TEXT | No | Example: `utf-8`. |
| `Delimiter` | TEXT | No | Example: `,`, `;`, `\t`. |
| `HasHeader` | INTEGER | Yes | 0/1. |
| `QuoteCharacter` | TEXT | No | Example: `"`. |
| `DecimalSeparator` | TEXT | No | Future. |
| `Culture` | TEXT | No | Future. |
| `CreatedAtUtc` | TEXT | Yes | UTC. |

### 11.6 `ColumnProfiles`

Purpose: store computed technical profile for each column.

Suggested columns:

| Column | Type | Required | Notes |
|---|---:|---:|---|
| `ColumnProfileId` | TEXT | Yes | Primary key. |
| `DatasetVersionId` | TEXT | Yes | FK. |
| `ColumnName` | TEXT | Yes | Original column name. |
| `ColumnIndex` | INTEGER | Yes | Zero-based. |
| `InferredDataType` | TEXT | Yes | `Text`, `Integer`, `Decimal`, `Boolean`, `DateTime`, etc. |
| `InferredVariableKind` | TEXT | Yes | `Discrete`, `Continuous`, `Text`, `Date`, `Unknown`. |
| `ReviewStatus` | TEXT | Yes | `AutoInferred`, `UserReviewed`, `NeedsReview`. |
| `RowCount` | INTEGER | Yes | Total rows considered. |
| `MissingCount` | INTEGER | Yes | Missing values. |
| `MissingRatio` | REAL | Yes | 0 to 1. |
| `DistinctCount` | INTEGER | No | May be approximate later. |
| `MinValue` | TEXT | No | Serialized for display. |
| `MaxValue` | TEXT | No | Serialized for display. |
| `Mean` | REAL | No | Numeric columns only. |
| `Median` | REAL | No | Numeric columns only. |
| `StandardDeviation` | REAL | No | Numeric columns only. |
| `Variance` | REAL | No | Numeric columns only. |
| `Q1` | REAL | No | Numeric columns only. |
| `Q3` | REAL | No | Numeric columns only. |
| `Iqr` | REAL | No | Numeric columns only. |
| `OutlierCountIqr` | INTEGER | No | Numeric columns only. |
| `CreatedAtUtc` | TEXT | Yes | UTC. |

### 11.7 `ColumnValueCounts`

Purpose: store top values for discrete/text columns without storing all data.

Suggested columns:

| Column | Type | Required | Notes |
|---|---:|---:|---|
| `ColumnValueCountId` | TEXT | Yes | Primary key. |
| `ColumnProfileId` | TEXT | Yes | FK. |
| `DisplayValue` | TEXT | Yes | Redaction may be added later. |
| `Count` | INTEGER | Yes | Occurrence count. |
| `Ratio` | REAL | Yes | Occurrence ratio. |
| `Rank` | INTEGER | Yes | 1 = most common. |

MVP limit:

```text
Store top 25 values per column by default.
```

The limit should be configurable later.

### 11.8 `VariableDefinitions`

Purpose: store user-reviewed meaning of variables.

Suggested columns:

| Column | Type | Required | Notes |
|---|---:|---:|---|
| `VariableDefinitionId` | TEXT | Yes | Primary key. |
| `DatasetVersionId` | TEXT | Yes | FK. |
| `ColumnProfileId` | TEXT | Yes | FK. |
| `VariableName` | TEXT | Yes | Usually original column name. |
| `UserDescription` | TEXT | No | Meaning of the variable. |
| `UserVariableKind` | TEXT | No | User-reviewed kind. |
| `Unit` | TEXT | No | Example: `kg`, `EUR`, `seconds`. |
| `Role` | TEXT | No | `Feature`, `TargetCandidate`, `Identifier`, `Ignored`, future. |
| `QualityNotes` | TEXT | No | User notes. |
| `ReviewedAtUtc` | TEXT | No | Set when reviewed. |

### 11.9 `FundamentalAnalysisAnswers`

Purpose: store What/Who/When/Where/How/Why answers.

Suggested columns:

| Column | Type | Required | Notes |
|---|---:|---:|---|
| `AnswerId` | TEXT | Yes | Primary key. |
| `ProjectId` | TEXT | Yes | FK. |
| `DatasetId` | TEXT | No | Optional scope. |
| `QuestionKey` | TEXT | Yes | Example: `What.VariableMeaning`. |
| `QuestionGroup` | TEXT | Yes | `What`, `Who`, `When`, `Where`, `How`, `Why`. |
| `AnswerText` | TEXT | No | User answer. |
| `Status` | TEXT | Yes | `Unknown`, `Draft`, `Answered`, `NeedsReview`. |
| `ConfidenceLevel` | TEXT | No | `Low`, `Medium`, `High`. |
| `SourceNote` | TEXT | No | Where the answer came from. |
| `UpdatedAtUtc` | TEXT | Yes | UTC. |

### 11.10 `DecisionLogEntries`

Purpose: store the user's reasoning over time.

Suggested columns:

| Column | Type | Required | Notes |
|---|---:|---:|---|
| `DecisionLogEntryId` | TEXT | Yes | Primary key. |
| `ProjectId` | TEXT | Yes | FK. |
| `DatasetId` | TEXT | No | Optional. |
| `DatasetVersionId` | TEXT | No | Optional. |
| `WorkflowStep` | TEXT | Yes | Example: `Understand`, `Report`. |
| `Title` | TEXT | Yes | Short decision title. |
| `Rationale` | TEXT | Yes | Why the choice was made. |
| `Impact` | TEXT | No | Expected effect. |
| `Risk` | TEXT | No | Known limitation. |
| `CreatedAtUtc` | TEXT | Yes | UTC. |
| `CreatedBy` | TEXT | No | Future multi-user compatibility. |

### 11.11 `Reports`

Purpose: track generated report artifacts.

Suggested columns:

| Column | Type | Required | Notes |
|---|---:|---:|---|
| `ReportId` | TEXT | Yes | Primary key. |
| `ProjectId` | TEXT | Yes | FK. |
| `ReportKind` | TEXT | Yes | `MethodologySummary`, future. |
| `Format` | TEXT | Yes | `Markdown` in MVP. |
| `StoredRelativePath` | TEXT | Yes | Relative to project root. |
| `GeneratedAtUtc` | TEXT | Yes | UTC. |
| `GeneratorVersion` | TEXT | Yes | App/report generator version. |
| `ContentSha256` | TEXT | No | Integrity of report file. |

### 11.12 `ProjectStorageEvents`

Purpose: provide local, non-sensitive diagnostics.

Suggested columns:

| Column | Type | Required | Notes |
|---|---:|---:|---|
| `StorageEventId` | TEXT | Yes | Primary key. |
| `ProjectId` | TEXT | Yes | FK. |
| `EventType` | TEXT | Yes | Example: `DatasetImported`. |
| `Severity` | TEXT | Yes | `Info`, `Warning`, `Error`. |
| `Message` | TEXT | Yes | Must avoid raw data values. |
| `CreatedAtUtc` | TEXT | Yes | UTC. |

---

## 12. Project creation flow

When creating a project, Ariadne should follow this sequence.

```text
1. Validate project name.
2. Generate ProjectId.
3. Resolve project root folder.
4. Create project directory structure in a staging folder.
5. Create project manifest.
6. Create project SQLite database.
7. Apply migrations.
8. Insert ProjectInfo.
9. Insert initial WorkflowSteps.
10. Move staging folder to final project path.
11. Register project in catalog database.
12. Return ProjectSummary to UI.
```

### 12.1 Staging folder rule

Project creation should use a staging folder to avoid half-created projects.

Example:

```text
<AppData>/Ariadne/temp/project-create/<operation-id>/
```

Only after successful creation should the folder be moved to its final location.

### 12.2 Initial workflow state

A new project should initialize these workflow steps:

| Step | Initial status |
|---|---|
| `Project` | `Completed` |
| `Dataset` | `NotStarted` |
| `Understand` | `NotStarted` |
| `Report` | `NotStarted` |

Future steps may be initialized as `Locked` or `NotAvailable` until their modules exist.

---

## 13. Project opening flow

When opening a project, Ariadne should:

```text
1. Resolve project path from catalog or user selection.
2. Check that ariadne.project.json exists.
3. Read manifest.
4. Check format and minimum supported version.
5. Open project SQLite database.
6. Apply pending migrations if allowed.
7. Verify ProjectInfo.ProjectId matches manifest.projectId.
8. Load project summary.
9. Update catalog LastOpenedAtUtc.
10. Return project context to application services/UI.
```

### 13.1 Missing project folders

If a catalog entry points to a missing folder:

- mark `CatalogProjects.IsMissing = true`;
- show a recoverable UI state;
- allow the user to locate the folder manually;
- do not delete the catalog entry automatically.

### 13.2 Project version too new

If a project was created by a newer Ariadne version and cannot be safely opened:

- do not attempt migration;
- show an explicit compatibility error;
- do not modify the project database;
- suggest updating Ariadne.

---

## 14. CSV import storage flow

The MVP import format is CSV.

Ariadne should store enough information to reproduce parsing and profiling decisions.

### 14.1 Import sequence

```text
1. User selects a CSV file.
2. Infrastructure receives a readable stream or file handle.
3. Create import operation ID.
4. Create staging import folder.
5. Copy source file to staging while computing SHA-256.
6. Detect or receive import options:
   - encoding
   - delimiter
   - header presence
   - quote character
7. Parse header and sample rows.
8. Create dataset record.
9. Create dataset version record.
10. Store import options.
11. Move staged file into data/original/<dataset-version-id>/.
12. Run profiling, ideally streaming.
13. Store column profiles and value counts.
14. Store preview cache.
15. Add decision log entry: dataset imported.
16. Update workflow step statuses.
17. Commit transaction.
```

### 14.2 Import transaction boundary

The import touches both file system and database.

Because file-system operations and SQLite transactions are not a single distributed transaction, use this safety model:

```text
File copy into staging first.
Database transaction second.
Final move after successful metadata creation.
Cleanup on failure.
```

If final move succeeds but DB commit fails, the cleanup routine should remove unreferenced staged files or mark the import as failed.

### 14.3 Original file copy vs external reference

MVP recommendation:

```text
Copy original CSV into the project folder by default.
```

Reason:

- reproducibility;
- project portability;
- dataset hash stability;
- avoids broken external paths;
- makes reports more defensible.

Future option:

```text
Reference external file without copying.
```

This may be useful for very large datasets, but it introduces risk because the external file can be moved, modified or deleted.

### 14.4 File hash

Ariadne must compute a SHA-256 hash for every imported original dataset file.

Use cases:

- detect changed external files later;
- verify project integrity;
- identify duplicate imports;
- support reproducible reports;
- support future package export/import.

### 14.5 Import manifest

Each imported dataset version should have a JSON manifest next to the stored file.

Example:

```json
{
  "format": "ariadne.dataset.import",
  "formatVersion": 1,
  "datasetId": "d7de4e67-fd83-4ecf-a797-73ef7fc0a34d",
  "datasetVersionId": "0f1bc6eb-5ecb-4d8c-a02b-cfcb94ff4c45",
  "originalFileName": "customers.csv",
  "storedFileName": "source.csv",
  "storedRelativePath": "data/original/0f1bc6eb-5ecb-4d8c-a02b-cfcb94ff4c45/source.csv",
  "contentSha256": "...",
  "fileSizeBytes": 1248576,
  "importedAtUtc": "2026-01-15T10:40:00Z",
  "importOptions": {
    "format": "Csv",
    "encoding": "utf-8",
    "delimiter": ",",
    "hasHeader": true,
    "quoteCharacter": "\""
  }
}
```

The database remains the source of truth. The manifest helps transparency and recovery.

---

## 15. Dataset preview storage

The UI needs to show imported data previews, but Ariadne should avoid storing unnecessary raw data.

### 15.1 Preview strategy

MVP recommendation:

```text
Store only a limited preview cache.
```

Default limits:

| Item | Default limit |
|---|---:|
| Preview rows | 100 |
| Preview columns | All columns for MVP, virtualized in UI later. |
| Sample values per column | 20 |
| Top value counts | 25 |

### 15.2 Preview file

Preview data may be stored as JSON under:

```text
data/preview/<dataset-version-id>/preview.json
```

Example structure:

```json
{
  "datasetVersionId": "0f1bc6eb-5ecb-4d8c-a02b-cfcb94ff4c45",
  "rowLimit": 100,
  "columns": ["customer_id", "age", "country", "churned"],
  "rows": [
    ["C001", "42", "France", "false"],
    ["C002", "31", "Belgium", "true"]
  ]
}
```

### 15.3 Sensitive preview data

Preview caches may contain sensitive information.

Ariadne should eventually provide:

- a setting to disable preview persistence;
- a command to purge preview caches;
- a report setting to exclude sample values;
- a warning when exporting projects with preview caches included.

MVP can store preview locally but must not include raw preview rows in generated reports by default.

---

## 16. Column profile storage

Column profiles are computed technical observations.

They should be stored as structured records because they drive:

- dataset overview;
- variable review;
- warnings;
- methodology progress;
- report generation;
- future preprocessing suggestions.

### 16.1 Computed vs reviewed

Ariadne must distinguish:

```text
Computed profile:
  What the profiler inferred from data.

Variable definition:
  What the user reviewed and confirmed.
```

Example:

```text
Column name: age
Inferred type: Integer
Inferred variable kind: Continuous or Discrete depending heuristics
User-reviewed kind: Continuous
Unit: years
Role: Feature candidate
```

### 16.2 Profile versioning

A profile belongs to a dataset version.

If the dataset changes, a new dataset version should get a new profile.

Never silently reuse a profile from another dataset version unless the content hash is identical and the user/application explicitly accepts it.

### 16.3 Numeric profile fields

Numeric columns should support:

```text
count
missing count
missing ratio
min
max
mean
median
variance
standard deviation
Q1
Q3
IQR
IQR outlier count
```

### 16.4 Discrete/text profile fields

Discrete and text columns should support:

```text
count
missing count
missing ratio
distinct count
top value counts
sample values
min length
max length
```

### 16.5 Date/time profile fields

Date/time columns should support:

```text
count
missing count
missing ratio
min date
max date
detected date formats
```

This can be implemented after the MVP if date detection is deferred.

---

## 17. Fundamental analysis storage

Fundamental analysis is central to Ariadne.

It captures the meaning and context of the dataset.

### 17.1 The six groups

Store answers for:

```text
What
Who
When
Where
How
Why
```

These groups should be stable keys in the methodology workflow.

### 17.2 Answer status

Each answer should have a status:

```text
Unknown
Draft
Answered
NeedsReview
NotApplicable
```

This allows Ariadne to treat missing knowledge as explicit information rather than hiding it.

### 17.3 Confidence level

Each answer may optionally include:

```text
Low
Medium
High
```

This is not statistical confidence. It is the user's confidence in the contextual answer.

### 17.4 Source note

Each answer may include a source note:

```text
Dataset documentation
Client interview
Public source
User assumption
Unknown
```

### 17.5 Report behavior

Generated reports should include:

- answered fundamental analysis sections;
- unknown sections;
- low-confidence answers;
- source notes;
- limitations.

Unknowns should not be hidden. They increase credibility when clearly stated.

---

## 18. Decision log storage

The decision log is one of Ariadne's most important storage objects.

It captures why the user made choices.

### 18.1 Required fields

Every decision log entry should include:

```text
Title
Workflow step
Rationale
CreatedAtUtc
```

Optional fields:

```text
Dataset scope
Impact
Risk
Alternative considered
Evidence reference
```

### 18.2 Automatic entries

Ariadne may create automatic decision entries for important events:

```text
Project created
Dataset imported
Dataset profiled
Report generated
```

Automatic entries must be clearly marked as automatic if needed.

### 18.3 User entries

Users should be able to add manual entries for:

```text
Why a dataset was selected
Why a column was ignored
Why a variable was reviewed as continuous/discrete
Why missing answers remain unknown
Why the project is ready or not ready for modelling
```

### 18.4 Future decision types

Future modules should log decisions for:

```text
Outlier handling
NaN handling
Encoding choices
Feature engineering choices
Feature selection
Train/test split
Model choice
Metric choice
Applicability statement
```

---

## 19. Report storage

Reports are generated artifacts.

They should be reproducible from project state, but storing generated report files is still useful for sharing and audit.

### 19.1 MVP report format

The MVP report format is Markdown.

Stored under:

```text
reports/markdown/<timestamp>-project-report.md
```

Example:

```text
reports/markdown/2026-01-15T104500Z-project-report.md
```

### 19.2 Report metadata

The project database should store report metadata:

```text
ReportId
ProjectId
ReportKind
Format
StoredRelativePath
GeneratedAtUtc
GeneratorVersion
ContentSha256
```

### 19.3 Report source of truth

The report is not the source of truth.

The source of truth is:

```text
Project database
+
Dataset files
+
Project manifests
```

The report is a snapshot generated from those sources.

### 19.4 Report privacy

By default, reports should include:

- project summary;
- dataset metadata;
- column-level profiles;
- missing value statistics;
- outlier summaries;
- fundamental analysis answers;
- decision log entries;
- limitations;
- next steps.

By default, reports should not include:

- raw data rows;
- full unique value lists;
- sensitive sample values;
- absolute file paths.

---

## 20. Backup strategy

### 20.1 MVP backup behavior

The MVP should at minimum support safe writes and avoid destructive operations.

Before risky operations, Ariadne may create metadata backups under:

```text
<project-root>/.ariadne/backups/<timestamp>/
```

Example:

```text
.ariadne/backups/2026-01-15T110000Z/project.sqlite
.ariadne/backups/2026-01-15T110000Z/manifest.json
```

### 20.2 What to back up

MVP backups should include:

- project SQLite database;
- project manifest;
- import manifests.

Large raw dataset files should not be duplicated in every automatic backup by default.

### 20.3 Backup triggers

Suggested backup triggers:

```text
Before schema migration
Before destructive dataset deletion
Before project export
Before bulk decision-log rewrite
```

### 20.4 Retention

MVP can use a simple retention policy:

```text
Keep latest 5 automatic backups per project.
```

Future versions can make this configurable.

---

## 21. Export and import strategy

### 21.1 Future `.ariadnepkg`

A future export format can be a zip archive:

```text
project-name.ariadnepkg
```

Internally:

```text
ariadne.project.json
.ariadne/project.sqlite
data/original/...
reports/...
exports/...
```

### 21.2 MVP export

The MVP should prioritize Markdown report export.

Full project export can be deferred, but storage layout must not block it.

### 21.3 Export privacy warning

When exporting a full project package later, Ariadne must warn users that the package may include:

- original datasets;
- preview caches;
- column sample values;
- user notes;
- decision logs.

### 21.4 Import package validation

Future import should verify:

```text
Manifest format
Project database presence
Schema compatibility
Dataset file hashes
No path traversal in zip entries
```

---

## 22. Deletion and retention

### 22.1 Delete project

Deleting a project should be explicit.

Recommended behavior:

```text
Remove catalog entry.
Move project folder to trash/recycle if platform supports it.
Otherwise ask for confirmation before permanent deletion.
```

MVP can implement catalog removal first and defer physical deletion.

### 22.2 Remove from recent projects

Users should be able to remove a project from Ariadne's catalog without deleting the project folder.

This is safer and should be the default action for missing projects.

### 22.3 Delete dataset

Deleting a dataset should be treated as destructive.

Rules:

- require confirmation;
- create metadata backup first;
- remove dataset database records;
- remove associated files;
- add decision log entry if appropriate;
- update workflow state.

MVP can defer full dataset deletion if needed.

### 22.4 Purge caches

Ariadne should eventually provide a command:

```text
Purge local caches
```

It should remove:

- preview caches;
- report rendering caches;
- thumbnails;
- temporary import files;
- old logs.

It must not remove:

- original dataset files;
- project database;
- generated reports;
- decision logs.

---

## 23. Integrity and validation

### 23.1 Project integrity checks

Ariadne should be able to run a project integrity check.

Checks:

```text
Project manifest exists.
Project database exists.
ProjectId matches between manifest and database.
Referenced dataset files exist.
Dataset hashes match stored hashes.
Report files referenced by DB exist.
Foreign key relationships are valid.
Storage schema version is supported.
```

### 23.2 Dataset integrity checks

For each dataset version:

```text
StoredRelativePath resolves under project root.
File exists.
SHA-256 matches stored ContentSha256.
Import manifest exists.
Import manifest matches DB metadata.
```

### 23.3 Path traversal protection

All relative paths stored in a project database must be validated before use.

Rules:

- normalize paths;
- reject paths containing `..` that escape project root;
- reject absolute paths inside project DB unless explicitly allowed by a future external reference feature;
- never trust paths from imported package files without validation.

### 23.4 Schema version validation

The project manifest and project database must expose storage schema version information.

Ariadne must know whether it can:

```text
Open project safely
Migrate project safely
Refuse project because it is too new
```

---

## 24. Migrations

### 24.1 Migration ownership

Migrations belong to `Ariadne.Infrastructure.Local`.

They must not affect domain classes directly.

### 24.2 Migration rules

Every schema change must:

- be represented by a migration;
- be tested against an existing sample database;
- preserve data where possible;
- update `StorageSchemaVersion`;
- create a backup before applying to user projects.

### 24.3 Startup migration policy

For MVP:

```text
Apply catalog database migrations automatically.
For project database migrations, create a backup first, then migrate.
```

Future versions may offer a manual migration confirmation for major versions.

### 24.4 Migration testing

Tests should include:

```text
Create v0 database.
Seed sample project.
Apply latest migration.
Load project through repository.
Verify data preserved.
```

---

## 25. Concurrency model

### 25.1 MVP concurrency assumption

The MVP is a single-user local application.

Assumptions:

- one Ariadne process edits a project at a time;
- no real-time collaboration;
- no cloud sync;
- no multi-user locking.

### 25.2 Project lock file

A simple lock file can be introduced later:

```text
<project-root>/.ariadne/locks/project.lock
```

It may contain:

```json
{
  "processId": 12345,
  "machineName": "desktop-01",
  "openedAtUtc": "2026-01-15T10:30:00Z",
  "applicationVersion": "0.1.0"
}
```

MVP can defer lock enforcement.

### 25.3 Multi-window behavior

If the same project is opened twice, Ariadne should avoid conflicting writes.

MVP recommendation:

```text
Prevent multiple edit sessions for the same project within one app instance.
```

---

## 26. Performance and scalability

### 26.1 MVP target

The MVP should support common CSV workflows without attempting to be a big data platform.

Recommended behavior:

- stream CSV parsing where possible;
- limit preview rows;
- limit stored top values;
- avoid loading entire large files into memory;
- avoid storing full raw datasets inside SQLite;
- show progress for import/profiling operations.

### 26.2 Soft limits

Initial soft limits can be documented and enforced gently:

| Item | Suggested MVP value |
|---|---:|
| Preview rows | 100 |
| Top values per column | 25 |
| Sample values per column | 20 |
| Max report raw rows | 0 by default |
| Import progress threshold | Show progress for files over 10 MB |

A hard file size limit can be deferred until real testing.

### 26.3 Large datasets

For larger datasets, Ariadne should eventually support:

- streaming profile computation;
- sampled profiling;
- external file references;
- warning when exact distinct counts are expensive;
- approximate algorithms for distinct counts;
- DuckDB or other analytical backend if needed.

MVP should stay simple and reliable.

---

## 27. Logging

### 27.1 Local logs

Ariadne may write local logs under:

```text
<AppData>/Ariadne/logs/
```

Logs should help diagnose:

- failed imports;
- missing project folders;
- migration failures;
- report generation failures;
- database access errors.

### 27.2 Sensitive data rules

Logs must not include:

- raw dataset rows;
- full sample values;
- secrets;
- access tokens;
- full file content;
- personal data from datasets.

Logs may include:

- project ID;
- operation ID;
- error type;
- sanitized file name;
- file size;
- elapsed time;
- stack trace in development builds.

### 27.3 Telemetry

No telemetry in the open-source MVP unless explicitly added later with opt-in consent.

---

## 28. Security and privacy

### 28.1 Local storage is not encryption

Local-first does not mean encrypted.

By default, files stored in the project folder can be read by anyone with access to the user's file system.

Ariadne should communicate this clearly.

### 28.2 Encryption future

Future options:

```text
Project-level encryption
Encrypted dataset storage
Encrypted package export
OS credential store for keys
```

These are not MVP features.

### 28.3 Network isolation

The MVP must not send project data over the network.

Future features involving LLMs, cloud sync or hosted collaboration must be opt-in and clearly separated from local-first behavior.

### 28.4 File picker permissions

The MAUI host should handle platform-specific file selection and permissions.

Infrastructure.Local should receive a stream or safe path abstraction rather than assuming unrestricted access everywhere.

---

## 29. Error handling

### 29.1 Storage error categories

Ariadne should classify storage errors.

Suggested categories:

```text
ProjectNotFound
ProjectVersionUnsupported
ProjectDatabaseMissing
ProjectDatabaseCorrupt
DatasetFileMissing
DatasetHashMismatch
ImportFailed
MigrationFailed
ReportWriteFailed
PermissionDenied
InsufficientDiskSpace
```

### 29.2 User-facing messages

User-facing messages should be helpful and non-technical where possible.

Example:

```text
Ariadne found this project in your recent list, but the project folder no longer exists.
You can locate it manually, remove it from the recent list, or cancel.
```

### 29.3 Developer diagnostics

Developer diagnostics can include technical details, but must avoid sensitive data.

---

## 30. Web and commercial migration path

The storage design must support future web and/or commercial versions without changing the domain model.

### 30.1 Future web equivalents

| Local-first storage | Future web/commercial equivalent |
|---|---|
| Project folder | Workspace/project in backend. |
| Project SQLite DB | PostgreSQL database. |
| Original dataset file | Object storage. |
| Local catalog DB | User/project dashboard. |
| Local report file | Stored report artifact. |
| Decision log table | Shared team audit trail. |
| Fundamental answers | Collaborative project documentation. |

### 30.2 Application interfaces are the migration seam

The future web version should reuse:

```text
Ariadne.Domain
Ariadne.Application
Ariadne.Analytics
Ariadne.SharedUi where possible
```

It should replace:

```text
Ariadne.Infrastructure.Local
Ariadne.Maui
```

with:

```text
Ariadne.Infrastructure.Web
Ariadne.Web
```

### 30.3 Multi-tenant fields

Do not add multi-tenant complexity to the MVP domain.

Future infrastructure can add:

```text
WorkspaceId
TenantId
UserId
OrganizationId
Permissions
```

Storage DTOs can evolve, but domain should remain focused on methodology.

### 30.4 Sync is not MVP

Do not implement sync in the MVP.

Sync creates complex issues:

- conflict resolution;
- file versioning;
- identity;
- permissions;
- partial uploads;
- encryption;
- audit trails;
- offline edits.

Ariadne should first become a reliable local workbench.

---

## 31. Git and open-source workflows

Ariadne is open source, but user projects may contain sensitive data.

### 31.1 Suggested project `.gitignore`

When users store a project in a Git repository, Ariadne should suggest ignoring raw data by default.

Example:

```gitignore
# Ariadne raw data
data/original/
data/preview/
data/derived/

# Ariadne local internals
.ariadne/*.sqlite
.ariadne/*.sqlite-wal
.ariadne/*.sqlite-shm
.ariadne/backups/
.ariadne/locks/

# Optional: keep reports if desired
# reports/
```

This is a recommendation, not an application-level requirement.

### 31.2 Documentation-only export

For open-source project sharing, a user may want to share only:

```text
README
Report markdown
Decision log export
Variable dictionary
```

without raw datasets.

Future Ariadne should support documentation-only export.

---

## 32. Storage-related domain events

The domain may define events for important project changes, but infrastructure storage events must remain separate.

Potential domain events:

```text
ProjectCreated
DatasetRegistered
DatasetVersionAdded
VariableReviewed
FundamentalAnswerUpdated
DecisionLogged
ReportGenerated
```

Potential infrastructure events:

```text
ProjectFolderCreated
DatasetFileCopied
ProjectDatabaseMigrated
ReportFileWritten
PreviewCachePurged
```

Domain events describe methodology state.

Infrastructure events describe local storage operations.

---

## 33. Testing strategy

### 33.1 Unit tests

Unit tests should cover:

- path sanitization;
- project slug generation;
- relative path validation;
- manifest parsing;
- import option serialization;
- hash computation;
- domain snapshot mapping.

### 33.2 Integration tests

Integration tests should use temporary folders.

Required test scenarios:

```text
Create project in temp workspace.
Open project from catalog.
Import small CSV.
Verify original file copied.
Verify SHA-256 stored.
Verify dataset version created.
Verify column profiles stored.
Generate markdown report.
Verify report file exists.
Remove project from catalog without deleting folder.
Open project directly from manifest.
```

### 33.3 Migration tests

Migration tests should verify:

```text
Fresh database creation works.
Existing database can migrate.
Project backup is created before migration.
Data survives migration.
Unsupported future schema is rejected.
```

### 33.4 Failure tests

Failure tests should cover:

```text
Missing project folder.
Missing project database.
Missing dataset file.
Hash mismatch.
Invalid manifest.
Invalid relative path escaping project root.
Permission-denied write.
Failed import cleanup.
```

### 33.5 Platform tests

Where possible, test the storage abstraction on:

```text
Windows
macOS
Linux if non-MAUI helper tests are run separately
```

MAUI-specific file picker behavior can be tested manually or through platform-specific test harnesses later.

---

## 34. Codex implementation rules

Codex must follow these rules when implementing local-first storage.

### 34.1 Dependency rules

Do not add direct SQLite, EF Core or file-system dependencies to:

```text
Ariadne.Domain
Ariadne.Application
Ariadne.Analytics
Ariadne.SharedUi
```

`Ariadne.Infrastructure.Local` owns storage implementation.

### 34.2 Path rules

Do not store absolute paths inside the project database unless implementing a clearly documented external reference feature.

Use relative paths from project root.

### 34.3 Dataset rules

Do not store full raw dataset contents in SQLite for the MVP.

Store original files under:

```text
data/original/<dataset-version-id>/
```

### 34.4 Privacy rules

Do not add telemetry.

Do not log raw dataset values.

Do not include raw dataset rows in reports by default.

### 34.5 Migration rules

Do not change the SQLite schema without adding a migration and tests.

### 34.6 Report rules

Generated reports should be treated as artifacts.

Do not make generated Markdown the source of truth for project state.

### 34.7 UI rules

Do not make Razor components read or write project files directly.

Razor components call application services.

### 34.8 Incremental implementation

Implement storage in small slices:

```text
1. App data directory abstraction.
2. Catalog database.
3. Project folder creation.
4. Project manifest.
5. Project database.
6. Project repository.
7. CSV file store.
8. Dataset import metadata.
9. Profiling persistence.
10. Report file store.
```

---

## 35. MVP acceptance criteria

The local-first storage MVP is acceptable when the following are true.

### 35.1 Project storage

```text
A user can create a local project.
A project folder is created.
A project manifest is created.
A project SQLite database is created.
The project appears in the recent projects catalog.
The project can be closed and reopened.
```

### 35.2 Dataset storage

```text
A user can import a CSV file.
The original CSV is copied into the project folder.
The file hash is computed and stored.
Import options are stored.
A dataset and dataset version are stored.
A preview is generated and stored locally.
Column profiles are stored.
```

### 35.3 Fundamental analysis storage

```text
A user can answer What/Who/When/Where/How/Why questions.
Answers are persisted.
Unknown answers are persisted as unknown, not ignored.
Answers survive app restart.
```

### 35.4 Decision log storage

```text
A user can create a decision log entry.
Automatic entries can be created for project and dataset events.
Entries survive app restart.
```

### 35.5 Report storage

```text
A Markdown report can be generated.
The report is written under the project folder.
Report metadata is stored in the project database.
The report excludes raw rows by default.
```

### 35.6 Safety

```text
No network is required.
No telemetry is emitted.
No raw dataset rows are written to logs.
Project-relative paths cannot escape the project root.
```

---

## 36. Non-goals

The local-first MVP should not attempt to implement:

- cloud sync;
- user accounts;
- real-time collaboration;
- project encryption;
- full `.ariadnepkg` import/export;
- external dataset references;
- PostgreSQL backend;
- object storage backend;
- notebook storage;
- large-scale data lake support;
- automatic model training storage;
- experiment tracking beyond future placeholders;
- telemetry pipelines.

These features may come later, but they must not complicate the MVP.

---

## 37. Future extensions

### 37.1 Preprocessing storage

Future tables:

```text
PreprocessingPipelines
PreprocessingSteps
PreprocessingStepParameters
PreparedDatasetVersions
```

### 37.2 Train/test split storage

Future tables:

```text
DatasetSplits
DatasetSplitAssignments
SplitStrategies
```

Important rule:

```text
Train/test split must be reproducible.
Store random seed and split strategy.
```

### 37.3 Model experiment storage

Future tables:

```text
ExperimentRuns
ModelCandidates
ModelHyperparameters
TrainingRuns
EvaluationRuns
EvaluationMetrics
ConfidenceIntervals
ApplicabilityStatements
```

### 37.4 Python bridge storage

Future storage may include:

```text
Generated Python scripts
Environment manifests
Requirements files
Run outputs
Model artifacts
```

These should live under a dedicated folder:

```text
experiments/
```

Do not mix them with MVP project metadata.

### 37.5 Web backend storage

Future web version may use:

```text
PostgreSQL for structured project state
Object storage for datasets and reports
Background jobs for profiling
Identity provider for users
Workspace/team permissions
```

This should be implemented through new infrastructure adapters.

---

## 38. Suggested implementation milestones

### 38.1 Milestone LS-1: storage abstractions

Deliver:

```text
IAppDataDirectoryProvider
IProjectCatalogStore
IProjectRepository
IDatasetFileStore
IReportStore
```

Tests:

```text
Interfaces compile.
Application does not depend on Infrastructure.Local.
```

### 38.2 Milestone LS-2: catalog database

Deliver:

```text
Catalog DbContext or SQLite access layer
Catalog migrations
List/register/remove recent projects
```

Tests:

```text
Register project.
List project.
Mark missing project.
Remove from catalog.
```

### 38.3 Milestone LS-3: project folder and manifest

Deliver:

```text
Project folder creation
Manifest write/read
Project database creation
ProjectInfo insert
```

Tests:

```text
Create project in temp folder.
Open project from manifest.
Invalid manifest rejected.
```

### 38.4 Milestone LS-4: dataset file store

Deliver:

```text
Copy original CSV
Compute hash
Write import manifest
Store dataset version metadata
```

Tests:

```text
File copied.
Hash matches.
Relative path stored.
Path traversal rejected.
```

### 38.5 Milestone LS-5: profiling persistence

Deliver:

```text
ColumnProfiles table
ColumnValueCounts table
Preview cache
```

Tests:

```text
Import small CSV.
Profiles persisted.
Preview loaded.
Value counts limited.
```

### 38.6 Milestone LS-6: report storage

Deliver:

```text
Markdown report file store
Reports table
Report hash
```

Tests:

```text
Generate report.
File exists.
Metadata stored.
No raw rows by default.
```

---

## 39. Open decisions

The following decisions can remain open during early implementation.

| Decision | Default for now |
|---|---|
| EF Core vs lightweight SQLite access | EF Core acceptable in Infrastructure.Local. |
| Project database per project vs one global DB | Project DB per project + small catalog DB. |
| Copy original dataset or reference external file | Copy original by default. |
| Store preview in SQLite or JSON file | JSON file acceptable; metadata in SQLite. |
| Store sample values by default | Store limited local preview; exclude from reports. |
| Encryption | Not MVP. |
| Full project package export | Not MVP. |
| Sync | Not MVP. |
| Hard dataset size limit | Defer until empirical testing. |

---

## 40. Summary

Ariadne's local-first storage strategy is based on a simple idea:

```text
The user's AI/ML methodology project should be understandable, portable, private and reproducible.
```

The recommended architecture is:

```text
Small app catalog database
+
Self-contained project folder
+
Project SQLite database
+
File-system datasets and reports
```

The MVP should implement:

```text
Project creation
Project opening
Local catalog
Project manifest
Project database
CSV import
Original file copy
Dataset hash
Column profile persistence
Fundamental analysis persistence
Decision log persistence
Markdown report storage
```

The storage design must protect future options:

```text
Blazor Web
PostgreSQL
Object storage
Commercial collaboration
Project package export
Experiment tracking
Model evaluation
```

Most importantly, storage must reinforce Ariadne's methodology-first philosophy:

```text
Do not just save files.
Save context.
Save assumptions.
Save decisions.
Save uncertainty.
Save enough evidence to make the project understandable later.
```
