# 04 - Domain Model

# Ariadne AI Workbench

**Status:** Draft v0.1  
**Project type:** Open-source local-first application  
**Primary platform:** .NET 10 MAUI Blazor Hybrid  
**Future platform:** Blazor Web application, if useful and/or commercializable  
**Repository name:** `ariadne-ai-workbench`  
**Solution name:** `Ariadne.sln`  
**Root namespace:** `Ariadne`  
**Related documents:** `00-project-brief.md`, `01-product-vision.md`, `02-functional-specification.md`, `03-technical-architecture.md`

---

## 1. Purpose of this document

This document defines the domain model for **Ariadne AI Workbench**.

It describes the core business concepts, aggregate boundaries, entities, value objects, enums, invariants and future extension points that Codex and human contributors must follow when implementing the application.

Ariadne is a methodology-first application. Its domain model must represent the work of conducting a rigorous AI or machine learning project, not the technical details of MAUI, Blazor, SQLite, files, charts or ML libraries.

This document is intended to guide:

- implementation of `Ariadne.Domain`;
- implementation of application use cases in `Ariadne.Application`;
- local persistence mappings in `Ariadne.Infrastructure.Local`;
- UI workflows in `Ariadne.SharedUi`;
- test design;
- Codex task decomposition.

This document is **not** a database schema. Persistence belongs in `06-local-first-storage.md` and infrastructure mappings.

---

## 2. Domain philosophy

Ariadne models a data science project as a sequence of documented, evidence-based decisions.

The domain model must support this core loop:

```text
Observe data
Understand context
Profile variables
Document meaning
Record assumptions
Track decisions
Generate report
```

Later versions will extend the loop:

```text
Formulate hypotheses
Test hypotheses
Plan preprocessing
Run experiments
Evaluate models
State applicability
```

The application must never imply that an inferred profile, statistical result or model metric is final truth. Ariadne should model uncertainty explicitly.

Key modelling ideas:

1. **A dataset is a sample**, not the whole population.
2. **A profile is an estimate**, not a definitive explanation.
3. **An inferred variable type needs review**, not blind acceptance.
4. **A decision needs a rationale**, not only a result.
5. **A report must show missing knowledge**, not hide it.

---

## 3. Core domain language

Ariadne should use consistent terminology across code, UI and documentation.

| Term | Meaning |
|---|---|
| Project | Local methodology workspace for one AI/ML initiative. |
| Dataset | Imported data source registered inside a project. |
| Dataset version | Specific imported state of a dataset. |
| Dataset profile | Deterministic summary computed from a dataset version. |
| Column profile | Summary of one column: type inference, missing values, stats and warnings. |
| Variable | A dataset column interpreted as a methodological concept. |
| Variable dictionary | User-reviewed documentation of variables. |
| Fundamental analysis | Contextual analysis answering What, Who, When, Where, How and Why. |
| Decision log | Journal of assumptions, decisions, limitations, risks and notes. |
| Methodology step | One stage in the Ariadne workflow. |
| Report | Generated methodology summary from project state. |
| Inference | A value suggested by Ariadne and requiring possible user review. |
| Review | User confirmation or correction of an inferred or documented item. |

Future terms:

| Term | Meaning |
|---|---|
| Hypothesis | Testable statement derived from observation or prior knowledge. |
| Test plan | Planned statistical test with assumptions and threshold. |
| Test run | Executed statistical test result. |
| Preprocessing pipeline | Ordered set of data preparation steps. |
| Experiment | Model training/evaluation run. |
| Applicability statement | Final statement about where results can and cannot be trusted. |

---

## 4. Domain boundaries

The domain must remain independent from technical concerns.

### 4.1 Allowed in `Ariadne.Domain`

The domain may contain:

- entities;
- aggregate roots;
- value objects;
- enums;
- domain services with no infrastructure dependencies;
- domain errors/exceptions;
- domain events as simple records if useful;
- pure validation logic;
- pure calculations that belong to the language of the domain.

### 4.2 Not allowed in `Ariadne.Domain`

The domain must not depend on:

- MAUI;
- Blazor;
- EF Core;
- SQLite;
- ASP.NET Core;
- file system APIs;
- dialogs or file pickers;
- logging frameworks;
- ML.NET;
- Python;
- CSV libraries;
- HTTP clients;
- UI DTOs;
- persistence attributes;
- JSON serialization attributes unless explicitly justified and architecture-neutral.

### 4.3 Domain vs application vs analytics

| Concern | Project |
|---|---|
| Business concepts and invariants | `Ariadne.Domain` |
| Use cases and orchestration | `Ariadne.Application` |
| Statistical profiling algorithms | `Ariadne.Analytics` |
| SQLite, files and OS-specific behavior | `Ariadne.Infrastructure.Local` |
| Blazor components | `Ariadne.SharedUi` |
| MAUI host and platform wiring | `Ariadne.Maui` |

Example:

```text
ColumnProfile is a domain concept.
Computing quartiles is analytics logic.
Saving the profile to SQLite is infrastructure logic.
Displaying it in a table is UI logic.
```

---

## 5. Bounded contexts

For the MVP, Ariadne has these bounded contexts:

```text
Project Management
Dataset Management
Dataset Profiling
Variable Dictionary
Fundamental Analysis
Decision Logging
Methodology Progress
Reporting
```

Future contexts:

```text
Technical Analysis
Hypothesis Testing
Preprocessing Planning
Model Experimentation
Evaluation and Applicability
Collaboration / Commercial Web
```

The first implementation must focus on the MVP contexts only.

---

## 6. Recommended domain project structure

Recommended folder structure for `Ariadne.Domain`:

```text
src/Ariadne.Domain/
  Common/
    Entity.cs
    AggregateRoot.cs
    DomainException.cs
    DomainError.cs
    IHasDomainEvents.cs

  Common/ValueObjects/
    Ratio.cs
    NonNegativeInt.cs
    NonNegativeLong.cs
    NonEmptyString.cs
    ContentHash.cs
    StorageKey.cs
    Tag.cs

  Projects/
    ProjectId.cs
    ProjectName.cs
    AiProject.cs
    ProjectStatus.cs

  Methodology/
    MethodologyStep.cs
    MethodologyStepStatus.cs
    MethodologyProgress.cs
    MethodologyStepProgress.cs

  Datasets/
    DatasetId.cs
    DatasetVersionId.cs
    DatasetName.cs
    Dataset.cs
    DatasetVersion.cs
    DatasetFileReference.cs
    DataSourceKind.cs
    DatasetImportStatus.cs
    ParsingWarning.cs

  Profiling/
    ProfileRunId.cs
    DatasetProfile.cs
    ColumnProfile.cs
    ColumnName.cs
    PrimitiveDataType.cs
    MethodologicalVariableType.cs
    InferredValue.cs
    NumericColumnStatistics.cs
    MissingValueSummary.cs
    DistinctValueSummary.cs
    OutlierCandidateSummary.cs

  Variables/
    VariableDictionaryId.cs
    VariableDictionary.cs
    VariableDefinition.cs
    VariableRole.cs
    VariableReviewStatus.cs
    UnitOfMeasure.cs

  FundamentalAnalysis/
    FundamentalAnalysisId.cs
    FundamentalAnalysis.cs
    FundamentalSection.cs
    FundamentalQuestionGroup.cs
    FundamentalAnswer.cs
    KnowledgeStatus.cs

  Decisions/
    DecisionLogEntryId.cs
    DecisionLogEntry.cs
    DecisionEntryType.cs
    DecisionStatus.cs
    DecisionImpact.cs
    RelatedArtifactRef.cs

  Reports/
    ReportId.cs
    MethodologyReportSnapshot.cs
    ReportFormat.cs
    ReportSectionStatus.cs
```

Future folders should be added only when the corresponding feature is implemented:

```text
  Hypotheses/
  Preprocessing/
  Experiments/
  Evaluation/
```

---

## 7. Aggregate overview

### 7.1 MVP aggregate roots

| Aggregate root | Main responsibility |
|---|---|
| `AiProject` | Project identity, metadata, methodology progress and high-level links. |
| `Dataset` | Dataset metadata and dataset versions. |
| `DatasetProfile` | Immutable profiling snapshot for one dataset version. |
| `VariableDictionary` | User-reviewed variable definitions for one dataset version. |
| `FundamentalAnalysis` | Contextual What/Who/When/Where/How/Why answers. |
| `DecisionLogEntry` | One documented decision, assumption, limitation, risk or note. |
| `MethodologyReportSnapshot` | Generated report metadata and included section status. |

### 7.2 Future aggregate roots

