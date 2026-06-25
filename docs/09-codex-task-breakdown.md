# 09 - Codex Task Breakdown

# Ariadne AI Workbench

Document status: Draft v1  
Audience: project owner, maintainers, contributors, Codex  
Project type: local-first open-source .NET 10 MAUI Blazor Hybrid application  
Primary goal: convert the roadmap into small, reviewable Codex tasks

---

## 1. Purpose of this document

This document translates the Ariadne AI Workbench product, architecture, workflow and roadmap documents into concrete implementation tasks that can be delegated to Codex.

It is not a replacement for the product documentation. It is an execution layer.

Codex should use this file to understand:

```text
what to build,
in what order,
which documents to read,
which files are likely to change,
which acceptance criteria must be met,
which commands should be run,
and what must stay out of scope.
```

Ariadne must be built incrementally. The product is methodology-first: the first valuable version helps users create a project, import a dataset, understand variables, document context, record decisions and generate a defensible Markdown report. It must not jump directly to modelling, AutoML or cloud features.

---

## 2. How to use this document with Codex

Use this file in three ways:

1. As a task backlog.
2. As a source of copy-paste prompts.
3. As a review checklist after each Codex change.

Recommended workflow:

```text
1. Pick exactly one task ID.
2. Copy the task prompt into Codex.
3. Let Codex plan briefly before editing.
4. Let Codex implement only the requested slice.
5. Run the requested validation commands.
6. Review the summary, tests, changed files and risks.
7. Commit the change manually after review.
```

Do not ask Codex to implement multiple milestones at once.

---

## 3. Codex operating assumptions

Codex should be treated as a coding agent working inside the repository. It may read project files, edit code and run local commands when used through a local coding workflow. The project will also contain an `AGENTS.md` file because Codex can automatically load repository-specific instructions from such files.

Codex-specific project rules:

```text
- Always work from the repository root.
- Read AGENTS.md before making changes.
- Read the documents listed in the task prompt.
- Prefer small vertical slices.
- Never invent a new architecture.
- Never add external services unless the task explicitly allows it.
- Never move business logic into Razor components.
- Never add ML, LLM, cloud or web-host features during MVP tasks.
- Always report validation limitations honestly.
```

Official Codex references for maintainers:

```text
https://developers.openai.com/codex/cli
https://developers.openai.com/codex/guides/agents-md
https://developers.openai.com/codex/learn/best-practices
https://developers.openai.com/codex/cloud
```

---

## 4. Methodological foundation

Ariadne is based on a rigorous data science workflow:

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

The MVP implements only the beginning of this path:

```text
Project
→ Dataset
→ Understand
→ Report
```

The methodological rules that should influence implementation are:

```text
A dataset is only a sample, not the full population.
Variables must be understood before being modelled.
Technical analysis must be combined with fundamental analysis.
User-reviewed meaning is more important than automatic inference.
Unknown information should be explicit, not hidden.
Every important methodological decision should be traceable.
Preprocessing and modelling must not be implemented before the user can understand and document the dataset.
Reports must communicate uncertainty and limits honestly.
```

For implementation, this means the application should not merely compute statistics. It should help the user document what the data means, where it comes from, what is known, what is unknown and which decisions were made.

---

## 5. Required project documents

Codex tasks must reference the appropriate documentation files.

| Document | Purpose for Codex |
|---|---|
| `00-project-brief.md` | Product purpose, MVP scope, principles, non-goals. |
| `01-product-vision.md` | Product positioning, users, promise, workflow philosophy. |
| `02-functional-specification.md` | Functional requirements and acceptance criteria. |
| `03-technical-architecture.md` | Solution structure, dependencies, layering rules. |
| `04-domain-model.md` | Domain entities, value objects, enums, invariants. |
| `05-methodology-workflow.md` | Workflow stages, gates, methodology rules. |
| `06-local-first-storage.md` | Local persistence, project folders, SQLite strategy. |
| `07-ui-ux-guidelines.md` | UI structure, components, visual and UX rules. |
| `08-development-roadmap.md` | Milestones, dependencies, release sequence. |
| `09-codex-task-breakdown.md` | Concrete tasks and prompts for Codex. |
| `AGENTS.md` | Always-on rules that Codex must load before work. |
| `README.md` | Public-facing project description. |

---

## 6. Solution baseline expected by tasks

The intended solution structure is:

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

Future projects, not part of the MVP:

```text
src/
  Ariadne.Web/
  Ariadne.Infrastructure.Web/
```

Codex must not create the future web projects until a task explicitly requests a web reuse experiment.

---

## 7. Dependency rules

The dependency direction must remain clean.

```text
Ariadne.Domain
  depends on nothing project-specific

Ariadne.Application
  depends on Ariadne.Domain

Ariadne.Analytics
  depends on Ariadne.Domain
  may depend on Ariadne.Application only if a task explicitly needs application contracts

Ariadne.Infrastructure.Local
  depends on Ariadne.Domain, Ariadne.Application, Ariadne.Analytics as needed
  implements local persistence, local files and local adapters

Ariadne.SharedUi
  depends on Ariadne.Application and shared contracts/query models
  must not depend on Ariadne.Maui
  must not depend directly on SQLite or filesystem APIs

Ariadne.Maui
  depends on Ariadne.SharedUi, Ariadne.Application, Ariadne.Infrastructure.Local
  is only the native host and DI composition root
```

Hard rule:

```text
Domain logic belongs in Domain.
Use-case orchestration belongs in Application.
Analytics calculations belong in Analytics.
Persistence belongs in Infrastructure.Local.
Reusable UI belongs in SharedUi.
MAUI-specific hosting belongs in Maui.
```

---

## 8. Global task constraints

Unless a task explicitly says otherwise, Codex must not:

```text
add a web application;
add authentication;
add cloud synchronization;
add remote telemetry;
add OpenAI API calls;
add LLM runtime features;
add ML.NET;
add Python integration;
add AutoML;
add paid/commercial features;
store raw datasets in application logs;
change the namespace root from Ariadne;
rename projects;
replace the documented architecture;
introduce unapproved database providers;
create broad refactors outside the requested scope.
```

---

## 9. Validation command tiers

Different development machines may have different .NET MAUI workloads installed. Codex must validate as much as possible and must report limitations clearly.

### 9.1 Preferred validation

Use when the machine has the required .NET SDK and MAUI workloads:

```bash
dotnet restore
dotnet build
dotnet test
```

### 9.2 Non-MAUI validation fallback

Use when the MAUI workload is not installed or the host platform cannot build MAUI targets:

```bash
dotnet restore
dotnet build Ariadne.sln --ignore-failed-sources
dotnet test
```

If `Ariadne.Maui` cannot build because workloads are missing, Codex must say so explicitly and still validate all non-MAUI projects.

### 9.3 Focused project validation

Use for small tasks when full solution build is temporarily blocked:

```bash
dotnet build src/Ariadne.Domain/Ariadne.Domain.csproj
dotnet build src/Ariadne.Application/Ariadne.Application.csproj
dotnet build src/Ariadne.Analytics/Ariadne.Analytics.csproj
dotnet test tests/Ariadne.Domain.Tests/Ariadne.Domain.Tests.csproj
dotnet test tests/Ariadne.Application.Tests/Ariadne.Application.Tests.csproj
dotnet test tests/Ariadne.Analytics.Tests/Ariadne.Analytics.Tests.csproj
```

### 9.4 Documentation-only validation

Use for Markdown-only changes:

```bash
git diff --check
```

If a Markdown linting tool is later added, use it as well.

---

## 10. Standard Codex prompt template

Use this template for every task.

```text
You are working on Ariadne AI Workbench.

Task ID:
<task id>

Goal:
<one clear goal>

Relevant documents:
- <doc 1>
- <doc 2>
- <doc 3>

Files or projects likely to edit:
- <path 1>
- <path 2>

Scope:
- <what to implement>

Non-goals:
- <what not to implement>

Architecture constraints:
- Respect Clean Architecture dependency direction.
- Do not put domain logic in UI.
- Do not add unapproved packages.
- Keep the change small and reviewable.

Acceptance criteria:
- <criterion 1>
- <criterion 2>

Commands to run:
- dotnet build
- dotnet test

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

---

## 11. Standard Codex response format

Codex should always finish with:

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

If something failed, Codex must include:

```text
What failed:
Why it failed:
What was still validated:
Recommended next step:
```

---

## 12. Task ID convention

Task IDs follow this pattern:

```text
CDX-M<roadmap milestone>-T<task number>
```

Examples:

```text
CDX-M01-T01
CDX-M06-T03
CDX-M12-T02
```

Task type labels:

| Label | Meaning |
|---|---|
| `setup` | Repository, solution, CI, tooling. |
| `domain` | Domain entities, value objects, invariants. |
| `application` | Use cases, ports, DTOs, orchestration. |
| `analytics` | Deterministic profiling/statistical calculations. |
| `infrastructure` | SQLite, files, repositories, adapters. |
| `ui` | Shared Blazor components and screens. |
| `maui` | MAUI host and platform-specific code. |
| `reporting` | Markdown report generation/export. |
| `test` | Test-only or test-heavy work. |
| `docs` | Documentation. |
| `hardening` | Errors, edge cases, release cleanup. |

---

# MVP task breakdown

The MVP target is `v0.1`. It includes:

```text
- repository and solution setup;
- domain/application foundations;
- shared UI and MAUI shell;
- local storage foundation;
- project management;
- CSV dataset import and preview;
- dataset profiling;
- variable dictionary;
- fundamental analysis;
- decision log;
- methodology progress;
- Markdown report generation;
- MVP hardening.
```

---

## 13. M00 - Documentation baseline tasks

### CDX-M00-T01 - Verify documentation baseline

Type: `docs`  
Priority: Must have  
Depends on: existing documentation files

Goal:

```text
Verify that the project documentation set is present, consistently named and referenced by README/AGENTS.md planning.
```

Scope:

```text
- Confirm docs 00 through 09 exist.
- Confirm filenames match the documented sequence.
- Do not rewrite all docs.
- Add a small docs index only if README does not already include one.
```

Acceptance criteria:

```text
- Documentation files are present.
- No implementation code is changed.
- Any missing doc is reported rather than invented silently.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M00-T01

