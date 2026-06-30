# 08 - Development Roadmap

# Ariadne AI Workbench

**Status:** Draft v0.1  
**Project type:** Open-source local-first application  
**Primary platform:** .NET 10 MAUI Blazor Hybrid  
**Future platform:** Blazor Web application, if useful and/or commercializable  
**Repository name:** `ariadne-ai-workbench`  
**Solution name:** `Ariadne.sln`  
**Root namespace:** `Ariadne`  
**Related documents:** `00-project-brief.md`, `01-product-vision.md`, `02-functional-specification.md`, `03-technical-architecture.md`, `04-domain-model.md`, `05-methodology-workflow.md`, `06-local-first-storage.md`, `07-ui-ux-guidelines.md`

---

## 1. Purpose of this document

This document defines the **development roadmap** for **Ariadne AI Workbench**.

It translates the product vision, functional specification, technical architecture, domain model, methodology workflow, local-first storage strategy and UI/UX guidelines into a staged implementation plan that can be executed by human contributors and by Codex.

The roadmap is designed to answer:

```text
What should be built first?
What should be deferred?
Which milestones create usable value?
Which milestones unlock future milestones?
What must Codex not implement too early?
How do we know a milestone is complete?
```

This document is **not** a calendar plan. It does not assign fixed dates. The project should move through milestones based on quality gates, not artificial deadlines.

This document is also **not** the detailed Codex task list. That belongs in:

```text
09-codex-task-breakdown.md
```

This file defines the roadmap shape, milestones, order of delivery, quality gates and release boundaries.

---

## 2. Roadmap thesis

Ariadne must become useful before it becomes sophisticated.

The first valuable version is not an AutoML tool and not a model training platform. The first valuable version is a local workbench that helps a user:

```text
create an AI/ML project;
import a dataset;
inspect what the dataset contains;
understand variables and context;
record decisions;
generate a methodology report.
```

Only after this foundation is stable should Ariadne move toward:

```text
technical analysis;
hypothesis tracking;
statistical tests;
preprocessing planning;
model selection;
evaluation;
confidence intervals;
web reuse;
commercial collaboration features.
```

The roadmap must therefore follow the same methodological order as the product itself:

```text
Project
→ Dataset
→ Understand
→ Analyze
→ Hypothesize
→ Prepare
→ Model
→ Evaluate
→ Report
```

The most important rule is:

```text
Do not build modelling before the project can document data understanding.
```

---

## 3. Methodological alignment

Ariadne's development order must respect the methodology behind the application.

The source methodology emphasizes that a dataset is only an observed sample of a broader phenomenon. Before modelling, the practitioner must estimate what is known, understand variable domains and distributions, document context, formulate hypotheses carefully, preprocess data intentionally, and evaluate with metrics, uncertainty and applicability.

The roadmap must therefore prioritize capabilities in this order:

| Methodological priority | Product capability | Development priority |
|---|---|---|
| A dataset is an observed sample. | Project and dataset metadata. | Very high. |
| Variables must be understood before modelling. | Dataset profiling and variable dictionary. | Very high. |
| Technical analysis starts with simple descriptive statistics. | Column profiles and later univariate/multivariate analysis. | High. |
| Fundamental analysis gives meaning to statistics. | What, Who, When, Where, How, Why questionnaire. | Very high. |
| Decisions must be traceable. | Decision log. | Very high. |
| Missing context must be visible. | Report gaps and warnings. | High. |
| Hypotheses can create bias. | Hypothesis tracking with observation/confirmation distinction. | Later. |
| Preprocessing changes the problem. | Preprocessing plan with rationale. | Later. |
| Models must generalize. | Train/test split, validation, evaluation and confidence intervals. | Much later. |

The first implementation must make the user slow down enough to understand their project.

---

## 4. Roadmap principles

### 4.1 Local-first before cloud

The first product must work without:

```text
user account;
cloud storage;
remote API;
internet connection;
team workspace;
subscription;
server deployment.
```

A user should be able to download Ariadne, open it, create a project and work locally.

### 4.2 Reusable core before platform expansion

The project must be structured so that later Blazor Web reuse is possible.

This means:

- domain logic belongs in `Ariadne.Domain`;
- use cases belong in `Ariadne.Application`;
- analytics logic belongs in `Ariadne.Analytics`;
- local infrastructure belongs in `Ariadne.Infrastructure.Local`;
- reusable Razor UI belongs in `Ariadne.SharedUi`;
- MAUI is only a host in `Ariadne.Maui`;
- future web hosting must not require rewriting the core.

### 4.3 Small vertical slices

Each milestone should produce a working vertical slice where possible.

A vertical slice should include:

```text
Domain model
Application use case
Infrastructure implementation, if needed
Shared UI component or screen, if needed
Tests
Documentation update
```

Avoid large isolated technical changes that do not improve a user-visible workflow.

### 4.4 Quality gates over feature volume

A milestone is complete only when:

```text
dotnet build passes;
dotnet test passes;
architecture boundaries are respected;
new behavior is covered by tests;
no hidden cloud dependency is introduced;
README or docs are updated when behavior changes;
the feature works from the MAUI host if it is user-facing.
```

### 4.5 No premature ML

No ML.NET, Python bridge, Scikit-Learn runner, AutoML or LLM assistant should be introduced in the MVP unless explicitly approved in a later roadmap revision.

The MVP should focus on:

```text
methodology;
local storage;
CSV import;
profiling;
fundamental analysis;
decision log;
reporting.
```

### 4.6 Documentation drives implementation

Codex should treat the documentation as a source of truth.

When implementation and documentation disagree, the contributor must either:

1. update the implementation to follow the documentation; or
2. propose a documentation change before changing the architecture.

### 4.7 Conservative dependencies

The project should start with minimal dependencies.

Add a dependency only when it clearly solves a real problem and does not compromise:

```text
local-first operation;
cross-platform behavior;
future web reuse;
testability;
maintainability;
open-source friendliness.
```

---

## 5. Product maturity stages

Ariadne should mature through staged releases.

