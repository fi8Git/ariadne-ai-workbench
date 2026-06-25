# 02 - Functional Specification

# Ariadne AI Workbench

**Status:** Draft v0.1  
**Project type:** Open-source local-first application  
**Primary platform:** .NET 10 MAUI Blazor Hybrid  
**Future platform:** Blazor Web application, if useful and/or commercializable  
**Repository name:** `ariadne-ai-workbench`  
**Solution name:** `Ariadne.sln`  
**Root namespace:** `Ariadne`  
**Related documents:** `00-project-brief.md`, `01-product-vision.md`

---

## 1. Purpose of this document

This document defines the functional specification for **Ariadne AI Workbench**.

It describes what the application must allow users to do, how the main workflows should behave, what data must be captured, what belongs to the first MVP, and what is deferred to later stages.

This document is intended for:

- product planning;
- open-source contributors;
- Codex implementation tasks;
- acceptance testing;
- future architectural decisions;
- future UI/UX design.

This document is not the technical architecture. Implementation details belong in:

```text
03-technical-architecture.md
04-domain-model.md
06-local-first-storage.md
09-codex-task-breakdown.md
AGENTS.md
```

---

## 2. Functional summary

Ariadne AI Workbench is a local-first application that guides users through a rigorous AI/ML methodology.

The MVP must let a user complete this journey:

```text
Launch Ariadne
Create a local project
Import a CSV dataset
Preview the data
Profile the dataset
Review and document variables
Complete fundamental analysis
Log methodology decisions
Generate a Markdown report
```

The first MVP does **not** need to train models. It must prove that Ariadne is useful as a methodology workbench before becoming a modelling assistant.

---

## 3. Functional philosophy

Ariadne is not an AutoML tool. It is a guided methodology tool.

Its functional behavior should reflect the following principles:

1. **The user owns the conclusion.**  
   Ariadne may infer, suggest, warn and summarize, but the user must be able to review and correct methodological decisions.

2. **Inferences are never final truths.**  
   Column types, outliers, missing value risks and report statements must be presented as candidates or inferred results.

3. **Documentation is part of the workflow.**  
   The application must make it natural to record why a choice was made.

4. **Technical analysis and fundamental analysis are complementary.**  
   Dataset profiling alone is not enough. The application must ask contextual questions about meaning, source, collection and representativeness.

5. **Preprocessing decisions must be traceable.**  
   Even before transformations are executable, Ariadne must help users document what they intend to clean, encode, normalize, ignore or transform.

6. **Evaluation must focus on generalization.**  
   Later modelling features must distinguish training results from test results and help detect overfitting or underfitting.

7. **Reports are a first-class output.**  
   A project should produce a readable methodology report outside the application.

---

## 4. Scope levels

Ariadne features are divided into three scope levels.

### 4.1 MVP v0.1 scope

The MVP includes the minimum complete local methodology workflow:

- application shell;
- project creation;
- local project workspace;
- CSV import;
- dataset preview;
- basic column profiling;
- inferred variable classification;
- editable variable dictionary;
- fundamental analysis questionnaire;
- decision log;
- Markdown report generation.

### 4.2 Near-term post-MVP scope

Post-MVP features expand analysis and planning:

- richer univariate analysis;
- basic multivariate analysis;
- charts;
- hypothesis tracking;
- statistical test suggestions;
- preprocessing planner;
- better report sections.

### 4.3 Deferred advanced scope

Advanced features are intentionally deferred:

- ML.NET modelling;
- train/test split tracking;
- model evaluation;
- confidence intervals;
- optional Python runners;
- plugin architecture;
- Blazor Web host;
- cloud synchronization;
- collaboration;
- commercial hosted version.

---

## 5. Functional actors

### 5.1 Local user

The local user is the primary actor.

They can:

- create projects;
- import datasets;
- inspect data;
- document variables;
- complete methodology sections;
- generate reports;
- manage local files.

No login is required for MVP v0.1.

### 5.2 Future contributor

The contributor is not a runtime user, but the product must be understandable and extensible.

The functional specification should help contributors know:

- which user flows exist;
- which features are in scope;
- what acceptance criteria matter;
- what must not be implemented yet.

### 5.3 Future team user

Team/collaboration features are out of scope for the MVP.

A future team user may need:

- shared projects;
- comments;
- review workflows;
- role-based permissions;
- cloud sync;
- audit trails.

These must not influence MVP complexity unless a design choice would prevent future support.

---

## 6. Information architecture

The product should be organized around the methodology workflow.

Recommended top-level navigation:

```text
Home
Projects
Project Workspace
  Overview
  Dataset
  Understand
  Analyze
  Hypotheses
  Prepare
  Model
  Evaluate
  Report
Settings
Help / Methodology Notes
```

For MVP v0.1, only these sections must be active:

```text
Home
Projects
Project Workspace
  Overview
  Dataset
  Understand
  Report
Settings / About
```

The sections below may appear as disabled or “Coming later” navigation items if useful:

```text
Analyze
Hypotheses
Prepare
Model
Evaluate
```

Disabled sections must not pretend to be implemented.

---

## 7. Methodology workflow

The long-term Ariadne workflow is:

```text
1. Project
2. Dataset
3. Understand
4. Analyze
5. Hypothesize
6. Prepare
7. Model
8. Evaluate
9. Report
```

### 7.1 MVP workflow

The MVP workflow is a reduced but complete subset:

```text
1. Project
2. Dataset
3. Understand
4. Report
```

### 7.2 Workflow completion states

Each methodology step should have a clear completion state.

Recommended states:

| State | Meaning |
|---|---|
| `NotStarted` | The user has not begun this step. |
| `InProgress` | The user has entered some information but the step is incomplete. |
| `NeedsReview` | Ariadne generated or inferred information that needs user confirmation. |
| `Completed` | The user has reviewed the step and marked it complete. |
| `Deferred` | The user intentionally skipped or postponed the step. |
| `NotAvailable` | The feature is not implemented yet. |

Ariadne should never force all steps to be complete before report generation. Instead, the report must show missing or deferred sections clearly.

---

## 8. Functional modules overview