Goal:
Verify the documentation baseline and report any missing or inconsistent documentation files.

Relevant documents:
- 00-project-brief.md
- 01-product-vision.md
- 02-functional-specification.md
- 03-technical-architecture.md
- 04-domain-model.md
- 05-methodology-workflow.md
- 06-local-first-storage.md
- 07-ui-ux-guidelines.md
- 08-development-roadmap.md
- 09-codex-task-breakdown.md

Scope:
- Check that the docs exist.
- Check that the naming sequence is consistent.
- Add or update a small docs index only if needed.

Non-goals:
- Do not rewrite the documentation.
- Do not create application code.

Commands to run:
- git diff --check

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M00-T02 - Create repository `AGENTS.md`

Type: `docs`  
Priority: Must have  
Depends on: docs 00 through 09

Goal:

```text
Create the repository-level AGENTS.md that Codex will read before work.
```

Scope:

```text
- Define architecture rules.
- Define validation expectations.
- Define response format.
- Define non-goals.
- Reference the documentation sequence.
- Keep it concise enough to be loaded frequently.
```

Acceptance criteria:

```text
- AGENTS.md exists at repository root.
- It instructs Codex to read relevant docs before edits.
- It includes dependency direction rules.
- It includes the standard response format.
- It forbids premature cloud, web, ML and LLM features.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M00-T02

Goal:
Create a repository-level AGENTS.md for Codex.

Relevant documents:
- 00-project-brief.md
- 03-technical-architecture.md
- 08-development-roadmap.md
- 09-codex-task-breakdown.md

Files likely to edit:
- AGENTS.md

Scope:
- Create AGENTS.md at repository root.
- Include architecture rules, validation expectations, non-goals and response format.
- Keep it concise and practical.

Non-goals:
- Do not create code.
- Do not rewrite the existing documentation set.

Acceptance criteria:
- AGENTS.md exists.
- It preserves Clean Architecture dependency direction.
- It tells Codex not to add web, cloud, ML, LLM or auth features during MVP tasks.
- It includes the response format: Summary, Tests, Changed files, Risks / notes.

Commands to run:
- git diff --check

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M00-T03 - Create initial public README

Type: `docs`  
Priority: Must have  
Depends on: docs 00 and 01

Goal:

```text
Create the initial public-facing README for the repository.
```

Scope:

```text
- Product name and one-liner.
- Local-first open-source positioning.
- MVP status.
- Documentation index.
- Build/test placeholders.
- Contribution status.
- License decision note if license is still open.
```

Acceptance criteria:

```text
- README is clear for a first GitHub visitor.
- README does not overpromise current features.
- README clearly says the project is early-stage if no app exists yet.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M00-T03

Goal:
Create the initial README.md for the project.

Relevant documents:
- 00-project-brief.md
- 01-product-vision.md
- 08-development-roadmap.md

Files likely to edit:
- README.md

Scope:
- Explain what Ariadne AI Workbench is.
- Explain local-first and open-source positioning.
- Add a documentation index.
- Add early build/test instructions as placeholders if the solution does not exist yet.
- Mention that the license decision is open if LICENSE is not present.

Non-goals:
- Do not claim the app is production-ready.
- Do not add commercial features.
- Do not add implementation code.

Commands to run:
- git diff --check

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

---

## 14. M01 - Repository and solution setup tasks

### CDX-M01-T01 - Create repository hygiene files

Type: `setup`  
Priority: Must have  
Depends on: M00

Goal:

```text
Add repository-level hygiene files before creating application code.
```

Scope:

```text
- .gitignore for .NET, MAUI, IDE files and local datasets.
- .editorconfig for C# and Markdown basics.
- Directory.Build.props for common .NET settings.
- Optional global.json only if the SDK policy is already decided.
```

Important privacy rule:

```text
Local imported datasets and generated project data must not be committed accidentally.
```

Acceptance criteria:

```text
- Local dataset folders are ignored by default.
- Build artifacts are ignored.
- Common C# compiler settings are centralized.
- Nullable and implicit usings defaults are explicitly configured.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M01-T01

Goal:
Create repository hygiene files for a .NET 10 MAUI Blazor Hybrid project.

Relevant documents:
- 03-technical-architecture.md
- 06-local-first-storage.md
- 08-development-roadmap.md

Files likely to edit:
- .gitignore
- .editorconfig
- Directory.Build.props

Scope:
- Add .gitignore entries for .NET, MAUI, IDE folders, build output and local dataset/project data.
- Add .editorconfig with C# formatting basics.
- Add Directory.Build.props with common settings.

Non-goals:
- Do not create solution projects yet.
- Do not add packages.
- Do not add CI yet.

Acceptance criteria:
- Local data folders are ignored.
- Nullable reference types are enabled by default.
- Treat warnings policy is documented or configured conservatively.

Commands to run:
- git diff --check

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M01-T02 - Create .NET solution and projects

Type: `setup`  
Priority: Must have  
Depends on: CDX-M01-T01

Goal:

```text
Create the .NET solution and baseline project structure.
```

Scope:

```text
- Create Ariadne.sln.
- Create class library projects.
- Create Razor Class Library for SharedUi.
- Create MAUI Blazor Hybrid app project.
- Create xUnit test projects.
- Add projects to solution.
```

Acceptance criteria:

```text
- Solution contains all MVP projects.
- Projects use the Ariadne namespace root.
- No business features are implemented yet.
- Build validation is attempted.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M01-T02

Goal:
Create the initial .NET solution and project structure.

Relevant documents:
- 03-technical-architecture.md
- 08-development-roadmap.md
- 09-codex-task-breakdown.md

Files or projects likely to create:
- Ariadne.sln
- src/Ariadne.Domain/
- src/Ariadne.Application/
- src/Ariadne.Analytics/
- src/Ariadne.Infrastructure.Local/
- src/Ariadne.SharedUi/
- src/Ariadne.Maui/
- tests/Ariadne.Domain.Tests/
- tests/Ariadne.Application.Tests/
- tests/Ariadne.Analytics.Tests/
- tests/Ariadne.Infrastructure.Local.Tests/

Scope:
- Create the solution and projects.
- Add all projects to the solution.
- Do not implement features yet.

Non-goals:
- Do not add SQLite yet.
- Do not add ML.NET.
- Do not add a Web project.
- Do not add authentication.

Architecture constraints:
- SharedUi must not depend on Maui.
- Domain must not depend on any project.

Acceptance criteria:
- dotnet sln lists the projects.
- Project names and folders match the architecture docs.
- The solution builds as far as the local workload environment allows.

Commands to run:
- dotnet restore
- dotnet build
- dotnet test

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M01-T03 - Add project references and dependency guardrails

Type: `setup`  
Priority: Must have  
Depends on: CDX-M01-T02

Goal:

```text
Wire project references according to Clean Architecture.
```

Expected references:

```text
Ariadne.Application -> Ariadne.Domain
Ariadne.Analytics -> Ariadne.Domain
Ariadne.Infrastructure.Local -> Ariadne.Domain, Ariadne.Application, Ariadne.Analytics
Ariadne.SharedUi -> Ariadne.Application, Ariadne.Domain if needed for shared types
Ariadne.Maui -> Ariadne.SharedUi, Ariadne.Application, Ariadne.Infrastructure.Local
Test projects -> their target projects
```

Acceptance criteria:

```text
- No reverse dependency from Domain.
- SharedUi does not reference Maui.
- Infrastructure does not leak into Domain.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M01-T03

Goal:
Add project references that respect Clean Architecture.

Relevant documents:
- 03-technical-architecture.md
- 04-domain-model.md
- 08-development-roadmap.md

Scope:
- Add the documented project references.
- Add a short architecture note in README or docs if useful.
- Ensure test projects reference the correct production projects.

Non-goals:
- Do not implement domain entities yet.
- Do not add persistence packages yet.
- Do not add web or cloud projects.

Acceptance criteria:
- Project references match the dependency matrix.
- Domain has no project references.
- SharedUi does not reference Maui.

Commands to run:
- dotnet build
- dotnet test

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M01-T04 - Add baseline CI workflow

Type: `setup`  
Priority: Should have  
Depends on: CDX-M01-T02

Goal:

```text
Add a conservative GitHub Actions workflow for restore, build and tests.
```

CI note:

```text
MAUI workloads may complicate CI. Prefer validating non-MAUI projects first if full MAUI CI is not ready.
```

Acceptance criteria:

```text
- CI does not publish artifacts.
- CI does not require secrets.
- CI runs tests.
- CI documents any MAUI limitation.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M01-T04

Goal:
Add an initial GitHub Actions workflow for build and tests.

Relevant documents:
- 03-technical-architecture.md
- 08-development-roadmap.md
- 09-codex-task-breakdown.md

Files likely to edit:
- .github/workflows/ci.yml
- README.md if build badge or CI note is useful

Scope:
- Add a basic CI workflow for restore, build and test.
- Prefer non-MAUI project validation if MAUI workloads are not practical in CI yet.
- Document any limitation clearly.

Non-goals:
- Do not add release packaging.
- Do not add code signing.
- Do not add deployment.

Acceptance criteria:
- CI file is valid YAML.
- CI has no secrets.
- CI runs tests.

Commands to run:
- git diff --check

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

---

## 15. M02 - Domain and application foundation tasks

### CDX-M02-T01 - Add domain primitives

Type: `domain`  
Priority: Must have  
Depends on: M01

Goal:

```text
Implement the basic domain primitives used by all later features.
```

Scope:

```text
- Entity base type.
- AggregateRoot base type.
- Strongly typed IDs.
- Common value objects such as ProjectName, DatasetName, ColumnName, Ratio, ContentHash.
- Core enums.
- Unit tests for value object validation.
```

Acceptance criteria:

```text
- Primitives are immutable where appropriate.
- Invalid values are rejected.
- Domain project has no external infrastructure dependencies.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M02-T01

Goal:
Implement core domain primitives and tests.

Relevant documents:
- 04-domain-model.md
- 03-technical-architecture.md

Files likely to edit:
- src/Ariadne.Domain/
- tests/Ariadne.Domain.Tests/

Scope:
- Add entity and aggregate root base classes if appropriate.
- Add strongly typed IDs for ProjectId, DatasetId, DatasetVersionId, DecisionLogEntryId.
- Add value objects ProjectName, DatasetName, ColumnName, Ratio and ContentHash.
- Add core enums required by MVP.
- Add unit tests for validation and equality.

Non-goals:
- Do not add EF Core attributes.
- Do not add repositories.
- Do not add UI.

Acceptance criteria:
- Domain compiles with no infrastructure dependencies.
- Invalid names/ratios are rejected.
- Tests cover equality and validation.

Commands to run:
- dotnet build src/Ariadne.Domain/Ariadne.Domain.csproj
- dotnet test tests/Ariadne.Domain.Tests/Ariadne.Domain.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M02-T02 - Implement `AiProject` aggregate

Type: `domain`  
Priority: Must have  
Depends on: CDX-M02-T01

Goal:

```text
Implement the project aggregate that owns project metadata and workflow progress.
```

Scope:

```text
- AiProject aggregate.
- Project status.
- Created/updated timestamps.
- Basic behavior: create, rename, archive/reactivate if in docs, update description.
- Initial methodology progress.
- Unit tests.
```

Acceptance criteria:

```text
- Project name cannot be empty.
- Project starts with expected methodology progress.
- UpdatedAt changes when mutable fields change.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M02-T02

Goal:
Implement the AiProject aggregate and tests.

Relevant documents:
- 04-domain-model.md
- 05-methodology-workflow.md

Files likely to edit:
- src/Ariadne.Domain/Projects/
- tests/Ariadne.Domain.Tests/Projects/

Scope:
- Implement AiProject according to the domain model.
- Include project metadata, status and initial methodology progress.
- Add behavior for creation and safe updates.
- Add unit tests.

Non-goals:
- Do not implement persistence.
- Do not implement project UI.
- Do not implement dataset import.

Acceptance criteria:
- AiProject enforces invariants.
- Tests cover creation, invalid names and updates.

Commands to run:
- dotnet build src/Ariadne.Domain/Ariadne.Domain.csproj
- dotnet test tests/Ariadne.Domain.Tests/Ariadne.Domain.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M02-T03 - Implement `Dataset` and `DatasetVersion`

Type: `domain`  
Priority: Must have  
Depends on: CDX-M02-T01

Goal:

```text
Implement the dataset aggregate and immutable dataset version records.
```

Scope:

```text
- Dataset aggregate.
- DatasetVersion entity.
- DatasetFileReference value object.
- Import metadata fields.
- Add version behavior.
- Unit tests.
```

Acceptance criteria:

```text
- Raw import versions are append-only.
- Dataset cannot have an empty name.
- Dataset version stores file reference and content hash.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M02-T03

Goal:
Implement Dataset and DatasetVersion domain concepts.

Relevant documents:
- 04-domain-model.md
- 06-local-first-storage.md

Files likely to edit:
- src/Ariadne.Domain/Datasets/
- tests/Ariadne.Domain.Tests/Datasets/

Scope:
- Add Dataset aggregate.
- Add DatasetVersion entity.
- Add DatasetFileReference if not already present.
- Add behavior for adding an imported version.
- Add tests for invariants and version behavior.

Non-goals:
- Do not implement CSV parsing.
- Do not copy files.
- Do not implement SQLite.

Acceptance criteria:
- Dataset and DatasetVersion enforce invariants.
- Tests cover invalid names, valid versions and file reference validation.

Commands to run:
- dotnet build src/Ariadne.Domain/Ariadne.Domain.csproj
- dotnet test tests/Ariadne.Domain.Tests/Ariadne.Domain.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M02-T04 - Implement profiling domain models

Type: `domain`  
Priority: Must have  
Depends on: CDX-M02-T01

Goal:

```text
Implement domain models that store dataset and column profiling results.
```

Scope:

```text
- DatasetProfile.
- ColumnProfile.
- Numeric profile fields.
- Discrete/text profile fields.
- Missing value profile fields.
- Inferred methodological type.
- Unit tests.
```

Acceptance criteria:

```text
- Profile values cannot contradict row counts.
- Missing ratios use the Ratio value object.
- Inferred values are clearly marked as inferred, not user-reviewed.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M02-T04

Goal:
Implement DatasetProfile and ColumnProfile domain models with tests.

Relevant documents:
- 04-domain-model.md
- 02-functional-specification.md

Files likely to edit:
- src/Ariadne.Domain/Profiling/
- tests/Ariadne.Domain.Tests/Profiling/

Scope:
- Add DatasetProfile and ColumnProfile domain models.
- Represent row count, column count, missing values, unique counts and basic numeric stats.
- Represent primitive and methodological type inference states.
- Add unit tests.

Non-goals:
- Do not implement profiling calculations yet.
- Do not implement charts.
- Do not implement persistence.

Acceptance criteria:
- Profiles enforce basic consistency.
- Tests cover invalid counts and ratios.

Commands to run:
- dotnet build src/Ariadne.Domain/Ariadne.Domain.csproj
- dotnet test tests/Ariadne.Domain.Tests/Ariadne.Domain.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M02-T05 - Implement variable dictionary domain models

Type: `domain`  
Priority: Must have  
Depends on: CDX-M02-T01

Goal:

```text
Implement the domain concept for reviewed variable meaning.
```

Scope:

```text
- VariableDefinition.
- Variable role.
- User-reviewed methodological type.
- Unit of measure.
- Description and notes.
- Review status.
- Unit tests.
```

Acceptance criteria:

```text
- Automatic inference and user-reviewed type remain separate.
- The user can mark a variable as unknown or reviewed.
- Target role can be represented, but no modelling is implemented.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M02-T05

Goal:
Implement VariableDefinition domain models and tests.

Relevant documents:
- 04-domain-model.md
- 02-functional-specification.md
- 05-methodology-workflow.md

Files likely to edit:
- src/Ariadne.Domain/Variables/
- tests/Ariadne.Domain.Tests/Variables/

Scope:
- Add VariableDefinition with column name, description, unit, role, methodological type and review status.
- Add behavior to update review information.
- Add tests for valid and invalid updates.

Non-goals:
- Do not implement UI.
- Do not implement persistence.
- Do not implement model target selection logic beyond domain representation.

Acceptance criteria:
- VariableDefinition distinguishes inferred from reviewed information.
- Tests cover review state transitions.

Commands to run:
- dotnet build src/Ariadne.Domain/Ariadne.Domain.csproj
- dotnet test tests/Ariadne.Domain.Tests/Ariadne.Domain.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M02-T06 - Implement fundamental analysis and decision log domain models

Type: `domain`  
Priority: Must have  
Depends on: CDX-M02-T01

Goal:

```text
Implement the domain concepts used to document context and methodological decisions.
```

Scope:

```text
- FundamentalAnalysisAnswer.
- Question groups: What, Who, When, Where, How, Why.
- Knowledge status: Known, Unknown, Partial, NotApplicable.
- DecisionLogEntry.
- Decision type, status, rationale, evidence.
- Unit tests.
```

Acceptance criteria:

```text
- Unknown answers are valid if explicitly marked.
- Decisions require a title and rationale.
- Decision log supports traceability without requiring modelling.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M02-T06

Goal:
Implement fundamental analysis and decision log domain models.

Relevant documents:
- 04-domain-model.md
- 05-methodology-workflow.md
- 02-functional-specification.md

Files likely to edit:
- src/Ariadne.Domain/FundamentalAnalysis/
- src/Ariadne.Domain/Decisions/
- tests/Ariadne.Domain.Tests/FundamentalAnalysis/
- tests/Ariadne.Domain.Tests/Decisions/

Scope:
- Add FundamentalAnalysisAnswer with question group, prompt, answer, knowledge status and notes.
- Add DecisionLogEntry with type, status, title, rationale and optional evidence.
- Add tests for valid/invalid states.

Non-goals:
- Do not implement UI.
- Do not implement report generation.
- Do not implement automated recommendations.

Acceptance criteria:
- Fundamental answers allow explicit unknowns.
- Decisions require clear rationale.
- Tests pass.

Commands to run:
- dotnet build src/Ariadne.Domain/Ariadne.Domain.csproj
- dotnet test tests/Ariadne.Domain.Tests/Ariadne.Domain.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M02-T07 - Add application result and use-case conventions

Type: `application`  
Priority: Must have  
Depends on: CDX-M02-T01

Goal:

```text
Create the application-layer conventions for use cases, commands, queries and results.
```

Scope:

```text
- Result<T> or equivalent error pattern.
- Application error types.
- Command/query request and response conventions.
- CancellationToken usage pattern.
- Unit tests if behavior exists.
```

Acceptance criteria:

```text
- Use cases can return success or failure without throwing for expected errors.
- No infrastructure dependencies are introduced.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M02-T07

Goal:
Add application-layer result and use-case conventions.

Relevant documents:
- 03-technical-architecture.md
- 04-domain-model.md

Files likely to edit:
- src/Ariadne.Application/Common/
- tests/Ariadne.Application.Tests/Common/

Scope:
- Add a simple Result or Result<T> type.
- Add application error representation.
- Add conventions for use-case request/response classes if helpful.
- Add tests for result behavior.

Non-goals:
- Do not add MediatR unless explicitly justified and approved.
- Do not add infrastructure.
- Do not add UI.

Acceptance criteria:
- Application project depends only on Domain.
- Expected validation failures can be represented without exceptions.
- Tests pass.

Commands to run:
- dotnet build src/Ariadne.Application/Ariadne.Application.csproj
- dotnet test tests/Ariadne.Application.Tests/Ariadne.Application.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

---

## 16. M03 - Shared UI and MAUI shell tasks

### CDX-M03-T01 - Create reusable SharedUi shell components

Type: `ui`  
Priority: Must have  
Depends on: M01

Goal:

```text
Create the shared Blazor UI shell that can later be reused by a web host.
```

Scope:

```text
- Shared layout.
- Navigation rail placeholder.
- Top bar.
- Welcome page.
- Basic design tokens/CSS.
```

Acceptance criteria:

```text
- SharedUi compiles independently of MAUI.
- No MAUI-specific APIs are used in SharedUi.
- Welcome page presents Ariadne clearly.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M03-T01

