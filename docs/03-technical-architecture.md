# 03 - Technical Architecture

# Ariadne AI Workbench

**Status:** Draft v0.1  
**Project type:** Open-source local-first application  
**Primary platform:** .NET 10 MAUI Blazor Hybrid  
**Future platform:** Blazor Web application, if useful and/or commercializable  
**Repository name:** `ariadne-ai-workbench`  
**Solution name:** `Ariadne.sln`  
**Root namespace:** `Ariadne`  
**Related documents:** `00-project-brief.md`, `01-product-vision.md`, `02-functional-specification.md`

---

## 1. Purpose of this document

This document defines the technical architecture for **Ariadne AI Workbench**.

It translates the product and functional vision into a concrete engineering structure that Codex and human contributors must follow when creating the first implementation.

This document defines:

- solution structure;
- project boundaries;
- dependency rules;
- runtime hosting model;
- local-first storage approach;
- UI architecture;
- analytics architecture;
- import and profiling flow;
- reporting architecture;
- testing strategy;
- security and privacy constraints;
- future web/commercial extensibility;
- Codex implementation rules.

This document does **not** define every domain entity in detail. The domain model belongs in:

```text
04-domain-model.md
```

Local storage details belong in:

```text
06-local-first-storage.md
```

Codex task sequencing belongs in:

```text
09-codex-task-breakdown.md
AGENTS.md
```

---

## 2. Architectural intent

Ariadne must be built as a **local-first, modular, methodology-driven workbench**.

The first version is a .NET MAUI Blazor Hybrid application. However, the architecture must avoid locking the product into MAUI. The product should later be able to reuse most of its domain, application, analytics and UI code inside a Blazor Web host.

The application must be designed around the methodology workflow:

```text
Project
Dataset
Understand
Analyze
Hypothesize
Prepare
Model
Evaluate
Report
```

The MVP implements only a subset:

```text
Project
Dataset
Understand
Report
```

The technical architecture must therefore support future stages without implementing them prematurely.

---

## 3. Architectural principles

### 3.1 Local-first by default

Ariadne must run without a server, account, cloud storage, remote AI service or internet dependency.

MVP rules:

- datasets remain on the user's machine;
- project metadata is stored locally;
- reports are generated locally;
- no telemetry is enabled by default;
- no external API call is required;
- no authentication is required.

Future cloud or commercial features must be additive and must not contaminate the local-first core.

### 3.2 Methodology-first, not model-first

The architecture must make it easy to implement methodological steps before modelling steps.

For the MVP, the system should emphasize:

- project creation;
- dataset import;
- dataset preview;
- dataset profiling;
- variable classification;
- fundamental analysis;
- decision logging;
- report generation.

ML.NET, Python runners, model training and experiment tracking are future modules, not MVP dependencies.

### 3.3 Host independence

The MAUI project is an application host, not the application core.

The host may contain:

- MAUI bootstrapping;
- platform-specific file access;
- service registration;
- `BlazorWebView` configuration;
- platform-specific resources.

The host must not contain:

- business rules;
- analytics algorithms;
- dataset profiling logic;
- report generation logic;
- workflow rules;
- domain invariants.

### 3.4 Shared UI independence

Reusable Razor components must live in `Ariadne.SharedUi`.

`Ariadne.SharedUi` must not depend on MAUI. It should depend on application contracts and view models only. This keeps the future Blazor Web host realistic.

### 3.5 Clean dependency direction

Dependencies must point inward.

```text
Host / UI / Infrastructure
        ↓
Application
        ↓
Domain
```

`Domain` must have no dependency on infrastructure, UI, persistence, MAUI, Blazor, ML.NET, logging frameworks or file systems.

### 3.6 Deterministic analytics first

The initial analytics engine must be deterministic and testable.

Codex must prefer pure C# implementations for MVP analytics:

- CSV schema inference;
- missing value counts;
- unique value counts;
- numeric statistics;
- basic type inference;
- IQR outlier candidate detection;
- simple report summaries.

Avoid introducing advanced statistical libraries before the relevant functional slice needs them.

### 3.7 Traceability over automation

Every important inference or transformation should be traceable.

Examples:

- inferred column type;
- user override of a variable type;
- dataset import decision;
- missing-value warning;
- outlier candidate warning;
- fundamental analysis limitation;
- report generation event.

The architecture must make decision logging easy from application use cases.

### 3.8 Replaceable infrastructure

Local persistence, file access, report export and future model runners must be behind interfaces.

This allows future implementations such as:

- SQLite local storage;
- file-based project storage;
- PostgreSQL web storage;
- ML.NET runner;
- Python runner;
- cloud sync service;
- commercial report exporter.

### 3.9 Small vertical slices

Codex must implement the application as small, complete vertical slices.

A slice should include, when relevant:

```text
Domain model
Application use case
Infrastructure implementation
Shared UI component/page
Tests
Documentation update
```

Avoid large speculative frameworks.

---

## 4. Technology baseline

### 4.1 Runtime and language

| Area | Decision |
|---|---|
| Runtime | .NET 10 |
| Language | C# |
| UI host | .NET MAUI Blazor Hybrid |
| Reusable UI | Razor Class Library |
| Future web UI | Blazor Web App |
| Local persistence | To be finalized in `06-local-first-storage.md`; SQLite is the preferred direction |
| Initial dataset format | CSV |
| Initial report format | Markdown |
| Tests | xUnit |
| Architecture style | Clean Architecture with local-first infrastructure |

### 4.2 Official platform references

The architecture follows these platform facts from official documentation:

- .NET MAUI can build native mobile and desktop apps with a shared .NET codebase across Android, iOS, macOS and Windows.
- `BlazorWebView` hosts a Blazor web app inside a .NET MAUI app and integrates the Blazor UI with device features.
- Microsoft documents a pattern for combining .NET MAUI Blazor Hybrid with a Blazor Web App using a shared Razor Class Library.
- SQLite is an appropriate local database option for .NET MAUI applications.
- EF Core provides an official SQLite provider if the project chooses EF Core for local persistence.
- Codex reads `AGENTS.md` files before work and benefits from practical project-specific build, test and architecture instructions.

Reference links are listed in section 33.

---

## 5. Solution structure

The recommended solution structure is:

```text
ariadne-ai-workbench/
  Ariadne.sln
  README.md
  AGENTS.md
  LICENSE
  docs/
    00-project-brief.md
    01-product-vision.md
    02-functional-specification.md
    03-technical-architecture.md
    04-domain-model.md
    05-methodology-workflow.md
    06-local-first-storage.md
    07-ui-ux-guidelines.md
    08-development-roadmap.md
    09-codex-task-breakdown.md

  src/
    Ariadne.Domain/
    Ariadne.Application/
    Ariadne.Analytics/
    Ariadne.Infrastructure.Local/
    Ariadne.SharedUi/
    Ariadne.Maui/

  tests/
    Ariadne.Domain.Tests/
    Ariadne.Application.Tests/
    Ariadne.Analytics.Tests/
    Ariadne.Infrastructure.Local.Tests/
```

Future optional projects:

```text
  src/
    Ariadne.Web/
    Ariadne.Infrastructure.Web/
    Ariadne.Infrastructure.MlNet/
    Ariadne.Infrastructure.Python/
    Ariadne.Reporting.Pdf/

  tests/
    Ariadne.Web.Tests/
    Ariadne.Infrastructure.MlNet.Tests/
    Ariadne.Infrastructure.Python.Tests/
```

