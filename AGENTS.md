# AGENTS.md

# Ariadne AI Workbench

Project guidance for Codex and human contributors.

This file is intentionally practical. It should be read before making changes in this repository. Keep it focused on rules that must be applied repeatedly. Detailed product, architecture and roadmap decisions live in the project documentation files listed below.

---

## 1. Project identity

**Product name:** Ariadne AI Workbench  
**Short name:** Ariadne  
**Repository:** `ariadne-ai-workbench`  
**Solution:** `Ariadne.sln`  
**Root namespace:** `Ariadne`  
**Primary platform:** .NET 10 MAUI Blazor Hybrid  
**UI model:** Blazor components hosted in MAUI through a reusable Razor Class Library  
**Storage model:** local-first  
**Initial distribution:** open source  
**Future option:** reuse the core and shared UI for a Blazor Web/commercial version

Ariadne is a methodology-first workbench for AI and machine learning projects. Its first goal is not AutoML, model training, cloud collaboration or LLM automation. Its first goal is to help a user create a local project, import a dataset, understand variables, document context, record decisions and generate a defensible report.

---

## 2. Required reading order

Before a task, read only the documents needed for that task. Do not waste context reading everything when the task is small.

For product/context tasks, read:

```text
00-project-brief.md
01-product-vision.md
02-functional-specification.md
```

For architecture or project-structure tasks, read:

```text
03-technical-architecture.md
04-domain-model.md
06-local-first-storage.md
```

For workflow or data-science methodology tasks, read:

```text
05-methodology-workflow.md
methodologie_IA.pdf when available
```

For UI tasks, read:

```text
07-ui-ux-guidelines.md
02-functional-specification.md for the target screen behavior
```

For planning or Codex execution tasks, read:

```text
08-development-roadmap.md
09-codex-task-breakdown.md
AGENTS.md
```

When documents disagree, use this priority order:

```text
1. Explicit user request in the current task
2. AGENTS.md
3. 09-codex-task-breakdown.md for implementation sequencing
4. 03-technical-architecture.md for technical boundaries
5. 04-domain-model.md for domain rules
6. 02-functional-specification.md for feature behavior
7. 05-methodology-workflow.md for methodology behavior
8. 07-ui-ux-guidelines.md for UI behavior
9. 08-development-roadmap.md for milestone ordering
10. 01-product-vision.md and 00-project-brief.md for broader product intent
```

---

## 3. Core methodology principles

Ariadne must encode a rigorous data-science workflow.

Always preserve these principles:

1. Treat a dataset as a sample of a broader phenomenon, not as the full truth.
2. Distinguish inferred information from user-reviewed information.
3. Distinguish discrete, continuous, text, date/time, boolean and unknown variables.
4. Combine technical analysis with fundamental analysis.
5. Fundamental analysis must capture context: What, Who, When, Where, How and Why.
6. Do not encourage modelling before the dataset and variables are understood.
7. Do not remove outliers or missing values without documenting the decision.
8. Do not turn observations into conclusions without caveats.
9. Future hypothesis testing must avoid confirmation bias.
10. Future model evaluation must include metric, confidence/uncertainty and applicability.

Ariadne should guide users toward careful reasoning, not hide uncertainty behind automation.

---

## 4. Current MVP scope

The MVP is a local-first workbench with these capabilities:

```text
Create local project
Import CSV dataset
Preview dataset
Profile columns
Review and edit variable definitions
Answer fundamental-analysis questions
Record decisions in a decision log
Show methodology progress
Generate Markdown report
Persist project locally
```

Implement the MVP incrementally. Prefer small vertical slices that compile and test.

---

## 5. Explicit non-goals unless requested

Do not implement these unless the current task explicitly asks for them:

```text
AutoML
LLM agents
RAG
OpenAI API calls
Cloud synchronization
User accounts
ASP.NET Identity
PostgreSQL
Web application host
Python integration
Scikit-Learn runner
ML.NET model training
Hyperparameter search
Collaboration features
Paid/commercial features
Telemetry or analytics collection
Remote dataset upload
```

Do not add network calls, background cloud services or hidden external dependencies.

---

## 6. Solution structure

Target structure:

```text
src/
  Ariadne.Domain/
  Ariadne.Application/
  Ariadne.Analytics/
  Ariadne.Infrastructure.Local/
  Ariadne.SharedUi/
  Ariadne.Maui/

future/
  Ariadne.Web/
  Ariadne.Infrastructure.Web/

tests/
  Ariadne.Domain.Tests/
  Ariadne.Application.Tests/
  Ariadne.Analytics.Tests/
  Ariadne.Infrastructure.Local.Tests/
```

