# Ariadne AI Workbench

**Le fil conducteur de vos projets IA.**
**Your thread through AI methodology.**

Ariadne AI Workbench is a **local-first, open-source, methodology-first** workbench for AI and machine learning projects.

It is designed to help developers, learners, data analysts and consultants follow a rigorous workflow before jumping into modelling:

```text
Understand the data.
Document the context.
Formulate hypotheses carefully.
Prepare the dataset intentionally.
Evaluate generalization honestly.
Report assumptions, decisions, limits and uncertainty.
```

Ariadne is not intended to make AI look magical. It is intended to make good AI and ML methodology easier to follow, explain, reproduce and teach.

---

## Project status

**Current status:** documentation-first / pre-MVP planning phase.
**Target first application:** .NET 10 MAUI Blazor Hybrid local desktop/mobile application.
**Future option:** reuse the core and shared UI for a Blazor Web version if useful and/or commercializable.

The repository planning set is expected to contain:

```text
docs/00-project-brief.md
docs/01-product-vision.md
docs/02-functional-specification.md
docs/03-technical-architecture.md
docs/04-domain-model.md
docs/05-methodology-workflow.md
docs/06-local-first-storage.md
docs/07-ui-ux-guidelines.md
docs/08-development-roadmap.md
docs/09-codex-task-breakdown.md
AGENTS.md
README.md
```

The next implementation milestone is to create the initial .NET solution and MAUI Blazor Hybrid shell.

---

## Why Ariadne exists

Many AI and machine learning projects fail because teams move too quickly from a dataset to a model.

Typical problems include:

- variables are used before their meaning is understood;
- data origin, collection period and reliability are not documented;
- missing values and outliers are removed without rationale;
- exploratory observations are treated as conclusions;
- hypotheses are tested on the same data used to discover them;
- preprocessing decisions are not traceable;
- train/test separation is poorly applied or poorly explained;
- evaluation focuses on one metric without uncertainty or applicability boundaries.

Ariadne turns the methodology into a guided workflow and a living project record.

---

## Product thesis

Ariadne should make this workflow natural:

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

The MVP focuses on the beginning of this path:

```text
Project
→ Dataset
→ Understand
→ Report
```

The first useful version does not need AutoML or advanced modelling. It needs to help users create a project, import a dataset, inspect columns, document context, track decisions and generate a clear report.

---

## MVP scope

MVP v0.1 should include:

- local project creation;
- CSV import;
- dataset preview;
- basic column profiling;
- inferred variable types;
- manual variable review;
- fundamental-analysis questionnaire;
- decision log;
- methodology progress view;
- Markdown report generation;
- local persistence;
- unit tests for domain and analytics logic;
- MAUI Blazor Hybrid host;
- reusable Razor Class Library for future Web reuse.

MVP v0.1 should **not** include:

- AutoML;
- LLM agents;
- RAG;
- cloud sync;
- user accounts;
- PostgreSQL;
- collaboration;
- ML.NET model training;
- Python or Scikit-Learn integration;
- hyperparameter search;
- commercial features.

---

## Core methodology principles

Ariadne is built around a few non-negotiable principles.

### 1. A dataset is only a sample

The available data is not the full truth. It is an observed sample of a broader phenomenon. Ariadne should help the user document what can reasonably be inferred and what remains uncertain.

### 2. Variables must be understood before modelling

A column is not just a type. It has a meaning, a unit, an origin, a measurement process, a possible bias and a relevance to the project goal.

### 3. Technical analysis is not enough

Descriptive statistics, charts and tests are useful, but they must be combined with fundamental analysis:

```text
What does the data represent?
Who collected it?
When was it collected?
Where does it come from?
How was it measured?
Why was it collected?
```

### 4. Decisions must be traceable

Every meaningful decision should have a reason:

```text
Why is this column ignored?
Why is this variable considered continuous?
Why is this outlier kept or removed?
Why is this missing-value strategy acceptable?
Why is this metric relevant?
```

### 5. Modelling comes after understanding

The application should not encourage users to train models before the dataset, variables, context and assumptions have been reviewed.

### 6. Evaluation must include applicability

A model is not just a score. Ariadne should eventually help users document:

```text
metric;
confidence or uncertainty;
train/test behavior;
underfitting or overfitting signs;
known limitations;
scope of applicability.
```

---

## Target users

Ariadne is intended for:

- developers learning AI or machine learning;
- data analysts who need a structured workflow;
- students who want to apply a rigorous project method;
- educators building guided exercises;
- consultants documenting data analysis decisions;
- small teams working with sensitive datasets locally;
- open-source contributors interested in methodology tooling.

---

## Technology direction

Target stack:

```text
.NET 10
.NET MAUI Blazor Hybrid
Blazor components
Razor Class Library
Clean Architecture
SQLite local storage
xUnit
Markdown report generation
```

The first implementation should keep platform-specific code thin. The MAUI project is only a host. Reusable product logic belongs in domain, application, analytics, local infrastructure and shared UI projects.

---

## Planned solution structure

```text
src/
  Ariadne.Domain/
    Core domain entities, value objects, enums and invariants.

  Ariadne.Application/
    Use cases, orchestration, service contracts and application models.

  Ariadne.Analytics/
    Dataset profiling, descriptive statistics and future analysis helpers.

  Ariadne.Infrastructure.Local/
    Local storage, file access, SQLite, CSV import and report export.

  Ariadne.SharedUi/
    Reusable Blazor components and pages, independent from MAUI.

  Ariadne.Maui/
    .NET MAUI Blazor Hybrid host and platform-specific bootstrapping.

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
```

These should only be added when the local-first MVP is stable.

---

## Architecture rules

The solution should follow these dependency rules:

```text
Ariadne.Domain
  depends on nothing internal.

Ariadne.Application
  depends on Ariadne.Domain.

Ariadne.Analytics
  depends on Ariadne.Domain and, only if needed, Ariadne.Application abstractions.

Ariadne.Infrastructure.Local
  depends on Ariadne.Domain, Ariadne.Application and Ariadne.Analytics.

Ariadne.SharedUi
  depends on Ariadne.Application contracts and view models when needed.
  It must not depend on Ariadne.Maui.

Ariadne.Maui
  depends on SharedUi and infrastructure implementations.
  It must not contain business logic.
```

Business logic should not be placed in Razor components or MAUI pages.

---

## Local-first philosophy

Ariadne should work without:

```text
internet connection;
cloud account;
remote API;
telemetry;
subscription;
external AI service;
team workspace.
```

User datasets should remain local by default.

The planned local storage model is:

```text
A small local application catalog
+ one project folder per Ariadne project
+ one SQLite database per project
+ copied/imported dataset files
+ generated Markdown reports
+ optional exports/backups
```

Do not commit private datasets, local SQLite databases, generated reports containing sensitive information or user-specific project workspaces.

---

## Getting started

The codebase is expected to be generated during the first implementation milestone. Until the solution is created, the commands below describe the intended development workflow rather than the current repository state.

### Prerequisites

Install:

- .NET 10 SDK;
- .NET MAUI workload;
- Git;
- an IDE/editor that supports .NET MAUI and Blazor development.

Typical MAUI workload installation:

```bash
dotnet workload install maui
```

### Restore, build and test

Once the solution exists:

```bash
dotnet restore
dotnet build
dotnet test
```

### Run the MAUI app

The exact command depends on the host platform and target framework.

Example Windows target once `Ariadne.Maui` exists:

```bash
dotnet build src/Ariadne.Maui/Ariadne.Maui.csproj -t:Run -f net10.0-windows10.0.19041.0
```

For day-to-day MAUI development, running from Visual Studio or another MAUI-aware IDE may be simpler.

---

## Documentation map

Read these files according to the task.

| File | Purpose |
|---|---|
| [`00-project-brief.md`](docs/00-project-brief.md) | High-level product brief, scope and principles. |
| [`01-product-vision.md`](docs/01-product-vision.md) | Product vision, users, positioning and long-term ambition. |
| [`02-functional-specification.md`](docs/02-functional-specification.md) | MVP functional requirements and acceptance criteria. |
| [`03-technical-architecture.md`](docs/03-technical-architecture.md) | Technical architecture, project structure and boundaries. |
| [`04-domain-model.md`](docs/04-domain-model.md) | Domain entities, value objects, invariants and future extensions. |
| [`05-methodology-workflow.md`](docs/05-methodology-workflow.md) | Ariadne methodology stages, gates and workflow states. |
| [`06-local-first-storage.md`](docs/06-local-first-storage.md) | Local storage strategy, files, SQLite and privacy rules. |
| [`07-ui-ux-guidelines.md`](docs/07-ui-ux-guidelines.md) | UI/UX direction, components, navigation and accessibility rules. |
| [`08-development-roadmap.md`](docs/08-development-roadmap.md) | Roadmap, milestones, quality gates and sequencing. |
| [`09-codex-task-breakdown.md`](docs/09-codex-task-breakdown.md) | Small Codex-ready implementation tasks and prompts. |
| [`AGENTS.md`](AGENTS.md) | Persistent rules for Codex and contributors. |

