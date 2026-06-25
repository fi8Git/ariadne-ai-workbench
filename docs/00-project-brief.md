# 00 - Project Brief

# Ariadne AI Workbench

**Status:** Draft v0.1  
**Project type:** Open-source local-first application  
**Primary platform:** .NET 10 MAUI Blazor Hybrid  
**Future platform:** Blazor Web application, if useful and/or commercializable  
**Repository name:** `ariadne-ai-workbench`  
**Solution name:** `Ariadne.sln`  
**Root namespace:** `Ariadne`

---

## 1. Purpose of this document

This document is the initial project brief for **Ariadne AI Workbench**.

It defines the product intent, scope, guiding principles, technical direction and first delivery boundaries. It must be treated as stable context by contributors and by Codex when generating code, documentation, tests or architecture proposals.

This file is intentionally high-level. More detailed specifications will be written in the following documents:

```text
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

---

## 2. One-liner

**Ariadne AI Workbench is a local-first, open-source MAUI Blazor application that guides developers, data practitioners and learners through a rigorous AI and machine learning methodology: dataset profiling, fundamental analysis, hypothesis tracking, preprocessing decisions, model evaluation and reporting.**

---

## 3. Product tagline

**French:** Le fil conducteur de vos projets IA.  
**English:** Your thread through AI methodology.

The name **Ariadne** refers to Ariadne's thread: a guide through a complex maze. In this project, the maze is the sequence of data understanding, assumptions, hypotheses, transformations, modelling choices, evaluation metrics and uncertainty management.

---

## 4. Problem statement

Many AI and machine learning projects fail or become unreliable because teams jump too quickly to model training.

Common issues include:

- datasets are imported without understanding their origin or context;
- variables are used without documenting their meaning, units or collection process;
- exploratory analysis is informal and not reproducible;
- hypotheses are discovered and tested on the same data, creating confirmation bias;
- preprocessing decisions are made manually without traceability;
- train/test separation is poorly documented or incorrectly applied;
- evaluation metrics are selected without considering the real business objective;
- model limitations, uncertainty and applicability boundaries are not clearly reported.

The result is often a model that appears to work technically but is hard to trust, explain, reproduce or maintain.

Ariadne AI Workbench aims to solve this by turning the AI/ML methodology into a guided, documented and repeatable workflow.

---

## 5. Methodological foundation

Ariadne is methodology-first. The product is inspired by a structured data science approach where the practitioner must first understand the phenomenon, the dataset and the confidence level of any conclusion before building a model.

The core methodological ideas are:

1. **Data is only a sample of a larger phenomenon.**  
   The application must help the user reason about what can and cannot be inferred from the available data.

2. **Variables must be understood before they are processed.**  
   The tool must help classify variables, understand their meaning, identify their units, document their source and evaluate their reliability.

3. **Technical analysis and fundamental analysis must be combined.**  
   Statistical summaries and charts are not enough. Users must also document the context of the data: what, who, when, where, how and why.

4. **Hypotheses must be explicit and testable.**  
   Ariadne should encourage the user to formulate hypotheses before validating them, and to separate observation from confirmation whenever possible.

5. **Preprocessing is part of modelling.**  
   Encoding, normalization, missing-value handling, outlier handling, feature engineering and feature selection must be represented as explicit, traceable decisions.

6. **Evaluation must diagnose generalization.**  
   The workflow must distinguish training performance from test performance and help detect underfitting, overfitting, metric misuse and limited applicability.

7. **Reports matter.**  
   A project is not complete unless assumptions, decisions, metrics, limitations and open questions are documented.

---

## 6. Product vision

Ariadne should become a practical workbench for building trustworthy AI/ML projects locally.

The first version is not an AutoML platform and not a cloud SaaS. It is a local, open-source, methodology-driven assistant that helps users structure their work.

Long term, the project may evolve into:

- a reusable open-source methodology engine;
- a desktop/mobile companion for data science projects;
- a Blazor Web version using the same shared UI and application core;
- a commercial hosted version with collaboration, cloud sync, team workspaces and premium reporting.

The first product must remain small, understandable and useful without any external service.

---

## 7. Target users

Primary users:

- developers learning AI or machine learning;
- data analysts who need a structured project workflow;
- students and learners who want to apply a rigorous methodology;
- consultants who need to document data analysis decisions;
- small teams working with sensitive datasets that should remain local.

Secondary users:

- educators creating guided exercises;
- ML engineers who want lightweight project documentation;
- open-source contributors interested in data methodology tooling.

---

## 8. Primary use cases

Ariadne should eventually support the following use cases:

1. **Create a local AI/ML project**  
   The user creates a project workspace with a name, description, objective and local storage location.

2. **Import a dataset**  
   The user imports a CSV file in the first MVP. Other formats may be added later.

3. **Preview and profile the dataset**  
   The application displays rows, columns, inferred types, missing values, unique values and basic descriptive statistics.

4. **Classify variables**  
   The user reviews inferred variable types and classifies columns as discrete, continuous, text, date/time, identifier, target or ignored.

5. **Perform fundamental analysis**  
   The user answers guided questions about the dataset: what, who, when, where, how and why.

6. **Perform technical analysis**  
   The application helps generate univariate and multivariate summaries and charts.

7. **Track hypotheses**  
   The user records assumptions, hypotheses, observations, confirmation steps and decisions.

8. **Plan preprocessing**  
   The user documents transformations such as missing-value handling, outlier handling, encoding, normalization and feature engineering.

9. **Run simple modelling experiments**  
   Later versions may support ML.NET and/or Python-backed model runners.

10. **Evaluate and diagnose models**  
    The user compares train/test metrics and documents underfitting, overfitting, limitations and applicability boundaries.

11. **Export a methodology report**  
    The user exports a Markdown report first. PDF and richer formats may come later.

---

## 9. MVP scope

The first MVP must focus on local methodology support, not advanced machine learning.

MVP v0.1 includes:

- MAUI Blazor Hybrid application shell;
- shared Razor UI project designed for future web reuse;
- Clean Architecture solution structure;
- local project creation;
- CSV import;
- dataset preview;
- basic column profiling;
- variable type inference;
- manual correction of inferred variable types;
- fundamental analysis questionnaire;
- decision log;
- Markdown report generation;
- unit tests for domain and analytics logic;
- documentation for contributors and Codex.

MVP v0.1 does **not** need to train machine learning models.

---

## 10. Explicit non-goals for the MVP

The following items are out of scope for the first MVP:

- no AutoML;
- no cloud synchronization;
- no authentication;
- no team collaboration;
- no hosted SaaS backend;
- no payment or licensing system;
- no advanced Python integration;
- no LLM/agent runtime integration;
- no model registry;
- no experiment tracking server;
- no advanced dashboards;
- no PDF export unless trivial to add after Markdown export;
- no database server dependency;
- no hard dependency on internet access.

These features may be reconsidered after the local-first foundation is stable.

---

## 11. Technical direction

The project starts as a **.NET 10 MAUI Blazor Hybrid** application.

The application must be designed so that the business logic and most UI components can later be reused by a Blazor Web application.

Planned solution structure:

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
```