| ID | Module | MVP | Description |
|---|---:|---|---|
| F01 | Application shell | Yes | Launch app, navigate, show product identity. |
| F02 | Project management | Yes | Create, list, open and edit local projects. |
| F03 | Dataset import | Yes | Import CSV and register dataset metadata. |
| F04 | Dataset preview | Yes | Show a safe preview of imported rows/columns. |
| F05 | Dataset profiling | Yes | Compute row/column counts, missing values, basic stats and inferred types. |
| F06 | Variable dictionary | Yes | Let user review and document variables. |
| F07 | Fundamental analysis | Yes | Guide user through What/Who/When/Where/How/Why. |
| F08 | Decision log | Yes | Record assumptions, decisions, limitations and notes. |
| F09 | Methodology progress | Yes | Show what is complete, missing or needing review. |
| F10 | Markdown report | Yes | Generate a readable local methodology report. |
| F11 | Local persistence | Yes, minimal | Save project metadata locally. |
| F12 | Settings/About | Yes, minimal | Show app info and local-first behavior. |
| F20 | Technical analysis | Later | Univariate and multivariate analysis. |
| F30 | Hypothesis tracking | Later | Observations, H0/H1, tests and evidence levels. |
| F40 | Preprocessing planner | Later | Missing values, outliers, encoding, normalization and feature steps. |
| F50 | Modelling assistant | Later | Simple ML.NET experiments. |
| F60 | Evaluation assistant | Later | Metrics, diagnostics and applicability. |
| F70 | Web/commercial features | Deferred | Optional future hosted version and collaboration. |

---

# MVP v0.1 functional requirements

---

## 9. F01 - Application shell

### 9.1 Objective

Provide a clean MAUI Blazor Hybrid application shell that introduces Ariadne and lets the user access the main workflow.

### 9.2 Requirements

| ID | Requirement |
|---|---|
| F01-R01 | The app must launch without requiring a user account. |
| F01-R02 | The home screen must display the product name `Ariadne AI Workbench`. |
| F01-R03 | The home screen must display a short local-first methodology description. |
| F01-R04 | The user must be able to navigate to the project list. |
| F01-R05 | The user must be able to create a new project from the home screen or project list. |
| F01-R06 | The shell must expose navigation to active MVP sections only, or clearly mark unavailable sections as future features. |
| F01-R07 | The app must not require internet access for MVP features. |

### 9.3 Acceptance criteria

- A user can open the app and understand what it does within a few seconds.
- A user can reach project creation without reading external documentation.
- The app must not show login, cloud sync or remote AI settings in the MVP journey.

### 9.4 Out of scope

- Authentication;
- user accounts;
- cloud sync;
- telemetry dashboards;
- commercial onboarding.

---

## 10. F02 - Project management

### 10.1 Objective

Allow users to create and manage local AI/ML methodology projects.

A project is the container for datasets, context, variable documentation, decisions and reports.

### 10.2 Project fields

The MVP project model must capture:

| Field | Required | Description |
|---|---:|---|
| Project name | Yes | Human-readable name. |
| Description | No | Short description of the project. |
| Objective | No | What the user wants to understand, explain or predict. |
| Created date | Yes | Automatically generated. |
| Last modified date | Yes | Automatically updated. |
| Status | Yes | Draft, Active, Archived, or similar. |
| Local storage location | Yes | Where project metadata is stored. |

### 10.3 Requirements

| ID | Requirement |
|---|---|
| F02-R01 | The user must be able to create a local project. |
| F02-R02 | The project name must be required. |
| F02-R03 | The project name must not be only whitespace. |
| F02-R04 | The user must be able to edit project description and objective. |
| F02-R05 | The app must show a list of local projects. |
| F02-R06 | The user must be able to open an existing project. |
| F02-R07 | The app must show project creation and last modified dates. |
| F02-R08 | The app must show project methodology progress. |
| F02-R09 | The app must prevent accidental data loss when leaving unsaved changes, unless autosave is implemented. |
| F02-R10 | The app must store project metadata locally. |

### 10.4 Acceptance criteria

- Creating a project creates a local workspace record.
- Reopening the app shows the created project if persistence is implemented in the MVP sprint.
- A project with no dataset clearly shows the next recommended action: import dataset.
- The user can edit the project objective after creation.

### 10.5 Decision log integration

The following events should create optional or automatic decision log entries:

- project created;
- objective changed;
- project archived;
- dataset imported;
- report generated.

Automatic entries should be concise and should not clutter the log excessively.

### 10.6 Out of scope

- project sharing;
- project templates;
- user roles;
- project permissions;
- cloud backup.

---

## 11. F03 - Dataset import

### 11.1 Objective

Allow users to import a CSV dataset into a local project and register its metadata.

### 11.2 Supported formats for MVP

| Format | MVP status | Notes |
|---|---:|---|
| CSV | Supported | Required for MVP. |
| TSV | Optional | Can be supported if trivial through delimiter selection. |
| Excel | Deferred | Later. |
| Parquet | Deferred | Later. |
| JSON | Deferred | Later. |
| Database connection | Deferred | Later. |

### 11.3 Dataset metadata

The MVP must capture:

| Field | Required | Description |
|---|---:|---|
| Dataset name | Yes | Defaults to file name but editable. |
| Source file path | Yes | Local path or copied internal file path. |
| Original file name | Yes | File name at import. |
| File size | Yes | Size in bytes or readable format. |
| Imported date | Yes | Date/time of import. |
| Row count | Yes | Computed after parsing. |
| Column count | Yes | Computed after parsing. |
| Delimiter | Yes | Detected or selected delimiter. |
| Encoding | If known | UTF-8 by default if detection is not implemented. |
| Has header row | Yes | Detected or user-selected. |
| Dataset version | Yes | Initial version number or generated ID. |

### 11.4 Import requirements

| ID | Requirement |
|---|---|
| F03-R01 | The user must be able to select a local CSV file. |
| F03-R02 | The app must parse the CSV without sending data to any remote service. |
| F03-R03 | The app must detect or ask for delimiter if parsing is ambiguous. |
| F03-R04 | The app must detect whether a header row exists, or allow the user to confirm. |
| F03-R05 | The app must show import errors in plain language. |
| F03-R06 | The app must reject empty files with a clear error. |
| F03-R07 | The app must reject files with no usable columns. |
| F03-R08 | The app must create a dataset version after successful import. |
| F03-R09 | The app must not overwrite existing project data without user confirmation. |
| F03-R10 | The app must store enough metadata to regenerate a report without reparsing the full file when possible. |

### 11.5 CSV parsing assumptions

MVP parsing should support:

- comma delimiter;
- semicolon delimiter;
- quoted strings;
- header row;
- missing values represented by empty cells;
- line endings used on Windows, macOS and Linux.

MVP parsing may defer:

- complex encoding detection;
- malformed CSV recovery;
- multi-line quoted values;
- very large streaming previews;
- schema inference from multiple files.

### 11.6 Import preview before confirmation

If practical, the app should show a small preview before final import:

- detected delimiter;
- first rows;
- detected columns;
- row count estimate or final count;
- parsing warnings.

For MVP simplicity, import may happen first and preview immediately after, as long as errors are handled clearly.

### 11.7 Acceptance criteria

- A user can import a valid CSV and see it in the project workspace.
- A user receives an understandable error when the file is empty, unreadable or invalid.
- No network call is made during import.
- The imported dataset has a dataset version and basic metadata.