Goal:
Create reusable SharedUi shell components.

Relevant documents:
- 07-ui-ux-guidelines.md
- 03-technical-architecture.md
- 01-product-vision.md

Files likely to edit:
- src/Ariadne.SharedUi/

Scope:
- Add a reusable app layout in the Razor Class Library.
- Add a welcome page with product name, one-liner and early-stage callout.
- Add a navigation rail placeholder for Project, Dataset, Understand, Report.
- Add CSS isolation or shared stylesheet according to Blazor conventions.

Non-goals:
- Do not implement project persistence.
- Do not add dataset import.
- Do not use MAUI APIs inside SharedUi.

Acceptance criteria:
- SharedUi builds.
- Components are reusable by a future web host.
- UI text does not overclaim unavailable features.

Commands to run:
- dotnet build src/Ariadne.SharedUi/Ariadne.SharedUi.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M03-T02 - Wire MAUI Blazor host to SharedUi

Type: `maui`  
Priority: Must have  
Depends on: CDX-M03-T01

Goal:

```text
Host the SharedUi shell inside the MAUI Blazor Hybrid app.
```

Scope:

```text
- Configure BlazorWebView.
- Register basic services.
- Render SharedUi welcome shell.
- Keep MAUI project as host only.
```

Acceptance criteria:

```text
- MAUI host displays SharedUi content.
- No business logic is added to MAUI.
- Full build is attempted; workload limitations are reported.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M03-T02

Goal:
Wire the MAUI Blazor Hybrid host to render SharedUi.

Relevant documents:
- 03-technical-architecture.md
- 07-ui-ux-guidelines.md

Files likely to edit:
- src/Ariadne.Maui/
- src/Ariadne.SharedUi/ if a host integration component is needed

Scope:
- Configure the MAUI BlazorWebView to render the SharedUi root component.
- Register minimal DI required for the UI shell.
- Keep MAUI as a host and composition root only.

Non-goals:
- Do not implement project persistence.
- Do not implement platform-specific file picking.
- Do not add analytics.

Acceptance criteria:
- The MAUI app can display the Ariadne welcome screen where workloads are available.
- SharedUi still has no dependency on Maui.
- Any MAUI workload validation issue is reported clearly.

Commands to run:
- dotnet build
- dotnet test

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M03-T03 - Add basic UI component library primitives

Type: `ui`  
Priority: Should have  
Depends on: CDX-M03-T01

Goal:

```text
Add reusable UI primitives for later screens.
```

Scope:

```text
- AriadneCard.
- AriadneButton styles or wrappers.
- StatusBadge.
- MethodologyCallout.
- EmptyState.
- LoadingState.
```

Acceptance criteria:

```text
- Components are simple and generic.
- Components do not call application services directly unless intentionally screen-level.
- Styling follows UI guidelines.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M03-T03

Goal:
Add reusable SharedUi component primitives.

Relevant documents:
- 07-ui-ux-guidelines.md

Files likely to edit:
- src/Ariadne.SharedUi/Components/
- src/Ariadne.SharedUi/Styles/

Scope:
- Add simple reusable components: card, status badge, callout, empty state and loading state.
- Use neutral, methodology-focused copy.
- Keep components host-independent.

Non-goals:
- Do not add charts.
- Do not add data grids.
- Do not add persistence or services.

Acceptance criteria:
- SharedUi builds.
- Components are documented with minimal examples or comments where useful.

Commands to run:
- dotnet build src/Ariadne.SharedUi/Ariadne.SharedUi.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

---

## 17. M04 - Local storage foundation tasks

### CDX-M04-T01 - Add local storage application ports

Type: `application`  
Priority: Must have  
Depends on: M02

Goal:

```text
Define application-layer interfaces for local project and file storage.
```

Scope:

```text
- IProjectCatalog.
- IProjectRepository.
- IDatasetRepository.
- IFileStorage or IProjectFileStore.
- IClock if needed.
- Query models for recent projects.
```

Acceptance criteria:

```text
- Interfaces are in Application.
- Application still has no dependency on Infrastructure.Local.
- Interfaces do not expose SQLite-specific concepts.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M04-T01

Goal:
Add application-layer storage ports for local persistence.

Relevant documents:
- 03-technical-architecture.md
- 06-local-first-storage.md
- 04-domain-model.md

Files likely to edit:
- src/Ariadne.Application/Projects/
- src/Ariadne.Application/Datasets/
- tests/Ariadne.Application.Tests/

Scope:
- Define repository and file storage interfaces needed for projects and datasets.
- Add simple query models for recent/open projects.
- Keep interfaces persistence-neutral.

Non-goals:
- Do not implement SQLite.
- Do not implement file copying.
- Do not implement UI.

Acceptance criteria:
- Application depends only on Domain.
- Interfaces avoid SQLite-specific terms.
- Tests compile.

Commands to run:
- dotnet build src/Ariadne.Application/Ariadne.Application.csproj
- dotnet test tests/Ariadne.Application.Tests/Ariadne.Application.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M04-T02 - Add SQLite infrastructure skeleton

Type: `infrastructure`  
Priority: Must have  
Depends on: CDX-M04-T01

Goal:

```text
Create the EF Core SQLite persistence skeleton without implementing every table.
```

Scope:

```text
- Add EF Core SQLite packages to Infrastructure.Local only.
- Create catalog DbContext.
- Create project DbContext skeleton.
- Add design notes for migrations if needed.
- Add basic configuration.
```

Acceptance criteria:

```text
- EF Core references stay in Infrastructure.Local.
- Domain is not polluted with EF attributes.
- No raw dataset rows are stored in SQLite for MVP.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M04-T02

Goal:
Add the local SQLite infrastructure skeleton using EF Core.

Relevant documents:
- 06-local-first-storage.md
- 03-technical-architecture.md

Files likely to edit:
- src/Ariadne.Infrastructure.Local/
- tests/Ariadne.Infrastructure.Local.Tests/

Scope:
- Add EF Core SQLite packages only to Infrastructure.Local.
- Add catalog and project DbContext skeletons.
- Add basic configuration classes if needed.
- Add minimal tests that contexts can be created with SQLite in-memory or temp files.

Non-goals:
- Do not implement all repositories yet.
- Do not store raw dataset rows in SQLite.
- Do not add PostgreSQL.

Acceptance criteria:
- Infrastructure.Local compiles.
- Domain has no EF Core dependency.
- A basic context creation test passes.

Commands to run:
- dotnet build src/Ariadne.Infrastructure.Local/Ariadne.Infrastructure.Local.csproj
- dotnet test tests/Ariadne.Infrastructure.Local.Tests/Ariadne.Infrastructure.Local.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M04-T03 - Implement project workspace file service

Type: `infrastructure`  
Priority: Must have  
Depends on: CDX-M04-T01

Goal:

```text
Implement local project folder creation and file path management.
```

Scope:

```text
- Managed workspace root abstraction.
- Portable project folder support if simple.
- Safe folder name generation.
- Project manifest writing.
- Temp/staging folder handling.
```

Acceptance criteria:

```text
- Project folders are created deterministically.
- Invalid project names do not create unsafe paths.
- File service is behind an application interface.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M04-T03

Goal:
Implement a local project workspace file service.

Relevant documents:
- 06-local-first-storage.md
- 03-technical-architecture.md

Files likely to edit:
- src/Ariadne.Application/Storage/
- src/Ariadne.Infrastructure.Local/Storage/
- tests/Ariadne.Infrastructure.Local.Tests/Storage/

Scope:
- Implement project folder creation using a configurable root path.
- Generate safe folder names.
- Create the documented folders: data/raw, data/preview, reports, exports, logs if applicable.
- Add tests using temporary directories.

Non-goals:
- Do not use MAUI FileSystem helpers inside Application.
- Do not implement dataset import yet.
- Do not add cloud sync.

Acceptance criteria:
- Tests verify safe folder creation.
- No user data is written outside the configured root.
- Infrastructure.Local implements Application interfaces.

Commands to run:
- dotnet build src/Ariadne.Infrastructure.Local/Ariadne.Infrastructure.Local.csproj
- dotnet test tests/Ariadne.Infrastructure.Local.Tests/Ariadne.Infrastructure.Local.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

---

## 18. M05 - Project management tasks

### CDX-M05-T01 - Add create project use case

Type: `application`  
Priority: Must have  
Depends on: M02, M04

Goal:

```text
Create an application use case for creating a local Ariadne project.
```

Scope:

```text
- CreateProjectCommand.
- CreateProjectResult.
- Use case creates AiProject.
- Use case persists project through ports.
- Use case creates workspace through port.
- Tests with fake repositories.
```

Acceptance criteria:

```text
- Invalid project names return failure.
- Successful creation creates project metadata and initial workflow.
- No UI or SQLite-specific logic in the use case.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M05-T01

Goal:
Implement the Create Project application use case.

Relevant documents:
- 02-functional-specification.md
- 04-domain-model.md
- 06-local-first-storage.md

Files likely to edit:
- src/Ariadne.Application/Projects/
- tests/Ariadne.Application.Tests/Projects/

Scope:
- Add CreateProjectCommand and result model.
- Implement CreateProjectUseCase using application storage ports.
- Add tests with in-memory fakes.

Non-goals:
- Do not implement the Blazor page.
- Do not implement SQLite repositories in this task unless needed by existing ports.
- Do not add authentication.

Acceptance criteria:
- Use case handles success and validation failure.
- Tests cover invalid and valid creation.
- Application has no Infrastructure.Local dependency.

Commands to run:
- dotnet build src/Ariadne.Application/Ariadne.Application.csproj
- dotnet test tests/Ariadne.Application.Tests/Ariadne.Application.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M05-T02 - Implement local project repository

