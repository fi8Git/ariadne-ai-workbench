# AGENTS.md

# Ariadne AI Workbench

Repository guidance for Codex and human contributors. Keep this file practical and stable; detailed product, architecture, roadmap and task decisions live in `docs/`.

---

## Project Identity

- Product: Ariadne AI Workbench.
- Short name: Ariadne.
- Solution: `Ariadne.sln`.
- Root namespace: `Ariadne`.
- Primary platform: .NET 10 MAUI Blazor Hybrid.
- UI model: reusable Blazor components in `Ariadne.SharedUi`, hosted by `Ariadne.Maui`.
- Storage model: local-first.

Ariadne is a methodology-first workbench for AI and machine-learning projects. The MVP helps users create a local project, import a CSV dataset, understand variables, document context, record decisions and generate a defensible Markdown report.

---

## Read Before Work

Always read the current task first, then read only the docs needed for that task.

- Product/context: `docs/00-project-brief.md`, `docs/01-product-vision.md`, `docs/02-functional-specification.md`.
- Architecture/project structure: `docs/03-technical-architecture.md`, `docs/04-domain-model.md`, `docs/06-local-first-storage.md`.
- Methodology/workflow: `docs/05-methodology-workflow.md`.
- UI: `docs/07-ui-ux-guidelines.md` plus the functional spec for the target screen.
- Planning/Codex execution: `docs/08-development-roadmap.md`, `docs/09-codex-task-breakdown.md`, `AGENTS.md`.

If documents disagree, use this priority order:

1. Explicit user request in the current task.
2. `AGENTS.md`.
3. `docs/09-codex-task-breakdown.md`.
4. `docs/03-technical-architecture.md`.
5. `docs/04-domain-model.md`.
6. `docs/02-functional-specification.md`.
7. `docs/05-methodology-workflow.md`.
8. `docs/07-ui-ux-guidelines.md`.
9. `docs/08-development-roadmap.md`.
10. `docs/01-product-vision.md` and `docs/00-project-brief.md`.

---

## MVP Scope

The MVP is a local-first workbench with:

- local project creation and persistence;
- CSV import and dataset preview;
- column profiling and type inference;
- reviewed variable definitions;
- fundamental-analysis questions;
- decision log;
- methodology progress;
- Markdown report generation.

Build incrementally. Prefer small vertical slices that compile and test.

---

## Non-Goals Unless Explicitly Requested

Do not add these during MVP tasks unless the prompt explicitly asks for them:

- AutoML or model training.
- ML.NET, Python, Scikit-Learn or hyperparameter search.
- LLM agents, RAG or OpenAI/API-provider calls.
- Web host, cloud sync, remote dataset upload or background cloud services.
- User accounts, ASP.NET Identity or authentication.
- Telemetry, analytics collection or remote logging.
- Paid/commercial/collaboration features.

Data must stay local by default. Do not send dataset content to external services.

---

## Solution Structure

Target structure:

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

Future projects such as `Ariadne.Web` or `Ariadne.Infrastructure.Web` must not be created until explicitly requested.

---

## Dependency Rules

Preserve Clean Architecture boundaries.

Allowed dependencies:

```text
Ariadne.Domain
  -> no project dependency

Ariadne.Application
  -> Ariadne.Domain

Ariadne.Analytics
  -> Ariadne.Domain
  -> Ariadne.Application only when application contracts are required

Ariadne.Infrastructure.Local
  -> Ariadne.Domain
  -> Ariadne.Application
  -> Ariadne.Analytics only when implementing local analytics-backed services

Ariadne.SharedUi
  -> Ariadne.Application
  -> Ariadne.Domain only for simple read models/enums when unavoidable

Ariadne.Maui
  -> Ariadne.SharedUi
  -> Ariadne.Application
  -> Ariadne.Infrastructure.Local
  -> Ariadne.Analytics only for registration/composition
```

Forbidden dependencies:

```text
Ariadne.Domain -> anything
Ariadne.Application -> Infrastructure, SharedUi, Maui, Web
Ariadne.Analytics -> Infrastructure, SharedUi, Maui, Web
Ariadne.Infrastructure.Local -> SharedUi, Maui, Web
Ariadne.SharedUi -> Maui, Infrastructure.Local, Infrastructure.Web
```

`Ariadne.Maui` is only the host and composition root. `Ariadne.SharedUi` must stay MAUI-independent.

---

## Layer Rules

- Domain: pure C#, no filesystem, database, Blazor, MAUI, logging framework or service locator.
- Application: explicit use cases and interfaces for infrastructure concerns; return structured results.
- Analytics: deterministic C# analytics for MVP; label inferred information honestly.
- Infrastructure.Local: local storage, files, SQLite when introduced by the relevant milestone; no cloud.
- SharedUi: thin reusable Blazor UI that calls application services through dependency injection.
- Maui: host setup, platform wiring and `BlazorWebView`; no business logic.

---

## Methodology Principles

Preserve these product principles:

1. Treat a dataset as a sample, not the full truth.
2. Distinguish inferred information from user-reviewed information.
3. Understand variables before modelling.
4. Combine technical analysis with fundamental analysis.
5. Document decisions, assumptions, missing context and limitations.
6. Do not remove outliers or missing values without a recorded rationale.
7. Do not turn observations into conclusions without caveats.

---

## Testing And Validation

Every meaningful change should include or update tests where practical.

Preferred validation when the solution exists:

```bash
dotnet restore Ariadne.sln
dotnet build Ariadne.sln
dotnet test Ariadne.sln
```

For documentation-only tasks:

```bash
git diff --check
```

If MAUI workloads are missing, say so clearly and still run the non-MAUI builds/tests that apply.

Do not claim tests passed unless they were actually run.

---

## Change Discipline

- Keep changes small and focused.
- Inspect existing files before editing.
- Do not refactor unrelated code.
- Do not rename projects, folders or namespaces unless requested.
- Do not add dependencies casually; justify any new package.
- Do not commit imported datasets, generated local project files, secrets or personal data.
- Update documentation only when behavior, architecture or scope changes.
- Work with existing user changes; never revert unrelated changes without explicit permission.

---

## Required Response Format

For Codex tasks, finish with:

```text
Summary:
- ...

Tests:
- ...

Changed files:
- ...

Risks / notes:
- ...
```

If tests were not run:

```text
Tests:
- Not run: <reason>
```

If a command failed, include the command and the relevant failure summary.

---

## If Blocked

State:

- what is blocking the task;
- what was already checked;
- which files or commands were involved;
- one or two reasonable next options.

Do not invent facts or silently work around requirements.