### 11.8 Out of scope

- automatic data cleaning during import;
- remote dataset URLs;
- database imports;
- live data refresh;
- multi-file datasets.

---

## 12. F04 - Dataset preview

### 12.1 Objective

Show the user what the imported dataset looks like without overwhelming the UI or loading unnecessary data.

### 12.2 Requirements

| ID | Requirement |
|---|---|
| F04-R01 | The app must show column names. |
| F04-R02 | The app must show a limited number of preview rows. |
| F04-R03 | The app must show row count and column count. |
| F04-R04 | The app must visually distinguish missing or empty values. |
| F04-R05 | The app must allow horizontal scrolling or equivalent for wide datasets. |
| F04-R06 | The app must not freeze on moderately sized CSV files. |
| F04-R07 | The preview must be read-only in MVP v0.1. |

### 12.3 Preview limits

Recommended MVP defaults:

| Limit | Suggested default |
|---|---:|
| Preview rows | 50 |
| Maximum displayed columns before horizontal scroll | UI dependent |
| Maximum in-memory rows for profiling | Implementation decision; must be documented |

If the application samples data for performance reasons, it must clearly state that profiling is sample-based.

### 12.4 Acceptance criteria

- After import, a user can inspect the first rows and column names.
- Missing values are visible or identifiable.
- Wide datasets remain navigable.
- Preview does not allow accidental editing of raw data.

### 12.5 Out of scope

- spreadsheet-like data editing;
- row filtering;
- sorting;
- formulas;
- manual data correction.

---

## 13. F05 - Dataset profiling

### 13.1 Objective

Automatically compute deterministic summaries that help the user understand the dataset and identify review points.

Profiling must support the first methodology step: understanding what is inside the sample before analysis or modelling.

### 13.2 Dataset-level profile

The app must compute:

| Metric | MVP | Description |
|---|---:|---|
| Row count | Yes | Number of records. |
| Column count | Yes | Number of columns. |
| Total missing cells | Yes | Count of empty/null-like values. |
| Missing cell ratio | Yes | Missing cells divided by total cells. |
| Duplicate row count | Optional | Useful but not mandatory in MVP. |
| Memory estimate | Optional | Nice to have. |
| Parsing warnings | Yes, if any | Warnings captured during import. |

### 13.3 Column-level profile

For every column, the app must compute when possible:

| Metric | MVP | Description |
|---|---:|---|
| Column name | Yes | Original column name. |
| Inferred primitive type | Yes | Integer, decimal, boolean, date/time, text, unknown. |
| Inferred methodological type | Yes | Continuous, discrete, text, date/time, identifier, unknown. |
| Missing count | Yes | Number of missing values. |
| Missing ratio | Yes | Missing values divided by row count. |
| Unique count | Yes | Number of distinct non-missing values. |
| Sample values | Yes | Small list of example values. |
| Minimum | Numeric only | Minimum numeric value. |
| Maximum | Numeric only | Maximum numeric value. |
| Mean | Numeric only | Average. |
| Median | Numeric only | Median. |
| Q1/Q3 | Numeric only | Quartiles. |
| Standard deviation | Optional MVP | Useful but can be added after mean/median. |
| IQR outlier candidate count | Numeric only | Count outside `[Q1 - 1.5*IQR, Q3 + 1.5*IQR]`. |
| Top value counts | Categorical/text | Most frequent values. |

### 13.4 Type inference requirements

| ID | Requirement |
|---|---|
| F05-R01 | The app must infer a primitive type for each column. |
| F05-R02 | The app must infer a methodological type for each column. |
| F05-R03 | The user must be able to override inferred methodological type in the variable dictionary. |
| F05-R04 | Inferred values must be displayed as inferred, not definitive. |
| F05-R05 | Inference must ignore missing values where appropriate. |
| F05-R06 | The app must mark ambiguous columns as needing review. |

### 13.5 Suggested primitive types

```text
Integer
Decimal
Boolean
DateTime
Text
Unknown
```

### 13.6 Suggested methodological types

```text
Continuous
Discrete
Text
DateTime
Identifier
TargetCandidate
Ignored
Unknown
```

### 13.7 Methodological type inference guidance

Ariadne should use cautious heuristics.

Examples:

| Pattern | Suggested type |
|---|---|
| Numeric with many unique values | Continuous |
| Numeric with few unique values | Discrete or Identifier candidate, needs review |
| Text with few repeated values | Discrete |
| Text with mostly unique values | Text or Identifier candidate |
| Date-like values | DateTime |
| Column named `id`, `uuid`, `identifier` | Identifier candidate |
| Column with unique value for every row | Identifier candidate |

The user must always be able to correct these suggestions.

### 13.8 Missing value profiling

The app must show missing values clearly.

For each column, the UI should display:

- missing count;
- missing percentage;
- severity indicator.

Suggested severity thresholds:

| Missing ratio | Severity |
|---:|---|
| 0% | None |
| > 0% and < 5% | Low |
| >= 5% and < 30% | Medium |
| >= 30% and < 70% | High |
| >= 70% | Critical |

Thresholds are product defaults, not statistical truths. The UI should avoid overclaiming.

### 13.9 Outlier candidate profiling

For numeric columns, the MVP may compute IQR outlier candidates.

The UI must call them **candidate outliers**, not errors.

The user must be reminded that outliers require contextual review. A value can be statistically unusual but still meaningful.

### 13.10 Acceptance criteria

- After CSV import, the user can view a dataset profile.
- Every column has a profile row/card.
- Missing values are quantified.
- Numeric columns show basic statistics.
- Categorical columns show value counts or examples.
- Inferred types are visible and reviewable.
- Outlier candidates are presented cautiously.

### 13.11 Out of scope

- automated cleaning;
- automatic feature selection;
- advanced anomaly detection;
- causal claims;
- model-based profiling;
- remote profiling services.

---

## 14. F06 - Variable dictionary

### 14.1 Objective

Allow the user to document the meaning, role and methodological interpretation of each variable.

This is one of the most important MVP features because it connects technical profiling with real-world meaning.

### 14.2 Variable fields

For each column, Ariadne should support:

| Field | Required | Description |
|---|---:|---|
| Column name | Yes | Original dataset column name. |
| Display name | No | Human-friendly name. |
| Description | No | What the variable means. |
| Unit | No | Unit of measurement, if any. |
| Primitive type | Yes | Inferred technical type. |
| Methodological type | Yes | Continuous, discrete, text, date/time, identifier, etc. |
| Role | Yes | Feature, target, identifier, ignored, metadata, unknown. |
| Source notes | No | Where this variable came from. |
| Quality notes | No | Concerns, limitations, known issues. |
| Missing value interpretation | No | What missing values may mean. |
| Review status | Yes | Needs review, reviewed, ignored, etc. |