Type: `infrastructure`  
Priority: Must have  
Depends on: CDX-M05-T01

Goal:

```text
Persist and retrieve projects locally.
```

Scope:

```text
- Implement project repository using SQLite contexts.
- Map AiProject to persistence model without EF attributes in Domain.
- Save and retrieve project metadata.
- Add integration tests.
```

Acceptance criteria:

```text
- Project can be saved and loaded.
- Domain remains persistence-neutral.
- Tests use temp storage.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M05-T02

Goal:
Implement local project persistence for AiProject.

Relevant documents:
- 06-local-first-storage.md
- 04-domain-model.md
- 03-technical-architecture.md

Files likely to edit:
- src/Ariadne.Infrastructure.Local/Projects/
- tests/Ariadne.Infrastructure.Local.Tests/Projects/

Scope:
- Implement the project repository interface.
- Map AiProject without adding EF Core attributes to Domain.
- Add integration tests using temporary SQLite files.

Non-goals:
- Do not implement dataset repositories.
- Do not implement UI.
- Do not add migrations beyond what is needed for the testable skeleton.

Acceptance criteria:
- Project save/load works in tests.
- Domain has no EF dependency.
- Tests pass.

Commands to run:
- dotnet build src/Ariadne.Infrastructure.Local/Ariadne.Infrastructure.Local.csproj
- dotnet test tests/Ariadne.Infrastructure.Local.Tests/Ariadne.Infrastructure.Local.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M05-T03 - Add project creation UI

Type: `ui`  
Priority: Must have  
Depends on: CDX-M05-T01, CDX-M03-T01

Goal:

```text
Add a SharedUi screen for creating a project.
```

Scope:

```text
- Create project form.
- Fields: name, description, optional tags if already supported.
- Calls CreateProjectUseCase or an application service.
- Shows validation errors.
- Shows success state.
```

Acceptance criteria:

```text
- No domain logic in Razor.
- Form uses application use case.
- Empty project name is handled gracefully.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M05-T03

Goal:
Add the SharedUi create project screen.

Relevant documents:
- 02-functional-specification.md
- 07-ui-ux-guidelines.md
- 03-technical-architecture.md

Files likely to edit:
- src/Ariadne.SharedUi/Pages/Projects/
- src/Ariadne.SharedUi/Components/

Scope:
- Add a create project page/form.
- Bind to the application use case or a thin application service abstraction.
- Display validation errors and success feedback.

Non-goals:
- Do not implement dataset import.
- Do not put domain validation in Razor.
- Do not use MAUI APIs in SharedUi.

Acceptance criteria:
- SharedUi builds.
- Form validation errors are user-friendly.
- No business logic is implemented in the component.

Commands to run:
- dotnet build src/Ariadne.SharedUi/Ariadne.SharedUi.csproj
- dotnet test

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M05-T04 - Add recent projects view

Type: `ui`  
Priority: Should have  
Depends on: CDX-M05-T02

Goal:

```text
Show locally known projects on the welcome screen.
```

Scope:

```text
- Query recent projects through application port/service.
- Display empty state.
- Display project cards/list.
- Provide open project action placeholder or implementation if use case exists.
```

Acceptance criteria:

```text
- User sees existing local projects.
- Empty state explains how to create a project.
- No SQLite access in UI.
```

---

## 19. M06 - Dataset import and preview tasks

### CDX-M06-T01 - Add CSV import application contracts

Type: `application`  
Priority: Must have  
Depends on: M05

Goal:

```text
Define application contracts for importing CSV datasets.
```

Scope:

```text
- ImportDatasetCommand.
- Import options: delimiter, header row, encoding if supported.
- Dataset import result.
- Dataset preview model.
- CSV reader port.
- File copy/storage port if not already present.
```

Acceptance criteria:

```text
- Contracts are format-aware but not tied to a specific CSV library.
- Import result includes dataset ID/version ID and preview metadata.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M06-T01

Goal:
Add application contracts for CSV dataset import and preview.

Relevant documents:
- 02-functional-specification.md
- 03-technical-architecture.md
- 06-local-first-storage.md

Files likely to edit:
- src/Ariadne.Application/Datasets/
- tests/Ariadne.Application.Tests/Datasets/

Scope:
- Add import command/result models.
- Add import options for CSV delimiter and header handling.
- Add dataset preview models.
- Add ports for reading tabular files if needed.

Non-goals:
- Do not implement CSV parsing yet.
- Do not implement UI.
- Do not support Excel or Parquet.

Acceptance criteria:
- Application remains infrastructure-neutral.
- Contracts can represent import failure and preview data.
- Tests compile.

Commands to run:
- dotnet build src/Ariadne.Application/Ariadne.Application.csproj
- dotnet test tests/Ariadne.Application.Tests/Ariadne.Application.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M06-T02 - Implement CSV reader adapter

Type: `infrastructure`  
Priority: Must have  
Depends on: CDX-M06-T01

Goal:

```text
Implement local CSV reading for the MVP.
```

Scope:

```text
- Choose a conservative CSV parsing approach or package.
- Read header and rows.
- Support delimiter option.
- Return preview rows with string values.
- Handle empty files and inconsistent rows gracefully.
- Add tests with sample CSV files.
```

Acceptance criteria:

```text
- CSV reader does not infer ML types.
- CSV reader preserves raw string values for later profiling.
- Errors are returned clearly.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M06-T02

Goal:
Implement the local CSV reader adapter.

Relevant documents:
- 02-functional-specification.md
- 03-technical-architecture.md

Files likely to edit:
- src/Ariadne.Infrastructure.Local/Datasets/
- tests/Ariadne.Infrastructure.Local.Tests/Datasets/

Scope:
- Implement the CSV reader port.
- Read headers and rows as strings.
- Support delimiter option.
- Add tests for standard CSV, empty file and inconsistent row handling.

Non-goals:
- Do not support Excel.
- Do not support Parquet.
- Do not infer column types in this task.

Acceptance criteria:
- CSV reader tests pass.
- Reader reports parse issues clearly.
- No raw file contents are logged.

Commands to run:
- dotnet build src/Ariadne.Infrastructure.Local/Ariadne.Infrastructure.Local.csproj
- dotnet test tests/Ariadne.Infrastructure.Local.Tests/Ariadne.Infrastructure.Local.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M06-T03 - Implement import dataset use case

Type: `application`  
Priority: Must have  
Depends on: CDX-M06-T01, CDX-M06-T02, M04

Goal:

```text
Implement the use case that imports a CSV into a project and creates a dataset version.
```

Scope:

```text
- Validate project exists.
- Copy/store raw file through storage port.
- Compute or request content hash through port.
- Create Dataset and DatasetVersion.
- Generate preview model.
- Persist metadata.
- Add tests with fake ports.
```

Acceptance criteria:

```text
- Raw imported file is treated as immutable.
- Import creates a dataset version.
- Preview is limited.
- Failures do not leave inconsistent metadata.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M06-T03

Goal:
Implement the Import Dataset application use case.

Relevant documents:
- 02-functional-specification.md
- 04-domain-model.md
- 06-local-first-storage.md

Files likely to edit:
- src/Ariadne.Application/Datasets/
- tests/Ariadne.Application.Tests/Datasets/

Scope:
- Implement import orchestration using application ports.
- Create Dataset and DatasetVersion metadata.
- Return a limited preview.
- Add tests with fake CSV reader and storage ports.

Non-goals:
- Do not implement UI.
- Do not implement profiling yet.
- Do not store raw dataset rows in SQLite.

Acceptance criteria:
- Import success creates metadata and preview.
- Import failure is reported with no partial success ambiguity.
- Tests cover success and common failures.

Commands to run:
- dotnet build src/Ariadne.Application/Ariadne.Application.csproj
- dotnet test tests/Ariadne.Application.Tests/Ariadne.Application.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M06-T04 - Add dataset import UI

Type: `ui`  
Priority: Must have  
Depends on: CDX-M06-T03

Goal:

```text
Add the dataset import screen in SharedUi, with MAUI file picking injected through an abstraction.
```

Scope:

```text
- Import dataset page.
- File selection trigger through application/platform service abstraction.
- CSV options.
- Preview before confirmation if implemented.
- Error and success states.
```

Architecture rule:

```text
SharedUi must not call MAUI file picker APIs directly.
```

Acceptance criteria:

```text
- UI uses injected service for file selection/import.
- SharedUi remains reusable for future web host.
- Import errors are readable.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M06-T04

Goal:
Add the dataset import UI while keeping SharedUi host-independent.

Relevant documents:
- 02-functional-specification.md
- 07-ui-ux-guidelines.md
- 03-technical-architecture.md

Files likely to edit:
- src/Ariadne.SharedUi/Pages/Datasets/
- src/Ariadne.Maui/ if platform file picker composition is needed
- src/Ariadne.Application/Datasets/ if a UI-facing service abstraction is needed

Scope:
- Add an import dataset page.
- Use an injected abstraction for file selection/import.
- Show CSV options and preview/error/success states.

Non-goals:
- Do not call MAUI APIs directly from SharedUi.
- Do not implement profiling.
- Do not support Excel/Parquet.

Acceptance criteria:
- SharedUi builds.
- MAUI remains a host/composition root.
- Import flow can be manually smoke-tested where MAUI workloads are available.

Commands to run:
- dotnet build
- dotnet test

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M06-T05 - Add dataset preview grid

Type: `ui`  
Priority: Must have  
Depends on: CDX-M06-T03

Goal:

```text
Display imported dataset preview rows safely and clearly.
```

Scope:

```text
- Preview grid component.
- Row/column limits.
- Empty state.
- Sensitive data caution text.
- Horizontal scrolling.
```

Acceptance criteria:

```text
- Preview is visibly limited.
- Large datasets do not render entirely.
- Values are displayed as preview strings, not inferred truth.
```

---

## 20. M07 - Dataset profiling tasks

### CDX-M07-T01 - Implement primitive type inference

Type: `analytics`  
Priority: Must have  
Depends on: M06

Goal:

```text
Infer primitive column types from imported string values.
```