| Stage | Version range | Product identity | Main value |
|---|---:|---|---|
| Documentation baseline | `v0.0.x` | Project blueprint | Contributors and Codex understand what to build. |
| Technical foundation | `v0.1-alpha` | Running app shell | The repository builds and runs. |
| Local methodology MVP | `v0.1` | Local-first workbench | A user can document a dataset project and export a report. |
| Exploratory analysis | `v0.2` | Analysis assistant | A user can perform simple univariate and multivariate analysis. |
| Hypothesis workbench | `v0.3` | Evidence tracker | A user can formulate hypotheses and choose tests. |
| Preprocessing planner | `v0.4` | Preparation workbench | A user can plan preprocessing steps and rationale. |
| Modelling assistant | `v0.5` | Model planning tool | A user can select simple model families and record experiments. |
| Evaluation assistant | `v0.6` | Evaluation workbench | A user can assess metrics, confidence and applicability. |
| Stable local release | `v1.0` | Polished local product | A robust open-source local-first application. |
| Web reuse experiment | `v1.x` | Shared UI validation | The reusable core is hosted in Blazor Web. |
| Commercial-ready platform | Future | Team product | Collaboration, sync, hosted reports and premium workflows. |

The most important milestone is `v0.1`: a useful local methodology MVP.

---

## 6. Recommended release strategy

### 6.1 Pre-release labels

Use semantic pre-release labels to make project maturity explicit.

```text
v0.1.0-alpha.1    Repository builds and app starts.
v0.1.0-alpha.2    Local project creation works.
v0.1.0-alpha.3    CSV import and preview works.
v0.1.0-alpha.4    Dataset profiling works.
v0.1.0-beta.1     Full MVP flow works but needs hardening.
v0.1.0            First local methodology MVP.
```

### 6.2 Release rules

A release should not be tagged unless:

```text
all tests pass;
manual smoke test is completed;
no known data-loss issue exists;
release notes are written;
README reflects current behavior;
roadmap status is updated.
```

### 6.3 Platform release order

Recommended platform order:

| Priority | Platform | Reason |
|---:|---|---|
| 1 | Windows desktop | Most practical first target for local data workflows. |
| 2 | macOS desktop | Useful for data practitioners and developers. |
| 3 | Web host experiment | Validates UI reuse, not a commercial release yet. |
| 4 | Linux alternative host | MAUI desktop support is not the primary route; evaluate later. |
| 5 | Android/iOS | Possible through MAUI, but not central for dataset-heavy workflows. |

The MVP should not block on full cross-platform polish.

---

## 7. Workstreams

The roadmap is organized into workstreams. Milestones may touch several workstreams.

| Workstream | Purpose | Primary projects |
|---|---|---|
| Product documentation | Keep contributors aligned. | `/docs`, root markdown files. |
| Solution foundation | Create the buildable .NET solution. | all projects. |
| Domain model | Define core concepts and invariants. | `Ariadne.Domain`. |
| Application use cases | Coordinate user actions. | `Ariadne.Application`. |
| Analytics | Compute profiles and later analysis. | `Ariadne.Analytics`. |
| Local infrastructure | Persist projects and files locally. | `Ariadne.Infrastructure.Local`. |
| Shared UI | Build reusable Blazor components. | `Ariadne.SharedUi`. |
| MAUI host | Host the app locally. | `Ariadne.Maui`. |
| Reporting | Generate methodology artifacts. | `Ariadne.Application`, `Ariadne.Infrastructure.Local`, `Ariadne.SharedUi`. |
| Testing | Protect behavior and architecture. | `tests/*`. |
| Open-source operations | Prepare public collaboration. | README, LICENSE, CONTRIBUTING, issue templates. |

---

## 8. Milestone overview

| ID | Milestone | Target release | User-visible value | Main risk reduced |
|---|---|---:|---|---|
| M00 | Documentation baseline | `v0.0.x` | Clear blueprint. | Codex and contributors build the wrong thing. |
| M01 | Repository and solution setup | `v0.1-alpha.1` | App can build and start. | Technical feasibility. |
| M02 | Domain and application foundations | `v0.1-alpha.1` | Project concepts exist. | Weak domain base. |
| M03 | Shared UI and MAUI shell | `v0.1-alpha.1` | User sees app shell. | UI reuse uncertainty. |
| M04 | Local storage foundation | `v0.1-alpha.2` | Local persistence skeleton. | Data-loss and architecture risk. |
| M05 | Project management | `v0.1-alpha.2` | Create/open/list projects. | No user workflow. |
| M06 | Dataset import and preview | `v0.1-alpha.3` | Import CSV and preview rows. | File handling risk. |
| M07 | Dataset profiling | `v0.1-alpha.4` | Column statistics and type inference. | Analytics feasibility. |
| M08 | Variable dictionary | `v0.1-alpha.5` | User reviews variable meaning. | Automated guesses treated as truth. |
| M09 | Fundamental analysis | `v0.1-alpha.6` | User documents context. | Missing methodological value. |
| M10 | Decision log | `v0.1-alpha.7` | Decisions are traceable. | Unexplainable workflow. |
| M11 | Methodology progress | `v0.1-alpha.8` | User sees what is complete. | Navigation and completion ambiguity. |
| M12 | Markdown report | `v0.1-beta.1` | Export methodology report. | MVP not deliverable. |
| M13 | MVP hardening | `v0.1` | Stable local-first MVP. | Quality and usability. |
| M20 | Technical analysis | `v0.2` | Univariate and multivariate analysis. | Lack of exploratory value. |
| M30 | Hypothesis tracking | `v0.3` | H0/H1 and test planning. | Weak evidence workflow. |
| M40 | Preprocessing planner | `v0.4` | Document preparation steps. | Data transformation untracked. |
| M50 | Modelling assistant | `v0.5` | Plan simple models and experiments. | Premature or untraceable modelling. |
| M60 | Evaluation assistant | `v0.6` | Metrics, confidence, applicability. | Misleading performance claims. |
| M70 | Web reuse experiment | `v1.x` | Shared UI hosted on Web. | Reuse strategy uncertainty. |
| M80 | Commercial readiness | Future | Team workflows and hosted features. | Monetization uncertainty. |

---

## 9. M00 - Documentation baseline

### 9.1 Goal

Create enough project documentation for Codex and human contributors to build consistently.

### 9.2 Scope

M00 includes:

```text
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
AGENTS.md
README.md
```

### 9.3 Acceptance criteria

- Each document has a clear purpose.
- Documents are consistent with the product name `Ariadne AI Workbench`.
- Documents agree on the architecture and MVP scope.
- Documents clearly state that the MVP is local-first and not cloud-based.
- Documents clearly state that modelling is deferred.
- Codex can use the documents without guessing the product direction.

### 9.4 Non-goals

M00 does not require code.

---

## 10. M01 - Repository and solution setup

### 10.1 Goal

Create the initial repository and buildable .NET solution.

### 10.2 Scope

Create the solution structure:

```text
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
```

Add root files:

```text
README.md
AGENTS.md
LICENSE
.gitignore
.editorconfig
Directory.Build.props
Directory.Packages.props, if central package management is used
```

### 10.3 Required project references