### 14.3 Variable role values

Suggested role values:

```text
Unknown
Feature
Target
Identifier
Metadata
Ignored
```

### 14.4 Requirements

| ID | Requirement |
|---|---|
| F06-R01 | The user must be able to view all variables in a dataset. |
| F06-R02 | The user must be able to edit variable descriptions. |
| F06-R03 | The user must be able to edit units. |
| F06-R04 | The user must be able to override methodological type. |
| F06-R05 | The user must be able to assign a variable role. |
| F06-R06 | The user must be able to mark a variable as reviewed. |
| F06-R07 | The user must be able to add quality notes. |
| F06-R08 | The variable dictionary must be included in the generated report. |
| F06-R09 | Changes to important variable classifications should create or suggest a decision log entry. |

### 14.5 Review behavior

A variable should be considered reviewed when the user explicitly confirms it.

Ariadne should display a count such as:

```text
12 of 18 variables reviewed
```

The report must distinguish reviewed variables from unreviewed variables.

### 14.6 Acceptance criteria

- A user can correct an inferred type.
- A user can document what a column means.
- A user can mark variables as feature, target, identifier or ignored.
- The generated report contains a variable dictionary section.

### 14.7 Out of scope

- editing raw values;
- renaming columns in the underlying dataset;
- executing transformations;
- schema migrations;
- ontology management.

---

## 15. F07 - Fundamental analysis

### 15.1 Objective

Guide the user through contextual questions that determine whether the dataset can be trusted and interpreted.

Fundamental analysis complements technical analysis.

The MVP questionnaire follows six categories:

```text
What
Who
When
Where
How
Why
```

### 15.2 Questionnaire categories

#### What

The user should document:

- what the dataset represents;
- what each row represents;
- what the main variables mean;
- what the target phenomenon is;
- what units are used;
- what is missing from the dataset.

#### Who

The user should document:

- who collected the data;
- who owns or maintains it;
- who benefits from its use;
- whether the source is trustworthy;
- whether stakeholders may introduce bias.

#### When

The user should document:

- when the data was collected;
- the collection period;
- update frequency;
- whether the data is stale;
- whether the time period is representative.

#### Where

The user should document:

- geographic origin;
- location of represented entities;
- regional limitations;
- whether location affects interpretation.

#### How

The user should document:

- collection method;
- measurement process;
- instruments or systems used;
- quality control;
- known collection limitations;
- possible measurement bias.

#### Why

The user should document:

- why the data was collected;
- original intended use;
- why it is available;
- whether the collection purpose may introduce bias;
- whether the current project use differs from the original purpose.

### 15.3 Answer model

Each answer should support:

| Field | Required | Description |
|---|---:|---|
| Question ID | Yes | Stable internal identifier. |
| Category | Yes | What, Who, When, Where, How, Why. |
| Prompt | Yes | User-facing question. |
| Answer text | No | User response. |
| Confidence | Optional | High, medium, low, unknown. |
| Status | Yes | Unanswered, answered, unknown, not applicable. |
| Notes | Optional | Additional details. |

### 15.4 Requirements

| ID | Requirement |
|---|---|
| F07-R01 | The app must provide a guided fundamental analysis questionnaire. |
| F07-R02 | The questionnaire must be grouped by What, Who, When, Where, How and Why. |
| F07-R03 | The user must be able to answer questions in any order. |
| F07-R04 | The user must be able to mark a question as unknown. |
| F07-R05 | The user must be able to mark a question as not applicable. |
| F07-R06 | The app must show completion progress for the questionnaire. |
| F07-R07 | The report must include answered questions and explicitly list unknown or missing answers. |
| F07-R08 | The UI should explain that missing answers are acceptable if documented. |
| F07-R09 | The user must be able to update answers after initial completion. |

### 15.5 Completion rules

A fundamental analysis section is complete when:

- every required question is answered, marked unknown, or marked not applicable;
- the user explicitly marks the section reviewed.

Ariadne should not require perfect knowledge. Unknown answers should be treated as documented uncertainty.

### 15.6 Acceptance criteria

- A user can complete the six-part questionnaire.
- Unknown context can be recorded without blocking the workflow.
- The report includes both known and unknown context.
- Completion progress is visible.

### 15.7 Out of scope

- automatic web research about datasets;
- remote metadata enrichment;
- automatic bias certification;
- legal compliance scoring.

---

## 16. F08 - Decision log

### 16.1 Objective

Provide a structured place to record important decisions, assumptions, observations, limitations and next actions.

The decision log is the traceability backbone of Ariadne.

### 16.2 Decision entry fields

| Field | Required | Description |
|---|---:|---|
| Title | Yes | Short summary. |
| Type | Yes | Decision, assumption, observation, limitation, question, action. |
| Description | No | Detailed explanation. |
| Rationale | No | Why this decision was made. |
| Related section | No | Dataset, variable, fundamental analysis, etc. |
| Related entity ID | No | Optional project/dataset/variable reference. |
| Created date | Yes | Automatic. |
| Updated date | Yes | Automatic. |
| Status | Yes | Open, accepted, rejected, deferred, resolved. |
| Severity | Optional | Low, medium, high, critical. |
| Tags | Optional | User-defined labels. |

### 16.3 Entry types

Suggested types:

```text
Observation
Assumption
Decision
Limitation
Question
ActionItem
Risk
```

### 16.4 Requirements

| ID | Requirement |
|---|---|
| F08-R01 | The user must be able to create a decision log entry. |
| F08-R02 | The user must be able to edit a decision log entry. |
| F08-R03 | The user must be able to change entry status. |
| F08-R04 | The user must be able to associate an entry with a project section. |
| F08-R05 | The app should create automatic entries for major events when useful. |
| F08-R06 | The decision log must be included in the generated report. |
| F08-R07 | The user must be able to distinguish assumptions from decisions. |
| F08-R08 | The UI must not hide open questions or unresolved limitations. |

### 16.5 Automatic decision candidates

Ariadne may suggest entries for:

- imported dataset;
- changed variable role;
- changed methodological type;
- high missing values detected;
- high outlier candidate count;
- report generated.

Suggested entries should be editable before acceptance when possible.

### 16.6 Acceptance criteria

- A user can add a decision such as “Column `customer_id` marked as identifier and ignored for modelling.”
- A user can add a limitation such as “Dataset source organization unknown.”
- The report includes the decision log.
- Open questions remain visible.

### 16.7 Out of scope

- multi-user comments;
- approval workflows;
- digital signatures;
- enterprise audit logs.

---

## 17. F09 - Methodology progress

### 17.1 Objective

Help the user understand where they are in the project workflow and what still needs review.

### 17.2 Requirements