Do not create future projects early unless explicitly requested.

---

## 7. Dependency rules

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
  -> Ariadne.Analytics when registration is required
```

Forbidden dependencies:

```text
Ariadne.Domain -> anything
Ariadne.Application -> Infrastructure, SharedUi, Maui, Web
Ariadne.Analytics -> Infrastructure, SharedUi, Maui, Web
Ariadne.Infrastructure.Local -> SharedUi, Maui, Web
Ariadne.SharedUi -> Maui, Infrastructure.Local, Infrastructure.Web
```

`Ariadne.Maui` is only a host. It wires services, platform concerns and `BlazorWebView`. It must not contain domain logic.

`Ariadne.SharedUi` is reusable UI. It must stay MAUI-independent so that a future Blazor Web host can reuse it.

---

## 8. Domain rules

Domain code must be pure C#.

Rules:

1. No file system access in Domain.
2. No SQLite or EF Core in Domain.
3. No Blazor, MAUI or UI concepts in Domain.
4. No logging-framework dependency in Domain.
5. No service locator.
6. Use value objects for IDs where appropriate.
7. Keep invariants close to the aggregate/entity that owns them.
8. Prefer explicit methods over public mutable collections.
9. Validate required fields at construction or through factory methods.
10. Do not store UI state in domain objects.

Initial domain concepts include:

```text
AiProject
Dataset
DatasetVersion
ColumnProfile
VariableDefinition
FundamentalAnalysis
FundamentalAnalysisAnswer
DecisionLogEntry
MethodologyProgress
MethodologyReport
```

Future concepts include:

```text
Hypothesis
StatisticalTestRun
PreprocessingPipeline
PreprocessingStep
ExperimentRun
ModelCandidate
EvaluationMetric
ConfidenceInterval
ApplicabilityStatement
```

Do not implement future concepts until they are part of the requested task.

---

## 9. Application-layer rules

Application services orchestrate use cases. They may depend on Domain abstractions and application interfaces, but not on concrete local infrastructure.

Use application interfaces for infrastructure concerns:

```text
IProjectRepository
IProjectCatalog
IDatasetFileStore
ICsvDatasetReader
IDatasetProfiler
IReportGenerator
IClock
IIdGenerator
```

Rules:

1. Keep application use cases explicit.
2. Avoid anemic pass-through services when a use case has real behavior.
3. Return structured results, not ambiguous strings.
4. Surface validation errors clearly.
5. Do not swallow exceptions silently.
6. Keep external details behind interfaces.
7. Application code must be testable without MAUI or SQLite.

---

## 10. Analytics rules

For the MVP, prefer pure C# deterministic analytics.

MVP analytics may include:

```text
row count
column count
missing-value count
missing-value percentage
unique-value count
type inference
min/max for numeric columns
mean/median where feasible
quartiles where feasible
basic IQR outlier detection where requested
sample values
```

Rules:

1. Do not introduce Python for MVP analytics.
2. Do not introduce ML.NET unless explicitly requested.
3. Do not infer more confidence than the data supports.
4. Store whether a profile is automatically inferred or user-reviewed.
5. Use deterministic algorithms and unit tests for numeric/statistical behavior.
6. Handle empty columns, malformed data and culture-sensitive numeric formats carefully.
7. Keep raw dataset storage separate from computed profiling metadata.

---

## 11. Infrastructure.Local rules

The product is local-first.

Rules:

1. Data must remain on the user's machine by default.
2. No server is required for the app to work.
3. No account is required for the app to work.
4. No internet connection is required for the app to work.
5. Store imported datasets as local files, not as embedded blobs in domain objects.
6. Store project metadata, profiling results, analysis answers and decision logs in local persistence.
7. Prefer one self-contained project folder per Ariadne project when possible.
8. Do not commit imported datasets or generated local project files to git.
9. Use platform-safe file paths through infrastructure/platform services.
10. Make backup/export behavior explicit.

SQLite/EF Core may be introduced only when the relevant milestone requests it.

---

## 12. Shared UI rules

Reusable UI belongs in `Ariadne.SharedUi`.

Rules:

1. Do not put MAUI dependencies in `Ariadne.SharedUi`.
2. Do not put database or file-system logic in UI components.
3. UI components call application services through dependency injection.
4. Keep pages thin; move behavior into view models or application use cases.
5. Use reusable components for cards, progress indicators, dataset tables and forms.
6. Prefer clear workflow guidance over dense dashboards.
7. Show uncertainty, warnings and missing information honestly.
8. Use CSS isolation and shared design tokens where practical.
9. Do not scatter raw colors and spacing values across many files.
10. Ensure forms are keyboard-friendly and accessible.

The UI should feel like a workbench and guided lab notebook, not like an AutoML dashboard.

---

## 13. MAUI host rules

`Ariadne.Maui` hosts the app.

Rules:

1. Use `BlazorWebView` to host shared Blazor UI.
2. Register services in DI.
3. Keep platform-specific code minimal and isolated.
4. Do not put business logic in `MauiProgram.cs`.
5. Do not put feature workflows in MAUI pages when they can live in SharedUi.
6. If MAUI workloads are missing in the execution environment, report the limitation clearly and still run all possible non-MAUI tests.

---

## 14. Naming and language conventions

Use English for code, namespaces, class names, tests and repository documentation.

Use these names consistently:

```text
Ariadne
Ariadne AI Workbench
AiProject
Dataset
DatasetVersion
ColumnProfile
VariableDefinition
DecisionLogEntry
FundamentalAnalysis
MethodologyReport
```

Avoid vague names:

```text
Manager
Helper
Processor
Thing
DataObject
Common
Utils
```

Acceptable suffixes when meaningful:

```text
Service
Repository
Store
Reader
Writer
Generator
Profiler
Analyzer
Validator
Factory
Options
Result
Request
Response
```

---

## 15. C# coding rules

Use modern C# and .NET conventions.

Rules:

1. Enable nullable reference types.
2. Prefer immutable value objects and records where appropriate.
3. Use `async`/`await` for I/O.
4. Pass `CancellationToken` for async application/infrastructure operations where practical.
5. Avoid unnecessary reflection and dynamic typing.
6. Avoid global mutable state.
7. Avoid hidden static service access.
8. Prefer small classes with clear responsibilities.
9. Prefer explicit error results for expected validation failures.
10. Keep public APIs intentional and documented when they are central to the architecture.

Do not introduce large dependencies for small problems.

---

## 16. Testing rules

Every meaningful change should include or update tests.

Preferred test projects:

```text
Ariadne.Domain.Tests
Ariadne.Application.Tests
Ariadne.Analytics.Tests
Ariadne.Infrastructure.Local.Tests
```

Testing expectations:

1. Domain invariants must have unit tests.
2. Application use cases must have tests with fake infrastructure.
3. Analytics calculations must have deterministic tests with edge cases.
4. CSV import must have tests for malformed rows, empty files and type inference.
5. Local persistence must have tests when storage is implemented.
6. UI components should be tested when the project introduces a UI testing strategy; until then, keep UI logic thin and test the view models/application services.

Do not claim tests passed unless they were actually run.

---

## 17. Build and validation commands

Use these commands when applicable:

```bash
dotnet --info
dotnet restore Ariadne.sln
dotnet build Ariadne.sln
dotnet test Ariadne.sln
```

When formatting is configured:

```bash
dotnet format Ariadne.sln --verify-no-changes
```

If full solution build is not possible because MAUI workloads are unavailable, run the build/tests for projects that can be validated:

```bash
dotnet build src/Ariadne.Domain/Ariadne.Domain.csproj
dotnet build src/Ariadne.Application/Ariadne.Application.csproj
dotnet build src/Ariadne.Analytics/Ariadne.Analytics.csproj
dotnet test tests/Ariadne.Domain.Tests/Ariadne.Domain.Tests.csproj
dotnet test tests/Ariadne.Application.Tests/Ariadne.Application.Tests.csproj
dotnet test tests/Ariadne.Analytics.Tests/Ariadne.Analytics.Tests.csproj
```

Always state exactly what was run and what failed or was skipped.

---

## 18. Git and change discipline

For Codex tasks:

1. Make small, focused changes.
2. Do not refactor unrelated code.
3. Do not rename projects, folders or namespaces unless requested.
4. Do not change public behavior without updating tests and relevant docs.
5. Do not modify generated files unless necessary.
6. Do not add secrets, API keys or personal data.
7. Do not commit imported datasets.
8. Prefer one coherent vertical slice over many half-finished abstractions.

Before large changes, propose a short plan.

---

## 19. Documentation rules

Update documentation when behavior, architecture or scope changes.

Use this routing:

```text
Product intent -> 00-project-brief.md or 01-product-vision.md
Feature behavior -> 02-functional-specification.md
Technical structure -> 03-technical-architecture.md
Domain concepts -> 04-domain-model.md
Methodology stages -> 05-methodology-workflow.md
Local persistence -> 06-local-first-storage.md
UI/UX -> 07-ui-ux-guidelines.md
Roadmap -> 08-development-roadmap.md
Codex tasks -> 09-codex-task-breakdown.md
Always-on agent rules -> AGENTS.md
```

Do not duplicate large sections across documents. Link or reference the source document instead.

---

## 20. Report-generation rules

MVP reports are Markdown-first.

Reports should include:

```text
project summary
dataset summary
column profiles
reviewed variable dictionary
fundamental analysis answers
decision log
known limitations
missing information
next recommended methodology steps
```

Reports must not overstate conclusions. Missing context should be explicitly mentioned.

---

## 21. Privacy and safety rules

Ariadne may handle sensitive datasets.

Rules:

1. Keep data local by default.
2. Do not add telemetry without explicit consent and a dedicated task.
3. Do not upload datasets to external services.
4. Do not send dataset content to LLMs.
5. Do not log raw dataset rows unnecessarily.
6. Redact or avoid sensitive values in errors where possible.
7. Document when generated reports may contain sensitive data.

---

## 22. Error-handling rules

Errors should help the user recover.

For user-facing failures, prefer messages that explain:

```text
what happened
why it likely happened
what the user can do next
whether data was modified or preserved
```

For developer-facing failures, preserve enough diagnostic detail to debug without exposing raw sensitive data unnecessarily.

---

## 23. Accessibility and UX rules

Ariadne should be calm, readable and precise.

Rules:

1. Prefer clear labels over icons alone.
2. Use progressive disclosure for complex methodology content.
3. Show progress through the methodology workflow.
4. Distinguish complete, incomplete, warning and blocked states.
5. Do not use color as the only source of meaning.
6. Keep tables usable on desktop-sized windows first.
7. Use accessible form labels and validation messages.
8. Use plain language for methodology explanations.

---

## 24. When adding dependencies

Before adding a package, check whether the task can be solved with the platform or existing dependencies.

A new dependency must have:

```text
clear purpose
small scope
compatible license for open source
active maintenance
minimal architectural impact
```

Do not add a UI framework, charting library, CSV library, ORM, ML package or statistics package casually. Justify it in the task summary.

---

## 25. Codex task workflow

For implementation tasks, Codex should follow this workflow:

```text
1. Read AGENTS.md.
2. Read only the relevant project documents.
3. Inspect the existing files before editing.
4. Propose a brief plan for non-trivial work.
5. Implement the smallest complete change.
6. Add or update tests.
7. Run relevant build/test commands.
8. Update documentation if needed.
9. Summarize changes and validation honestly.
```

Do not skip file inspection and start coding from assumptions.

---

## 26. Required response format for Codex

For coding tasks, respond with:

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

If tests were not run, write:

```text
Tests:
- Not run: <reason>
```

If a command failed, include the command and the relevant failure summary.

---

## 27. Review checklist

Before considering a task complete, verify:

```text
Architecture boundaries are respected.
Domain remains infrastructure-free.
SharedUi remains MAUI-free.
No future non-goal was introduced accidentally.
New behavior is covered by tests where practical.
Build/test status is reported honestly.
Errors are recoverable and understandable.
Local-first/privacy assumptions are preserved.
Documentation is updated when necessary.
```

---

## 28. If blocked

When blocked, do not invent facts or silently work around requirements.

State:

```text
what is blocking the task
what was already checked
which files/commands were involved
one or two reasonable next options
```

Prefer asking for clarification over implementing a risky assumption.

---

## 29. Initial implementation sequence

Unless a task says otherwise, follow the roadmap order:

```text
M00 Documentation baseline
M01 Solution skeleton
M02 Domain foundation
M03 Application use cases
M04 Shared UI foundation
M05 Local project catalog
M06 CSV import and dataset preview
M07 Dataset profiling
M08 Variable dictionary
M09 Fundamental analysis
M10 Decision log
M11 Markdown report
M12 MVP stabilization
```

Do not jump to modelling, preprocessing automation, cloud or web-host work before the foundation is stable.

---

## 30. Final project reminder

Ariadne is the user's thread through the AI methodology labyrinth.

Build features that help users reason clearly, document honestly and move step by step from data understanding to defensible conclusions.