```text
Ariadne.Domain
  depends on: none

Ariadne.Application
  depends on: Ariadne.Domain

Ariadne.Analytics
  depends on: Ariadne.Domain, Ariadne.Application only if application abstractions are needed

Ariadne.Infrastructure.Local
  depends on: Ariadne.Domain, Ariadne.Application, Ariadne.Analytics if needed

Ariadne.SharedUi
  depends on: Ariadne.Application, Ariadne.Domain DTOs/read models only when appropriate

Ariadne.Maui
  depends on: Ariadne.SharedUi, Ariadne.Application, Ariadne.Infrastructure.Local
```

### 10.4 Acceptance criteria

```text
dotnet build
```

must pass.

```text
dotnet test
```

must pass, even if tests are initially minimal.

The MAUI project must start with a basic Blazor screen showing:

```text
Ariadne AI Workbench
Your thread through AI methodology.
```

### 10.5 Non-goals

Do not add:

```text
SQLite;
ML.NET;
Python bridge;
OpenAI or other LLM integration;
a web host;
a login system;
cloud sync;
charts;
CSV import.
```

---

## 11. M02 - Domain and application foundations

### 11.1 Goal

Implement the minimal domain and application abstractions needed for project creation and future dataset workflows.

### 11.2 Scope

Implement initial domain concepts:

```text
AiProject
ProjectId
Dataset
DatasetId
DatasetVersion
DatasetVersionId
DecisionLogEntry
DecisionLogEntryId
MethodologyStage
MethodologyStageStatus
```

Implement basic domain invariants:

- project name must not be empty;
- project ID must be stable;
- dataset must belong to a project;
- decision log entry must have a category and message;
- timestamps must be stored in UTC where applicable.

Implement application contracts:

```csharp
public interface IProjectRepository
public interface IProjectCatalog
public interface IClock
public interface IGuidFactory
```

Add simple use cases:

```text
CreateProject
RenameProject
GetProjectSummary
ListProjects
AddDecisionLogEntry
```

Detailed Codex task sequencing lives in `09-codex-task-breakdown.md`. When the roadmap and task breakdown differ on exact task boundaries, use `09-codex-task-breakdown.md` as the implementation-level source of truth.

### 11.3 Acceptance criteria

- Domain tests cover entity creation and invariants.
- Application tests cover project creation and decision logging with fake repositories.
- No persistence technology is required yet.
- Domain has no dependency on infrastructure, MAUI or Blazor.

### 11.4 Non-goals

Do not implement dataset parsing yet.

---

## 12. M03 - Shared UI and MAUI shell

### 12.1 Goal

Create the first user-facing shell using reusable Blazor components.

### 12.2 Scope

Implement in `Ariadne.SharedUi`:

```text
App layout
Home page
Project list page placeholder
Project detail page placeholder
Workflow rail component placeholder
Status badge component
Empty state component
```

Implement in `Ariadne.Maui`:

```text
BlazorWebView host
Dependency injection bootstrap
Local app configuration bootstrap
```

### 12.3 UI acceptance criteria

The app should feel like a calm workbench, not a generic admin dashboard.

The first shell must include:

- product name;
- short tagline;
- local-first privacy message;
- create project action placeholder or working button if M05 is already implemented;
- navigation placeholders for the MVP workflow.

### 12.4 Architecture acceptance criteria

- `Ariadne.SharedUi` must not reference `Ariadne.Maui`.
- UI components must not contain business rules.
- Screens should call application use cases through injected services or view models.

---

## 13. M04 - Local storage foundation

### 13.1 Goal

Create a local-first storage foundation without overbuilding synchronization or cloud behavior.

### 13.2 Scope

Implement storage concepts from `06-local-first-storage.md`:

```text
application catalog;
project folder layout;
project database placeholder or first SQLite database;
local file store abstraction;
project path resolution;
backup-ready file naming conventions.
```

Recommended first directory model:

```text
Ariadne/
  catalog.db
  projects/
    <project-id>/
      project.db
      datasets/
      reports/
      exports/
```

### 13.3 Application interfaces

Implement or prepare:

```csharp
public interface ILocalAppDataPathProvider
public interface IProjectStoragePathResolver
public interface IProjectFileStore
public interface IProjectRepository
public interface IProjectCatalog
```

### 13.4 Acceptance criteria

- A project can be saved locally.
- A project can be listed after application restart.
- Tests use temporary directories.
- Storage implementation is isolated in `Ariadne.Infrastructure.Local`.
- No MAUI-specific file API leaks into Domain or Application.

### 13.5 Non-goals

Do not implement:

```text
cloud sync;
remote database;
user accounts;
sharing;
project encryption;
merge/conflict resolution.
```

---

## 14. M05 - Project management

### 14.1 Goal

Allow users to create, open, rename and archive local projects.

### 14.2 Scope

User workflows:

```text
Create project
List recent projects
Open project
Edit project description
Archive project
View project metadata
```

Project metadata:

```text
name;
description;
created at;
updated at;
methodology status;
local project path;
last opened at.
```

### 14.3 Screens

Implement:

```text
Projects page
Create project dialog/page
Project overview page
Project settings section
```

### 14.4 Acceptance criteria

- Project creation works from the MAUI app.
- Project list persists across restarts.
- Empty state is helpful.
- Invalid project names are rejected with useful messages.
- Unit tests and at least one integration test exist.

---

## 15. M06 - Dataset import and preview

### 15.1 Goal

Allow a user to import a CSV dataset and preview it safely.

### 15.2 Scope

User workflows:

```text
Choose CSV file
Configure delimiter and encoding when needed
Import dataset into local project storage
Create dataset version
Preview first rows
Show row count and column count
Show import warnings
```

### 15.3 Technical scope

Implement:

```csharp
public interface IDatasetImporter
public interface IDatasetPreviewReader
public interface IDatasetFileStore
```

The first implementation may support only CSV.

### 15.4 Preview rules

The UI should show:

```text
column names;
first N rows;
row count if available;
column count;
file name;
file size;
import timestamp;
parsing warnings.
```

### 15.5 Acceptance criteria

- A valid CSV can be imported.
- The imported file is copied into the project folder.
- The app does not depend on the original external file path after import.
- Preview works for at least simple comma-separated and semicolon-separated files.
- Bad files produce clear errors, not crashes.

### 15.6 Non-goals

Do not implement yet:

```text
Excel import;
Parquet import;
large streaming analytics;
remote datasets;
database connectors;
automatic target selection;
model training.
```

---

## 16. M07 - Dataset profiling

### 16.1 Goal

Compute the first useful automatic dataset profile.

### 16.2 Scope

For each column, compute where applicable:

```text
column name;
inferred data type;
methodological variable kind candidate;
row count;
non-null count;
missing count;
missing percentage;
unique count;
example values;
minimum;
maximum;
mean;
median;
standard deviation;
Q1;
Q3;
IQR;
potential outlier count using IQR;
top values for categorical/discrete columns.
```

### 16.3 Type inference

Initial inferred types:

```text
Text
Integer
Decimal
Boolean
Date
DateTime
Unknown
```

Initial methodological variable kinds:

```text
DiscreteCandidate
ContinuousCandidate
TextCandidate
TemporalCandidate
IdentifierCandidate
Unknown
```

### 16.4 Important UX rule

The UI must clearly communicate that inferred types are suggestions.

Use language such as:

```text
Inferred by Ariadne
Needs review
Reviewed
```

Never imply that an automated profile is final truth.

### 16.5 Acceptance criteria

- Profiling works after CSV import.
- Numeric stats are correct on known test datasets.
- Missing values are counted consistently.
- IQR outlier detection is implemented only for numeric columns.
- Domain/application tests protect profile behavior.
- Large files fail gracefully if they exceed MVP limits.

### 16.6 Non-goals

Do not implement charts yet unless trivial.

---

## 17. M08 - Variable dictionary

### 17.1 Goal

Allow users to review, correct and document variables.

### 17.2 Scope

For each variable, user can edit:

```text
business name;
description;
methodological variable kind;
data role;
unit;
expected domain;
known limitations;
sensitivity level;
review status;
notes.
```

Data roles:

```text
FeatureCandidate
TargetCandidate
Identifier
Metadata
IgnoreCandidate
Unknown
```

### 17.3 Acceptance criteria

- A variable dictionary is created from the dataset profile.
- Users can correct inferred types.
- Reviewed variables are visually distinct from unreviewed variables.
- Changes persist locally.
- Report generation can later use the dictionary.

### 17.4 Non-goals

Do not force users to complete every field before proceeding.

Ariadne should expose gaps, not block learning unnecessarily.

---

## 18. M09 - Fundamental analysis

### 18.1 Goal

Make fundamental analysis a first-class project artifact.

### 18.2 Scope

Implement guided sections:

```text
What
Who
When
Where
How
Why
Representativeness
Known biases
Missing context
```

Each section should support:

```text
answer text;
confidence level;
source or reference text;
unknown / not available state;
last updated timestamp;
optional decision log link.
```

### 18.3 UX rule

Unknown answers are valid and should be reportable.

The UI should encourage honesty:

```text
It is better to document that this is unknown than to hide it.
```

### 18.4 Acceptance criteria

- Fundamental analysis can be edited and saved.
- Incomplete sections are visible in the workflow progress.
- Unknown answers appear as report gaps.
- The generated report includes fundamental analysis answers.

---

## 19. M10 - Decision log

### 19.1 Goal

Allow users to record decisions and rationale throughout the project.

### 19.2 Scope

Decision entry fields:

```text
title;
category;
summary;
rationale;
linked stage;
linked dataset version;
linked variable, optional;
impact;
risk;
created at;
updated at.
```

Categories:

```text
Project
Dataset
Variable
FundamentalAnalysis
TechnicalAnalysis
Hypothesis
Preprocessing
Modelling
Evaluation
Reporting
Other
```

### 19.3 Acceptance criteria

- User can create, edit and list decision entries.
- Decisions can be filtered by category and stage.
- Decisions are included in the report.
- Important workflow actions can suggest creating a decision entry.

### 19.4 Non-goals

Do not implement full audit logging yet.

The decision log is user-facing reasoning, not internal telemetry.

---

## 20. M11 - Methodology progress

### 20.1 Goal

Show the user where they are in the methodology workflow and what remains incomplete.

### 20.2 Scope

Implement workflow stages:

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

For MVP, only these stages should be active:

```text
Project
Dataset
Understand
Report
```

Other stages can appear as locked or planned.

### 20.3 Progress model

Each stage can be:

```text
NotStarted
InProgress
NeedsReview
Completed
Blocked
Deferred
```

### 20.4 Acceptance criteria

- Project overview shows methodology progress.
- Users can see why a stage is incomplete.
- The app does not pretend that locked future stages are implemented.
- Progress calculation is tested in application/domain logic.

---

## 21. M12 - Markdown report generation

### 21.1 Goal

Generate a first methodology report from local project data.

### 21.2 Scope

The MVP report should include:

```text
project summary;
dataset metadata;
dataset version;
profiling summary;
variable dictionary summary;
fundamental analysis;
known unknowns;
decision log;
methodology progress;
limitations;
next recommended steps.
```

### 21.3 Report philosophy

The report should be honest and cautious.

It should not say:

```text
The data is reliable.
```

unless the user has documented enough evidence.

It should prefer:

```text
The dataset has been profiled automatically. Several variables still need review.
The source and collection process are not fully documented.
These gaps limit the conclusions that can be drawn.
```

### 21.4 Acceptance criteria

- Report can be generated from the MAUI app.
- Report is saved in the project `reports/` folder.
- Report can be opened or copied by the user.
- Report includes missing context explicitly.
- Report generation is deterministic enough for snapshot-style tests.

### 21.5 Non-goals

Do not implement PDF export yet unless Markdown export is stable.

---

## 22. M13 - MVP hardening and release `v0.1`

### 22.1 Goal

Stabilize the first local methodology MVP.

### 22.2 MVP scope recap

`v0.1` includes:

```text
local MAUI Blazor app;
project creation and persistence;
CSV import;
dataset preview;
dataset profiling;
variable dictionary;
fundamental analysis;
decision log;
methodology progress;
Markdown report generation;
README and contributor guidance.
```

### 22.3 MVP non-scope recap

`v0.1` excludes:

```text
technical charts;
statistical tests;
preprocessing execution;
model training;
evaluation metrics;
confidence intervals;
LLM assistant;
cloud sync;
web hosting;
team collaboration;
commercial features.
```

### 22.4 Hardening tasks

Before tagging `v0.1`, perform:

```text
manual smoke test on a fresh machine/environment;
project creation/open/reopen test;
CSV import test with comma and semicolon delimiters;
invalid file test;
report generation test;
restart persistence test;
empty-state review;
error-message review;
README review;
license review;
architecture dependency review.
```

### 22.5 Acceptance criteria

- A new user can complete the MVP journey without developer assistance.
- Data stays local.
- The report is useful even without modelling.
- No unimplemented future stage appears as functional.
- The app communicates limitations clearly.

---

## 23. M20 - Technical analysis release `v0.2`

### 23.1 Goal