| ID | Requirement |
|---|---|
| F09-R01 | The app must show project-level methodology progress. |
| F09-R02 | The app must show which MVP sections are completed, in progress or needing review. |
| F09-R03 | The app must show a recommended next action. |
| F09-R04 | The app must not block report generation solely because a section is incomplete. |
| F09-R05 | The report must mention incomplete sections. |

### 17.3 Progress indicators

Suggested MVP indicators:

| Section | Completion signal |
|---|---|
| Project overview | Project has name and objective or user marked objective deferred. |
| Dataset | At least one dataset imported successfully. |
| Profiling | Dataset profile computed. |
| Variables | User reviewed some or all variables. |
| Fundamental analysis | Questions answered, unknown or not applicable. |
| Decisions | Optional but visible. |
| Report | Report generated at least once. |

### 17.4 Acceptance criteria

- A new project shows “Import a dataset” as next action.
- A project with imported dataset but no variable review shows “Review variable dictionary” as next action.
- Incomplete sections appear in the report as incomplete, not silently omitted.

---

## 18. F10 - Markdown report generation

### 18.1 Objective

Generate a human-readable methodology report from the project state.

Markdown is the first report format because it is simple, versionable and readable outside Ariadne.

### 18.2 Report sections for MVP

The MVP report must include:

```text
Title
Generated date
Project summary
Project objective
Dataset summary
Dataset profile summary
Variable dictionary
Fundamental analysis
Decision log
Limitations and unknowns
Next recommended steps
```

### 18.3 Report metadata

The report should include:

| Field | Required | Description |
|---|---:|---|
| Project name | Yes | From project metadata. |
| Generated date | Yes | Local date/time. |
| Ariadne version | If available | App version. |
| Dataset name | If available | Current dataset. |
| Dataset version | If available | Current dataset version. |
| Report status | Optional | Draft/final if implemented. |

### 18.4 Requirements

| ID | Requirement |
|---|---|
| F10-R01 | The user must be able to generate a Markdown report. |
| F10-R02 | The report must be generated locally. |
| F10-R03 | The user must be able to choose or see the output location. |
| F10-R04 | The report must include project summary. |
| F10-R05 | The report must include dataset metadata when a dataset exists. |
| F10-R06 | The report must include the variable dictionary. |
| F10-R07 | The report must include fundamental analysis answers. |
| F10-R08 | The report must explicitly mention unanswered or unknown fundamental analysis items. |
| F10-R09 | The report must include the decision log. |
| F10-R10 | The report must include limitations and next steps. |
| F10-R11 | The app must show success or failure after generation. |
| F10-R12 | The report must be readable without opening Ariadne. |

### 18.5 Report style

The report should use clear headings and practical language.

It should avoid:

- hype language;
- unsupported claims;
- pretending inferred types are definitive;
- hiding missing context;
- excessive raw data dumps.

### 18.6 Handling incomplete projects

The user should be able to generate a report at any point.

If sections are incomplete, the report should include notices such as:

```text
This section is incomplete.
The user has not yet reviewed all variable classifications.
The dataset source is unknown and should be verified before modelling.
```

### 18.7 Acceptance criteria

- A report can be generated after project creation and dataset import.
- A report can be generated even if fundamental analysis is incomplete.
- The report includes missing information warnings.
- The report can be opened in a normal text editor or Markdown viewer.

### 18.8 Out of scope

- PDF export;
- Word export;
- custom report templates;
- charts embedded in reports;
- signed reports;
- compliance certification.

---

## 19. F11 - Local persistence

### 19.1 Objective

Store project metadata locally so that users can close and reopen their work.

### 19.2 MVP persistence decision

The exact storage mechanism is defined in `06-local-first-storage.md`.

From a functional perspective, the MVP needs to persist:

- project metadata;
- dataset metadata;
- column profiles;
- variable dictionary edits;
- fundamental analysis answers;
- decision log entries;
- report generation records.

The raw dataset may be referenced by original path or copied into the project workspace. This decision must be finalized in the storage specification.

### 19.3 Requirements

| ID | Requirement |
|---|---|
| F11-R01 | The app must persist project metadata locally. |
| F11-R02 | The app must persist dataset metadata locally. |
| F11-R03 | The app must persist variable dictionary edits locally. |
| F11-R04 | The app must persist fundamental analysis answers locally. |
| F11-R05 | The app must persist decision log entries locally. |
| F11-R06 | The app must not require a server to reopen a project. |
| F11-R07 | The app must handle missing referenced dataset files gracefully. |
| F11-R08 | The app must avoid hardcoded absolute paths in code. |

### 19.4 Missing dataset file behavior

If a project references a source CSV that no longer exists, the app should:

- open the project metadata;
- show a warning that the source dataset file is missing;
- preserve existing profiles and documentation if available;
- offer a future option to relink the dataset.

### 19.5 Acceptance criteria

- A user can create a project, add metadata and close the app without losing work.
- A missing dataset file does not destroy project documentation.
- The app remains useful offline.

### 19.6 Out of scope

- cloud sync;
- remote backups;
- conflict resolution;
- multi-device synchronization;
- encryption at rest unless added later.

---

## 20. F12 - Settings and About

### 20.1 Objective

Provide basic information about the application, its local-first behavior and version.

### 20.2 Requirements

| ID | Requirement |
|---|---|
| F12-R01 | The app must provide an About screen or equivalent. |
| F12-R02 | The About screen must show product name and version if available. |
| F12-R03 | The app must state that MVP data stays local by default. |
| F12-R04 | The app must not include remote account settings in the MVP. |
| F12-R05 | The app may include links to project documentation or GitHub later. |

### 20.3 Acceptance criteria

- A user can confirm that Ariadne is local-first.
- The version is visible if build metadata is available.

---

# Post-MVP functional requirements

The following modules are planned but not required for MVP v0.1.

They are included here to guide architecture and prevent short-term design choices that would block them.

---

## 21. F20 - Technical analysis

### 21.1 Objective

Provide basic exploratory data analysis functionality based on variable types.

### 21.2 Functional categories

Ariadne should support the five analysis cases:

| Analysis type | Variables | Expected output |
|---|---|---|
| Univariate discrete | One discrete variable | Value counts, bar chart-ready data. |
| Univariate continuous | One continuous variable | Mean, median, variance, quartiles, histogram-ready data, boxplot-ready data. |
| Multivariate discrete-discrete | Two discrete variables | Contingency table. |
| Multivariate discrete-continuous | One discrete + one continuous variable | Grouped statistics. |
| Multivariate continuous-continuous | Two continuous variables | Scatterplot-ready data, correlation candidate. |

### 21.3 Requirements