| Aggregate root | Main responsibility |
|---|---|
| `Hypothesis` | Observation, H0/H1, variables, origin and status. |
| `StatisticalTestRun` | Executed test, assumptions, p-value and conclusion. |
| `PreprocessingPipeline` | Ordered transformation plan and rationale. |
| `ExperimentRun` | Model training/evaluation run and results. |
| `EvaluationSummary` | Metrics, diagnostics, confidence intervals and applicability. |

### 7.3 Aggregate boundary rules

1. Aggregates must not hold raw dataset rows.
2. Aggregates should reference other aggregates by ID, not object graph.
3. The domain must not force large eager-loaded graphs.
4. Derived artifacts must be reproducible where possible.
5. User-authored content must not be overwritten by regenerated profiles.
6. Future web collaboration must not require rewriting aggregate semantics.

---

## 8. Common modelling primitives

### 8.1 Entity base class

Suggested minimal pattern:

```csharp
namespace Ariadne.Domain.Common;

public abstract class Entity<TId>
    where TId : notnull
{
    protected Entity(TId id)
    {
        Id = id;
    }

    public TId Id { get; }

    public override bool Equals(object? obj)
        => obj is Entity<TId> other && EqualityComparer<TId>.Default.Equals(Id, other.Id);

    public override int GetHashCode()
        => EqualityComparer<TId>.Default.GetHashCode(Id);
}
```

### 8.2 Aggregate root base class

Keep it minimal.

```csharp
namespace Ariadne.Domain.Common;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    protected AggregateRoot(TId id) : base(id)
    {
    }
}
```

Domain events are optional for MVP. Do not introduce a complex event bus unless a real use case requires it.

### 8.3 Strongly typed IDs

Use named IDs instead of raw `Guid` values in domain entities.

```csharp
public readonly record struct ProjectId(Guid Value)
{
    public static ProjectId New() => new(Guid.NewGuid());
}

public readonly record struct DatasetId(Guid Value)
{
    public static DatasetId New() => new(Guid.NewGuid());
}

public readonly record struct DatasetVersionId(Guid Value)
{
    public static DatasetVersionId New() => new(Guid.NewGuid());
}
```

Recommended ID types:

```text
ProjectId
DatasetId
DatasetVersionId
ProfileRunId
VariableDictionaryId
FundamentalAnalysisId
DecisionLogEntryId
ReportId
```

Future ID types:

```text
HypothesisId
StatisticalTestRunId
PreprocessingPipelineId
ExperimentRunId
EvaluationSummaryId
```

### 8.4 Time handling

Domain methods should receive timestamps from the application layer.

Do not call directly:

```csharp
DateTime.UtcNow
DateTimeOffset.UtcNow
DateTime.Now
```

inside domain entities.

Prefer:

```csharp
project.Rename(newName, now);
decision.MarkResolved(now);
```

The application layer may provide `IClock`.

---

## 9. Core value objects

### 9.1 `ProjectName`

Represents a user-facing project name.

Rules:

- required;
- trimmed;
- non-empty;
- recommended max length: 200 characters.

```csharp
public sealed record ProjectName
{
    public ProjectName(string value)
    {
        Value = string.IsNullOrWhiteSpace(value)
            ? throw new DomainException("Project name is required.")
            : value.Trim();

        if (Value.Length > 200)
            throw new DomainException("Project name must be 200 characters or fewer.");
    }

    public string Value { get; }
    public override string ToString() => Value;
}
```

### 9.2 `DatasetName`

Rules:

- required;
- trimmed;
- non-empty;
- recommended max length: 200 characters.

### 9.3 `ColumnName`

Rules:

- required;
- trimmed;
- non-empty;
- must preserve original case and spelling from imported dataset;
- comparison rules should be explicit.

Recommended default:

- preserve exact string;
- compare by exact ordinal string inside a dataset version;
- allow duplicate handling at import/application layer if source contains duplicates.

### 9.4 `Ratio`

Represents a value between 0 and 1.

Rules:

- minimum: `0`;
- maximum: `1`;
- no NaN;
- no infinity.

Useful for:

- missing cell ratio;
- missing column ratio;
- inference confidence;
- completion ratio.

### 9.5 `NonNegativeInt` and `NonNegativeLong`

Use for counts and sizes.

Rules:

- value must be greater than or equal to zero.

### 9.6 `StorageKey`

Logical storage reference for local files.

Important:

- `StorageKey` is not necessarily an absolute OS path;
- infrastructure maps `StorageKey` to the actual filesystem;
- this keeps the domain portable to future web/cloud storage.

Example:

```text
projects/{projectId}/datasets/{datasetId}/versions/{versionId}/source.csv
```

### 9.7 `ContentHash`

Represents a hash of imported file content.

Purpose:

- detect duplicate imports;
- track dataset version identity;
- support reproducibility;
- support report traceability.

Rules:

- algorithm must be known, for example `SHA256`;
- hash value must be non-empty;
- domain should not compute the hash.

### 9.8 `UnitOfMeasure`

Represents a user-provided variable unit.

Examples:

```text
EUR
USD
kg
m
seconds
percent
ISO country code
```

Rules:

- optional;
- trimmed;
- recommended max length: 100 characters.

### 9.9 `Tag`

Represents a simple label attached to a decision, variable or report section.

Rules:

- optional collection item;
- trimmed;
- no empty tag;
- recommended max length: 50 characters.

---

## 10. Enums

### 10.1 `ProjectStatus`

```csharp
public enum ProjectStatus
{
    Active = 0,
    Archived = 1
}
```

### 10.2 `MethodologyStep`

```csharp
public enum MethodologyStep
{
    Project = 0,
    Dataset = 1,
    Understand = 2,
    Analyze = 3,
    Hypothesize = 4,
    Prepare = 5,
    Model = 6,
    Evaluate = 7,
    Report = 8
}
```

MVP active steps:

```text
Project
Dataset
Understand
Report
```

Future steps may exist in the enum but should be marked as `NotAvailable` until implemented.

### 10.3 `MethodologyStepStatus`

```csharp
public enum MethodologyStepStatus
{
    NotStarted = 0,
    InProgress = 1,
    NeedsReview = 2,
    Completed = 3,
    Deferred = 4,
    NotAvailable = 5
}
```

### 10.4 `PrimitiveDataType`

Technical inferred type.

```csharp
public enum PrimitiveDataType
{
    Unknown = 0,
    Boolean = 1,
    Integer = 2,
    Decimal = 3,
    Text = 4,
    Date = 5,
    DateTime = 6
}
```

### 10.5 `MethodologicalVariableType`

Methodological interpretation of a variable.

```csharp
public enum MethodologicalVariableType
{
    Unknown = 0,
    Discrete = 1,
    Continuous = 2,
    Text = 3,
    DateTime = 4,
    Identifier = 5,
    TargetCandidate = 6,
    Ignored = 7
}
```

Notes:

- a numeric column can be discrete;
- a text column can represent categories;
- an identifier can be numeric or text;
- inferred type must be reviewable.

### 10.6 `VariableRole`

```csharp
public enum VariableRole
{
    Unknown = 0,
    Feature = 1,
    Target = 2,
    Identifier = 3,
    Metadata = 4,
    Ignored = 5
}
```

### 10.7 `VariableReviewStatus`

```csharp
public enum VariableReviewStatus
{
    NeedsReview = 0,
    Reviewed = 1,
    Ignored = 2
}
```

### 10.8 `DataSourceKind`

```csharp
public enum DataSourceKind
{
    Unknown = 0,
    CsvFile = 1,
    Manual = 2,
    Generated = 3
}
```

Future values may include:

```text
ExcelFile
ParquetFile
DatabaseQuery
ApiExport
NotebookOutput
```

Do not add them until implemented.

### 10.9 `DecisionEntryType`

```csharp
public enum DecisionEntryType
{
    Note = 0,
    Observation = 1,
    Assumption = 2,
    Decision = 3,
    Limitation = 4,
    Risk = 5,
    Question = 6,
    HypothesisIdea = 7
}
```

### 10.10 `DecisionStatus`

```csharp
public enum DecisionStatus
{
    Open = 0,
    Confirmed = 1,
    Superseded = 2,
    Resolved = 3,
    Rejected = 4
}
```

### 10.11 `KnowledgeStatus`

Used in fundamental analysis.

```csharp
public enum KnowledgeStatus
{
    Unknown = 0,
    PartiallyKnown = 1,
    Known = 2,
    NotApplicable = 3
}
```

### 10.12 `FundamentalQuestionGroup`

```csharp
public enum FundamentalQuestionGroup
{
    What = 0,
    Who = 1,
    When = 2,
    Where = 3,
    How = 4,
    Why = 5
}
```