Add simple exploratory data analysis after the MVP is stable.

### 23.2 Scope

Univariate analysis:

```text
categorical/discrete counts;
numeric summary;
histogram-ready bins;
boxplot-ready summary;
top categories;
missing distribution summary.
```

Multivariate analysis:

```text
discrete-discrete contingency table;
discrete-continuous grouped summary;
continuous-continuous correlation candidate;
scatterplot-ready data sample;
correlation matrix for numeric variables.
```

### 23.3 UX rule

Charts must explain what they show and what they do not prove.

### 23.4 Acceptance criteria

- Analysis is performed on selected dataset version.
- Results can be included in the report.
- User can save notes about findings.
- Findings can be linked to decision log entries.

### 23.5 Non-goals

Do not add hypothesis test automation before M30.

---

## 24. M30 - Hypothesis tracking release `v0.3`

### 24.1 Goal

Allow users to formulate and track hypotheses before running tests.

### 24.2 Scope

Implement:

```text
Hypothesis
Null hypothesis H0
Alternative hypothesis H1
Related variables
Hypothesis origin
Observation dataset version
Confirmation dataset version, optional
Risk of confirmation bias note
Status
Conclusion
```

### 24.3 Bias prevention

The UI should distinguish:

```text
Hypothesis formulated before looking at data
Hypothesis formulated after exploratory observation
Hypothesis tested on same data
Hypothesis tested on confirmation data
```

### 24.4 Acceptance criteria

- User can create H0/H1 records.
- User can link hypotheses to variables and findings.
- App warns when a hypothesis is tested on the same data used to generate it.
- Hypotheses appear in the report.

---

## 25. M31 - Statistical test planning release `v0.3.x`

### 25.1 Goal

Help users choose appropriate statistical tests without overclaiming automation.

### 25.2 Scope

Initial test recommendations:

| Situation | Candidate test |
|---|---|
| One discrete variable vs theoretical proportion | Binomial test or chi-square goodness-of-fit. |
| One continuous variable vs theoretical mean | One-sample Student test. |
| Two discrete variables | Chi-square independence test. |
| Discrete variable with two groups + continuous variable | Two-sample Student or Welch test. |
| Discrete variable with more than two groups + continuous variable | ANOVA. |
| Two continuous variables | Pearson or Spearman correlation. |

### 25.3 Condition checks

The app should help document assumptions such as:

```text
independence;
sample size;
approximate normality;
equal variance;
number of groups;
variable types;
observation/confirmation split.
```

### 25.4 Acceptance criteria

- Ariadne can suggest candidate tests based on reviewed variable types.
- The user must confirm assumptions before recording a result.
- The app does not hide test limitations.
- Test decisions can be added to the decision log.

### 25.5 Non-goals

Automatic statistical test execution may be deferred.

Planning and documentation are more important than calculation at this stage.

---

## 26. M40 - Preprocessing planner release `v0.4`

### 26.1 Goal

Allow users to plan preprocessing decisions and document rationale.

### 26.2 Scope

Supported step types:

```text
MissingValueHandling
OutlierHandling
Encoding
Normalization
FeatureEngineering
FeatureSelection
ColumnDrop
RowFilter
CustomNote
```

### 26.3 Methodological sequence

Default recommended sequence:

```text
1. Missing values
2. Outliers
3. Encoding
4. Feature engineering
5. Feature selection
6. Final normalization
```

The app may allow custom order but should warn when the order is unusual.

### 26.4 Acceptance criteria

- User can create a preprocessing plan.
- Each step has a rationale.
- Steps can reference variables.
- Plan appears in the report.
- No destructive dataset transformation is required yet.

### 26.5 Non-goals

Do not execute transformations in the first preprocessing planner milestone.

A plan-first approach is intentional.

---

## 27. M41 - Preprocessing execution experiment `v0.4.x`

### 27.1 Goal

Evaluate whether Ariadne should execute simple preprocessing steps locally.

### 27.2 Possible scope

Potential first executable operations:

```text
column drop;
row drop for missing values;
simple missing value summary;
IQR outlier flagging;
ordinal mapping definition;
one-hot mapping preview;
min-max scaling preview;
standardization preview.
```

### 27.3 Key design constraint

Execution must be reversible or versioned.

Never overwrite the original imported dataset.

### 27.4 Acceptance criteria

- Original dataset remains unchanged.
- Transformed dataset is stored as a new version or derived artifact.
- Transformation rules are saved.
- Report distinguishes planned vs executed preprocessing.

---

## 28. M50 - Modelling assistant release `v0.5`

### 28.1 Goal

Add model planning and experiment documentation after preprocessing concepts are stable.

### 28.2 Scope

Initial model families:

```text
Regression
Classification
Clustering
AnomalyDetection
```

Initial simple model candidates:

```text
Linear regression
Logistic regression
Decision tree
K-nearest neighbors
```

### 28.3 Possible implementation paths

Ariadne may support modelling in stages:

1. model planning only;
2. manual experiment recording;
3. ML.NET runner;
4. optional Python/Scikit-Learn bridge;
5. advanced external integrations.

The first modelling milestone should not jump directly to full AutoML.

### 28.4 Acceptance criteria

- User can define a modelling objective.
- User can choose target and candidate features.
- User can record model family and rationale.
- User can record manual metrics from external tools.
- Report includes modelling plan and limitations.

---

## 29. M60 - Evaluation assistant release `v0.6`

### 29.1 Goal

Help users evaluate model performance responsibly.

### 29.2 Scope

Regression metrics:

```text
MAE
MSE
RMSE
R2
```

Classification metrics:

```text
confusion matrix
accuracy
precision
recall
F1
ROC/AUC notes
precision-recall notes
```

Evaluation context:

```text
train performance;
validation performance;
test performance;
confidence interval;
applicability domain;
error analysis notes;
known limitations.
```

### 29.3 Evaluation formula

The UI should reinforce:

```text
Evaluation = Metric + Confidence Interval + Applicability
```

### 29.4 Acceptance criteria

- User can record evaluation metrics.
- App explains metric meaning.
- App warns when accuracy may be misleading on imbalanced classification.
- App supports confidence interval documentation, even if not automated at first.
- Report includes evaluation and applicability.

---

## 30. M70 - Web reuse experiment

### 30.1 Goal

Validate that the architecture can support a Blazor Web host using the shared UI and core application logic.

### 30.2 Scope

Create optional project:

```text
src/Ariadne.Web/
```

The web host should reuse:

```text
Ariadne.Domain
Ariadne.Application
Ariadne.Analytics
Ariadne.SharedUi
```

It should not force the local-first MAUI product to become cloud-first.

