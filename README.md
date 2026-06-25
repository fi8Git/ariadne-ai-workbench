# Ariadne AI Workbench

Your thread through AI methodology.

Ariadne AI Workbench is a local-first, open-source, methodology-first workbench for AI and machine-learning projects.

The first goal is not AutoML, model training, cloud collaboration or LLM automation. The first goal is to help a user create a local project, import a dataset, understand variables, document context, record decisions and generate a defensible Markdown report.

## Status

Current status: solution skeleton / pre-MVP foundation.

The initial .NET 10 solution and project structure exist. The next milestones add domain foundations, application use cases, reusable UI and local storage.

## Why Ariadne Exists

Many AI and machine-learning projects move too quickly from "I have a dataset" to "I trained a model." Ariadne is designed to slow down the parts that matter:

- understand what the dataset represents;
- distinguish inferred metadata from user-reviewed meaning;
- document what, who, when, where, how and why;
- record decisions about variables, missing values, outliers and assumptions;
- make limitations and unknowns visible;
- generate a report that explains the project state without overstating conclusions.

## MVP Scope

MVP v0.1 is planned as a local-first workbench that supports:

- local project creation;
- CSV dataset import;
- dataset preview;
- basic column profiling;
- inferred variable types;
- manual variable review;
- fundamental-analysis questions;
- decision log;
- methodology progress;
- Markdown report generation;
- local persistence.

MVP v0.1 does not include:

- AutoML;
- LLM agents or RAG;
- cloud synchronization;
- user accounts or authentication;
- ML.NET model training;
- Python or Scikit-Learn integration;
- collaboration or paid/commercial features.

## Local-First Positioning

Ariadne is intended to work without an account, server, cloud workspace, telemetry pipeline or remote AI service.

User datasets should remain on the user's machine by default. Any future networked feature must be explicit, documented and opt-in.

## Target Technology Direction

The planned stack is:

- .NET 10;
- .NET MAUI Blazor Hybrid;
- reusable Blazor UI in a Razor Class Library;
- Clean Architecture;
- local persistence;
- xUnit tests;
- Markdown report generation.

Planned solution structure:

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
  Ariadne.Infrastructure.Local.Tests/
```

Future web or commercial projects should not be created until the local-first MVP is stable and a task explicitly requests them.

## Documentation Index

Read only the documents relevant to the task at hand.

| File | Purpose |
|---|---|
| [`docs/00-project-brief.md`](docs/00-project-brief.md) | Product brief, scope, principles and first delivery boundaries. |
| [`docs/01-product-vision.md`](docs/01-product-vision.md) | Product positioning, users, promise and long-term direction. |
| [`docs/02-functional-specification.md`](docs/02-functional-specification.md) | MVP requirements, workflows and acceptance criteria. |
| [`docs/03-technical-architecture.md`](docs/03-technical-architecture.md) | Solution structure, dependency rules and technical boundaries. |
| [`docs/04-domain-model.md`](docs/04-domain-model.md) | Domain entities, value objects, invariants and future extensions. |
| [`docs/05-methodology-workflow.md`](docs/05-methodology-workflow.md) | Methodology stages, gates and workflow states. |
| [`docs/06-local-first-storage.md`](docs/06-local-first-storage.md) | Local storage strategy, file layout, persistence and privacy rules. |
| [`docs/07-ui-ux-guidelines.md`](docs/07-ui-ux-guidelines.md) | UI/UX direction, navigation, components and accessibility guidance. |
| [`docs/08-development-roadmap.md`](docs/08-development-roadmap.md) | Milestones, sequencing, quality gates and release boundaries. |
| [`docs/09-codex-task-breakdown.md`](docs/09-codex-task-breakdown.md) | Codex-ready task breakdown and execution prompts. |
| [`AGENTS.md`](AGENTS.md) | Always-on guidance for Codex and contributors. |

## Development

Local development uses the solution at `Ariadne.sln`:

```bash
dotnet restore Ariadne.sln
dotnet build Ariadne.sln
dotnet test Ariadne.sln
```

If MAUI workloads are not installed, validate the non-MAUI projects that exist and report the limitation clearly.

## Continuous Integration

The initial GitHub Actions workflow validates a non-MAUI solution filter on Ubuntu:

- restore/build for Domain, Application, Analytics, Infrastructure.Local and SharedUi through `Ariadne.NonMaui.slnf`;
- tests for the four xUnit test projects.

The MAUI host is not built in CI yet because MAUI workloads require additional platform setup. Full MAUI validation remains a local or future CI enhancement.

## Security

Please report vulnerabilities privately. Do not open public issues with secrets, tokens, sensitive datasets, generated reports containing private data or exploitable vulnerability details.

See [`SECURITY.md`](SECURITY.md) for reporting guidance and local-first data handling expectations.

## Contributor Guidance

Before making changes:

1. Read [`AGENTS.md`](AGENTS.md).
2. Read only the project documents relevant to the task.
3. Keep the change small and reviewable.
4. Preserve Clean Architecture boundaries.
5. Do not add web, cloud, ML, LLM, authentication or telemetry features during MVP tasks unless explicitly requested.
6. Run the requested validation commands and report results honestly.

## License

No `LICENSE` file exists yet. The license decision is open and must be made before public distribution or external contribution.

Candidate licenses mentioned in the planning docs are:

- MIT;
- Apache-2.0.

Until a license is selected, do not assume redistribution rights beyond private project planning.