### 10.13 `ReportFormat`

```csharp
public enum ReportFormat
{
    Markdown = 0
}
```

Future values:

```text
Pdf
Html
Docx
```

Do not add until implemented.

---

## 11. `AiProject` aggregate

### 11.1 Responsibility

`AiProject` represents one local AI/ML methodology project.

It owns:

- project identity;
- project metadata;
- high-level objective;
- status;
- methodology progress;
- references to datasets;
- active dataset version reference if selected.

It does **not** own raw dataset rows, large profiles or generated report content.

### 11.2 Fields

| Field | Type | Required | Notes |
|---|---|---:|---|
| `Id` | `ProjectId` | Yes | Strong ID. |
| `Name` | `ProjectName` | Yes | User-facing name. |
| `Description` | `string?` | No | Free text. |
| `Objective` | `string?` | No | What the project tries to achieve. |
| `Status` | `ProjectStatus` | Yes | Active or archived. |
| `CreatedAtUtc` | `DateTimeOffset` | Yes | Provided by app layer. |
| `UpdatedAtUtc` | `DateTimeOffset` | Yes | Provided by app layer. |
| `DatasetIds` | `IReadOnlyCollection<DatasetId>` | Yes | References only. |
| `ActiveDatasetVersionId` | `DatasetVersionId?` | No | Current dataset version for workflow. |
| `Progress` | `MethodologyProgress` | Yes | Workflow state. |

### 11.3 Invariants

- Project name is required.
- Created timestamp cannot be after updated timestamp.
- Archived projects cannot be modified unless explicitly reactivated.
- Dataset IDs must not be duplicated.
- Future methodology steps can exist but must be `NotAvailable` until implemented.

### 11.4 Behavior

Suggested methods:

```csharp
public static AiProject Create(
    ProjectId id,
    ProjectName name,
    DateTimeOffset now,
    string? description = null,
    string? objective = null);

public void Rename(ProjectName name, DateTimeOffset now);
public void ChangeDescription(string? description, DateTimeOffset now);
public void DefineObjective(string? objective, DateTimeOffset now);
public void AttachDataset(DatasetId datasetId, DateTimeOffset now);
public void SetActiveDatasetVersion(DatasetVersionId datasetVersionId, DateTimeOffset now);
public void UpdateStepStatus(MethodologyStep step, MethodologyStepStatus status, DateTimeOffset now);
public void Archive(DateTimeOffset now);
public void Reactivate(DateTimeOffset now);
```

### 11.5 Suggested skeleton

```csharp
public sealed class AiProject : AggregateRoot<ProjectId>
{
    private readonly List<DatasetId> _datasetIds = new();

    private AiProject(
        ProjectId id,
        ProjectName name,
        DateTimeOffset createdAtUtc,
        string? description,
        string? objective)
        : base(id)
    {
        Name = name;
        Description = NormalizeOptionalText(description);
        Objective = NormalizeOptionalText(objective);
        Status = ProjectStatus.Active;
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = createdAtUtc;
        Progress = MethodologyProgress.CreateForMvp(createdAtUtc);
    }

    public ProjectName Name { get; private set; }
    public string? Description { get; private set; }
    public string? Objective { get; private set; }
    public ProjectStatus Status { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; }
    public DateTimeOffset UpdatedAtUtc { get; private set; }
    public DatasetVersionId? ActiveDatasetVersionId { get; private set; }
    public MethodologyProgress Progress { get; private set; }
    public IReadOnlyCollection<DatasetId> DatasetIds => _datasetIds.AsReadOnly();

    public static AiProject Create(
        ProjectId id,
        ProjectName name,
        DateTimeOffset now,
        string? description = null,
        string? objective = null)
        => new(id, name, now, description, objective);

    public void Rename(ProjectName name, DateTimeOffset now)
    {
        EnsureActive();
        Name = name;
        Touch(now);
    }

    public void AttachDataset(DatasetId datasetId, DateTimeOffset now)
    {
        EnsureActive();

        if (!_datasetIds.Contains(datasetId))
        {
            _datasetIds.Add(datasetId);
            Progress = Progress.WithStatus(MethodologyStep.Dataset, MethodologyStepStatus.InProgress, now);
            Touch(now);
        }
    }

    private void Touch(DateTimeOffset now)
    {
        if (now < CreatedAtUtc)
            throw new DomainException("Updated timestamp cannot be before project creation.");

        UpdatedAtUtc = now;
    }

    private void EnsureActive()
    {
        if (Status == ProjectStatus.Archived)
            throw new DomainException("Archived projects cannot be modified.");
    }

    private static string? NormalizeOptionalText(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();
}
```

### 11.6 MVP tests

- Creating a project with a valid name succeeds.
- Creating a project with an empty name fails.
- Renaming a project updates `UpdatedAtUtc`.
- Attaching the same dataset twice does not create duplicates.
- Archiving a project prevents further modification.
- Initial progress contains active MVP steps and future unavailable steps.

---

## 12. `MethodologyProgress` value object

### 12.1 Responsibility

`MethodologyProgress` tracks status for each stage of the Ariadne workflow.

It should help the UI and report answer:

```text
What has been done?
What still needs review?
What has been deferred?
What is not available yet?
```

### 12.2 Fields

`MethodologyProgress` contains a collection of `MethodologyStepProgress`.

| Field | Type | Required |
|---|---|---:|
| `Step` | `MethodologyStep` | Yes |
| `Status` | `MethodologyStepStatus` | Yes |
| `UpdatedAtUtc` | `DateTimeOffset` | Yes |
| `CompletedAtUtc` | `DateTimeOffset?` | No |
| `Notes` | `string?` | No |

### 12.3 MVP default state

```text
Project       Completed or InProgress after project creation
Dataset       NotStarted
Understand    NotStarted
Analyze       NotAvailable
Hypothesize   NotAvailable
Prepare       NotAvailable
Model         NotAvailable
Evaluate      NotAvailable
Report        NotStarted
```

The exact status of `Project` after creation can be either `Completed` or `InProgress`. Choose one and keep it consistent. Recommended: `Completed`, because project creation itself satisfies the step.

### 12.4 Invariants

- Every `MethodologyStep` must appear at most once.
- `CompletedAtUtc` is set only when status is `Completed`.
- `NotAvailable` steps should not be marked complete in MVP.
- Status changes should update `UpdatedAtUtc`.

### 12.5 Behavior

```csharp
public MethodologyProgress WithStatus(
    MethodologyStep step,
    MethodologyStepStatus status,
    DateTimeOffset now,
    string? notes = null);
```

This should return a new value object instead of mutating the existing one.

---

## 13. `Dataset` aggregate

### 13.1 Responsibility

`Dataset` represents a logical data source imported into a project.

It owns:

- dataset identity;
- project reference;
- dataset metadata;
- source kind;
- versions.

A dataset can have multiple versions when the user imports an updated file later.

### 13.2 Fields

| Field | Type | Required | Notes |
|---|---|---:|---|
| `Id` | `DatasetId` | Yes | Strong ID. |
| `ProjectId` | `ProjectId` | Yes | Parent project reference. |
| `Name` | `DatasetName` | Yes | User-facing dataset name. |
| `Description` | `string?` | No | Free text. |
| `SourceKind` | `DataSourceKind` | Yes | MVP: `CsvFile`. |
| `CreatedAtUtc` | `DateTimeOffset` | Yes | First import. |
| `UpdatedAtUtc` | `DateTimeOffset` | Yes | Last metadata/version change. |
| `CurrentVersionId` | `DatasetVersionId?` | No | Latest or selected version. |
| `Versions` | `IReadOnlyCollection<DatasetVersion>` | Yes | Metadata only. |

### 13.3 Invariants

- Dataset belongs to one project.
- Dataset name is required.
- Versions must belong to the dataset.
- Version numbers must be positive and unique inside a dataset.
- Current version, when set, must exist in `Versions`.
- Source kind must not pretend support for formats not implemented.

### 13.4 Behavior

```csharp
public static Dataset CreateImportedCsv(
    DatasetId id,
    ProjectId projectId,
    DatasetName name,
    DatasetVersion initialVersion,
    DateTimeOffset now,
    string? description = null);

public void Rename(DatasetName name, DateTimeOffset now);
public DatasetVersion AddVersion(
    DatasetVersionId versionId,
    DatasetFileReference fileReference,
    DateTimeOffset importedAtUtc,
    long? rowCount = null,
    int? columnCount = null);

public void SetCurrentVersion(DatasetVersionId versionId, DateTimeOffset now);
```

### 13.5 Suggested skeleton