### 30.3 Acceptance criteria

- Shared UI can render in web host.
- Core pages compile with minimal host-specific branching.
- Local storage abstractions can be swapped for web infrastructure later.
- No commercial features are required.

### 30.4 Non-goals

Do not implement production SaaS, billing, organizations or collaboration in this milestone.

---

## 31. M80 - Commercial readiness exploration

### 31.1 Goal

Explore whether the open-source local product can support a useful hosted/commercial offering.

### 31.2 Possible commercial capabilities

```text
team workspaces;
project sharing;
cloud backup;
hosted report publishing;
review workflows;
organization templates;
premium methodology templates;
connectors;
audit history;
role-based access;
commercial support.
```

### 31.3 Open-source boundary

The open-source local application should remain valuable on its own.

Commercial features should extend collaboration and hosting, not cripple the local product.

### 31.4 Acceptance criteria

- Clear open-core or dual-offering strategy exists before implementation.
- Licensing implications are reviewed.
- User data privacy model is documented.
- No commercial code is mixed into the local MVP by accident.

---

## 32. Suggested sprint sequence for `v0.1`

The following sprint sequence is recommended for Codex-driven implementation.

| Sprint | Main objective | Milestones touched | Expected output |
|---:|---|---|---|
| 0 | Finish documentation baseline | M00 | Docs, README draft, AGENTS draft. |
| 1 | Create solution skeleton | M01 | Buildable solution and app shell. |
| 2 | Add domain foundations | M02 | Domain entities, value objects, tests. |
| 3 | Add application use cases | M02 | Project use cases and fake test infrastructure. |
| 4 | Build shared UI shell | M03 | Layout, navigation, placeholders. |
| 5 | Add local storage foundation | M04 | Project persistence skeleton. |
| 6 | Implement project management | M05 | Create/list/open project. |
| 7 | Implement CSV import | M06 | Import file and create dataset version. |
| 8 | Implement dataset preview | M06 | Preview rows and import warnings. |
| 9 | Implement profiling engine | M07 | Column profiles and tests. |
| 10 | Implement profiling UI | M07 | Dataset profile screen. |
| 11 | Implement variable dictionary | M08 | Review and edit variables. |
| 12 | Implement fundamental analysis | M09 | Guided questionnaire. |
| 13 | Implement decision log | M10 | Decision capture and list. |
| 14 | Implement methodology progress | M11 | Workflow rail and status calculation. |
| 15 | Implement Markdown report | M12 | Generate and save report. |
| 16 | Harden MVP | M13 | Bug fixes, README, release notes. |

Each sprint should remain small enough for Codex to complete, build and test.

---

## 33. Recommended first issue backlog

### 33.1 Foundation issues

```text
ROAD-001 Create repository root files
ROAD-002 Create .NET solution and projects
ROAD-003 Configure project references
ROAD-004 Add AGENTS.md development rules
ROAD-005 Add initial README
ROAD-006 Add .editorconfig and build props
ROAD-007 Add test projects
```

### 33.2 Domain issues

```text
ROAD-020 Implement ProjectId and DatasetId value objects
ROAD-021 Implement AiProject aggregate
ROAD-022 Implement Dataset and DatasetVersion
ROAD-023 Implement DecisionLogEntry
ROAD-024 Implement MethodologyStage and status enums
ROAD-025 Add domain invariant tests
```

### 33.3 Application issues

```text
ROAD-040 Define repository interfaces
ROAD-041 Implement CreateProject use case
ROAD-042 Implement ListProjects use case
ROAD-043 Implement RenameProject use case
ROAD-044 Implement AddDecisionLogEntry use case
ROAD-045 Add application tests with fakes
```

### 33.4 UI issues

```text
ROAD-060 Build shared app layout
ROAD-061 Build home page
ROAD-062 Build project list page
ROAD-063 Build project overview page
ROAD-064 Build workflow rail component
ROAD-065 Build empty state and status badge components
```

### 33.5 Storage issues

```text
ROAD-080 Implement local path provider
ROAD-081 Implement project catalog store
ROAD-082 Implement project folder creation
ROAD-083 Implement project repository
ROAD-084 Add local storage integration tests
```

### 33.6 Dataset issues

```text
ROAD-100 Implement CSV file copy into project storage
ROAD-101 Implement CSV dialect detection baseline
ROAD-102 Implement dataset version creation
ROAD-103 Implement dataset preview reader
ROAD-104 Implement import warnings
ROAD-105 Add CSV import tests
```

### 33.7 Profiling issues

```text
ROAD-120 Implement column type inference
ROAD-121 Implement missing value counting
ROAD-122 Implement numeric statistics
ROAD-123 Implement categorical top values
ROAD-124 Implement IQR outlier flags
ROAD-125 Implement profile persistence
ROAD-126 Build profile UI
```

### 33.8 Methodology issues

```text
ROAD-140 Implement variable dictionary model
ROAD-141 Build variable dictionary UI
ROAD-142 Implement fundamental analysis model
ROAD-143 Build fundamental analysis UI
ROAD-144 Implement decision log UI
ROAD-145 Implement methodology progress calculation
ROAD-146 Build methodology progress UI
```

### 33.9 Reporting issues

```text
ROAD-160 Implement report snapshot model
ROAD-161 Implement Markdown report generator
ROAD-162 Save report into project folder
ROAD-163 Build report preview screen
ROAD-164 Add report snapshot tests
ROAD-165 Add report generation smoke test
```

---

## 34. Milestone dependency map

Recommended dependency order:

```text
M00 Documentation
  ↓
M01 Solution setup
  ↓
M02 Domain/Application foundations
  ↓
M03 Shared UI/MAUI shell
  ↓
M04 Local storage
  ↓
M05 Project management
  ↓
M06 Dataset import/preview
  ↓
M07 Dataset profiling
  ↓
M08 Variable dictionary
  ↓
M09 Fundamental analysis
  ↓
M10 Decision log
  ↓
M11 Methodology progress
  ↓
M12 Markdown report
  ↓
M13 MVP hardening
```

Some UI work can happen in parallel with domain/application work, but persistence and dataset import should not be implemented before project storage is clear.

---

## 35. Definition of Done

A task is done only when all relevant items are true.

### 35.1 General Definition of Done

```text
Code compiles.
Tests pass.
No architecture boundary is violated.
No unrequested dependency was added.
No future feature was silently implemented.
User-facing text is clear.
Errors are handled intentionally.
Documentation is updated when needed.
The implementation matches the roadmap scope.
```

### 35.2 Domain Definition of Done