---

## 6. Project responsibilities

### 6.1 `Ariadne.Domain`

Purpose: define the core business concepts and invariants.

Contains:

- entities;
- value objects;
- enums;
- domain services when necessary;
- domain events if useful later;
- pure validation rules;
- domain exceptions or result models.

Initial concepts:

```text
AiProject
Dataset
DatasetVersion
ColumnProfile
VariableDefinition
FundamentalAnalysisAnswer
DecisionLogEntry
MethodologyStep
MethodologyProgress
ReportRecord
```

Must not contain:

- EF Core attributes unless explicitly accepted later;
- SQLite code;
- MAUI code;
- Blazor code;
- file system code;
- CSV parser code;
- dependency injection setup;
- logging framework code;
- application services.

Dependency rule:

```text
Ariadne.Domain depends on no Ariadne project.
```

### 6.2 `Ariadne.Application`

Purpose: implement use cases and define ports/interfaces.

Contains:

- commands;
- queries;
- use case services;
- application DTOs;
- validation at use-case level;
- interfaces for persistence, files, clocks, report export, dataset readers and analytics services;
- workflow orchestration;
- decision log integration.

Initial use cases:

```text
CreateProject
ListProjects
OpenProject
UpdateProjectMetadata
ImportDataset
PreviewDataset
ProfileDataset
ReviewVariable
SaveFundamentalAnalysis
AddDecisionLogEntry
CalculateMethodologyProgress
GenerateMarkdownReport
```

Must not contain:

- SQLite-specific queries;
- MAUI APIs;
- Blazor components;
- actual file picker implementation;
- concrete CSV library usage if abstracted;
- ML.NET training code.

Dependency rule:

```text
Ariadne.Application -> Ariadne.Domain
```

### 6.3 `Ariadne.Analytics`

Purpose: provide deterministic data profiling and statistical helpers.

Contains:

- type inference;
- missing value profiling;
- numeric statistics;
- categorical summaries;
- IQR calculations;
- outlier candidate detection;
- dataset sampling helpers;
- future univariate and multivariate analysis primitives.

MVP analytics should operate on simple data abstractions, not on UI components or database entities.

Candidate services:

```text
ICsvSchemaInferer
IDatasetProfiler
IColumnProfiler
ITypeInferenceService
IMissingValueAnalyzer
INumericStatisticsCalculator
IOutlierCandidateDetector
```

Dependency rule:

```text
Ariadne.Analytics -> Ariadne.Domain
Ariadne.Analytics -> Ariadne.Application only if it implements application interfaces
```

Preferred rule for MVP:

```text
Ariadne.Analytics -> Ariadne.Domain
```

Then `Ariadne.Application` can depend on analytics abstractions if the abstractions are placed in Application.

### 6.4 `Ariadne.Infrastructure.Local`

Purpose: implement local-first infrastructure.

Contains:

- local project repository;
- local dataset metadata repository;
- local decision log persistence;
- local file storage;
- CSV reading implementation;
- local report file writer;
- SQLite or file-based persistence implementation;
- migrations if SQLite/EF Core is used;
- platform-neutral local paths where possible.

Must not contain:

- MAUI UI;
- Razor components;
- business rules;
- statistical algorithms that belong in Analytics;
- cloud services for MVP.

Dependency rule:

```text
Ariadne.Infrastructure.Local -> Ariadne.Application
Ariadne.Infrastructure.Local -> Ariadne.Domain
Ariadne.Infrastructure.Local -> Ariadne.Analytics only if required for concrete infrastructure composition
```

### 6.5 `Ariadne.SharedUi`

Purpose: reusable Blazor UI.

Contains:

- Razor pages;
- Razor components;
- layout components;
- UI-specific view models;
- UI state containers if needed;
- form components;
- validation display components;
- report preview components;
- chart wrappers later.

Initial UI areas:

```text
Home
Projects
Project Workspace
Dataset Import
Dataset Preview
Dataset Profile
Variable Dictionary
Fundamental Analysis
Decision Log
Report
Settings/About
```

Must not contain:

- MAUI APIs;
- file system APIs;
- database code;
- analytics algorithms;
- business invariants;
- raw SQL;
- platform-specific code.

Dependency rule:

```text
Ariadne.SharedUi -> Ariadne.Application
Ariadne.SharedUi -> Ariadne.Domain only for simple read models/enums if necessary
```

Preferred rule:

```text
Ariadne.SharedUi -> Ariadne.Application
```

The UI should talk to application use cases, not directly to repositories.

### 6.6 `Ariadne.Maui`

Purpose: native application host.

Contains:

- .NET MAUI startup;
- `MauiProgram`;
- `BlazorWebView` host page;
- DI composition root;
- platform-specific services;
- file picker implementation;
- app icons, fonts and resources;
- platform manifests.

Must not contain:

- business logic;
- analytics algorithms;
- report rendering logic;
- domain rules;
- direct SQL usage except during host-specific configuration if unavoidable.

Dependency rule:

```text
Ariadne.Maui -> Ariadne.SharedUi
Ariadne.Maui -> Ariadne.Application
Ariadne.Maui -> Ariadne.Infrastructure.Local
Ariadne.Maui -> Ariadne.Analytics
Ariadne.Maui -> Ariadne.Domain
```

The MAUI host is allowed to reference many projects because it is the composition root.

### 6.7 Future `Ariadne.Web`

Purpose: optional Blazor Web host.

Contains:

- Blazor Web App host;
- web-specific routing if needed;
- authentication later;
- server-side DI composition;
- web-specific storage implementation.

Dependency rule:

```text
Ariadne.Web -> Ariadne.SharedUi
Ariadne.Web -> Ariadne.Application
Ariadne.Web -> Ariadne.Infrastructure.Web
```

The web host should reuse the same `Ariadne.SharedUi` components and application use cases where possible.

---

## 7. Dependency matrix

Allowed dependencies:

| Project | May reference |
|---|---|
| `Ariadne.Domain` | None |
| `Ariadne.Application` | `Ariadne.Domain` |
| `Ariadne.Analytics` | `Ariadne.Domain`; optionally `Ariadne.Application` for interface implementations |
| `Ariadne.Infrastructure.Local` | `Ariadne.Application`, `Ariadne.Domain`, optionally `Ariadne.Analytics` |
| `Ariadne.SharedUi` | `Ariadne.Application`, optionally `Ariadne.Domain` |
| `Ariadne.Maui` | all required runtime projects |
| `Ariadne.Web` future | `Ariadne.SharedUi`, `Ariadne.Application`, web infrastructure |

Forbidden dependencies:

| Project | Must not reference |
|---|---|
| `Ariadne.Domain` | anything else |
| `Ariadne.Application` | Infrastructure, UI, MAUI, Web |
| `Ariadne.Analytics` | UI, MAUI, Web |
| `Ariadne.Infrastructure.Local` | SharedUi, Maui, Web |
| `Ariadne.SharedUi` | Maui, Infrastructure.Local, Infrastructure.Web |
| `Ariadne.Maui` | Future web-specific infrastructure unless explicitly needed later |

Codex must not break these rules to make a task easier.

---

## 8. High-level architecture diagram