```csharp
public sealed class Dataset : AggregateRoot<DatasetId>
{
    private readonly List<DatasetVersion> _versions = new();

    private Dataset(
        DatasetId id,
        ProjectId projectId,
        DatasetName name,
        DataSourceKind sourceKind,
        DateTimeOffset now,
        string? description)
        : base(id)
    {
        ProjectId = projectId;
        Name = name;
        SourceKind = sourceKind;
        Description = NormalizeOptionalText(description);
        CreatedAtUtc = now;
        UpdatedAtUtc = now;
    }

    public ProjectId ProjectId { get; }
    public DatasetName Name { get; private set; }
    public string? Description { get; private set; }
    public DataSourceKind SourceKind { get; }
    public DateTimeOffset CreatedAtUtc { get; }
    public DateTimeOffset UpdatedAtUtc { get; private set; }
    public DatasetVersionId? CurrentVersionId { get; private set; }
    public IReadOnlyCollection<DatasetVersion> Versions => _versions.AsReadOnly();

    public static Dataset CreateImportedCsv(
        DatasetId id,
        ProjectId projectId,
        DatasetName name,
        DatasetVersion initialVersion,
        DateTimeOffset now,
        string? description = null)
    {
        var dataset = new Dataset(id, projectId, name, DataSourceKind.CsvFile, now, description);
        dataset.AddExistingVersion(initialVersion, now);
        return dataset;
    }
}
```

### 13.6 MVP tests

- Creating a CSV dataset with one version succeeds.
- Creating a dataset without a version fails if using `CreateImportedCsv`.
- Adding a version increments version number or validates uniqueness.
- Setting current version to an unknown version fails.
- Dataset cannot contain versions from another dataset.

---

## 14. `DatasetVersion` entity

### 14.1 Responsibility

`DatasetVersion` represents one imported state of a dataset.

It stores metadata needed to identify and reproduce the import, not raw rows.

### 14.2 Fields

| Field | Type | Required | Notes |
|---|---|---:|---|
| `Id` | `DatasetVersionId` | Yes | Strong ID. |
| `DatasetId` | `DatasetId` | Yes | Parent dataset. |
| `VersionNumber` | `int` | Yes | Starts at 1. |
| `ImportedAtUtc` | `DateTimeOffset` | Yes | Import timestamp. |
| `FileReference` | `DatasetFileReference` | Yes | Logical storage reference. |
| `RowCount` | `long?` | No | May be known after profiling/import. |
| `ColumnCount` | `int?` | No | May be known after profiling/import. |
| `ParsingWarnings` | `IReadOnlyCollection<ParsingWarning>` | Yes | Empty if none. |

### 14.3 Invariants

- Version number must be greater than zero.
- Row count, if set, must be non-negative.
- Column count, if set, must be non-negative.
- File reference is required for imported file versions.

### 14.4 `DatasetFileReference`

Fields:

| Field | Type | Required | Notes |
|---|---|---:|---|
| `StorageKey` | `StorageKey` | Yes | Logical local storage key. |
| `OriginalFileName` | `string` | Yes | User-facing source name. |
| `ContentHash` | `ContentHash?` | No | Strongly recommended. |
| `SizeInBytes` | `long?` | No | Non-negative. |
| `MediaType` | `string?` | No | For example `text/csv`. |

Do not store full absolute paths directly in core domain unless wrapped and justified. Absolute paths can become invalid across machines and are not portable to the future web version.

---

## 15. Dataset preview model

Dataset preview is primarily an application read model, not a domain aggregate.

Recommended location:

```text
Ariadne.Application/Datasets/Preview/
```

Suggested model:

```csharp
public sealed record DatasetPreview(
    DatasetVersionId DatasetVersionId,
    IReadOnlyList<string> ColumnNames,
    IReadOnlyList<IReadOnlyList<string?>> Rows,
    int RowLimit,
    bool IsTruncated,
    IReadOnlyList<string> Warnings);
```

Reason:

- preview rows can contain sensitive raw data;
- preview is not a stable business object;
- preview is regenerated from stored file;
- domain should avoid storing raw sample rows unless explicitly required.

Column profiles may store small sample values, but the product should keep them limited and configurable.

---

## 16. `DatasetProfile` aggregate

### 16.1 Responsibility

`DatasetProfile` is an immutable summary of a dataset version generated by profiling.

It represents what Ariadne computed from the dataset at a point in time.

It should be treated as:

```text
computed evidence, not user-reviewed truth
```

### 16.2 Fields

| Field | Type | Required | Notes |
|---|---|---:|---|
| `Id` | `ProfileRunId` | Yes | Profiling run ID. |
| `DatasetVersionId` | `DatasetVersionId` | Yes | Profiled version. |
| `CreatedAtUtc` | `DateTimeOffset` | Yes | Profiling timestamp. |
| `RowCount` | `long` | Yes | Non-negative. |
| `ColumnCount` | `int` | Yes | Non-negative. |
| `TotalMissingCells` | `long` | Yes | Non-negative. |
| `MissingCellRatio` | `Ratio` | Yes | 0..1. |
| `DuplicateRowCount` | `long?` | No | Optional MVP. |
| `Columns` | `IReadOnlyCollection<ColumnProfile>` | Yes | One per column. |
| `Warnings` | `IReadOnlyCollection<ParsingWarning>` | Yes | Empty if none. |

### 16.3 Invariants

- Row count cannot be negative.
- Column count cannot be negative.
- Total missing cells cannot be negative.
- Missing ratio must be between 0 and 1.
- Column count should match number of column profiles.
- Column names should be unique inside a profile after import normalization.
- Profile is immutable after creation.

### 16.4 Behavior

`DatasetProfile` should be constructed from analytics results.

Recommended:

```csharp
public static DatasetProfile Create(
    ProfileRunId id,
    DatasetVersionId datasetVersionId,
    DateTimeOffset createdAtUtc,
    long rowCount,
    IReadOnlyCollection<ColumnProfile> columns,
    long totalMissingCells,
    IReadOnlyCollection<ParsingWarning> warnings);
```

Avoid mutation methods. Reprofiling should create a new profile run.

### 16.5 MVP tests

- Profile rejects negative row count.
- Profile rejects negative missing cell count.
- Profile rejects missing ratio outside 0..1.
- Profile rejects duplicate column names.
- Column count equals profile column collection count.

---

## 17. `ColumnProfile` entity

### 17.1 Responsibility

`ColumnProfile` summarizes one column in one dataset version.

It should answer:

```text
What kind of values seem to be in this column?
How complete is it?
What simple statistics describe it?
Does it need review?
```

### 17.2 Fields

| Field | Type | Required | Notes |
|---|---|---:|---|
| `ColumnName` | `ColumnName` | Yes | Original normalized column name. |
| `InferredPrimitiveType` | `InferredValue<PrimitiveDataType>` | Yes | Technical type inference. |
| `InferredMethodologicalType` | `InferredValue<MethodologicalVariableType>` | Yes | Methodological inference. |
| `Missing` | `MissingValueSummary` | Yes | Count and ratio. |
| `Distinct` | `DistinctValueSummary` | Yes | Unique count and samples. |
| `NumericStatistics` | `NumericColumnStatistics?` | No | Only when numeric. |
| `Outliers` | `OutlierCandidateSummary?` | No | Numeric only in MVP. |
| `TopValueCounts` | `IReadOnlyCollection<ValueCount>` | Yes | Empty if not computed. |
| `SampleValues` | `IReadOnlyCollection<string>` | Yes | Small bounded set. |
| `Warnings` | `IReadOnlyCollection<string>` | Yes | Empty if none. |

### 17.3 `InferredValue<T>`

Represents something Ariadne inferred.

```csharp
public sealed record InferredValue<T>(
    T Value,
    Ratio Confidence,
    string? Reason,
    bool NeedsReview);
```

Rules:

- `Confidence` must be 0..1.
- `NeedsReview` should be true for ambiguous inferences.
- The UI must display inferred values as suggestions.
- User corrections belong in `VariableDefinition`, not by editing `ColumnProfile`.

### 17.4 `MissingValueSummary`

Fields:

| Field | Type | Required |
|---|---|---:|
| `MissingCount` | `long` | Yes |
| `MissingRatio` | `Ratio` | Yes |
| `Severity` | `MissingValueSeverity` | Yes |

Suggested enum:

```csharp
public enum MissingValueSeverity
{
    None = 0,
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}
```

Severity thresholds are product defaults, not statistical truth.

### 17.5 `NumericColumnStatistics`

Fields:

| Field | Type | Required |
|---|---|---:|
| `Minimum` | `decimal?` | No |
| `Maximum` | `decimal?` | No |
| `Mean` | `decimal?` | No |
| `Median` | `decimal?` | No |
| `Q1` | `decimal?` | No |
| `Q3` | `decimal?` | No |
| `StandardDeviation` | `decimal?` | No |

Rules:

- If both min and max are present, min must be less than or equal to max.
- If quartiles are present, `Q1 <= Median <= Q3` should hold.
- Use `double` in analytics if needed, but domain representation should be deliberate.

### 17.6 `OutlierCandidateSummary`

Fields:

| Field | Type | Required | Notes |
|---|---|---:|---|
| `Method` | `string` | Yes | MVP: `IQR`. |
| `CandidateCount` | `long` | Yes | Non-negative. |
| `LowerBound` | `decimal?` | No | `Q1 - 1.5 * IQR`. |
| `UpperBound` | `decimal?` | No | `Q3 + 1.5 * IQR`. |
| `RequiresContextReview` | `bool` | Yes | Should be true by default. |

The UI and report must call them **candidate outliers**, not errors.

### 17.7 `ValueCount`

```csharp
public sealed record ValueCount(string DisplayValue, long Count, Ratio Ratio);
```

Rules:

- display value must not be null;
- count must be non-negative;
- ratio must be 0..1.

### 17.8 MVP tests

- Missing ratio is validated.
- Numeric statistics reject impossible quartile order.
- Outlier candidate count cannot be negative.
- Inferred values require valid confidence ratio.
- Column profile can represent non-numeric columns without numeric stats.

---

## 18. `VariableDictionary` aggregate

### 18.1 Responsibility

`VariableDictionary` contains user-reviewed documentation for the columns of one dataset version.

It is the bridge between technical profiling and real-world meaning.

A profile says:

```text
This column looks numeric and has 3% missing values.
```

A variable definition says:

```text
This column represents house surface in square meters.
It should be treated as continuous.
It is a feature.
Missing values mean unknown surface.
```

### 18.2 Fields

| Field | Type | Required | Notes |
|---|---|---:|---|
| `Id` | `VariableDictionaryId` | Yes | Strong ID. |
| `DatasetVersionId` | `DatasetVersionId` | Yes | Dictionary target. |
| `CreatedAtUtc` | `DateTimeOffset` | Yes | Creation timestamp. |
| `UpdatedAtUtc` | `DateTimeOffset` | Yes | Last change. |
| `Variables` | `IReadOnlyCollection<VariableDefinition>` | Yes | One per documented column. |

### 18.3 Invariants

- A dictionary belongs to one dataset version.
- Column names must be unique.
- Variable definitions must not be duplicated.
- Updated timestamp cannot be before created timestamp.

### 18.4 Behavior

```csharp
public static VariableDictionary CreateFromProfile(
    VariableDictionaryId id,
    DatasetProfile profile,
    DateTimeOffset now);

public void UpdateVariable(
    ColumnName columnName,
    Action<VariableDefinition> update,
    DateTimeOffset now);

public void MarkVariableReviewed(ColumnName columnName, DateTimeOffset now);
public int ReviewedCount { get; }
public int TotalCount { get; }
```

### 18.5 Profile regeneration rule

If a dataset is reprofiled, existing user-authored variable documentation must not be blindly overwritten.

Recommended application behavior:

1. Match variables by `ColumnName`.
2. Preserve user-authored fields.
3. Add new variables as `NeedsReview`.
4. Mark removed columns as no longer present or archive them.
5. Suggest decision log entries for major changes.

---

## 19. `VariableDefinition` entity

### 19.1 Responsibility

`VariableDefinition` describes one variable in a dataset version.

It stores user-reviewed metadata and user overrides.

### 19.2 Fields

| Field | Type | Required | Notes |
|---|---|---:|---|
| `ColumnName` | `ColumnName` | Yes | Source column. |
| `DisplayName` | `string?` | No | Human-friendly name. |
| `Description` | `string?` | No | Meaning of variable. |
| `Unit` | `UnitOfMeasure?` | No | Unit of measurement. |
| `PrimitiveType` | `PrimitiveDataType` | Yes | Usually inferred from profile. |
| `MethodologicalType` | `MethodologicalVariableType` | Yes | User-reviewable. |
| `Role` | `VariableRole` | Yes | Feature/target/etc. |
| `SourceNotes` | `string?` | No | Where variable comes from. |
| `QualityNotes` | `string?` | No | Limitations and concerns. |
| `MissingValueInterpretation` | `string?` | No | Meaning of missingness. |
| `ReviewStatus` | `VariableReviewStatus` | Yes | NeedsReview/Reviewed/Ignored. |
| `CreatedAtUtc` | `DateTimeOffset` | Yes | Timestamp. |
| `UpdatedAtUtc` | `DateTimeOffset` | Yes | Timestamp. |
| `ReviewedAtUtc` | `DateTimeOffset?` | No | Set when reviewed. |

### 19.3 Invariants

- Column name is required.
- Role defaults to `Unknown` unless inferred or user-set.
- Review status defaults to `NeedsReview`.
- `ReviewedAtUtc` is set only when status is `Reviewed`.
- Ignored variables should have role `Ignored`, or role changes to ignored should mark status `Ignored`.
- Type or role changes should have a reason at application level and may suggest a decision log entry.

### 19.4 Behavior

```csharp
public void ChangeDisplayName(string? displayName, DateTimeOffset now);
public void ChangeDescription(string? description, DateTimeOffset now);
public void ChangeUnit(UnitOfMeasure? unit, DateTimeOffset now);
public void ChangeMethodologicalType(MethodologicalVariableType type, string reason, DateTimeOffset now);
public void ChangeRole(VariableRole role, string reason, DateTimeOffset now);
public void ChangeSourceNotes(string? notes, DateTimeOffset now);
public void ChangeQualityNotes(string? notes, DateTimeOffset now);
public void ChangeMissingValueInterpretation(string? interpretation, DateTimeOffset now);
public void MarkReviewed(DateTimeOffset now);
public void MarkIgnored(string reason, DateTimeOffset now);
```

### 19.5 Suggested skeleton

```csharp
public sealed class VariableDefinition
{
    private VariableDefinition(
        ColumnName columnName,
        PrimitiveDataType primitiveType,
        MethodologicalVariableType methodologicalType,
        DateTimeOffset now)
    {
        ColumnName = columnName;
        PrimitiveType = primitiveType;
        MethodologicalType = methodologicalType;
        Role = VariableRole.Unknown;
        ReviewStatus = VariableReviewStatus.NeedsReview;
        CreatedAtUtc = now;
        UpdatedAtUtc = now;
    }

    public ColumnName ColumnName { get; }
    public string? DisplayName { get; private set; }
    public string? Description { get; private set; }
    public UnitOfMeasure? Unit { get; private set; }
    public PrimitiveDataType PrimitiveType { get; private set; }
    public MethodologicalVariableType MethodologicalType { get; private set; }
    public VariableRole Role { get; private set; }
    public string? SourceNotes { get; private set; }
    public string? QualityNotes { get; private set; }
    public string? MissingValueInterpretation { get; private set; }
    public VariableReviewStatus ReviewStatus { get; private set; }
    public DateTimeOffset CreatedAtUtc { get; }
    public DateTimeOffset UpdatedAtUtc { get; private set; }
    public DateTimeOffset? ReviewedAtUtc { get; private set; }

    public static VariableDefinition FromColumnProfile(ColumnProfile profile, DateTimeOffset now)
        => new(
            profile.ColumnName,
            profile.InferredPrimitiveType.Value,
            profile.InferredMethodologicalType.Value,
            now);

    public void ChangeRole(VariableRole role, string reason, DateTimeOffset now)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new DomainException("Changing a variable role requires a reason.");

        Role = role;
        ReviewStatus = role == VariableRole.Ignored
            ? VariableReviewStatus.Ignored
            : VariableReviewStatus.NeedsReview;

        ReviewedAtUtc = null;
        Touch(now);
    }
}
```

### 19.6 MVP tests

- Variable created from column profile starts as `NeedsReview`.
- Changing role without reason fails.
- Marking reviewed sets `ReviewedAtUtc`.
- Marking ignored sets role/status consistently.
- Changing methodological type resets review status if appropriate.

---

## 20. `FundamentalAnalysis` aggregate

### 20.1 Responsibility

`FundamentalAnalysis` captures contextual understanding of the project and dataset.

It answers six groups of questions:

```text
What?
Who?
When?
Where?
How?
Why?
```

This aggregate is central to Ariadne because technical analysis alone is not enough. The app must help users document meaning, source, collection context, representativeness and limitations.