| ID | Requirement |
|---|---|
| F20-R01 | The app should generate univariate summaries for reviewed variables. |
| F20-R02 | The app should generate value counts for discrete variables. |
| F20-R03 | The app should generate distribution statistics for continuous variables. |
| F20-R04 | The app should generate contingency tables for two discrete variables. |
| F20-R05 | The app should generate grouped continuous summaries by discrete variable. |
| F20-R06 | The app should generate scatterplot-ready data for two continuous variables. |
| F20-R07 | The app should allow users to save meaningful observations to the decision log or hypothesis module. |

### 21.4 Out of scope until explicitly planned

- full BI dashboarding;
- arbitrary chart builder;
- advanced statistical modelling;
- automatic insight generation;
- claims of causality.

---

## 22. F30 - Hypothesis tracking and statistical tests

### 22.1 Objective

Help users distinguish observations, assumptions, hypotheses, tests and conclusions.

### 22.2 Hypothesis concepts

A future hypothesis module should support:

| Concept | Description |
|---|---|
| Observation | Pattern seen in data. |
| Hypothesis | Testable claim. |
| Null hypothesis H0 | Baseline claim to test. |
| Alternative hypothesis H1 | Alternative claim. |
| Test plan | How the hypothesis will be tested. |
| Evidence | Result, p-value or qualitative support. |
| Conclusion | Rejected H0, failed to reject H0, inconclusive, or deferred. |

### 22.3 Requirements

| ID | Requirement |
|---|---|
| F30-R01 | The user should be able to create observations from analysis results. |
| F30-R02 | The user should be able to promote an observation into a hypothesis. |
| F30-R03 | The user should be able to define H0 and H1. |
| F30-R04 | The app should suggest possible statistical tests based on variable types. |
| F30-R05 | The app should show assumptions and conditions for suggested tests. |
| F30-R06 | The app should warn when a hypothesis is tested on the same data used to discover it. |
| F30-R07 | The app should support observation/confirmation dataset separation when implemented. |
| F30-R08 | Test results should be included in reports. |

### 22.4 Candidate tests

Initial test suggestions may include:

| Situation | Candidate test |
|---|---|
| One discrete variable vs theoretical proportion | Binomial test. |
| One discrete variable vs theoretical frequencies | Chi-square goodness-of-fit. |
| One continuous variable vs theoretical mean | One-sample Student test. |
| Two discrete variables | Chi-square independence test. |
| One discrete variable with two groups + one continuous variable | Two-sample Student test or Welch test. |
| One discrete variable with more than two groups + one continuous variable | ANOVA. |
| Two continuous variables | Pearson or Spearman correlation test. |

Ariadne should treat these as guidance, not automatic proof.

### 22.5 Out of scope until explicitly planned

- becoming a full statistical package;
- hiding test assumptions;
- automatic scientific conclusions;
- p-hacking workflows;
- Bayesian workflows in early versions.

---

## 23. F40 - Preprocessing planner

### 23.1 Objective

Represent preprocessing as a sequence of documented decisions before eventually executing transformations.

### 23.2 Preprocessing categories

Ariadne should support planning for:

- missing-value handling;
- outlier handling;
- encoding;
- normalization or standardization;
- feature engineering;
- feature selection;
- variable inclusion/exclusion;
- train/test-safe transformation rules.

### 23.3 Requirements

| ID | Requirement |
|---|---|
| F40-R01 | The user should be able to create a preprocessing plan. |
| F40-R02 | The user should be able to add preprocessing steps. |
| F40-R03 | Each step should include target columns, strategy, rationale and status. |
| F40-R04 | Missing-value strategies should be documentable. |
| F40-R05 | Outlier decisions should require contextual notes. |
| F40-R06 | Encoding choices should distinguish ordinal and one-hot encoding. |
| F40-R07 | Normalization choices should distinguish min-max scaling and standardization. |
| F40-R08 | The app should warn that fit-like transformation rules must be learned on training data only when modelling is implemented. |
| F40-R09 | The preprocessing plan should be included in reports. |

### 23.4 Preprocessing step fields

| Field | Description |
|---|---|
| Step name | Human-readable name. |
| Step type | Missing values, outliers, encoding, normalization, feature engineering, etc. |
| Target variables | Variables affected. |
| Strategy | Chosen approach. |
| Rationale | Why this approach was selected. |
| Status | Planned, applied, rejected, deferred. |
| Risks | Potential side effects. |
| Created date | Automatic. |

### 23.5 Out of scope for first preprocessing version

- executable transformations;
- automatic pipeline optimization;
- advanced feature stores;
- AutoML preprocessing search.

---

## 24. F50 - Modelling assistant

### 24.1 Objective

Add simple, transparent modelling once the methodology foundation is stable.

### 24.2 Modelling principles

When modelling is introduced, Ariadne should:

- start with simple models;
- always record train/test split information;
- document target variable selection;
- compare baseline and model performance;
- avoid pretending that model training is the whole project.

### 24.3 Candidate MVP modelling features for later

| Requirement | Description |
|---|---|
| Select target variable | User selects target from reviewed variable dictionary. |
| Select problem type | Regression, binary classification, multiclass classification. |
| Create train/test split | Record split ratio, random seed and method. |
| Run baseline model | Simple baseline for comparison. |
| Run simple ML.NET model | First native .NET modelling path. |
| Store experiment run | Save parameters and metrics. |
| Compare runs | Lightweight comparison, not full experiment tracker. |

### 24.4 Out of scope until explicitly planned

- AutoML;
- deep learning;
- model deployment;
- hosted model registry;
- GPU training;
- advanced Python integration.

---

## 25. F60 - Evaluation assistant

### 25.1 Objective

Help the user reason about generalization, not just report metrics.

### 25.2 Evaluation categories

Ariadne should eventually support:

| Problem type | Metrics |
|---|---|
| Regression | MAE, MSE, RMSE, R². |
| Classification | Confusion matrix, accuracy, precision, recall, F1, ROC/AUC where applicable. |

### 25.3 Requirements

| ID | Requirement |
|---|---|
| F60-R01 | The app should show train metrics and test metrics separately. |
| F60-R02 | The app should highlight large train/test gaps. |
| F60-R03 | The app should help identify possible overfitting. |
| F60-R04 | The app should help identify possible underfitting. |
| F60-R05 | The app should allow user-written error analysis notes. |
| F60-R06 | The app should allow documenting applicability boundaries. |
| F60-R07 | The app should support confidence interval notes or calculations when implemented. |
| F60-R08 | Evaluation results should be included in reports. |

### 25.4 Out of scope until explicitly planned

- automated model certification;
- fairness audits as a first implementation;
- production monitoring;
- online evaluation;
- deployment metrics.

---

## 26. F70 - Future web and commercial features

### 26.1 Objective

Keep the product functionally ready for reuse in a future Blazor Web or commercial version without complicating the MVP.

### 26.2 Future features

Potential future features:

- web project workspace;
- user accounts;
- team workspaces;
- comments;
- cloud sync;
- shared reports;
- organization templates;
- advanced exports;
- audit trails;
- governance workflows.

### 26.3 MVP rule

Do not implement web/commercial features in MVP v0.1.

The only MVP requirement is to avoid designing the local application in a way that prevents future reuse.

---

# Cross-functional behavior

---

## 27. Local-first behavior

### 27.1 Functional requirements

| ID | Requirement |
|---|---|
| LF-R01 | MVP features must work without internet access. |
| LF-R02 | Imported datasets must not be uploaded anywhere. |
| LF-R03 | Reports must be generated locally. |
| LF-R04 | Project metadata must be stored locally. |
| LF-R05 | Any future remote feature must be explicit and opt-in. |

### 27.2 User-facing language

The application should clearly communicate:

```text
Your datasets stay local by default.
Ariadne does not require an account for the local workflow.
```

Avoid making promises that are not implemented, such as encryption or compliance certification, unless those features are added.

---

## 28. Error handling

### 28.1 Principles

Errors should be:

- understandable;
- actionable;
- non-destructive;
- specific when possible;
- calm in tone.

### 28.2 Required error cases

| Scenario | Expected behavior |
|---|---|
| Empty project name | Show validation message. |
| CSV file missing | Show warning and allow project metadata to remain open. |
| Invalid CSV | Show parsing error. |
| Empty CSV | Reject with clear message. |
| Unsupported format | Explain supported formats. |
| Report write failure | Show output path and reason if available. |
| Storage failure | Warn user and avoid pretending data was saved. |
| Profiling failure for one column | Continue profiling other columns if possible and show warning. |

### 28.3 Error message style

Good:

```text
Ariadne could not read this CSV file. Check that the file is not empty and that it uses a supported delimiter.
```

Avoid:

```text
Unhandled exception: index out of range.
```

Technical details may be available in an expandable section or logs.

---

## 29. Validation rules

### 29.1 Project validation

- Project name is required.
- Project name must not be only whitespace.
- Project name should have a reasonable maximum length.
- Objective is optional.
- Description is optional.

### 29.2 Dataset validation

- File must exist at selection time.
- File must be readable.
- File must contain at least one row or header.
- File must contain at least one column.
- Duplicate column names should be detected and handled or warned about.

### 29.3 Variable validation

- Methodological type must be one of supported values.
- Role must be one of supported values.
- A target variable is optional in MVP.
- More than one target variable may be allowed as documentation, but modelling later may restrict this.

### 29.4 Fundamental analysis validation

- Answers may be empty while in progress.
- To mark a section complete, each question must be answered, unknown or not applicable.
- Unknown answers should not be treated as errors.

---

## 30. Accessibility and usability

### 30.1 MVP accessibility requirements

| ID | Requirement |
|---|---|
| A11Y-R01 | Interactive controls must have readable labels. |
| A11Y-R02 | Forms must show validation messages near relevant fields. |
| A11Y-R03 | Tables must remain readable with keyboard and screen scaling where practical. |
| A11Y-R04 | Color must not be the only way to communicate severity or status. |
| A11Y-R05 | Text should use clear contrast. |

### 30.2 Usability requirements

- The user should always know the next recommended action.
- The app should avoid dense dashboards in the MVP.
- Explanatory text should be short and close to the action.
- Expert users should not be blocked by long tutorials.
- Empty states should explain what to do next.

---

## 31. Internationalization and language

### 31.1 MVP language

The documentation is written in English for contributor consistency.

The UI language decision remains open:

| Option | Notes |
|---|---|
| English first | Easier for open-source contribution. |
| French first | Natural for the initial product creator and source methodology. |
| Bilingual later | Good long-term goal. |

### 31.2 Functional recommendation

Implement UI text in a way that can be localized later.

Do not hardcode large amounts of user-facing methodology text deep in business logic.

---

## 32. Performance expectations

### 32.1 MVP performance goals

Ariadne should handle small to medium CSV files comfortably.

Suggested functional targets:

| Dataset size | Expected behavior |
|---|---|
| Up to 10k rows | Smooth import/profile on normal hardware. |
| Up to 100k rows | Should work, may take noticeable time. |
| Over 100k rows | May require sampling or warning in MVP. |
| Very wide datasets | Must remain navigable, even if profiling takes time. |

These are product targets, not strict technical guarantees. Actual limits should be refined after implementation benchmarks.

### 32.2 Long-running operations

For operations that take noticeable time, the app should show:

- loading state;
- progress message if available;
- non-technical failure message if the operation fails.

---

## 33. Privacy and security behavior

### 33.1 MVP privacy requirements

| ID | Requirement |
|---|---|
| P-R01 | The app must not upload datasets. |
| P-R02 | The app must not call external AI APIs in the MVP. |
| P-R03 | The app must not require telemetry. |
| P-R04 | The app must not require account creation. |
| P-R05 | Local file paths should not be exposed in reports unless user-facing and useful. |

### 33.2 Security limitations

Unless implemented later, Ariadne should not claim:

- encrypted local storage;
- regulated compliance;
- secure multi-user access;
- data anonymization;
- privacy risk scoring.

---

## 34. Reporting functional examples

### 34.1 Example report warning for missing context

```markdown
## Limitations and Unknowns

- The data collector is unknown.
- The collection period has not been documented.
- 4 of 12 variables have not been reviewed.
- Column `age` has 18% missing values and requires preprocessing decisions before modelling.
```

### 34.2 Example report variable entry

```markdown
| Column | Type | Role | Unit | Missing | Notes |
|---|---|---|---|---:|---|
| price | Continuous | Target | EUR | 0% | Sale price of the property. |
| surface | Continuous | Feature | m² | 2.1% | Living surface. Needs source confirmation. |
| property_id | Identifier | Ignored | - | 0% | Unique row identifier. |
```

### 34.3 Example decision log entry

```markdown
### Decision - Ignore `property_id`

`property_id` was marked as an identifier and excluded from future modelling because it uniquely identifies rows and does not represent a predictive phenomenon.
```

---

## 35. Empty states

Every important screen must have a useful empty state.

| Screen | Empty state message goal |
|---|---|
| Project list | Explain how to create a first project. |
| Project overview | Explain that no dataset has been imported yet. |
| Dataset | Explain CSV import. |
| Variable dictionary | Explain that variables appear after dataset profiling. |
| Fundamental analysis | Explain why context matters. |
| Decision log | Explain what kinds of decisions to record. |
| Report | Explain what will be included and what is missing. |

Empty states should teach without being long.

---

## 36. Notifications and status messages

Ariadne should use status messages for important events.

Examples:

- project created;
- dataset imported;
- profiling completed;
- variable saved;
- fundamental analysis saved;
- decision added;
- report generated;
- report generation failed.