```text
Invariants are enforced.
Value objects validate input.
Entities do not expose inconsistent state.
Domain has no dependency on infrastructure.
Tests cover valid and invalid cases.
```

### 35.3 Application Definition of Done

```text
Use case has clear input and output.
Use case does not depend on MAUI or Blazor.
Interfaces are minimal.
Errors are explicit.
Tests use fakes or in-memory implementations.
```

### 35.4 Infrastructure Definition of Done

```text
Implementation is behind an application interface.
File paths are handled safely.
Tests use temporary directories.
Data-loss cases are considered.
Platform-specific APIs are isolated.
```

### 35.5 UI Definition of Done

```text
Component belongs in SharedUi unless host-specific.
Component is accessible by default.
Empty/loading/error states exist.
Business rules are not implemented in Razor markup.
UI text follows the tone guidelines.
```

### 35.6 Reporting Definition of Done

```text
Generated report is readable.
Missing context is included honestly.
Report does not overclaim.
Output is deterministic enough to test.
Report file is saved in the correct project folder.
```

---

## 36. Quality gates

### 36.1 Build gate

Every pull request or Codex task should run:

```text
dotnet build
```

### 36.2 Test gate

Every pull request or Codex task should run:

```text
dotnet test
```

### 36.3 Architecture gate

Before merging architecture-sensitive changes, verify:

```text
Domain references no other Ariadne project.
Application references Domain only.
SharedUi does not reference Maui.
Infrastructure.Local does not leak into Domain.
Maui acts only as host/composition root.
```

### 36.4 Privacy gate

Verify:

```text
no dataset content is sent externally;
no telemetry is added without explicit approval;
no cloud dependency is introduced;
logs do not expose dataset rows by default.
```

### 36.5 UX gate

Verify:

```text
empty states are useful;
errors explain next steps;
inferred values are labelled as inferred;
unknown context can be documented;
future features are not shown as working.
```

### 36.6 Methodology gate

Verify:

```text
the app does not encourage modelling before understanding;
reports mention gaps and limitations;
decisions can be traced;
automated statistics are not presented as absolute truth.
```

---

## 37. Testing roadmap

### 37.1 `Ariadne.Domain.Tests`

Initial focus:

```text
value object validation;
project creation invariants;
dataset version invariants;
decision log invariants;
methodology progress rules.
```

### 37.2 `Ariadne.Application.Tests`

Initial focus:

```text
create project;
list projects;
rename project;
add decision entry;
create dataset version;
generate report snapshot;
calculate progress.
```

### 37.3 `Ariadne.Analytics.Tests`

Initial focus:

```text
CSV value parsing samples;
column type inference;
missing value counting;
mean/median/std calculations;
quartiles and IQR;
outlier candidate detection;
categorical top values.
```

### 37.4 `Ariadne.Infrastructure.Local.Tests`

Add when infrastructure stabilizes:

```text
project folder creation;
catalog persistence;
project repository persistence;
CSV import file copy;
report file write;
restart/reopen behavior;
temporary directory cleanup.
```

### 37.5 UI tests

UI tests may be introduced after the UI stabilizes.

Initial priority is not screenshot-perfect testing. Initial priority is:

```text
component renders without exceptions;
required states are visible;
important actions call expected use cases;
accessibility basics are preserved.
```

### 37.6 Manual smoke tests

Before each tagged release:

```text
start app;
create project;
close app;
reopen app;
open project;
import CSV;
preview dataset;
run profiling;
review variable;
fill fundamental analysis answer;
add decision;
generate report;
open generated Markdown file.
```

---

## 38. Documentation roadmap

Documentation should evolve with the product.

### 38.1 MVP documentation

Required before `v0.1`:

```text
README.md
AGENTS.md
LICENSE
CONTRIBUTING.md, optional but recommended
CODE_OF_CONDUCT.md, optional
CHANGELOG.md
```

### 38.2 User documentation

Add when the MVP is usable:

```text
docs/getting-started.md
docs/create-project.md
docs/import-csv.md
docs/dataset-profiling.md
docs/fundamental-analysis.md
docs/generate-report.md
```

### 38.3 Developer documentation

Add as the codebase grows:

```text
docs/development/setup.md
docs/development/architecture-rules.md
docs/development/testing.md
docs/development/local-storage.md
docs/development/release-process.md
```

### 38.4 Methodology documentation

Add later:

```text
docs/methodology/dataset-as-sample.md
docs/methodology/variable-types.md
docs/methodology/fundamental-analysis.md
docs/methodology/technical-analysis.md
docs/methodology/hypotheses.md
docs/methodology/preprocessing.md
docs/methodology/evaluation.md
```

---

## 39. Open-source readiness roadmap

### 39.1 Before public release

Prepare:

```text
clear README;
license decision;
basic contribution instructions;
issue templates;
security policy, even if minimal;
screenshots or GIFs, optional;
roadmap badge or project board link;
first good-first-issues.
```

### 39.2 Recommended issue labels

```text
type: feature
type: bug
type: docs
type: refactor
type: test
type: architecture
area: domain
area: application
area: analytics
area: infrastructure-local
area: shared-ui
area: maui
area: reporting
area: methodology
priority: high
priority: medium
priority: low
good first issue
help wanted
blocked
needs decision
```

### 39.3 Contribution boundaries

Contributors should be told clearly:

```text
Do not add cloud services without approval.
Do not add LLM providers without approval.
Do not add model training before the modelling milestone.
Do not put business logic in Razor components.
Do not put MAUI-specific code in SharedUi.
Do not treat inferred dataset metadata as final truth.
```

---

## 40. Codex operating model

Codex should be used for small, reviewable tasks.

### 40.1 Task size

A Codex task should usually fit one of these shapes:

```text
Create one domain concept and tests.
Create one application use case and tests.
Create one infrastructure adapter and tests.
Create one UI component and minimal integration.
Create one documentation update.
Refactor one bounded area.
Fix one bug with regression test.
```

Avoid tasks such as:

```text
Build the whole MVP.
Add all dataset features.
Implement modelling.
Create the entire UI.
Refactor the architecture.
```

### 40.2 Codex prompt expectations

Each prompt should include:

```text
Goal
Relevant documents
Files likely to edit
Scope
Non-goals
Architecture constraints
Acceptance criteria
Commands to run
Expected response format
```

### 40.3 Codex response format

Codex should respond with:

```text
Summary:
Tests:
Changed files:
Risks / notes:
```

### 40.4 Codex safety rules

Codex must not:

```text
invent a new architecture;
rename projects without approval;
add web host early;
add cloud dependencies;
add LLM dependencies;
add database providers not in scope;
move domain logic into UI;
skip tests for domain/application logic;
silently change product scope.
```