### 20.2 Scope

A fundamental analysis can be linked to:

- a project;
- an active dataset version;
- both.

For MVP, recommended:

```text
one FundamentalAnalysis per Project, optionally referencing the active DatasetVersionId
```

### 20.3 Fields

| Field | Type | Required | Notes |
|---|---|---:|---|
| `Id` | `FundamentalAnalysisId` | Yes | Strong ID. |
| `ProjectId` | `ProjectId` | Yes | Parent project. |
| `DatasetVersionId` | `DatasetVersionId?` | No | Active dataset version. |
| `CreatedAtUtc` | `DateTimeOffset` | Yes | Timestamp. |
| `UpdatedAtUtc` | `DateTimeOffset` | Yes | Timestamp. |
| `Sections` | `IReadOnlyCollection<FundamentalSection>` | Yes | Six sections. |

### 20.4 `FundamentalSection`

| Field | Type | Required | Notes |
|---|---|---:|---|
| `Group` | `FundamentalQuestionGroup` | Yes | What/Who/etc. |
| `Status` | `MethodologyStepStatus` | Yes | Usually NotStarted/InProgress/Completed/Deferred. |
| `Answers` | `IReadOnlyCollection<FundamentalAnswer>` | Yes | Group answers. |
| `Notes` | `string?` | No | Section-level notes. |
| `UpdatedAtUtc` | `DateTimeOffset` | Yes | Timestamp. |

### 20.5 `FundamentalAnswer`

| Field | Type | Required | Notes |
|---|---|---:|---|
| `QuestionKey` | `string` | Yes | Stable key, e.g. `what.variables`. |
| `Prompt` | `string` | Yes | Human-readable question. |
| `AnswerText` | `string?` | No | User response. |
| `KnowledgeStatus` | `KnowledgeStatus` | Yes | Known/Unknown/etc. |
| `EvidenceSource` | `string?` | No | Source, link, file, person, dataset documentation. |
| `Limitations` | `string?` | No | Known gaps or caveats. |
| `UpdatedAtUtc` | `DateTimeOffset?` | No | Null until answered. |

### 20.6 Default questions

#### What

```text
what.variables
what.units
what.entities
what.target_problem
```

#### Who

```text
who.collected_data
who.owns_data
who.benefits
who.subjects
```

#### When

```text
when.collection_period
when.update_frequency
when.temporal_relevance
```

#### Where

```text
where.geographic_origin
where.entity_location
where.generalization_scope
```

#### How

```text
how.collection_method
how.collection_protocol
how.quality_controls
how.tools_used
```

#### Why

```text
why.initial_purpose
why.intended_use
why.available
why.potential_bias
```

The exact prompt text can live in application/UI resources. The domain should store stable keys and answers.

### 20.7 Invariants

- A fundamental analysis must contain all six question groups.
- Each answer must have a stable question key.
- `Known` answers should have non-empty answer text.
- `Unknown` answers should preferably include a limitation or note, but this can be a warning rather than a hard invariant.
- Completed sections should have every required answer either `Known`, `NotApplicable` or explicitly `Unknown`.

### 20.8 Behavior

```csharp
public static FundamentalAnalysis CreateDefault(
    FundamentalAnalysisId id,
    ProjectId projectId,
    DatasetVersionId? datasetVersionId,
    DateTimeOffset now);

public void AnswerQuestion(
    FundamentalQuestionGroup group,
    string questionKey,
    string answerText,
    string? evidenceSource,
    DateTimeOffset now);

public void MarkQuestionUnknown(
    FundamentalQuestionGroup group,
    string questionKey,
    string? limitation,
    DateTimeOffset now);

public void MarkSectionCompleted(FundamentalQuestionGroup group, DateTimeOffset now);
public void DeferSection(FundamentalQuestionGroup group, string reason, DateTimeOffset now);
```

### 20.9 MVP tests

- Default analysis contains six groups.
- Answering a question marks section in progress.
- Marking a known answer without text fails.
- Marking unknown without answer text succeeds.
- Completing a section with unanswered required questions fails or returns validation warnings, depending on chosen design.

Recommended design: use a validation result rather than throwing for section completeness. This allows reports to show incomplete knowledge.

---

## 21. `DecisionLogEntry` aggregate

### 21.1 Responsibility

`DecisionLogEntry` records one meaningful project note.

It can represent:

- assumption;
- observation;
- decision;
- limitation;
- risk;
- open question;
- hypothesis idea.

The decision log is a key part of Ariadne because it makes the methodology auditable and explainable.

### 21.2 Fields

| Field | Type | Required | Notes |
|---|---|---:|---|
| `Id` | `DecisionLogEntryId` | Yes | Strong ID. |
| `ProjectId` | `ProjectId` | Yes | Parent project. |
| `Type` | `DecisionEntryType` | Yes | Assumption/Decision/etc. |
| `Status` | `DecisionStatus` | Yes | Open by default. |
| `Title` | `string` | Yes | Short summary. |
| `Content` | `string?` | No | Detailed explanation. |
| `Rationale` | `string?` | No | Why this decision was made. |
| `Impact` | `DecisionImpact` | Yes | Low/Medium/High or Unknown. |
| `RelatedStep` | `MethodologyStep?` | No | Dataset, Understand, etc. |
| `RelatedArtifacts` | `IReadOnlyCollection<RelatedArtifactRef>` | Yes | Dataset/variable/report references. |
| `Tags` | `IReadOnlyCollection<Tag>` | Yes | Optional. |
| `CreatedAtUtc` | `DateTimeOffset` | Yes | Timestamp. |
| `UpdatedAtUtc` | `DateTimeOffset` | Yes | Timestamp. |
| `ResolvedAtUtc` | `DateTimeOffset?` | No | If resolved. |

### 21.3 `DecisionImpact`

```csharp
public enum DecisionImpact
{
    Unknown = 0,
    Low = 1,
    Medium = 2,
    High = 3
}
```

### 21.4 `RelatedArtifactRef`

A lightweight reference to another domain artifact.

```csharp
public sealed record RelatedArtifactRef(
    string ArtifactType,
    string ArtifactId,
    string? DisplayName = null);
```

Examples:

```text
Dataset / <DatasetId>
DatasetVersion / <DatasetVersionId>
Column / price
Variable / surface
FundamentalQuestion / how.collection_method
```

This avoids hard coupling and supports future artifacts.

### 21.5 Invariants

- Title is required.
- Project ID is required.
- Resolved entries must have `ResolvedAtUtc`.
- Superseded entries should reference the newer decision when possible.
- Content or rationale is strongly recommended for `Decision` and `Limitation` types.

### 21.6 Behavior

```csharp
public static DecisionLogEntry Create(
    DecisionLogEntryId id,
    ProjectId projectId,
    DecisionEntryType type,
    string title,
    DateTimeOffset now,
    string? content = null,
    string? rationale = null,
    DecisionImpact impact = DecisionImpact.Unknown,
    MethodologyStep? relatedStep = null);

public void UpdateContent(string? content, string? rationale, DateTimeOffset now);
public void AddTag(Tag tag, DateTimeOffset now);
public void LinkArtifact(RelatedArtifactRef artifact, DateTimeOffset now);
public void MarkConfirmed(DateTimeOffset now);
public void MarkResolved(DateTimeOffset now);
public void MarkSuperseded(RelatedArtifactRef supersededBy, DateTimeOffset now);
```

### 21.7 MVP tests

- Creating a decision without title fails.
- Adding duplicate tags does not create duplicates.
- Marking resolved sets resolved timestamp.
- Related artifacts can be attached.
- Updating content changes updated timestamp.

---

## 22. `MethodologyReportSnapshot` aggregate

### 22.1 Responsibility

`MethodologyReportSnapshot` stores metadata about a generated report.

The actual Markdown content can be generated by the application layer and stored by infrastructure. The domain should primarily track:

- what was included;
- when it was generated;
- which dataset version it refers to;
- whether sections were complete, incomplete or deferred.

### 22.2 Fields

| Field | Type | Required | Notes |
|---|---|---:|---|
| `Id` | `ReportId` | Yes | Strong ID. |
| `ProjectId` | `ProjectId` | Yes | Parent project. |
| `DatasetVersionId` | `DatasetVersionId?` | No | Active dataset version. |
| `Format` | `ReportFormat` | Yes | MVP: Markdown. |
| `GeneratedAtUtc` | `DateTimeOffset` | Yes | Timestamp. |
| `Title` | `string` | Yes | Report title. |
| `SectionStatuses` | `IReadOnlyCollection<ReportSectionStatus>` | Yes | Included/completeness state. |
| `ContentHash` | `ContentHash?` | No | Hash of generated report content. |
| `StorageKey` | `StorageKey?` | No | Where content was stored. |