Scope:

```text
- Boolean, integer, decimal, date/time, text detection.
- Culture-aware decisions documented.
- Confidence/ambiguity representation if supported.
- Tests with edge cases.
```

Acceptance criteria:

```text
- Inference is conservative.
- Ambiguous columns are not overclaimed.
- Raw values are not modified.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M07-T01

Goal:
Implement primitive type inference for dataset profiling.

Relevant documents:
- 02-functional-specification.md
- 04-domain-model.md
- 05-methodology-workflow.md

Files likely to edit:
- src/Ariadne.Analytics/Profiling/
- tests/Ariadne.Analytics.Tests/Profiling/

Scope:
- Add primitive type inference from string values.
- Support integer, decimal, boolean, date/time and text as MVP categories.
- Use conservative inference rules.
- Add tests for edge cases and ambiguous columns.

Non-goals:
- Do not implement methodological type review UI.
- Do not modify raw data.
- Do not implement charts.

Acceptance criteria:
- Inference tests pass.
- Ambiguous columns are handled conservatively.
- Analytics does not depend on Infrastructure.Local.

Commands to run:
- dotnet build src/Ariadne.Analytics/Ariadne.Analytics.csproj
- dotnet test tests/Ariadne.Analytics.Tests/Ariadne.Analytics.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M07-T02 - Implement numeric statistics profiler

Type: `analytics`  
Priority: Must have  
Depends on: CDX-M07-T01

Goal:

```text
Compute basic numeric statistics for continuous/numeric columns.
```

Scope:

```text
- Count, missing count.
- Min, max.
- Mean, median.
- Variance or standard deviation.
- Q1, Q3, IQR.
- IQR outlier candidate count.
- Tests.
```

Acceptance criteria:

```text
- Empty or all-missing columns are handled.
- Calculations are deterministic.
- Outliers are called candidates, not definitive errors.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M07-T02

Goal:
Implement numeric column profiling.

Relevant documents:
- 02-functional-specification.md
- 04-domain-model.md
- 05-methodology-workflow.md

Files likely to edit:
- src/Ariadne.Analytics/Profiling/
- tests/Ariadne.Analytics.Tests/Profiling/

Scope:
- Compute numeric statistics: count, missing count, min, max, mean, median, standard deviation, Q1, Q3, IQR.
- Compute IQR outlier candidate count.
- Add tests for normal, empty, all-missing and small sample inputs.

Non-goals:
- Do not remove outliers.
- Do not implement LOF or Isolation Forest.
- Do not create charts.

Acceptance criteria:
- Numeric statistics are correct for deterministic test data.
- Outliers are labelled as candidates.
- Tests pass.

Commands to run:
- dotnet build src/Ariadne.Analytics/Ariadne.Analytics.csproj
- dotnet test tests/Ariadne.Analytics.Tests/Ariadne.Analytics.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M07-T03 - Implement discrete/text profiler

Type: `analytics`  
Priority: Must have  
Depends on: CDX-M07-T01

Goal:

```text
Compute counts and value summaries for discrete/text columns.
```

Scope:

```text
- Unique count.
- Missing count.
- Top value counts.
- Cardinality ratio.
- Suggested methodological type: discrete/text candidate.
- Tests.
```

Acceptance criteria:

```text
- High-cardinality text is not automatically treated as categorical.
- Top values are capped.
- Missing values are counted consistently.
```

### CDX-M07-T04 - Implement dataset profile use case

Type: `application`  
Priority: Must have  
Depends on: CDX-M07-T01, CDX-M07-T02, CDX-M07-T03

Goal:

```text
Orchestrate profiling of an imported dataset version and persist the resulting profile.
```

Scope:

```text
- ProfileDatasetUseCase.
- Reads preview/sample or full file through ports.
- Calls Analytics profiler.
- Persists DatasetProfile.
- Emits decision candidates if supported.
- Tests with fakes.
```

Acceptance criteria:

```text
- Use case does not compute stats itself.
- Analytics does calculations.
- Profile is linked to dataset version.
```

### CDX-M07-T05 - Add profile UI screen

Type: `ui`  
Priority: Must have  
Depends on: CDX-M07-T04

Goal:

```text
Display dataset and column profiles in SharedUi.
```

Scope:

```text
- Dataset profile summary cards.
- Column profile table.
- Methodological type inference badge.
- Missing value indicators.
- Outlier candidate indicators.
- Empty/loading/error states.
```

Acceptance criteria:

```text
- UI labels automatic results as inferred/candidate.
- User is encouraged to review variables.
- No charts required for MVP.
```

---

## 21. M08 - Variable dictionary tasks

### CDX-M08-T01 - Add initialize variable dictionary use case

Type: `application`  
Priority: Must have  
Depends on: M07

Goal:

```text
Create variable definitions from column profiles for user review.
```

Scope:

```text
- InitializeVariableDictionaryUseCase.
- One variable definition per column.
- Copy inferred information as suggestion only.
- Tests.
```

Acceptance criteria:

```text
- User-reviewed fields start as unreviewed.
- Inferred methodological type remains separate.
```

### CDX-M08-T02 - Add update variable definition use case

Type: `application`  
Priority: Must have  
Depends on: CDX-M08-T01

Goal:

```text
Allow the user to review and update variable meaning.
```

Scope:

```text
- Update description.
- Update unit.
- Update variable role.
- Update reviewed methodological type.
- Mark as reviewed/unknown.
- Tests.
```

Acceptance criteria:

```text
- Invalid column names cannot be updated.
- Review status changes are explicit.
```

### CDX-M08-T03 - Add variable dictionary UI

Type: `ui`  
Priority: Must have  
Depends on: CDX-M08-T02

Goal:

```text
Add the screen where the user reviews variables.
```

Scope:

```text
- Table/list of variables.
- Editable detail panel or form.
- Fields: description, unit, role, reviewed type, notes.
- Review status badges.
```

Acceptance criteria:

```text
- UI makes inferred vs reviewed visible.
- User can mark unknown rather than invent meaning.
- No modelling features are added.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M08-T03

Goal:
Add the variable dictionary UI for reviewing column meaning.

Relevant documents:
- 02-functional-specification.md
- 04-domain-model.md
- 07-ui-ux-guidelines.md

Files likely to edit:
- src/Ariadne.SharedUi/Pages/Variables/
- src/Ariadne.SharedUi/Components/
- src/Ariadne.Application/Variables/ if UI query models are missing

Scope:
- Add a variable dictionary screen.
- Show one row per variable/column.
- Allow editing description, unit, role, methodological type and notes.
- Display inferred vs reviewed state clearly.

Non-goals:
- Do not implement model target training.
- Do not add charts.
- Do not call persistence directly from UI.

Acceptance criteria:
- SharedUi builds.
- UI makes unknown/reviewed states visible.
- No business logic is placed in Razor components.

Commands to run:
- dotnet build src/Ariadne.SharedUi/Ariadne.SharedUi.csproj
- dotnet test

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

---

## 22. M09 - Fundamental analysis tasks

### CDX-M09-T01 - Add fundamental questionnaire definitions

Type: `application`  
Priority: Must have  
Depends on: M02

Goal:

```text
Define the MVP fundamental analysis questionnaire around What, Who, When, Where, How and Why.
```

Scope:

```text
- Question definitions.
- Question group enum mapping.
- Recommended prompts.
- Completion rules.
- Tests for definition completeness.
```

Acceptance criteria:

```text
- Six question groups are represented.
- Unknown answers are allowed.
- Questions are methodology-focused, not generic survey filler.
```

### CDX-M09-T02 - Add save fundamental answers use case

Type: `application`  
Priority: Must have  
Depends on: CDX-M09-T01

Goal:

```text
Allow the user to save contextual answers and knowledge statuses.
```

Scope:

```text
- SaveFundamentalAnalysisAnswerUseCase.
- GetFundamentalAnalysisUseCase.
- Completion calculation.
- Tests.
```

Acceptance criteria:

```text
- User can answer, mark partial, unknown or not applicable.
- Completion does not require pretending all context is known.
```

### CDX-M09-T03 - Add fundamental analysis UI

Type: `ui`  
Priority: Must have  
Depends on: CDX-M09-T02

Goal:

```text
Add guided UI for documenting data context.
```

Scope:

```text
- What/Who/When/Where/How/Why sections.
- Long-form text inputs.
- Knowledge status selector.
- Contextual help text.
- Completion indicator.
```

Acceptance criteria:

```text
- UI encourages honest unknowns.
- Answers can be saved and edited.
- Report can later consume answers.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M09-T03

Goal:
Add the fundamental analysis UI.

Relevant documents:
- 02-functional-specification.md
- 05-methodology-workflow.md
- 07-ui-ux-guidelines.md

Files likely to edit:
- src/Ariadne.SharedUi/Pages/FundamentalAnalysis/
- src/Ariadne.SharedUi/Components/

Scope:
- Add a guided screen with What, Who, When, Where, How and Why sections.
- Allow free-text answers and knowledge status selection.
- Display completion without implying scientific validity.

Non-goals:
- Do not implement hypothesis testing.
- Do not generate reports in this task.
- Do not use external services to fill answers.

Acceptance criteria:
- SharedUi builds.
- Unknown/partial answers are supported.
- No external lookup is added.

Commands to run:
- dotnet build src/Ariadne.SharedUi/Ariadne.SharedUi.csproj
- dotnet test

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

---

## 23. M10 - Decision log tasks

### CDX-M10-T01 - Add decision log use cases

Type: `application`  
Priority: Must have  
Depends on: M02, M04

Goal:

```text
Allow users to create, edit, list and resolve methodological decisions.
```

Scope:

```text
- CreateDecisionLogEntryUseCase.
- UpdateDecisionLogEntryUseCase.
- ListDecisionLogEntriesUseCase.
- Resolve or supersede decision if in domain model.
- Tests.
```

Acceptance criteria:

```text
- Decision entries require rationale.
- Entries are timestamped.
- Entries can be linked to project/dataset/variable where supported.
```

### CDX-M10-T02 - Add decision log UI