The future web projects should not be created until there is a clear reason to do so.

---

## 12. Architecture principles

Ariadne must follow a Clean Architecture-inspired structure.

Rules:

1. `Ariadne.Domain` contains pure domain concepts and must not depend on any other project.
2. `Ariadne.Application` contains use cases, application services and interfaces. It may depend on `Ariadne.Domain`.
3. `Ariadne.Analytics` contains deterministic statistical and profiling logic. It must remain testable without MAUI or Blazor.
4. `Ariadne.Infrastructure.Local` implements local storage, local files, CSV readers and report exporters.
5. `Ariadne.SharedUi` contains reusable Razor components and pages.
6. `Ariadne.SharedUi` must not depend on MAUI-specific APIs.
7. `Ariadne.Maui` is only an application host and composition root.
8. Business logic must not be implemented in Razor components.
9. Business logic must not be implemented in the MAUI host project.
10. All important methodology decisions must be represented in the domain or application layer, not only in UI state.

---

## 13. Local-first principles

Ariadne must be useful without an account, a server or an internet connection.

Local-first means:

- user data stays on the user's machine by default;
- imported datasets are not sent to an external service;
- project metadata is saved locally;
- reports can be generated locally;
- the application should remain functional offline;
- future cloud features must be optional.

For MVP v0.1, persistence may start simple. SQLite can be introduced when needed. The domain and application layers should not be coupled to SQLite.

---

## 14. Open-source and commercial strategy