### 22.3 `ReportSectionStatus`

```csharp
public sealed record ReportSectionStatus(
    string SectionKey,
    MethodologyStep RelatedStep,
    MethodologyStepStatus Status,
    bool Included,
    string? Notes);
```

### 22.4 Invariants

- Report title is required.
- Format is Markdown in MVP.
- Section keys must be unique.
- Generated timestamp is required.

### 22.5 Important rule

A generated report is not the source of truth.

The source of truth remains:

```text
Project
Dataset metadata
Dataset profile
Variable dictionary
Fundamental analysis
Decision log
```

Reports are snapshots and may become stale after project changes.

---

## 23. Derived data vs user-authored data

Ariadne must clearly distinguish generated information from user-reviewed information.

| Data | Source | Editable? | Truth level |
|---|---|---:|---|
| Dataset metadata | Import process | Partly | Factual metadata. |
| Dataset preview | File reader | No | Temporary view. |
| Dataset profile | Analytics | No | Computed estimate. |
| Inferred primitive type | Analytics | No in profile, override via variable | Suggestion. |
| Inferred methodological type | Analytics | No in profile, override via variable | Suggestion. |
| Variable definition | User | Yes | Reviewed interpretation. |
| Fundamental analysis | User | Yes | Contextual documentation. |
| Decision log | User / suggested by app | Yes | Audit trail. |
| Report | Generated | Regenerate | Snapshot. |

Rule for Codex:

```text
Do not add setters that allow the UI to directly mutate computed profiles.
User corrections belong in VariableDefinition or DecisionLogEntry.
```

---

## 24. Future domain: hypotheses

This section is not MVP implementation scope. It defines the future direction so the MVP does not block it.

### 24.1 `Hypothesis` aggregate

A future `Hypothesis` should represent a testable claim.

Fields:

| Field | Type | Notes |
|---|---|---|
| `Id` | `HypothesisId` | Strong ID. |
| `ProjectId` | `ProjectId` | Parent project. |
| `DatasetVersionId` | `DatasetVersionId?` | Dataset context. |
| `Origin` | `HypothesisOrigin` | Pre-registered, derived from observation, imported, etc. |
| `Observation` | `string?` | What triggered the hypothesis. |
| `NullHypothesis` | `string` | H0. |
| `AlternativeHypothesis` | `string?` | H1. |
| `Variables` | `IReadOnlyCollection<ColumnName>` | Involved variables. |
| `Status` | `HypothesisStatus` | Draft/planned/tested/etc. |
| `CreatedAtUtc` | `DateTimeOffset` | Timestamp. |

Possible enums:

```csharp
public enum HypothesisOrigin
{
    Unknown = 0,
    PreRegistered = 1,
    DerivedFromObservation = 2,
    ImportedFromDomainKnowledge = 3
}

public enum HypothesisStatus
{
    Draft = 0,
    ReadyForTesting = 1,
    Tested = 2,
    Deferred = 3,
    Rejected = 4
}
```

### 24.2 Confirmation bias support

Future hypothesis features should support this distinction:

```text
Observation dataset
Confirmation dataset
```

If a hypothesis was generated from the same data used for testing, Ariadne should warn the user and encourage a confirmation split.

---

## 25. Future domain: statistical tests

Future statistical testing should model test plans and test runs separately.

### 25.1 `StatisticalTestPlan`

Represents intended analysis before execution.

Fields:

```text
HypothesisId
TestKind
Variables
AlphaThreshold
AssumptionsToCheck
DataScope
CreatedAtUtc
```

Potential `StatisticalTestKind` values:

```text
Binomial
ChiSquareGoodnessOfFit
StudentOneSample
ChiSquareIndependence
StudentTwoIndependentSamples
WelchTwoIndependentSamples
AnovaOneWay
PearsonCorrelation
SpearmanCorrelation
```

### 25.2 `StatisticalTestRun`

Represents an executed test.

Fields:

```text
TestRunId
TestPlanId
DatasetVersionId
ExecutedAtUtc
PValue
StatisticValue
AlphaThreshold
Conclusion
AssumptionsChecked
Warnings
```

Conclusion should be cautious:

```text
RejectNullHypothesis
FailToRejectNullHypothesis
Inconclusive
InvalidAssumptions
```

Do not use wording such as “H0 is proven false” or “H1 is proven true”.

---

## 26. Future domain: preprocessing

Preprocessing will eventually represent planned transformations and their rationale.

### 26.1 `PreprocessingPipeline` aggregate

Fields:

```text
PreprocessingPipelineId
ProjectId
InputDatasetVersionId
Name
Status
Steps
CreatedAtUtc
UpdatedAtUtc
```

### 26.2 `PreprocessingStep`

Potential step kinds:

```text
MissingValueTreatment
OutlierTreatment
Encoding
Normalization
FeatureEngineering
FeatureSelection
ColumnDrop
RowFilter
```

### 26.3 Fit/transform rule

Future preprocessing must distinguish:

```text
fit on train set
transform train set
transform test set
```

The domain should eventually be able to represent that a transformer was fitted on training data only and reused on test data.

### 26.4 Rationale required

Preprocessing decisions should be documented.

Examples:

```text
Dropped column `customer_id` because it is an identifier.
Kept candidate outlier because fundamental analysis confirms it represents a valid rare case.
Imputed missing values with median because missing ratio is low and distribution is skewed.
Used one-hot encoding because categories have no ordinal meaning.
Used standardization because model is distance-based.
```

---

## 27. Future domain: experiments and evaluation

Future modelling should be experiment-based.

### 27.1 `ExperimentRun`

Fields:

```text
ExperimentRunId
ProjectId
DatasetVersionId
PreprocessingPipelineId
ModelKind
TaskKind
Parameters
StartedAtUtc
CompletedAtUtc
Status
Metrics
Warnings
```

### 27.2 Task kinds

```text
Regression
BinaryClassification
MultiClassClassification
Clustering
Unknown
```

### 27.3 Evaluation metrics

Regression:

```text
MAE
MSE
RMSE
R2
```

Classification:

```text
Accuracy
Precision
Recall
F1
RocAuc
ConfusionMatrix
```

### 27.4 Diagnostics

Future model diagnostics should represent:

```text
Underfitting
Overfitting
LikelyGoodFit
Inconclusive
```

### 27.5 Confidence intervals

The future domain should support uncertainty around performance metrics.

Example:

```csharp
public sealed record ConfidenceInterval(
    string MetricName,
    decimal LowerBound,
    decimal UpperBound,
    Ratio ConfidenceLevel);
```

Performance without uncertainty should be marked as incomplete for serious reports.

---

## 28. Validation strategy

### 28.1 Exceptions vs validation results

Use domain exceptions for impossible or invalid state:

```text
empty project name
negative row count
ratio greater than 1
unknown dataset version used as current version
```

Use validation results for incomplete methodology:

```text
fundamental analysis has unanswered questions
variable dictionary not fully reviewed
report includes deferred sections
project has no dataset yet
```

Reason:

- invalid state should be prevented;
- incomplete work should be allowed and documented.

### 28.2 Suggested validation result model

Location can be `Ariadne.Domain.Common` or `Ariadne.Application` depending on usage.

```csharp
public sealed record ValidationIssue(
    string Code,
    string Message,
    ValidationSeverity Severity,
    MethodologyStep? RelatedStep = null);

public enum ValidationSeverity
{
    Info = 0,
    Warning = 1,
    Error = 2
}
```

For MVP, keep validation simple. Do not build an oversized rules engine.

---

## 29. Domain errors

Suggested common domain exception:

```csharp
public sealed class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}
```

Optional error codes can be added later.

Recommended error messages should be readable and testable.

Examples:

```text
Project name is required.
Dataset name is required.
Ratio must be between 0 and 1.
Row count cannot be negative.
Column name is required.
Changing a variable role requires a reason.
```

---

## 30. Repository boundaries

Repositories are application-layer interfaces. They should not be defined in `Ariadne.Domain` unless there is a strong reason.

Recommended location:

```text
src/Ariadne.Application/Abstractions/Persistence/
```

Candidate interfaces:

```csharp
public interface IProjectRepository
{
    Task AddAsync(AiProject project, CancellationToken cancellationToken);
    Task<AiProject?> GetAsync(ProjectId id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AiProject>> ListAsync(CancellationToken cancellationToken);
    Task SaveAsync(AiProject project, CancellationToken cancellationToken);
}

public interface IDatasetRepository
{
    Task AddAsync(Dataset dataset, CancellationToken cancellationToken);
    Task<Dataset?> GetAsync(DatasetId id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Dataset>> ListByProjectAsync(ProjectId projectId, CancellationToken cancellationToken);
    Task SaveAsync(Dataset dataset, CancellationToken cancellationToken);
}
```