Type: `ui`  
Priority: Must have  
Depends on: CDX-M10-T01

Goal:

```text
Display and edit the decision log.
```

Scope:

```text
- Decision list.
- Create/edit form.
- Decision type/status filters.
- Empty state.
```

Acceptance criteria:

```text
- User can see why decisions were made.
- UI encourages rationale, not just a title.
```

### CDX-M10-T03 - Add automatic decision candidates from profiling

Type: `application`  
Priority: Could have for MVP  
Depends on: M07, M10

Goal:

```text
Create optional suggested decision entries from profiling findings.
```

Examples:

```text
- Column has high missing value ratio.
- Column inferred as high-cardinality text.
- Numeric column has outlier candidates.
```

Acceptance criteria:

```text
- Candidates are not final decisions.
- User must accept/edit/ignore them.
- Wording avoids overclaiming.
```

---

## 24. M11 - Methodology progress tasks

### CDX-M11-T01 - Add workflow progress calculation

Type: `application`  
Priority: Must have  
Depends on: M05-M10

Goal:

```text
Calculate the methodology progress status for MVP stages.
```

MVP stages:

```text
Project
Dataset
Understand
Report
```

Future stages displayed as locked or not started:

```text
Analyze
Hypothesize
Prepare
Model
Evaluate
```

Acceptance criteria:

```text
- Progress is derived from project state where possible.
- Completion means documented, not scientifically proven.
- Future stages do not become active prematurely.
```

### CDX-M11-T02 - Add workflow rail UI

Type: `ui`  
Priority: Must have  
Depends on: CDX-M11-T01

Goal:

```text
Show methodology progress in the app shell.
```

Scope:

```text
- Workflow rail component.
- Status badges.
- Current stage highlight.
- Locked/deferred future stages.
```

Acceptance criteria:

```text
- User sees where they are in the method.
- UI does not imply that a stage is valid only because it is complete.
```

---

## 25. M12 - Markdown report generation tasks

### CDX-M12-T01 - Add report model and generator contracts

Type: `application`  
Priority: Must have  
Depends on: M05-M10

Goal:

```text
Define report generation contracts and report section models.
```

Scope:

```text
- GenerateReportCommand.
- ReportSection model.
- ReportGenerationResult.
- IReportWriter or IMarkdownReportGenerator port.
```

Acceptance criteria:

```text
- Report structure is deterministic.
- Report can include missing/unknown information.
- No PDF export yet.
```

### CDX-M12-T02 - Implement Markdown report generator

Type: `reporting`  
Priority: Must have  
Depends on: CDX-M12-T01

Goal:

```text
Generate a Markdown report from project, dataset, variable, fundamental analysis and decision log data.
```

Required report sections:

```text
- Project summary.
- Dataset summary.
- Dataset profile summary.
- Variable dictionary summary.
- Fundamental analysis.
- Decision log.
- Known limitations.
- Next recommended methodology steps.
```

Acceptance criteria:

```text
- Report does not hide unknowns.
- Report labels inferred values as inferred.
- Report avoids claiming model performance because no model exists yet.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M12-T02

Goal:
Implement the MVP Markdown report generator.

Relevant documents:
- 02-functional-specification.md
- 05-methodology-workflow.md
- 06-local-first-storage.md
- 07-ui-ux-guidelines.md

Files likely to edit:
- src/Ariadne.Application/Reports/
- src/Ariadne.Infrastructure.Local/Reports/ if file writing is needed
- tests/Ariadne.Application.Tests/Reports/

Scope:
- Generate Markdown with project summary, dataset summary, profile summary, variable dictionary, fundamental analysis, decision log and limitations.
- Clearly mark unknown and inferred information.
- Add tests for generated Markdown sections.

Non-goals:
- Do not implement PDF export.
- Do not implement model evaluation report sections beyond placeholders.
- Do not call external services.

Acceptance criteria:
- Markdown output is deterministic.
- Tests verify required sections exist.
- No model performance claims are generated.

Commands to run:
- dotnet build src/Ariadne.Application/Ariadne.Application.csproj
- dotnet test tests/Ariadne.Application.Tests/Ariadne.Application.Tests.csproj

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M12-T03 - Add report preview and export UI

Type: `ui`  
Priority: Must have  
Depends on: CDX-M12-T02

Goal:

```text
Allow the user to preview and export the Markdown report.
```

Scope:

```text
- Report preview screen.
- Generate button.
- Save/export action through injected file service.
- Copy to clipboard if easy and host-safe.
```

Acceptance criteria:

```text
- Export writes a Markdown file locally.
- SharedUi remains host-independent.
- Report preview clearly says this is an MVP methodology report.
```

---

## 26. M13 - MVP hardening tasks

### CDX-M13-T01 - MVP architecture audit

Type: `hardening`  
Priority: Must have  
Depends on: M01-M12

Goal:

```text
Audit the repository for architecture violations before v0.1.
```

Checklist:

```text
- Domain has no infrastructure dependency.
- SharedUi has no MAUI dependency.
- Application has no infrastructure dependency.
- No web project exists.
- No cloud dependency exists.
- No ML/LLM dependency exists.
- No raw dataset files are tracked by git.
```

Acceptance criteria:

```text
- Violations are fixed if small.
- Larger issues are documented as release blockers.
```

Prompt:

```text
You are working on Ariadne AI Workbench.

Task ID:
CDX-M13-T01

Goal:
Perform an MVP architecture audit and fix small violations.

Relevant documents:
- 03-technical-architecture.md
- 08-development-roadmap.md
- 09-codex-task-breakdown.md

Scope:
- Check project references.
- Check package references.
- Check that Domain/Application/SharedUi boundaries are respected.
- Fix small violations only.
- Report larger issues as blockers.

Non-goals:
- Do not refactor the whole app.
- Do not add new features.
- Do not add web/cloud/ML/LLM dependencies.

Acceptance criteria:
- Architecture violations are fixed or documented.
- dotnet build/test are run as far as possible.

Commands to run:
- dotnet build
- dotnet test

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

### CDX-M13-T02 - MVP privacy and local-first audit

Type: `hardening`  
Priority: Must have  
Depends on: M06-M12

Goal:

```text
Ensure local-first and privacy expectations are met.
```

Checklist:

```text
- No telemetry.
- No external network calls.
- No raw dataset logging.
- Imported files stored locally.
- Local project data ignored by git.
- Report export path is explicit.
```

Acceptance criteria:

```text
- Any privacy risks are fixed or documented.
- README mentions local-first behavior honestly.
```

### CDX-M13-T03 - MVP UX copy audit

Type: `hardening`  
Priority: Should have  
Depends on: M03-M12

Goal:

```text
Review UI copy for clarity, humility and methodology consistency.
```

Checklist:

```text
- Inferred data is labelled inferred.
- Outliers are labelled candidates.
- Unknown context is acceptable.
- No screen claims that the app validates models in MVP.
- Empty states guide the next methodological step.
```

### CDX-M13-T04 - MVP release checklist

Type: `hardening`  
Priority: Must have  
Depends on: M13 audits

Goal:

```text
Prepare the repository for a v0.1 pre-release.
```

Scope:

```text
- README status update.
- CHANGELOG initial entry.
- Known limitations section.
- Manual smoke test checklist.
- Confirm license decision remains documented if unresolved.
```

Acceptance criteria:

```text
- Project is ready for review or private dogfooding.
- Limitations are explicit.
- No unsupported platform claims are made.
```

---

# Post-MVP task breakdown

The following tasks are intentionally less detailed because they should only be started after MVP v0.1 is stable and useful.

---

## 27. M20 - Technical analysis release `v0.2`

### CDX-M20-T01 - Add univariate analysis models

Type: `analytics`  
Priority: Future

Goal:

```text
Represent and compute univariate analysis outputs for discrete and continuous variables.
```

Scope:

```text
- Discrete: value counts, frequency ratios.
- Continuous: histogram bins, boxplot summary.
- No hypothesis testing yet.
```

### CDX-M20-T02 - Add univariate analysis UI

Type: `ui`  
Priority: Future

Goal:

```text
Display univariate analysis per variable.
```

Scope:

```text
- Variable selector.
- Summary stats.
- Basic chart placeholders or simple charts if charting approach is approved.
```

### CDX-M20-T03 - Add multivariate analysis models

Type: `analytics`  
Priority: Future

Goal:

```text
Compute basic multivariate summaries.
```

Scope:

```text
- Discrete-discrete: contingency table.
- Discrete-continuous: grouped numeric stats.
- Continuous-continuous: correlation candidate and scatter data.
```

### CDX-M20-T04 - Add multivariate analysis UI

Type: `ui`  
Priority: Future

Goal:

```text
Display relationships between pairs of variables.
```

Non-goal:

```text
Do not implement statistical hypothesis testing in M20.
```

---

## 28. M30 - Hypothesis tracking release `v0.3`

### CDX-M30-T01 - Add hypothesis domain models

Type: `domain`  
Priority: Future

Goal:

```text
Represent H0/H1, source, status, linked variables and confirmation-bias safeguards.
```

### CDX-M30-T02 - Add hypothesis use cases

Type: `application`  
Priority: Future

Goal:

```text
Create, update, list and archive hypotheses.
```

### CDX-M30-T03 - Add hypothesis UI

Type: `ui`  
Priority: Future

Goal:

```text
Allow users to formulate hypotheses explicitly before testing.
```

### CDX-M30-T04 - Add observation/confirmation split guidance

Type: `application`  
Priority: Future

Goal:

```text
Help users avoid testing hypotheses on the same data that suggested them.
```

---

## 29. M31 - Statistical test planning release `v0.3.x`

### CDX-M31-T01 - Add statistical test selection guide

Type: `application`  
Priority: Future

Goal:

```text
Recommend candidate tests from variable types and hypothesis structure.
```

Supported planning candidates:

```text
- Binomial test.
- Chi-square goodness-of-fit.
- One-sample Student test.
- Chi-square independence test.
- Two-sample Student test.
- Welch test fallback when variance condition fails.
- ANOVA.
- Pearson/Spearman correlation.
```

Non-goal:

```text
Do not overclaim that a test is valid unless assumptions are checked.
```

### CDX-M31-T02 - Add test condition checklist UI

Type: `ui`  
Priority: Future

Goal:

```text
Let users review test assumptions before running or recording test results.
```

### CDX-M31-T03 - Add p-value result recording

Type: `domain` / `application`  
Priority: Future

Goal:

```text
Record test results, alpha threshold, p-value and conclusion wording.
```

---

## 30. M40 - Preprocessing planner release `v0.4`

### CDX-M40-T01 - Add preprocessing pipeline domain model

Type: `domain`  
Priority: Future

Goal:

```text
Represent a planned preprocessing sequence without necessarily executing it.
```

Recommended sequence:

```text
1. Missing value handling.
2. Outlier review/handling.
3. Encoding.
4. Feature engineering.
5. Feature selection.
6. Final normalization/standardization.
```

### CDX-M40-T02 - Add missing value decision workflow

Type: `application` / `ui`  
Priority: Future

Goal:

```text
Guide users through delete vs replace decisions and require rationale.
```

### CDX-M40-T03 - Add outlier review workflow

Type: `application` / `ui`  
Priority: Future

Goal:

```text
Show outlier candidates and require fundamental analysis before removal decisions.
```

### CDX-M40-T04 - Add encoding planning workflow

Type: `application` / `ui`  
Priority: Future

Goal:

```text
Plan ordinal vs one-hot encoding based on reviewed variable meaning.
```

### CDX-M40-T05 - Add normalization planning workflow

Type: `application` / `ui`  
Priority: Future

Goal:

```text
Plan Min-Max vs standardization and explain trade-offs.
```

---

## 31. M50 - Modelling assistant release `v0.5`

### CDX-M50-T01 - Add modelling problem definition

Type: `domain` / `application`  
Priority: Future

Goal:

```text
Let the user define target variable, task type and modelling objective.
```

### CDX-M50-T02 - Add model candidate planning

Type: `application`  
Priority: Future

Goal:

```text
Suggest simple baseline models before complex models.
```

Initial candidates:

```text
- Linear regression.
- Logistic regression.
- Decision tree.
- K-nearest neighbors.
```

### CDX-M50-T03 - Add ML.NET experiment spike

Type: `analytics` / `infrastructure`  
Priority: Future

Goal:

```text
Investigate a small ML.NET baseline runner behind an IModelRunner abstraction.
```

Non-goal:

```text
Do not make ML.NET a hard dependency of the core domain.
```

---

## 32. M60 - Evaluation assistant release `v0.6`

### CDX-M60-T01 - Add evaluation metric models

Type: `domain` / `application`  
Priority: Future

Goal:

```text
Represent metrics for regression and classification.
```

Regression metrics:

```text
- MAE.
- MSE.
- RMSE.
- R².
```

Classification metrics:

```text
- Confusion matrix.
- Accuracy.
- Precision.
- Recall.
- F1.
- ROC/AUC where supported.
- Precision/Recall curve where supported.
```

### CDX-M60-T02 - Add evaluation diagnostic workflow

Type: `application` / `ui`  
Priority: Future

Goal:

```text
Compare train/test performance and help diagnose overfitting or underfitting.
```

### CDX-M60-T03 - Add confidence interval calculations

Type: `analytics`  
Priority: Future

Goal:

```text
Compute confidence intervals for selected metrics where assumptions are clear.
```

### CDX-M60-T04 - Add applicability statement builder

Type: `application` / `ui`  
Priority: Future

Goal:

```text
Help users describe where the model is applicable and where it is not.
```

---

## 33. M70 - Web reuse experiment

### CDX-M70-T01 - Add minimal Blazor Web host experiment

Type: `setup` / `ui`  
Priority: Future

Goal:

```text
Validate that SharedUi can be reused in a web host.
```

Scope:

```text
- Create Ariadne.Web only when approved.
- Reference SharedUi.
- Render existing shell.
- Do not implement SaaS features.
```

Acceptance criteria:

```text
- SharedUi reuse is proven.
- No commercial/cloud architecture is added.
```

---

## 34. M80 - Commercial readiness exploration

### CDX-M80-T01 - Document open-source/commercial boundary

Type: `docs`  
Priority: Future

Goal:

```text
Clarify which future features remain open-source and which may belong to a hosted/commercial edition.
```

Possible commercial features:

```text
- Team workspaces.
- Cloud synchronization.
- Hosted reports.
- Collaboration.
- Premium templates.
- Enterprise governance.
```

Non-goal:

```text
Do not implement commercial features in this task.
```

---

# Cross-cutting Codex prompts

## 35. Prompt - Add tests for an existing feature

```text
You are working on Ariadne AI Workbench.

Task ID:
<task id>

Goal:
Improve test coverage for <feature> without changing product behavior.

Relevant documents:
- <relevant doc>

Scope:
- Add or improve tests for current behavior.
- Cover edge cases and failure paths.
- Keep production changes minimal and only if needed for testability.

Non-goals:
- Do not refactor unrelated code.
- Do not change public behavior unless a bug is found and documented.

Acceptance criteria:
- Tests cover the requested behavior.
- Existing tests still pass.

Commands to run:
- dotnet test

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

## 36. Prompt - Fix a bug

```text
You are working on Ariadne AI Workbench.

Task ID:
<task id>

Goal:
Fix this bug:
<bug description>

Relevant documents:
- <relevant doc>

Scope:
- Reproduce the bug with a regression test where practical.
- Fix the smallest area of code necessary.
- Explain the root cause.

Non-goals:
- Do not refactor unrelated code.
- Do not add new features.

Acceptance criteria:
- Regression test fails before the fix or clearly captures the bug.
- Regression test passes after the fix.
- Existing tests pass.

Commands to run:
- dotnet test

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

## 37. Prompt - Refactor one bounded area

```text
You are working on Ariadne AI Workbench.

Task ID:
<task id>

Goal:
Refactor <bounded area> to improve <specific quality>.

Relevant documents:
- 03-technical-architecture.md

Scope:
- Refactor only the named area.
- Preserve behavior.
- Keep public APIs stable unless explicitly requested.

Non-goals:
- Do not change product scope.
- Do not rename projects.
- Do not perform broad formatting-only changes.

Acceptance criteria:
- Behavior is preserved.
- Tests pass.
- The refactor improves the stated quality.

Commands to run:
- dotnet build
- dotnet test

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

## 38. Prompt - Update documentation after implementation

```text
You are working on Ariadne AI Workbench.

Task ID:
<task id>

Goal:
Update documentation to reflect the implementation of <feature>.

Relevant documents:
- <document to update>

Scope:
- Update only the relevant sections.
- Keep wording factual.
- Document limitations and known gaps.

Non-goals:
- Do not rewrite the full document.
- Do not claim unsupported features.

Acceptance criteria:
- Documentation matches implementation.
- Unknown or deferred behavior is explicit.

Commands to run:
- git diff --check

Expected response format:
Summary:
Tests:
Changed files:
Risks / notes:
```

---

# Review checklists

## 39. General review checklist after every Codex task

```text
Does the solution build, or did Codex clearly report why it could not?
Do tests pass, or did Codex clearly report failures?
Did Codex stay inside the requested scope?
Did Codex edit only relevant files?
Did Codex add any unexpected package?
Did Codex preserve dependency direction?
Did Codex avoid adding cloud, web, ML, LLM or auth features prematurely?
Did Codex avoid business logic in Razor components?
Did Codex avoid infrastructure details in Domain/Application?
Did Codex document risks honestly?
```

## 40. Domain review checklist

```text
Are invariants enforced in the domain model?
Are value objects immutable?
Are IDs strongly typed?
Is Domain free from EF Core, MAUI, Blazor and filesystem dependencies?
Are invalid states hard or impossible to represent?
Are tests focused on behavior rather than implementation details?
```

## 41. Application review checklist

```text
Does the use case orchestrate behavior without infrastructure dependencies?
Does it return clear success/failure results?
Does it accept CancellationToken where appropriate?
Does it avoid computing analytics directly if Analytics should own the calculation?
Are tests using fakes rather than real infrastructure unless integration is intended?
```

## 42. Infrastructure review checklist

```text
Does Infrastructure.Local implement Application ports?
Does it keep SQLite and filesystem concerns out of Domain/Application?
Are temporary files cleaned up in tests?
Are raw datasets protected from accidental logging?
Are project folders created under the expected root?
```

## 43. Shared UI review checklist

```text
Does SharedUi avoid MAUI-specific APIs?
Does the component call use cases/services rather than repositories?
Does the UI label inferred/candidate information clearly?
Does it include empty/loading/error states?
Does it guide the next methodological step?
```

## 44. Methodology review checklist

```text
Does the feature support understanding before modelling?
Does it avoid treating inferred information as truth?
Does it allow explicit unknowns?
Does it create or support decision traceability?
Does it prevent premature hypothesis testing or modelling?
Does it avoid misleading certainty?
```

---

# Recommended first execution order

Use this order to create the initial repository safely:

```text
CDX-M00-T02 - Create AGENTS.md
CDX-M00-T03 - Create README.md
CDX-M01-T01 - Repository hygiene files
CDX-M01-T02 - Solution and projects
CDX-M01-T03 - Project references
CDX-M01-T04 - Baseline CI
CDX-M02-T01 - Domain primitives
CDX-M02-T02 - AiProject aggregate
CDX-M02-T03 - Dataset aggregate
CDX-M02-T06 - Fundamental analysis and decision log domain models
CDX-M02-T07 - Application result/use-case conventions
CDX-M03-T01 - SharedUi shell
CDX-M03-T02 - MAUI host wiring
```

Do not start dataset import until project creation and local storage have at least a minimal end-to-end path.

---

# Final instruction to Codex

Ariadne AI Workbench is not an AutoML tool. It is a rigorous methodology workbench.

When implementing any task, prefer:

```text
clarity over cleverness,
traceability over automation,
local-first over cloud-first,
small vertical slices over broad rewrites,
explicit uncertainty over false confidence,
and documented decisions over hidden assumptions.
```