### 40.5 Codex review checklist

After a Codex change, review:

```text
Does it compile?
Do tests pass?
Did it stay in scope?
Did it respect dependencies?
Did it add unapproved packages?
Did it preserve local-first behavior?
Did it update docs if needed?
Did it introduce user-facing overclaims?
```

---

## 41. Architecture evolution rules

The architecture should evolve carefully.

### 41.1 When adding a new project

A new project may be added only if it has a clear responsibility that cannot fit existing layers.

Potential future projects:

```text
Ariadne.Web
Ariadne.Infrastructure.Web
Ariadne.ML.MLNet
Ariadne.ML.PythonBridge
Ariadne.Report.Pdf
```

Do not add these before the milestone requires them.

### 41.2 When adding a new package

Before adding a package, document:

```text
why it is needed;
which project owns it;
whether it works offline;
whether it works cross-platform;
whether it creates web reuse constraints;
whether it changes licensing concerns.
```

### 41.3 When adding persistence

Persistence must stay behind application interfaces.

Do not let EF Core entities become the domain model unless explicitly approved.

### 41.4 When adding analytics

Analytics must be deterministic and testable.

Avoid analytics logic that depends on UI state or file paths directly.

---

## 42. Risk register

| Risk | Probability | Impact | Mitigation |
|---|---:|---:|---|
| Scope creep into AutoML. | High | High | Roadmap explicitly defers modelling. |
| MAUI complexity slows progress. | Medium | Medium | Keep MAUI as host only; start with simple screens. |
| Shared UI becomes host-specific. | Medium | High | Enforce `SharedUi` independence. |
| Dataset import becomes too broad. | High | Medium | Start with CSV only. |
| Profiling is wrong or misleading. | Medium | High | Label as inferred; allow user review; test calculations. |
| Local storage causes data loss. | Medium | High | Version files; use project folders; test restart flows. |
| Reports overclaim. | Medium | High | Include gaps, limitations and unknowns. |
| Dependencies become heavy. | Medium | Medium | Conservative dependency rule. |
| Contributors add cloud services early. | Medium | Medium | AGENTS and roadmap prohibit it. |
| UI becomes too dense. | Medium | Medium | Follow `07-ui-ux-guidelines.md`. |
| Web reuse is harder than expected. | Medium | Medium | Keep core and SharedUi clean from start. |

---

## 43. Priority rules

When deciding between features, use this order:

1. Protect local user data.
2. Preserve architecture boundaries.
3. Improve the methodology workflow.
4. Make unknowns and limitations visible.
5. Improve report quality.
6. Improve dataset understanding.
7. Add technical analysis.
8. Add hypothesis support.
9. Add preprocessing support.
10. Add modelling support.
11. Add evaluation support.
12. Add web and commercial features.

When in doubt, choose the feature that makes the project more explainable, not the feature that makes it look more impressive.

---

## 44. Backlog ordering rules

A backlog item should be prioritized only if it satisfies at least one condition:

```text
It unblocks the MVP workflow.
It reduces architectural risk.
It reduces data-loss risk.
It improves user understanding of the dataset.
It improves report quality.
It improves test coverage for core logic.
It clarifies open-source contribution.
```

Backlog items should be delayed if they primarily:

```text
make the app look more advanced without improving methodology;
introduce external services;
require account management;
require cloud infrastructure;
require model training;
require unsupported file formats;
increase UI complexity without clear user value.
```

---

## 45. MVP success criteria

The MVP is successful when a user can say:

```text
I can create a local AI/ML project.
I can import my dataset.
I can see what variables exist.
I can see what Ariadne inferred and what still needs review.
I can document what the data means.
I can record important decisions.
I can generate a report that explains the project state and its limitations.
My data stayed local.
```

The MVP is not judged by:

```text
number of models trained;
number of charts;
number of algorithms;
cloud features;
AI assistant behavior;
commercial readiness.
```

---

## 46. Metrics for project progress

Use engineering and product metrics carefully.

### 46.1 Engineering metrics

```text
build success;
test pass rate;
number of architecture violations;
number of untested domain/application changes;
number of critical bugs;
time to complete smoke test.
```

### 46.2 Product metrics for MVP dogfooding

```text
Can a user complete the MVP flow?
How many fields remain unclear?
Does the report read like a methodology artifact?
Do users understand inferred vs reviewed data?
Do users trust that data stays local?
Where do users get stuck?
```

### 46.3 Open-source metrics after public release

```text
stars and forks;
issues opened with real use cases;
contributor interest;
downloads or release installs;
documentation feedback;
requests for web/collaboration features.
```

These metrics should inform the roadmap, not dictate it blindly.

---

## 47. Open decisions

The following decisions remain open and should not block the initial MVP unless they become necessary.

| Decision | Current recommendation |
|---|---|
| License | Choose before public release. MIT or Apache-2.0 are likely candidates. |
| UI component library | Start with custom/lightweight components unless a library becomes clearly useful. |
| SQLite ORM | EF Core SQLite is likely, but local storage can begin behind interfaces. |
| CSV parser library | Start simple or use a mature library if needed; keep behind `IDatasetImporter`. |
| Charting library | Defer until technical analysis milestone. |
| PDF export | Defer until Markdown report is stable. |
| ML.NET | Defer until modelling milestone. |
| Python bridge | Defer until ML.NET limitations are understood. |
| Web host | Defer until local MVP is stable. |
| Telemetry | Disabled by default; discuss explicitly later. |
| Encryption | Defer; document sensitive data guidance first. |
| Plugin architecture | Defer until real extension needs appear. |

---

## 48. Roadmap change process

The roadmap may change, but changes should be explicit.

A roadmap change should include:

```text
What changes?
Why is it needed?
Which milestone is affected?
Which documents need updates?
Which risks are introduced?
Which features move later?
```

Do not silently expand the MVP.

If a new feature is added to the MVP, another feature should usually be removed or delayed.

---

## 49. Summary

Ariadne AI Workbench should be built in a disciplined sequence.

The first goal is not to train models. The first goal is to create a local-first methodology workbench that helps users understand and document their data.

The roadmap starts with documentation and architecture, then builds a working MAUI Blazor Hybrid app with reusable UI, local storage, project management, CSV import, profiling, variable review, fundamental analysis, decision logging and Markdown reporting.

Only after `v0.1` should Ariadne move toward technical analysis, hypotheses, preprocessing, modelling and evaluation.

The development strategy is:

```text
Build the thread before building the labyrinth.
```

Ariadne should guide users through AI methodology step by step, while giving contributors and Codex a roadmap that is small enough to execute and strict enough to protect the product vision.