Additional repositories:

```text
IDatasetProfileRepository
IVariableDictionaryRepository
IFundamentalAnalysisRepository
IDecisionLogRepository
IReportSnapshotRepository
```

Infrastructure implements them with SQLite and local files.

---

## 31. Read models and DTOs

Domain entities are not UI DTOs.

Use application read models for screens.

Examples:

```text
ProjectListItemDto
ProjectOverviewDto
DatasetSummaryDto
DatasetPreviewDto
DatasetProfileDto
ColumnProfileDto
VariableDefinitionDto
FundamentalAnalysisDto
DecisionLogEntryDto
ReportPreviewDto
```

Why:

- UI needs flattened data;
- domain entities should keep behavior and invariants;
- persistence can evolve separately;
- future web API can reuse application DTOs.

---

## 32. Privacy and sensitive data rules

Ariadne is local-first and may process sensitive datasets.

Domain modelling rules:

1. Do not store raw dataset rows in domain entities.
2. Keep sample values bounded and optional.
3. Store logical file references, not unnecessary absolute paths.
4. Allow future settings to disable sample value persistence.
5. Reports should include warnings if sensitive values are included.
6. Generated reports are local artifacts and may contain sensitive metadata.

The MVP may store small sample values in profiles if useful, but this must be deliberate and documented.

---

## 33. Persistence mapping guidance

The domain should be persistence-neutral.

### 33.1 Avoid in domain

Do not add:

```csharp
[Key]
[Table]
[Column]
[Owned]
[JsonPropertyName]
```

in domain classes unless the architecture explicitly changes.

### 33.2 Infrastructure mapping

`Ariadne.Infrastructure.Local` may define persistence models such as:

```text
ProjectRecord
DatasetRecord
DatasetVersionRecord
DatasetProfileRecord
ColumnProfileRecord
VariableDefinitionRecord
FundamentalAnswerRecord
DecisionLogEntryRecord
ReportSnapshotRecord
```

Mapping code converts between records and domain entities.

### 33.3 Serialization of value objects

Infrastructure decides how to persist value objects.

Examples:

```text
ProjectId -> Guid
ProjectName -> string
Ratio -> decimal/double
StorageKey -> string
ContentHash -> string + algorithm
```

---

## 34. Testing strategy for `Ariadne.Domain`

Domain tests should be small, deterministic and fast.

Test project:

```text
tests/Ariadne.Domain.Tests/
```

### 34.1 Test categories

| Category | Examples |
|---|---|
| Value object validation | Names, ratios, counts, storage keys. |
| Entity invariants | Dataset versions, profile consistency, variable statuses. |
| Aggregate behavior | Project creation, attaching datasets, progress changes. |
| User-review behavior | Variable review, type override, role change. |
| Fundamental analysis | default sections, answer updates, unknown handling. |
| Decision log | entry creation, resolving, artifact links. |
| Report snapshot | required title, unique sections. |

### 34.2 Naming convention

Recommended test naming:

```text
MethodName_WhenCondition_ShouldExpectedBehavior
```

Examples:

```text
Create_WhenProjectNameIsEmpty_ShouldThrowDomainException
AttachDataset_WhenDatasetAlreadyAttached_ShouldNotDuplicateDatasetId
CreateFromProfile_WhenProfileHasDuplicateColumns_ShouldThrowDomainException
MarkReviewed_WhenVariableIsReviewed_ShouldSetReviewedAtUtc
```

### 34.3 Test style

Prefer readable tests over over-abstracted test utilities.

Example:

```csharp
[Fact]
public void Create_WhenProjectNameIsEmpty_ShouldThrowDomainException()
{
    var action = () => new ProjectName(" ");

    action.Should().Throw<DomainException>()
        .WithMessage("Project name is required.");
}
```

If FluentAssertions is not added, use plain xUnit assertions.

---

## 35. MVP implementation order for Codex

Codex should implement the domain in this order.

### Step 1 - Common primitives

Implement:

```text
DomainException
Entity<TId>
AggregateRoot<TId>
ProjectId
DatasetId
DatasetVersionId
DecisionLogEntryId
ProjectName
DatasetName
ColumnName
Ratio
StorageKey
ContentHash
```

Add tests.

### Step 2 - Methodology progress

Implement:

```text
MethodologyStep
MethodologyStepStatus
MethodologyStepProgress
MethodologyProgress
```

Add tests for default MVP state and transitions.

### Step 3 - Project aggregate

Implement:

```text
AiProject
ProjectStatus
```

Add tests for creation, rename, archive and dataset attachment.

### Step 4 - Dataset aggregate

Implement:

```text
Dataset
DatasetVersion
DatasetFileReference
DataSourceKind
ParsingWarning
```

Add tests for version invariants.

### Step 5 - Profiling domain objects

Implement:

```text
ProfileRunId
DatasetProfile
ColumnProfile
PrimitiveDataType
MethodologicalVariableType
InferredValue<T>
MissingValueSummary
NumericColumnStatistics
OutlierCandidateSummary
ValueCount
```

Add tests for profile consistency and value validation.

### Step 6 - Variable dictionary

Implement:

```text
VariableDictionaryId
VariableDictionary
VariableDefinition
VariableRole
VariableReviewStatus
UnitOfMeasure
```

Add tests for review and overrides.

### Step 7 - Fundamental analysis

Implement:

```text
FundamentalAnalysisId
FundamentalAnalysis
FundamentalSection
FundamentalAnswer
FundamentalQuestionGroup
KnowledgeStatus
```

Add tests for default sections and answer behavior.

### Step 8 - Decision log

Implement:

```text
DecisionLogEntry
DecisionEntryType
DecisionStatus
DecisionImpact
RelatedArtifactRef
Tag
```

Add tests.

### Step 9 - Report snapshot

Implement:

```text
ReportId
MethodologyReportSnapshot
ReportFormat
ReportSectionStatus
```

Add tests.

### Step 10 - Stop

Do not implement future modules unless a later task explicitly requests them.

No hypothesis, preprocessing, modelling or evaluation code should be added during the MVP domain pass.

---

## 36. Codex rules for this document

When implementing from this domain model, Codex must follow these rules:

1. Keep domain classes free from UI, persistence and infrastructure dependencies.
2. Use strongly typed IDs for aggregate identity.
3. Use value objects for validated concepts.
4. Prefer methods over public setters.
5. Do not call system time directly in domain entities.
6. Do not store raw dataset rows in domain aggregates.
7. Do not overwrite user-authored variable definitions with inferred profile values.
8. Distinguish inferred values from reviewed values.
9. Use clear domain exceptions for invalid state.
10. Use validation warnings for incomplete methodology.
11. Add unit tests with every domain entity or value object.
12. Do not implement future domains prematurely.
13. Keep the code understandable for open-source contributors.
14. If a modelling decision is ambiguous, choose the simpler option and document it.

---

## 37. Open decisions

The following decisions can be refined during implementation.

| ID | Decision | Current recommendation |
|---|---|---|
| DM-OD-001 | Should `DatasetProfile` be an aggregate root or child of `Dataset`? | Aggregate root, because profiles can be large and independently loaded. |
| DM-OD-002 | Should `DecisionLogEntry` be aggregate root or child collection of project? | Aggregate root, to avoid loading large logs with every project. |
| DM-OD-003 | Should `VariableDictionary` be one aggregate or many variable roots? | One aggregate per dataset version for MVP. |
| DM-OD-004 | Should profile sample values be persisted? | Yes, bounded and configurable; revisit for privacy settings. |
| DM-OD-005 | Should `Project` store workspace path? | Prefer infrastructure storage configuration; store logical references in domain. |
| DM-OD-006 | Should methodology completeness block report generation? | No. Reports should show missing/deferred sections. |
| DM-OD-007 | Should future hypothesis testing be included in MVP domain? | No. Keep future notes only. |

---

## 38. Summary

The Ariadne domain model should make rigorous methodology explicit.

The MVP domain is centered on:

```text
Project
Dataset
Dataset profile
Variable dictionary
Fundamental analysis
Decision log
Methodology progress
Markdown report snapshot
```

The model must keep a strong separation between:

```text
computed profile
user-reviewed interpretation
contextual understanding
documented decision
```

This separation is the foundation of Ariadne.

It allows the application to remain useful before modelling, extensible toward future ML workflows, and trustworthy as an open-source local-first workbench.