The first product is open-source.

The open-source version should provide real value on its own:

- local project workflow;
- dataset profiling;
- methodology guidance;
- decision tracking;
- report generation.

Potential future commercial features:

- hosted web version;
- team collaboration;
- cloud sync;
- shared workspaces;
- advanced report templates;
- enterprise connectors;
- audit trails;
- compliance-oriented exports;
- premium methodology templates.

The open-source core should not be intentionally crippled. Commercial features should focus on collaboration, hosting, governance and advanced productivity.

License decision is still open. Candidate licenses: MIT or Apache-2.0.

---

## 15. Product principles

Ariadne must be:

- **methodology-first**: guide the user through sound reasoning before modelling;
- **transparent**: show assumptions, transformations and decisions;
- **local-first**: keep data on the user's machine by default;
- **reproducible**: make decisions and generated outputs traceable;
- **educational**: help users understand why a step matters;
- **non-magical**: avoid pretending that automated results are definitive;
- **extensible**: allow future ML.NET, Python or web integrations;
- **privacy-aware**: avoid external calls unless explicitly configured;
- **small at first**: prefer a useful narrow MVP over a large incomplete platform.

---

## 16. First domain concepts

The initial domain model should include at least:

```text
AiProject
Dataset
DatasetVersion
ColumnProfile
VariableDefinition
FundamentalAnalysis
FundamentalAnalysisAnswer
DecisionLogEntry
MethodologyReport
```

Future domain concepts may include:

```text
Hypothesis
StatisticalTestRun
PreprocessingPipeline
PreprocessingStep
ExperimentRun
ModelCandidate
EvaluationMetric
ConfidenceInterval
ApplicabilityAssessment
```

Domain concepts must use explicit names. Avoid vague entities such as `DataItem`, `Result`, `Manager` or `Helper` unless justified.

---

## 17. First analytics capabilities

Initial analytics logic should be deterministic and unit tested.

MVP analytics may include:

- row count;
- column count;
- missing value count;
- missing value ratio;
- unique value count;
- inferred primitive type;
- inferred methodological type;
- numeric minimum and maximum;
- numeric mean;
- numeric median;
- numeric quartiles;
- simple IQR outlier count;
- categorical value counts;
- sample values.

Advanced analytics such as statistical tests, correlation analysis, model training and confidence intervals should come later.

---

## 18. Reporting direction

The first report format is Markdown.

A generated report should eventually contain:

- project summary;
- dataset summary;
- variable dictionary;
- fundamental analysis answers;
- technical analysis summary;
- hypotheses and decisions;
- preprocessing choices;
- modelling summary, when implemented;
- evaluation summary, when implemented;
- limitations;
- unresolved questions;
- generated date and application version.

The report should be readable by a human without opening the application.

---

## 19. UX direction

The UI should feel like a guided workbench, not like a generic dashboard.

Preferred interaction model:

```text
Project
  -> Dataset
  -> Understand
  -> Analyze
  -> Hypothesize
  -> Prepare
  -> Model
  -> Evaluate
  -> Report
```

The first screens should prioritize clarity over visual complexity.

UX principles:

- show progress through the methodology;
- make missing steps visible;
- encourage documentation without blocking exploration;
- keep technical language accessible;
- provide explanations close to the relevant action;
- allow expert users to move quickly;
- avoid overwhelming charts in the first MVP.

---

## 20. Quality expectations

The project should be developed with production-quality habits from the beginning.

Minimum expectations:

- solution builds successfully;
- tests pass;
- domain logic is unit tested;
- analytics calculations are unit tested;
- nullability enabled where practical;
- no business logic in UI components;
- no hardcoded absolute paths;
- no hidden external service calls;
- meaningful names for classes, methods and files;
- small focused commits or Codex tasks;
- documentation updated when architecture changes.

---

## 21. Success criteria for MVP v0.1

MVP v0.1 is successful when a user can:

1. launch the MAUI Blazor application locally;
2. create a local Ariadne project;
3. import a CSV dataset;
4. preview the imported data;
5. see automatic column profiles;
6. review and correct variable classifications;
7. complete a fundamental analysis questionnaire;
8. add decision log entries;
9. generate a Markdown report;
10. close and reopen the project without losing metadata, if persistence is included in the sprint.

Technical success criteria:

- the solution builds;
- tests pass;
- architecture boundaries are respected;
- Shared UI does not depend on MAUI-specific APIs;
- the app is ready for a future Blazor Web host without rewriting the domain/application layers.

---

## 22. Suggested milestone plan

```text
Sprint 0 - Documentation foundation
  Write project brief, product vision, architecture notes, domain model draft, AGENTS.md and README.md.

Sprint 1 - Solution skeleton
  Create .NET solution, MAUI Blazor host, Shared UI library, domain/application/analytics projects and test projects.

Sprint 2 - Local project workspace
  Create project entities, local project creation flow and basic persistence abstraction.

Sprint 3 - CSV import and preview
  Add CSV reader, dataset preview and initial dataset metadata.

Sprint 4 - Column profiling
  Implement deterministic profiling logic and tests.

Sprint 5 - Fundamental analysis
  Add guided what/who/when/where/how/why questionnaire and decision log.

Sprint 6 - Markdown reporting
  Generate first methodology report from project metadata and dataset profile.

Sprint 7 - Technical analysis basics
  Add univariate summaries and simple charts.

Sprint 8 - Preprocessing planning
  Add traceable preprocessing pipeline planning without necessarily transforming data yet.

Sprint 9 - First modelling integration
  Add simple ML.NET experiments only after the methodology foundation is stable.
```

---

## 23. Risks and mitigations

| Risk | Impact | Mitigation |
|---|---|---|
| Scope creep toward AutoML | The app becomes too large too early | Keep MVP focused on methodology, profiling and reporting |
| MAUI complexity | Cross-platform friction slows progress | Keep MAUI as a thin host and put reusable logic elsewhere |
| UI/business logic mixing | Future Web reuse becomes difficult | Enforce architecture rules in AGENTS.md |
| Data-size performance issues | CSV profiling may become slow | Start with reasonable file limits and stream later |
| Premature ML integration | Core workflow becomes unstable | Delay ML.NET/Python until profiling/reporting work well |
| Ambiguous open-source/commercial boundary | Community trust may suffer | Keep local core genuinely useful and monetize hosted/team features later |
| Methodology becomes too rigid | Expert users may feel constrained | Provide guidance and traceability without forcing every step |

---

## 24. Naming conventions

Product names:

```text
Public product name: Ariadne AI Workbench
Short product name: Ariadne
Repository: ariadne-ai-workbench
Solution: Ariadne.sln
Root namespace: Ariadne
```

Suggested projects:

```text
Ariadne.Domain
Ariadne.Application
Ariadne.Analytics
Ariadne.Infrastructure.Local
Ariadne.SharedUi
Ariadne.Maui
```

Potential future projects:

```text
Ariadne.Web
Ariadne.Infrastructure.Web
Ariadne.Infrastructure.Python
Ariadne.MLNet
```

---

## 25. Guidance for Codex

When working on Ariadne, Codex must:

1. respect the MVP scope;
2. avoid adding external services unless explicitly requested;
3. keep MAUI as a host, not as the place for business logic;
4. place reusable UI in `Ariadne.SharedUi`;
5. place domain concepts in `Ariadne.Domain`;
6. place use cases and interfaces in `Ariadne.Application`;
7. place profiling/statistics logic in `Ariadne.Analytics`;
8. place local file/database implementations in `Ariadne.Infrastructure.Local`;
9. write or update tests for domain and analytics changes;
10. update documentation when changing architecture or project scope;
11. prefer small, reviewable changes;
12. never silently introduce cloud calls, telemetry or remote AI APIs.

Codex should not implement Web, Identity, PostgreSQL, AutoML, LLM agents or Python integration unless a later task explicitly requests it.

---

## 26. Open questions

The following decisions remain open:

- final open-source license: MIT or Apache-2.0;
- exact local persistence strategy for MVP: JSON files first or SQLite directly;
- maximum CSV size for the first release;
- target platforms for the first tested build: Windows first, then macOS/Linux alternatives if practical;
- whether documentation should be fully English, fully French or bilingual;
- first public GitHub release target;
- visual identity and logo.

---

## 27. Final intent

Ariadne AI Workbench should help users slow down where it matters: understanding data, documenting assumptions, testing ideas, preparing inputs and evaluating applicability.

The application should not try to replace the data practitioner. It should act as a methodological companion that makes good practice easier, more visible and more repeatable.