For Codex-driven work, start with [`AGENTS.md`](AGENTS.md).

---

## Codex workflow

Codex should be guided by small, testable tasks.

A typical task should include:

```text
Goal:
Context:
Files to read:
Constraints:
Expected changes:
Acceptance criteria:
Validation commands:
```

Expected response format from Codex:

```text
Summary:
Tests:
Changed files:
Risks / notes:
```

Codex should always preserve the architecture boundaries and should not introduce non-MVP features unless the task explicitly requests them.

---

## Initial Codex milestone

The first implementation task should create the solution shell.

Expected result:

```text
Ariadne.sln
src/Ariadne.Domain
src/Ariadne.Application
src/Ariadne.Analytics
src/Ariadne.Infrastructure.Local
src/Ariadne.SharedUi
src/Ariadne.Maui
tests/Ariadne.Domain.Tests
tests/Ariadne.Application.Tests
tests/Ariadne.Analytics.Tests
```

The first code milestone should pass:

```bash
dotnet build
dotnet test
```

No database, ML model, cloud feature or web host should be added in the first milestone.

---

## Development principles

Prefer:

- small vertical slices;
- explicit domain concepts;
- deterministic analytics code;
- clear tests;
- simple UI components;
- local-first behavior;
- honest uncertainty;
- documentation updates when behavior changes.

Avoid:

- hidden external services;
- premature AutoML;
- business logic in UI components;
- platform-specific logic in shared UI;
- large untested refactors;
- adding dependencies without justification;
- treating inferred data as user-reviewed data.

---

## Testing strategy

The project should include tests for:

- domain invariants;
- value objects;
- project and dataset lifecycle rules;
- CSV import behavior;
- column profiling;
- variable type inference;
- missing value counting;
- report generation;
- application use cases;
- local persistence once implemented.

All significant domain and analytics behavior should be testable without running the MAUI app.

---

## Roadmap summary

Recommended delivery sequence:

| Phase | Focus |
|---|---|
| v0.0 | Documentation and project planning. |
| v0.1 | Local project, CSV import, profiling, fundamental analysis, decision log, Markdown report. |
| v0.2 | Technical analysis: univariate and simple multivariate summaries. |
| v0.3 | Hypothesis tracking and statistical-test guidance. |
| v0.4 | Preprocessing planning and decision traceability. |
| v0.5 | First modelling experiments, likely ML.NET or an abstract model runner. |
| v0.6 | Evaluation, train/test diagnostics, uncertainty and applicability reporting. |
| v1.0 | Stable local-first methodology workbench. |
| Future | Optional Blazor Web/commercial collaboration version. |

---

## Security and privacy

Ariadne is intended to handle user datasets locally. Contributors should treat data privacy as a core product constraint.

Do not add:

```text
telemetry;
remote logging;
automatic cloud upload;
remote dataset processing;
external AI calls;
analytics collection;
crash uploads containing user data.
```

Any future networked feature must be explicit, documented and opt-in.

---

## Third-party material notice

The project may be inspired by external learning material or methodology notes during planning. Do not commit third-party PDFs, course material or copyrighted datasets to a public repository unless the license explicitly allows redistribution.

When methodology concepts are encoded into the product, write original documentation and cite external sources where appropriate.

---

## Contributing

The project is intended to be open source, but the contribution process will be formalized after the initial solution exists.

Recommended contribution flow:

1. Read [`AGENTS.md`](AGENTS.md).
2. Read only the project documents relevant to the task.
3. Open or select a small issue.
4. Keep the change focused.
5. Add or update tests.
6. Run `dotnet build` and `dotnet test`.
7. Update documentation when behavior changes.
8. Submit a pull request with a clear summary and risk notes.

---

## License

License is **TBD**.

The project is intended to be open source. A license file must be added before public distribution or external contribution. Recommended candidates to evaluate before the first public release:

```text
MIT
Apache-2.0
```

Until a license is selected, do not assume redistribution rights beyond private project planning.

---

## Name

**Ariadne** refers to Ariadne's thread: a guide through a maze.

For this project, the maze is the path through data understanding, assumptions, hypotheses, preprocessing, modelling choices, evaluation metrics, uncertainty and reporting.

Ariadne AI Workbench is the thread that keeps the project understandable.