```text
+------------------------------------------------------------+
|                       Ariadne.Maui                         |
|  MAUI host, BlazorWebView, platform services, DI root       |
+------------------------------+-----------------------------+
                               |
                               v
+------------------------------------------------------------+
|                     Ariadne.SharedUi                       |
|  Razor components, layouts, pages, UI state, forms          |
+------------------------------+-----------------------------+
                               |
                               v
+------------------------------------------------------------+
|                    Ariadne.Application                     |
|  Use cases, commands, queries, workflow orchestration,      |
|  ports/interfaces, application DTOs                         |
+---------------+------------------------------+-------------+
                |                              |
                v                              v
+------------------------------+   +--------------------------+
|       Ariadne.Domain         |   |     Ariadne.Analytics    |
|  Entities, value objects,    |   | Profiling, inference,    |
|  invariants, domain rules    |   | statistics, outliers     |
+------------------------------+   +--------------------------+
                ^                              ^
                |                              |
+---------------+------------------------------+-------------+
|                Ariadne.Infrastructure.Local                  |
|  Local persistence, CSV readers, file storage, report writer |
+------------------------------------------------------------+
```

Future web path:

```text
+--------------------+        +--------------------+
|   Ariadne.Maui     |        |    Ariadne.Web     |
+---------+----------+        +----------+---------+
          |                              |
          v                              v
+-------------------------------------------------+
|               Ariadne.SharedUi                  |
+-------------------------------------------------+
          |                              |
          v                              v
+-------------------------------------------------+
|              Ariadne.Application                |
+-------------------------------------------------+
          |                              |
          v                              v
+--------------------+        +--------------------+
| Infrastructure.Local|       | Infrastructure.Web |
+--------------------+        +--------------------+
```

---

## 9. Runtime hosting model

### 9.1 MVP runtime

The MVP runtime is:

```text
.NET MAUI native app
  -> BlazorWebView
    -> Shared Razor UI
      -> Application services
        -> Local infrastructure
        -> Analytics services
        -> Domain model
```

The Blazor UI runs inside the MAUI process. The application should feel like a desktop workbench first, even if mobile targets remain possible.

### 9.2 Initial platform priority

Although MAUI supports mobile and desktop platforms, MVP implementation should prioritize:

```text
1. Windows desktop
2. macOS desktop, if practical
3. Android/iOS later, only if UI and file workflows make sense
```

Reasoning:

- CSV import and dataset profiling are more natural on desktop;
- local file navigation is simpler on desktop;
- wide tables and reports require screen space;
- mobile support may require additional UX design.

### 9.3 Future web host

The future web host should reuse:

- domain model;
- application use cases;
- analytics services;
- shared UI components;
- report generation abstractions.

The future web host should replace:

- local project storage with server storage;
- local file picker with browser upload;
- local file paths with object storage references;
- no-auth workflow with user/workspace identity;
- local report files with downloadable or stored report artifacts.

---

## 10. Application layering in practice

### 10.1 UI calls use cases

Razor components should call application use cases through interfaces.

Example:

```csharp
public interface ICreateProjectUseCase
{
    Task<CreateProjectResult> ExecuteAsync(CreateProjectCommand command, CancellationToken cancellationToken);
}
```

The UI should not instantiate domain entities directly unless it is only displaying simple read models.

### 10.2 Use cases orchestrate behavior

Application use cases should orchestrate:

- validation;
- repository access;
- analytics service calls;
- decision log creation;
- report generation;
- error handling.

Example import flow:

```text
DatasetImportPage
  -> ImportDatasetUseCase
    -> ILocalFilePicker / selected file reference
    -> ICsvDatasetReader
    -> IDatasetProfiler
    -> IProjectRepository
    -> IDecisionLogService
    -> result DTO
```

### 10.3 Domain enforces invariants

Domain entities should protect invariants such as:

- project name must not be empty;
- dataset version must belong to a dataset;
- decision log entry must have a decision or observation text;
- variable definition must be tied to a dataset version;
- methodology step status must be valid.

### 10.4 Infrastructure implements ports

Application defines ports:

```csharp
public interface IProjectRepository { ... }
public interface IDatasetFileReader { ... }
public interface IReportFileWriter { ... }
public interface ISystemClock { ... }
public interface IFilePickerService { ... }
```

Infrastructure implements them:

```csharp
public sealed class LocalProjectRepository : IProjectRepository { ... }
public sealed class CsvDatasetFileReader : IDatasetFileReader { ... }
public sealed class MarkdownReportFileWriter : IReportFileWriter { ... }
public sealed class SystemClock : ISystemClock { ... }
public sealed class MauiFilePickerService : IFilePickerService { ... }
```

If a service requires MAUI APIs, the implementation belongs in `Ariadne.Maui` or in a MAUI-specific infrastructure project, not in `Ariadne.Infrastructure.Local`.

---

## 11. Feature-to-layer mapping

| Functional module | Domain | Application | Analytics | Infrastructure.Local | SharedUi | Maui |
|---|---:|---:|---:|---:|---:|---:|
| F01 Application shell | No | No | No | No | Yes | Yes |
| F02 Project management | Yes | Yes | No | Yes | Yes | Yes |
| F03 Dataset import | Yes | Yes | Maybe | Yes | Yes | Yes |
| F04 Dataset preview | Maybe | Yes | No | Yes | Yes | No |
| F05 Dataset profiling | Yes | Yes | Yes | Maybe | Yes | No |
| F06 Variable dictionary | Yes | Yes | Maybe | Yes | Yes | No |
| F07 Fundamental analysis | Yes | Yes | No | Yes | Yes | No |
| F08 Decision log | Yes | Yes | No | Yes | Yes | No |
| F09 Methodology progress | Yes | Yes | No | Yes | Yes | No |
| F10 Markdown report | Yes | Yes | Maybe | Yes | Yes | No |
| F11 Local persistence | Yes | Yes | No | Yes | No | Maybe |
| F12 Settings/About | No | Maybe | No | Maybe | Yes | Yes |
| F20 Technical analysis later | Yes | Yes | Yes | Maybe | Yes | No |
| F30 Hypotheses later | Yes | Yes | Yes | Maybe | Yes | No |
| F40 Preprocessing later | Yes | Yes | Yes | Yes | Yes | No |
| F50 Modelling later | Yes | Yes | Maybe | Future | Yes | No |
| F60 Evaluation later | Yes | Yes | Yes | Future | Yes | No |

---

## 12. Domain modelling approach

Detailed entities belong in `04-domain-model.md`, but the architecture assumes the following style.

### 12.1 Entity identity

Use strongly typed IDs where practical.

Examples:

```csharp
public readonly record struct ProjectId(Guid Value);
public readonly record struct DatasetId(Guid Value);
public readonly record struct DatasetVersionId(Guid Value);
public readonly record struct DecisionLogEntryId(Guid Value);
```

Avoid passing raw `Guid` values through the domain when a named ID improves clarity.

### 12.2 Value objects

Use value objects for concepts with validation or meaning.

Examples:

```text
ProjectName
DatasetName
ColumnName
SourcePath
MarkdownContent
MethodologyStepStatus
VariableRole
PrimitiveDataType
MethodologicalVariableType
```

### 12.3 Enums

Use enums for stable finite sets.

Candidate enums:

```csharp
public enum PrimitiveDataType
{
    Unknown,
    Boolean,
    Integer,
    Decimal,
    Text,
    Date,
    DateTime
}

public enum MethodologicalVariableType
{
    Unknown,
    Discrete,
    Continuous,
    Text,
    DateTime,
    Identifier
}

public enum VariableRole
{
    Feature,
    Target,
    Identifier,
    Ignored,
    Metadata,
    Unknown
}
```

### 12.4 Entity behavior

Prefer behavior-rich domain methods over public mutable state.

Example:

```csharp
project.Rename(newName, clock.UtcNow);
project.AddDecision(decisionEntry);
variableDefinition.ChangeRole(VariableRole.Target, reason);
```

### 12.5 Persistence neutrality

Domain entities should not be shaped primarily for SQLite or EF Core. If persistence requires mapping compromises, place them in infrastructure mapping code.

---

## 13. Application use-case architecture

### 13.1 Use-case naming

Use cases should be explicit and task-oriented.

Examples:

```text
CreateProjectUseCase
ListProjectsUseCase
OpenProjectUseCase
ImportDatasetUseCase
PreviewDatasetUseCase
ProfileDatasetUseCase
UpdateVariableDefinitionUseCase
SaveFundamentalAnalysisUseCase
AddDecisionLogEntryUseCase
GenerateMarkdownReportUseCase
```

### 13.2 Commands and results

Use explicit command/result models.

Example:

```csharp
public sealed record CreateProjectCommand(
    string Name,
    string? Description,
    string? Objective,
    string? WorkspacePath);

public sealed record CreateProjectResult(
    ProjectId ProjectId,
    string Name,
    DateTimeOffset CreatedAtUtc);
```

Avoid returning domain entities directly to the UI if a DTO is clearer.

### 13.3 Query models

Use simple read models for UI display.

Examples:

```text
ProjectSummaryDto
ProjectWorkspaceDto
DatasetPreviewDto
DatasetProfileDto
ColumnProfileDto
VariableDefinitionDto
FundamentalAnalysisSectionDto
DecisionLogEntryDto
ReportPreviewDto
```

### 13.4 Result and error pattern

Use a consistent result pattern for expected failures.

Candidate shape:

```csharp
public sealed record Result<T>(
    bool IsSuccess,
    T? Value,
    Error? Error);

public sealed record Error(
    string Code,
    string Message,
    string? Details = null);
```

Expected failures should not be thrown as exceptions.

Expected failures include:

- invalid project name;
- missing CSV file;
- unsupported file format;
- empty dataset;
- unreadable encoding;
- report output path unavailable;
- project not found.

Exceptions should be reserved for unexpected failures.

### 13.5 Cancellation

All file, import, profiling and persistence operations should accept `CancellationToken`.

Long-running MVP operations:

- CSV import;
- CSV preview;
- profiling;
- report generation.

---

## 14. Local persistence architecture

Detailed storage decisions belong in `06-local-first-storage.md`.

This architecture assumes the following direction.

### 14.1 Persistence goals

The app must persist:

- project metadata;
- dataset metadata;
- dataset version metadata;
- column profiles;
- variable dictionary edits;
- fundamental analysis answers;
- decision log entries;
- methodology progress;
- report generation records.

The app must not require a server to reopen a project.

### 14.2 Preferred persistence model

Preferred long-term local model:

```text
Local SQLite database for structured metadata
Project workspace folder for imported datasets and generated reports
```

Potential layout:

```text
Ariadne/
  ariadne.db
  projects/
    {project-id}/
      project.json              optional metadata snapshot
      datasets/
        {dataset-version-id}/
          source.csv             copied dataset, if copy mode selected
          profile.json           optional profile snapshot
      reports/
        methodology-report.md
```

### 14.3 MVP simplification option

If SQLite slows the first implementation, MVP can start with JSON files behind the same repository interfaces.

The application layer must not care whether storage is:

- SQLite;
- JSON;
- LiteDB;
- another local storage technology.

The storage mechanism must be replaceable.

### 14.4 Dataset file strategy

The raw dataset strategy must be finalized in `06-local-first-storage.md`.

Two options:

| Strategy | Pros | Cons |
|---|---|---|
| Reference original path | no copy, low storage use | file may disappear or move |
| Copy into project workspace | reproducible, robust | uses more disk space |

Recommended MVP default:

```text
Copy dataset into the project workspace when possible.
```

Reasoning:

- project remains reproducible;
- missing file errors are reduced;
- reports can reference project-owned artifacts;
- future web upload model is easier to map.

If copying is not implemented immediately, Ariadne must still handle missing source files gracefully.

### 14.5 Repository interfaces

Candidate interfaces:

```csharp
public interface IProjectRepository
{
    Task<ProjectSummary[]> ListAsync(CancellationToken cancellationToken);
    Task<AiProject?> GetAsync(ProjectId id, CancellationToken cancellationToken);
    Task SaveAsync(AiProject project, CancellationToken cancellationToken);
    Task DeleteAsync(ProjectId id, CancellationToken cancellationToken);
}

public interface IDatasetRepository
{
    Task SaveAsync(Dataset dataset, CancellationToken cancellationToken);
    Task<Dataset?> GetAsync(DatasetId id, CancellationToken cancellationToken);
}

public interface IColumnProfileRepository
{
    Task SaveProfileAsync(DatasetVersionId versionId, DatasetProfile profile, CancellationToken cancellationToken);
    Task<DatasetProfile?> GetProfileAsync(DatasetVersionId versionId, CancellationToken cancellationToken);
}
```

### 14.6 Unit of work

A unit-of-work abstraction may be added if SQLite/EF Core is used and multiple aggregates must be saved atomically.

Do not add a complex transaction system before it is needed.

---

## 15. File access architecture

### 15.1 File picker

MAUI file picking is platform-specific and belongs in the MAUI host or a MAUI-specific service.

Application should depend on an abstraction:

```csharp
public interface IUserFilePicker
{
    Task<PickedFile?> PickCsvFileAsync(CancellationToken cancellationToken);
}
```

The selected file should be represented by a neutral model:

```csharp
public sealed record PickedFile(
    string DisplayName,
    string? LocalPath,
    Stream OpenReadStream,
    long? SizeInBytes);
```

If passing a stream directly is impractical due to lifetime issues, use a service method that copies the selected file to the project workspace immediately.

### 15.2 File storage service

Use a storage abstraction:

```csharp
public interface IProjectFileStorage
{
    Task<ProjectWorkspace> CreateWorkspaceAsync(ProjectId projectId, CancellationToken cancellationToken);
    Task<StoredDatasetFile> StoreDatasetAsync(ProjectId projectId, PickedFile file, CancellationToken cancellationToken);
    Task<StoredReportFile> StoreReportAsync(ProjectId projectId, string fileName, string markdown, CancellationToken cancellationToken);
}
```

### 15.3 Path handling

Rules:

- avoid hardcoded absolute paths;
- normalize path separators;
- avoid exposing full local paths in reports unless useful;
- do not store transient OS-specific file picker handles as durable references;
- use platform app data directories for internal metadata.

---

## 16. Dataset import architecture

### 16.1 Import pipeline

MVP CSV import should follow this flow:

```text
User selects CSV
  -> file validation
  -> import preview
  -> user confirms
  -> file is copied or referenced
  -> dataset metadata is created
  -> first dataset version is created
  -> profile is generated
  -> variable dictionary is initialized
  -> decision log entry is created
```

### 16.2 CSV reader abstraction

Application should not depend directly on a specific CSV library.

Use an abstraction:

```csharp
public interface IDatasetReader
{
    Task<DatasetPreview> ReadPreviewAsync(DatasetReadRequest request, CancellationToken cancellationToken);
    Task<DatasetProfileInput> ReadForProfilingAsync(DatasetReadRequest request, CancellationToken cancellationToken);
}
```

### 16.3 CSV parsing assumptions for MVP

MVP assumptions:

- CSV has a header row;
- delimiter auto-detection may support comma and semicolon;
- UTF-8 should be supported;
- quoted fields should be supported if a robust CSV library is used;
- preview should avoid loading huge files fully into UI memory;
- profiling may sample large datasets if necessary.

### 16.4 Dataset preview model

Candidate model:

```csharp
public sealed record DatasetPreview(
    string[] Columns,
    IReadOnlyList<IReadOnlyList<string?>> Rows,
    int DisplayedRowCount,
    long? TotalRowCountEstimate,
    bool IsTruncated,
    string? Warning);
```

### 16.5 Dataset profile input model

For MVP, use a simple row/column abstraction.

```csharp
public sealed record DatasetProfileInput(
    string[] Columns,
    IReadOnlyList<IReadOnlyDictionary<string, string?>> Rows,
    bool IsSampled,
    int? TotalRowsRead);
```

If memory becomes an issue, introduce streaming profiling later.

---

## 17. Analytics architecture

### 17.1 Profiling pipeline

Dataset profiling should produce:

- row count;
- column count;
- column names;
- missing value counts;
- missing value ratio;
- unique value count;
- sample values;
- inferred primitive type;
- inferred methodological type;
- numeric statistics when applicable;
- outlier candidates when applicable.

### 17.2 Column profiler

Candidate interface:

```csharp
public interface IColumnProfiler
{
    ColumnProfile Analyze(ColumnProfileInput input);
}
```

Candidate input:

```csharp
public sealed record ColumnProfileInput(
    string ColumnName,
    IReadOnlyList<string?> RawValues,
    DatasetProfilingOptions Options);
```

### 17.3 Numeric statistics

For numeric columns, compute:

- count;
- missing count;
- min;
- max;
- mean;
- median;
- variance;
- standard deviation;
- Q1;
- Q3;
- IQR.

Statistics must be deterministic and tested with known datasets.

### 17.4 Categorical statistics

For discrete/categorical columns, compute:

- count by value;
- top values;
- unique count;
- high-cardinality warning;
- missing count.

### 17.5 Type inference

Type inference must be treated as a suggestion.

Candidate rules:

| Signal | Possible inference |
|---|---|
| all non-empty values parse as integers | `Integer` |
| all non-empty values parse as decimals | `Decimal` |
| all non-empty values parse as booleans | `Boolean` |
| all non-empty values parse as dates | `Date` or `DateTime` |
| low unique count relative to rows | `Discrete` |
| numeric with many unique values | `Continuous` |
| unique count close to row count and name contains `id` | `Identifier` |
| free text length high | `Text` |

The user must be able to override inferred types.

### 17.6 Missing value profiling

Missing values include:

```text
null
empty string
whitespace-only string
NA
N/A
NaN
NULL
null
?      optional, if configured
```

The exact list should be configurable later.

### 17.7 Outlier candidate detection

For MVP, implement IQR-based outlier candidate detection for numeric columns.

Formula:

```text
IQR = Q3 - Q1
lower bound = Q1 - 1.5 * IQR
upper bound = Q3 + 1.5 * IQR
```

Values outside the bounds are candidates, not final truth.

The UI and reports must use careful language:

```text
Outlier candidate
Potential outlier
Requires contextual validation
```

### 17.8 Future analytics modules

Future modules may include:

- univariate charts;
- multivariate relationships;
- contingency tables;
- grouped statistics;
- Pearson/Spearman correlations;
- hypothesis test suggestions;
- statistical test execution;
- train/test split diagnostics;
- model evaluation metrics;
- confidence intervals.

Do not implement these in the MVP unless explicitly planned.

---

## 18. Variable dictionary architecture

### 18.1 Purpose

The variable dictionary bridges technical profiling and methodological understanding.

It stores:

- column name;
- display name;
- description;
- unit;
- primitive type;
- methodological type;
- variable role;
- source note;
- user review status;
- user notes.

### 18.2 Initialization

After profiling, Ariadne should create a variable definition for each column.

Initial values come from the profile:

```text
ColumnProfile -> VariableDefinition candidate
```

The user can then review and modify:

- methodological type;
- role;
- description;
- unit;
- notes;
- whether the variable is reviewed.

### 18.3 Review state

Suggested states:

```text
NotReviewed
InReview
Reviewed
NeedsClarification
Ignored
```

A variable can be technically profiled but methodologically incomplete.

### 18.4 Decision logging

Important variable changes should create decision log candidates.

Examples:

- user marks a column as target;
- user marks an ID column as ignored;
- user changes inferred continuous type to discrete;
- user flags a variable as unreliable;
- user adds a unit.

The application can either create automatic log entries or suggest them for confirmation. MVP should prefer simple automatic entries for explicit user changes.

---

## 19. Fundamental analysis architecture

### 19.1 Purpose

Fundamental analysis captures contextual understanding:

```text
What
Who
When
Where
How
Why
```

The architecture must treat this as structured project data, not as a single free-text note.

### 19.2 Data model

Candidate model:

```csharp
public sealed class FundamentalAnalysisAnswer
{
    public FundamentalAnalysisQuestionId QuestionId { get; }
    public FundamentalAnalysisCategory Category { get; }
    public string Answer { get; private set; }
    public EvidenceConfidence Confidence { get; private set; }
    public bool IsUnknown { get; private set; }
    public DateTimeOffset UpdatedAtUtc { get; private set; }
}
```

Candidate categories:

```csharp
public enum FundamentalAnalysisCategory
{
    What,
    Who,
    When,
    Where,
    How,
    Why
}
```

Candidate confidence:

```csharp
public enum EvidenceConfidence
{
    Unknown,
    Low,
    Medium,
    High
}
```

### 19.3 Application service

Use case:

```text
SaveFundamentalAnalysisUseCase
```

Responsibilities:

- validate project exists;
- validate question IDs;
- save answers;
- update completion status;
- add decision or note if important fields remain unknown;
- expose report-ready summaries.

### 19.4 Report behavior

The report must include both completed and missing contextual information.

Missing answers should be presented honestly:

```text
The collection period is unknown. This limits the ability to assess temporal relevance.
```

---

## 20. Decision log architecture

### 20.1 Purpose

The decision log is the audit trail of the methodology.

It records:

- assumptions;
- observations;
- warnings;
- methodological decisions;
- user overrides;
- import events;
- report generation events;
- later preprocessing and modelling decisions.

### 20.2 Entry model

Candidate fields:

```text
Id
ProjectId
DatasetVersionId optional
RelatedColumnName optional
Type
Title
Content
Rationale
Impact
CreatedAtUtc
CreatedBy
Source
Tags
```

Candidate types:

```text
Observation
Assumption
Decision
Warning
Limitation
Question
Import
Report
```

### 20.3 Source

Candidate sources:

```text
User
System
Analytics
Import
ReportGenerator
```

### 20.4 Rules

- decision entries must be append-first;
- editing should preserve updated timestamps;
- deletion should be avoided or soft-deleted later;
- automatic entries must be clearly marked as system-generated;
- report generation should include relevant decision log entries.

---

## 21. Methodology progress architecture

### 21.1 Purpose

Progress helps the user understand where they are in the methodology workflow.

MVP steps:

```text
Project
Dataset
Understand
Report
```

Future steps:

```text
Analyze
Hypothesize
Prepare
Model
Evaluate
```

### 21.2 Progress model

Candidate state:

```csharp
public enum MethodologyStepStatus
{
    NotStarted,
    InProgress,
    NeedsReview,
    Completed,
    Blocked,
    NotApplicable
}
```

### 21.3 Calculation

Progress should be derived where possible, not manually maintained.

Examples:

- `Project` is completed if project metadata is valid.
- `Dataset` is completed if a dataset exists and has a profile.
- `Understand` is completed if required fundamental questions and variable review minimums are satisfied.
- `Report` is completed if a report has been generated after the latest major changes.

### 21.4 UI behavior

Shared UI should show:

- current step;
- completed steps;
- blocked steps;
- disabled future steps;
- helpful next action.

---

## 22. Report generation architecture

### 22.1 MVP output

MVP report format:

```text
Markdown
```

The report generator must be independent of MAUI and UI.

### 22.2 Application flow

```text
GenerateMarkdownReportUseCase
  -> load project
  -> load dataset metadata
  -> load profile
  -> load variable dictionary
  -> load fundamental analysis answers
  -> load decision log
  -> build report model
  -> render Markdown
  -> store report file
  -> save report record
  -> return report path/preview
```

### 22.3 Report model

Use an intermediate report model rather than concatenating strings directly inside the use case.

Candidate components:

```text
MethodologyReportModel
ReportSection
ReportTable
ReportWarning
ReportDecisionEntry
ReportVariableEntry
```

### 22.4 Renderer

Candidate interface:

```csharp
public interface IMarkdownReportRenderer
{
    string Render(MethodologyReportModel report);
}
```

### 22.5 Report sections

MVP report sections:

```text
Project summary
Dataset summary
Dataset profile
Variable dictionary
Fundamental analysis
Decision log
Limitations and unknowns
Generated metadata
```

Future sections:

```text
Technical analysis
Hypotheses
Preprocessing plan
Model experiments
Evaluation metrics
Confidence intervals
Applicability boundary
```

### 22.6 Report determinism

Report generation must be deterministic for the same project state, except for generated timestamps.

Testing should verify:

- section ordering;
- inclusion of missing context warnings;
- variable entries;
- decision log entries;
- Markdown escaping for table values.

---

## 23. UI architecture

### 23.1 UI project structure

Recommended structure inside `Ariadne.SharedUi`:

```text
Ariadne.SharedUi/
  Components/
    AppShell/
    Cards/
    Forms/
    Tables/
    Methodology/
    Dataset/
    Report/
  Layout/
    MainLayout.razor
    ProjectLayout.razor
  Pages/
    Home.razor
    Projects/
      ProjectListPage.razor
      CreateProjectPage.razor
    Workspace/
      ProjectOverviewPage.razor
      DatasetPage.razor
      DatasetProfilePage.razor
      VariableDictionaryPage.razor
      FundamentalAnalysisPage.razor
      DecisionLogPage.razor
      ReportPage.razor
    SettingsPage.razor
    AboutPage.razor
  State/
    ProjectWorkspaceState.cs
    UiNotificationState.cs
  ViewModels/
  Routes.cs
  _Imports.razor
```

### 23.2 UI rules

Razor components must:

- stay thin;
- call use cases or UI services;
- display state;
- handle form input;
- show validation errors;
- avoid business logic;
- avoid direct database or file access;
- avoid heavy analytics logic.

### 23.3 UI state

For MVP, prefer simple scoped services over complex state frameworks.

Candidate:

```csharp
public sealed class ProjectWorkspaceState
{
    public ProjectId? CurrentProjectId { get; private set; }
    public ProjectWorkspaceDto? CurrentProject { get; private set; }
}
```

Avoid introducing Redux-style state management until needed.

### 23.4 Navigation

Routes should be centralized.

Candidate:

```csharp
public static class AriadneRoutes
{
    public const string Home = "/";
    public const string Projects = "/projects";
    public static string Project(ProjectId id) => $"/projects/{id.Value}";
}
```

### 23.5 Components to create early

Reusable MVP components:

```text
PageHeader
EmptyState
LoadingPanel
ErrorPanel
MethodologyStepList
ProjectCard
DatasetPreviewTable
ColumnProfileTable
VariableDictionaryTable
FundamentalQuestionCard
DecisionLogList
ReportPreview
```

### 23.6 Styling

The design system is defined in `07-ui-ux-guidelines.md` later.

Until then:

- use simple, accessible layouts;
- avoid heavy custom CSS frameworks unless chosen explicitly;
- keep UI responsive for desktop and tablet sizes;
- avoid mobile-first compromises that make dataset tables hard to use.

---

## 24. Dependency injection architecture

### 24.1 Composition root

`Ariadne.Maui` is the MVP composition root.

It registers:

- application use cases;
- analytics services;
- local infrastructure services;
- UI services;
- platform services.

### 24.2 Registration extensions

Each project should expose service registration extension methods where appropriate.

Examples:

```csharp
public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddAriadneApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateProjectUseCase, CreateProjectUseCase>();
        return services;
    }
}
```

```csharp
public static class AnalyticsServiceCollectionExtensions
{
    public static IServiceCollection AddAriadneAnalytics(this IServiceCollection services)
    {
        services.AddSingleton<IColumnProfiler, ColumnProfiler>();
        return services;
    }
}
```

```csharp
public static class LocalInfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddAriadneLocalInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IProjectRepository, LocalProjectRepository>();
        return services;
    }
}
```

### 24.3 Lifetime guidance

| Service type | Suggested lifetime |
|---|---|
| Pure stateless analytics service | Singleton |
| Use case service | Scoped |
| Repository | Scoped |
| UI state container | Scoped |
| Clock | Singleton |
| File picker | Scoped or Singleton depending on implementation |
| Database context | Scoped |

### 24.4 Avoid service locator

Do not pass `IServiceProvider` around to resolve dependencies inside business logic.

Use constructor injection.

---

## 25. Configuration architecture

### 25.1 Configuration sources

MVP configuration sources:

- app constants;
- local settings file;
- MAUI app configuration if needed.

Candidate settings:

```text
Default workspace location
Maximum preview rows
Maximum profiling rows before sampling
CSV delimiter detection enabled
Copy dataset into workspace enabled
Theme preference
Language preference later
```

### 25.2 Application settings abstraction

Use an abstraction:

```csharp
public interface IAriadneSettings
{
    int DatasetPreviewRowLimit { get; }
    int ProfilingRowWarningThreshold { get; }
    bool CopyDatasetsIntoWorkspace { get; }
}
```

Avoid hardcoding behavior in UI components.

---

## 26. Error handling architecture

### 26.1 Error categories