Status messages should not interrupt unnecessarily.

---

## 37. Search, filtering and sorting

### 37.1 MVP support

Search/filtering is useful but not mandatory for the first MVP unless the UI becomes hard to use.

Recommended lightweight filters:

- variable list filter by type;
- variable list filter by review status;
- decision log filter by type/status.

### 37.2 Deferred

- global search;
- full-text search;
- saved filters;
- advanced query language.

---

## 38. Functional testing guidance

Functional tests should be derived from user flows and acceptance criteria.

### 38.1 MVP acceptance scenarios

#### Scenario 1 - Create project

```text
Given the user launches Ariadne
When they create a project named "Housing Price Analysis"
Then the project appears in the project list
And the project overview shows the objective field
And the next recommended action is to import a dataset
```

#### Scenario 2 - Import CSV

```text
Given an existing project
When the user imports a valid CSV file
Then Ariadne registers a dataset
And shows row count and column count
And shows a preview of rows and columns
And computes a dataset profile
```

#### Scenario 3 - Review variables

```text
Given a profiled dataset
When the user changes column `price` from Continuous Feature to Continuous Target
Then the variable dictionary is updated
And the variable is marked as needing or having review according to the UI action
And the change can be included in the report
```

#### Scenario 4 - Complete fundamental analysis

```text
Given a project with a dataset
When the user answers What, Who, When, Where, How and Why questions
And marks unknown answers where needed
Then Ariadne shows the fundamental analysis as complete or reviewed
And the report includes both answers and unknowns
```

#### Scenario 5 - Add decision

```text
Given a project
When the user adds a decision log entry
Then it appears in the decision log
And it appears in the generated report
```

#### Scenario 6 - Generate report

```text
Given a project with dataset profile and some documentation
When the user generates a Markdown report
Then a Markdown file is created locally
And the report contains project summary, dataset summary, variables, fundamental analysis and decision log
```

#### Scenario 7 - Incomplete report

```text
Given a project with incomplete fundamental analysis
When the user generates a report
Then the report is generated
And the report clearly marks incomplete or unknown sections
```

### 38.2 Deterministic analytics tests

Analytics tests should cover:

- row count;
- column count;
- missing count;
- missing ratio;
- unique count;
- numeric min/max;
- mean;
- median;
- quartiles;
- IQR outlier count;
- type inference;
- value counts.

### 38.3 Report tests

Report generation tests should verify:

- required sections exist;
- missing sections are reported;
- variable dictionary rows are included;
- decision entries are included;
- Markdown output is stable enough for snapshot or structured assertions.

---

## 39. Codex implementation guidance

When Codex implements features from this specification, it must:

1. preserve the MVP boundaries;
2. implement one functional slice at a time;
3. prefer simple deterministic behavior before advanced automation;
4. keep business logic outside Razor components;
5. keep MAUI host code minimal;
6. write tests for domain and analytics behavior;
7. update documentation when behavior changes;
8. avoid remote services;
9. avoid adding authentication or cloud storage;
10. use requirement IDs in commits, PRs or task summaries when helpful.

### 39.1 Recommended implementation order

```text
1. F01 - Application shell
2. F02 - Project management
3. F11 - Minimal local persistence
4. F03 - Dataset import
5. F04 - Dataset preview
6. F05 - Dataset profiling
7. F06 - Variable dictionary
8. F07 - Fundamental analysis
9. F08 - Decision log
10. F09 - Methodology progress
11. F10 - Markdown report generation
12. F12 - Settings/About
```

### 39.2 Codex summary format

For implementation tasks, Codex should answer with:

```text
Summary:
Tests:
Changed files:
Risks / notes:
Requirement IDs:
```

---

## 40. MVP completion definition

MVP v0.1 is functionally complete when:

1. A user can launch the app locally.
2. A user can create a local project.
3. A user can import a CSV dataset.
4. A user can preview the dataset.
5. Ariadne computes a useful dataset and column profile.
6. A user can review and edit variable documentation.
7. A user can complete fundamental analysis questions.
8. A user can create decision log entries.
9. A user can see methodology progress.
10. A user can generate a Markdown report.
11. The generated report is readable outside Ariadne.
12. The app does not require an account, server or internet connection.
13. The solution builds.
14. Unit tests pass for domain and analytics logic.
15. The implementation respects Clean Architecture boundaries.

---

## 41. Explicit MVP non-goals

The following features must not be added to MVP v0.1 unless the project owner explicitly changes scope:

- AutoML;
- model training;
- ML.NET integration;
- Python integration;
- web host;
- PostgreSQL;
- authentication;
- cloud sync;
- collaboration;
- user permissions;
- telemetry;
- LLM assistant;
- remote dataset enrichment;
- PDF export;
- dashboard builder;
- production deployment;
- model registry;
- experiment tracking server.

---

## 42. Open decisions

| Decision | Status | Notes |
|---|---|---|
| Local storage format | Open | JSON workspace vs SQLite. To be defined in `06-local-first-storage.md`. |
| UI default language | Open | English first vs French first vs localization-ready. |
| Raw dataset storage | Open | Reference original file vs copy into project workspace. |
| CSV parser library | Open | To be decided in technical architecture. |
| Charting library | Deferred | Not needed for MVP unless simple charts are added. |
| Report template structure | Open | Simple hardcoded Markdown first vs template files. |
| License | Open | MIT or Apache-2.0. |

---

## 43. Glossary

| Term | Meaning |
|---|---|
| Project | Local workspace containing datasets, analysis, decisions and reports. |
| Dataset | Imported data source registered inside a project. |
| Dataset version | Specific imported version of a dataset. |
| Column profile | Automatically computed technical summary of a column. |
| Variable dictionary | User-reviewed documentation of dataset columns. |
| Primitive type | Technical data type such as integer, decimal, date/time or text. |
| Methodological type | Data science interpretation such as continuous, discrete, identifier or target. |
| Fundamental analysis | Contextual analysis using What, Who, When, Where, How and Why. |
| Technical analysis | Statistical and graphical exploration of variables and relationships. |
| Decision log | Trace of observations, assumptions, decisions, risks and limitations. |
| Preprocessing | Preparation of data before modelling. |
| Report | Markdown output summarizing methodology, data, decisions and limitations. |

---

## 44. Final functional intent

Ariadne AI Workbench should make the methodology of an AI/ML project visible and repeatable.

The first version succeeds if it helps users answer:

```text
What data do I have?
What does it mean?
Where did it come from?
What is missing or uncertain?
Which variables have I reviewed?
Which assumptions and decisions have I made?
Can I generate a clear methodology report from this work?
```

Only after this foundation is useful should Ariadne move deeper into hypotheses, preprocessing execution, modelling and evaluation.