Use consistent error codes.

Suggested prefixes:

```text
PROJECT_*
DATASET_*
CSV_*
PROFILE_*
VARIABLE_*
FUNDAMENTAL_*
DECISION_*
REPORT_*
STORAGE_*
PLATFORM_*
```

Examples:

```text
PROJECT_NAME_REQUIRED
DATASET_FILE_NOT_FOUND
CSV_UNSUPPORTED_FORMAT
CSV_EMPTY_FILE
PROFILE_NO_COLUMNS
REPORT_OUTPUT_FAILED
STORAGE_PROJECT_NOT_FOUND
```

### 26.2 User-facing messages

Application results may include technical details, but UI should show friendly messages.

Example:

```text
We could not read this CSV file. Check that the file is not empty and uses a supported delimiter.
```

Avoid exposing stack traces in UI.

### 26.3 Exception policy

Expected problems return result errors.

Unexpected failures may throw exceptions, but must be logged and converted into user-friendly errors at the UI boundary.

---

## 27. Logging and diagnostics

### 27.1 MVP logging

Use standard .NET logging abstractions.

Log:

- app startup;
- project creation;
- dataset import start/end/failure;
- profiling start/end/failure;
- report generation start/end/failure;
- storage errors.

Do not log:

- full dataset content;
- sensitive cell values;
- full local paths unless necessary for debugging and not exposed in reports;
- secrets or future API keys.

### 27.2 Local diagnostic logs

If local log files are added later, they must be optional and documented.

MVP should not require telemetry.

---

## 28. Privacy and security architecture

### 28.1 Privacy guarantees for MVP

The MVP should be able to truthfully say:

```text
Ariadne runs locally and does not upload your datasets.
```

Only say this if the implementation actually respects it.

### 28.2 Security non-claims

Unless implemented later, Ariadne must not claim:

- encryption at rest;
- compliance with GDPR, HIPAA, SOC2 or similar frameworks;
- secure multi-user access;
- enterprise-grade audit logging;
- tamper-proof reports.

### 28.3 Future security extensions

Future features may include:

- local encryption;
- project password protection;
- signed reports;
- cloud authentication;
- role-based access control;
- team audit trails.

These must remain outside MVP unless explicitly planned.

---

## 29. Performance architecture

### 29.1 MVP performance targets

Functional specification targets:

| Dataset size | Expected behavior |
|---|---|
| Up to 10k rows | Smooth import/profile on normal hardware |
| Up to 100k rows | Should work, may take noticeable time |
| Over 100k rows | May require sampling or warning in MVP |
| Very wide datasets | Must remain navigable |

### 29.2 Profiling strategy

Start simple:

- read rows for preview separately from full profiling;
- cap preview rows;
- warn before profiling very large files;
- consider sampling if file exceeds threshold;
- run profiling asynchronously;
- show loading state.

### 29.3 Memory rules

Avoid storing multiple full dataset copies in memory.

For MVP, it is acceptable to load small/medium datasets into memory for profiling. For larger datasets, introduce streaming later.

### 29.4 Future streaming profiler

A future profiler can process rows incrementally:

```text
Read row
  -> update column accumulators
  -> update type inference
  -> update missing counts
  -> update unique counts with cap
  -> update numeric statistics
Finalize profile
```

Do not build this complexity until benchmarks require it.

---

## 30. Testing architecture

### 30.1 Test project responsibilities

| Test project | Focus |
|---|---|
| `Ariadne.Domain.Tests` | entities, value objects, invariants |
| `Ariadne.Application.Tests` | use cases with fake repositories/services |
| `Ariadne.Analytics.Tests` | deterministic profiling/statistics/type inference |
| `Ariadne.Infrastructure.Local.Tests` | local storage, CSV reader, report file writer |
| Future UI tests | component rendering and key flows |

### 30.2 Domain tests

Test examples:

- project name validation;
- project rename updates timestamp;
- decision log entries require content;
- variable role changes are valid;
- dataset version belongs to dataset.

### 30.3 Application tests

Use fake repositories and fake services.

Test examples:

- create project saves project;
- import dataset creates metadata and decision log entry;
- missing file returns expected error;
- saving fundamental analysis updates progress;
- report generation includes required sections.

### 30.4 Analytics tests

Analytics tests are critical.

Test examples:

- numeric column statistics are correct;
- median handles odd/even counts;
- missing values are counted correctly;
- type inference detects integer/decimal/date/text;
- high-cardinality ID-like columns are flagged;
- IQR outlier bounds are correct;
- outlier candidates are identified deterministically.

### 30.5 Infrastructure tests

Test examples:

- CSV reader handles comma delimiter;
- CSV reader handles semicolon delimiter;
- CSV reader returns preview limit;
- local repository can save and reload project;
- report writer creates Markdown file;
- missing dataset file is handled gracefully.

### 30.6 UI tests

Do not block early MVP on advanced UI automation.

Later, consider bUnit for Razor components.

### 30.7 Test command

Standard command:

```bash
dotnet test
```

Codex must run tests for the affected projects whenever practical.

---

## 31. Build and development workflow

### 31.1 Required commands

Basic validation:

```bash
dotnet build
dotnet test
```

The second command is intentionally shown without extra filters. Codex may run targeted tests first, but final validation should run the full test suite when feasible.

### 31.2 Formatting

Use standard C# formatting.

If `.editorconfig` exists, Codex must follow it.

Recommended initial `.editorconfig` rules:

```text
nullable enable
implicit usings enable
file-scoped namespaces preferred
var usage according to clarity
private fields _camelCase
async methods end with Async
```

### 31.3 Continuous integration

Initial CI should run:

```text
restore
build
test
```

Future CI may add:

- formatting check;
- static analysis;
- package vulnerability scan;
- platform-specific MAUI builds;
- release packaging.

---

## 32. Future modelling architecture

Modelling is out of scope for MVP, but the architecture must not block it.

### 32.1 Model runner abstraction

Future interface:

```csharp
public interface IModelRunner
{
    Task<ModelRunResult> RunAsync(ModelRunRequest request, CancellationToken cancellationToken);
}
```

Potential implementations:

```text
MlNetModelRunner
PythonScikitLearnRunner
MockModelRunner
RemoteModelRunner later
```

### 32.2 Preprocessing pipeline abstraction

Future preprocessing steps should be explicit domain/application objects.

Candidate step types:

```text
MissingValueHandling
OutlierHandling
OrdinalEncoding
OneHotEncoding
MinMaxNormalization
Standardization
FeatureEngineering
FeatureSelection
```

### 32.3 Train/test separation

Future modelling must enforce or document train/test separation.

Rules:

- transformations are fitted on train only;
- test is transformed using train-fitted transformation rules;
- evaluation must distinguish train and test metrics;
- report must mention generalization limits.

### 32.4 ML.NET position

ML.NET is a good future first model runner because it keeps the app within the .NET ecosystem and supports offline/local usage.

However, ML.NET must not be added until the methodology MVP is stable.

### 32.5 Python position

A Python runner may be useful later for Scikit-Learn, StatsModels or XGBoost compatibility.

If added, it must be optional and isolated behind `IModelRunner` or similar interfaces.

The core application must remain usable without Python installed.

---

## 33. External technical references

Official references used to shape this architecture:

- .NET MAUI overview: https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui?view=net-maui-10.0
- .NET MAUI supported platforms: https://learn.microsoft.com/en-us/dotnet/maui/supported-platforms?view=net-maui-10.0
- BlazorWebView in .NET MAUI: https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/blazorwebview?view=net-maui-10.0
- Build a .NET MAUI Blazor Hybrid app with a Blazor Web App: https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/tutorials/maui-blazor-web-app?view=aspnetcore-10.0
- Blazor Hybrid Razor Class Library best practices: https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/class-libraries-best-practices?view=aspnetcore-10.0
- .NET MAUI local SQLite databases: https://learn.microsoft.com/en-us/dotnet/maui/data-cloud/database-sqlite?view=net-maui-10.0
- EF Core SQLite provider: https://learn.microsoft.com/en-us/ef/core/providers/sqlite/
- ML.NET overview: https://learn.microsoft.com/en-us/dotnet/machine-learning/mldotnet-api
- ML.NET metrics: https://learn.microsoft.com/en-us/dotnet/machine-learning/resources/metrics
- Codex CLI: https://developers.openai.com/codex/cli
- Codex `AGENTS.md` guide: https://developers.openai.com/codex/guides/agents-md
- Codex best practices: https://developers.openai.com/codex/learn/best-practices

---

## 34. Codex architecture rules

When Codex modifies this repository, it must follow these rules.

### 34.1 General rules

1. Preserve Clean Architecture dependency direction.
2. Keep MAUI as a thin host.
3. Keep `Ariadne.SharedUi` independent from MAUI.
4. Keep domain entities free from persistence and UI concerns.
5. Implement one functional slice at a time.
6. Do not add cloud services for MVP.
7. Do not add authentication for MVP.
8. Do not add ML.NET before the modelling slice is explicitly requested.
9. Do not add Python before the optional Python runner is explicitly requested.
10. Do not introduce speculative abstractions without a near-term use.

### 34.2 Implementation answer format

For coding tasks, Codex should answer with:

```text
Summary:
Tests:
Changed files:
Risks / notes:
Requirement IDs:
```

### 34.3 Build/test expectation

Codex should run:

```bash
dotnet build
dotnet test
```

If MAUI build constraints prevent full validation in an environment, Codex must state what was and was not validated.

### 34.4 Documentation expectation

When Codex changes behavior, it should update the relevant document:

| Change type | Documentation |
|---|---|
| architecture | `03-technical-architecture.md` |
| domain entity | `04-domain-model.md` |
| workflow | `05-methodology-workflow.md` |
| storage | `06-local-first-storage.md` |
| UI/UX | `07-ui-ux-guidelines.md` |
| roadmap/task planning | `08-development-roadmap.md`, `09-codex-task-breakdown.md` |
| contributor rules | `AGENTS.md` |

---

## 35. Initial implementation sequence

Architecture-driven implementation order:

```text
1. Create solution and project structure
2. Add project references according to dependency matrix
3. Add minimal domain entities and value objects
4. Add application use case skeletons
5. Add analytics service skeletons
6. Add local infrastructure skeletons
7. Add shared UI shell
8. Add MAUI host and DI registration
9. Add tests for Domain and Analytics
10. Implement project creation
11. Implement local persistence
12. Implement CSV import and preview
13. Implement profiling
14. Implement variable dictionary
15. Implement fundamental analysis
16. Implement decision log
17. Implement Markdown report generation
```

Do not start with advanced UI polish or model training.

---

## 36. Architecture acceptance criteria

The architecture is acceptable for MVP when:

- solution compiles;
- dependency rules are respected;
- MAUI app starts and displays Shared UI;
- `Ariadne.SharedUi` has no MAUI dependency;
- `Ariadne.Domain` has no external infrastructure dependency;
- project creation works locally;
- dataset import is implemented through application interfaces;
- profiling logic is testable without UI or MAUI;
- report generation is testable without UI or MAUI;
- no external API call is required;
- no authentication is required;
- unit tests cover domain and analytics basics;
- documentation explains how to build and test.

---

## 37. Architecture risks

| Risk | Impact | Mitigation |
|---|---|---|
| MAUI build complexity slows contributors | Medium | Keep most logic outside MAUI; allow library tests without MAUI build |
| Shared UI accidentally depends on MAUI | High | Enforce dependency rules; review project references |
| CSV parsing grows too complex | Medium | Start with simple robust parser; document limits |
| Dataset profiling loads too much memory | Medium | Add preview limits and profiling thresholds |
| Storage decision delays MVP | Medium | Hide storage behind repository interfaces; JSON-first acceptable if needed |
| Too many abstractions too early | High | Add abstractions only for real boundaries: storage, files, analytics, reports |
| Codex implements advanced ML prematurely | High | AGENTS.md and task breakdown must state MVP non-goals clearly |
| Future web reuse fails | Medium | Keep host-independent UI and application services |
| Reports expose local paths | Low/Medium | Sanitize report output and use display names |

---

## 38. Open technical decisions

These decisions must be finalized in later documents or implementation tasks.

| ID | Decision | Default direction |
|---|---|---|
| TA-OD-001 | Local persistence technology | SQLite preferred; JSON acceptable for first slice |
| TA-OD-002 | Dataset file strategy | Copy into project workspace preferred |
| TA-OD-003 | CSV parsing library | Use robust library if lightweight; otherwise simple parser initially |
| TA-OD-004 | UI component library | Start with native/simple CSS; avoid heavy dependency until UX spec |
| TA-OD-005 | Charting library | Deferred until technical analysis module |
| TA-OD-006 | Report PDF export | Deferred; Markdown first |
| TA-OD-007 | ML.NET integration | Deferred until modelling assistant |
| TA-OD-008 | Python runner | Deferred and optional |
| TA-OD-009 | Internationalization | English docs; UI language decision later |
| TA-OD-010 | Encryption at rest | Deferred |

---

## 39. Glossary

| Term | Meaning |
|---|---|
| Host | Runtime application that displays and wires the product, e.g. MAUI or future Web |
| Shared UI | Reusable Razor components independent of MAUI |
| Domain | Core entities, value objects and invariants |
| Application | Use cases, commands, queries and interfaces |
| Infrastructure | Concrete implementations for storage, files, reports and platform services |
| Analytics | Deterministic profiling/statistical helper services |
| Local-first | The app works locally without server dependency |
| Dataset version | A specific imported state of a dataset |
| Variable dictionary | User-reviewed metadata for dataset columns |
| Fundamental analysis | Contextual analysis of data meaning, source, timing, geography, collection and purpose |
| Decision log | Traceable record of observations, assumptions, decisions, warnings and limitations |
| Methodology progress | State of completion across the Ariadne workflow |
| Model runner | Future abstraction for executing ML.NET, Python or other modelling engines |

---

## 40. Final technical intent

Ariadne's architecture must make the methodology visible in the code.

The product should not become a MAUI demo with business logic inside Razor components. It should be a clean, testable, local-first methodology engine with a reusable Blazor interface and a thin native host.

The first technical milestone is not advanced machine learning. It is a trustworthy foundation:

```text
Create project
Import dataset
Profile columns
Document variables
Capture context
Log decisions
Generate report
```

Once that foundation is stable, Ariadne can safely grow toward technical analysis, hypotheses, preprocessing, modelling, evaluation, web reuse and commercial features.
