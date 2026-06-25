# 01 - Product Vision

# Ariadne AI Workbench

**Status:** Draft v0.1  
**Project type:** Open-source local-first application  
**Primary platform:** .NET 10 MAUI Blazor Hybrid  
**Future platform:** Blazor Web application, if useful and/or commercializable  
**Repository name:** `ariadne-ai-workbench`  
**Solution name:** `Ariadne.sln`  
**Root namespace:** `Ariadne`  
**Related document:** `00-project-brief.md`

---

## 1. Purpose of this document

This document defines the product vision for **Ariadne AI Workbench**.

It explains what Ariadne should become, who it serves, what value it provides, what experience it should create, and what trade-offs should guide product decisions. It is intended for human contributors and for Codex when generating product documentation, UI flows, domain concepts, issues, tests or implementation plans.

This document is not a technical architecture specification. Technical details belong in:

```text
03-technical-architecture.md
04-domain-model.md
06-local-first-storage.md
09-codex-task-breakdown.md
AGENTS.md
```

---

## 2. Vision statement

**Ariadne AI Workbench helps developers, learners and data practitioners turn AI and machine learning projects into guided, evidence-based, reproducible workflows.**

Ariadne is not designed to make machine learning look magical. It is designed to make good methodology easier to follow:

```text
Understand the data.
Document the context.
Formulate hypotheses.
Prepare the dataset.
Evaluate generalization.
Report assumptions, decisions and limits.
```

The long-term ambition is to become a trusted open-source workbench for methodology-first AI/ML projects: simple enough for learners, structured enough for consultants, and extensible enough for future advanced modelling workflows.

---

## 3. Product tagline

**French:** Le fil conducteur de vos projets IA.  
**English:** Your thread through AI methodology.

The name **Ariadne** refers to Ariadne's thread: a guide through a maze. In this product, the maze is not only model training. It is the full path from uncertain data to a responsible conclusion.

---

## 4. The product thesis

Most AI/ML tools optimize for modelling speed.

Ariadne optimizes for methodological clarity.

Many tools help users train models quickly, compare metrics or deploy APIs. Fewer tools help users answer questions such as:

- What does each variable really mean?
- Where did the data come from?
- Is the dataset representative of the phenomenon we want to model?
- Which assumptions did we make before modelling?
- Which hypotheses were observed and which were actually confirmed?
- Which preprocessing decisions changed the meaning of the data?
- Does the model generalize, or is it only good on a sample?
- What are the limitations and uncertainty of the conclusion?

Ariadne exists because these questions determine whether an AI project can be trusted.

---

## 5. Product positioning

Ariadne is a **local-first, open-source AI/ML methodology workbench**.

It sits between a notebook, a lightweight data profiling tool, a methodology checklist, and a project report generator.

It should feel like:

```text
A structured lab notebook for AI and machine learning projects.
```

It should not feel like:

```text
A black-box AutoML platform.
A generic BI dashboard.
A cloud-only experiment tracker.
A modelling framework disguised as a methodology tool.
```

### 5.1 Category

Ariadne belongs to a product category that can be described as:

```text
AI/ML Methodology Workbench
Local-first Data Science Companion
Evidence-based ML Project Notebook
```

### 5.2 Primary differentiation

Ariadne's differentiation is not that it trains more models than other tools.

Its differentiation is that it helps the user create a **traceable reasoning path** from data import to final report.

---

## 6. Core promise

Ariadne promises to help users produce AI/ML work that is:

- **structured**: each project follows a clear methodology;
- **traceable**: assumptions, choices and changes are recorded;
- **explainable**: the user can justify why a dataset, variable, test or transformation was used;
- **reproducible**: reports and project metadata can be regenerated;
- **privacy-aware**: data stays local by default;
- **educational**: the tool teaches why each step matters;
- **modelling-ready**: when the user eventually trains a model, the context is already documented.

---

## 7. Target users

Ariadne should be useful to several user groups, but the first version must avoid trying to satisfy everyone at once.

### 7.1 Primary user: the developer learning AI/ML

This user knows software development but is still learning data science methodology.

Needs:

- clear guidance;
- explanations near each action;
- a simple workflow;
- confidence that they are not skipping important steps;
- local execution without a complex cloud setup.

Ariadne helps this user by turning abstract methodology into concrete screens, checklists and generated reports.

### 7.2 Primary user: the data practitioner who wants structure

This user already analyzes data but wants a cleaner way to document projects.

Needs:

- fast dataset profiling;
- structured variable documentation;
- decision tracking;
- reusable reports;
- minimal friction.

Ariadne helps this user by acting as a lightweight methodology layer around their analysis work.

### 7.3 Primary user: the consultant or technical lead

This user must communicate data work to clients, stakeholders or teams.

Needs:

- transparent assumptions;
- documented limitations;
- readable reports;
- a professional process;
- an audit trail of decisions.

Ariadne helps this user by turning exploratory work into a reportable methodology narrative.

### 7.4 Secondary user: the educator

This user wants to teach applied AI/ML methodology.

Needs:

- guided exercises;
- clear step-by-step flows;
- visual explanations;
- exported student reports.

Ariadne helps this user by providing a reusable structure for assignments and workshops.

### 7.5 Secondary user: the open-source contributor

This user wants to contribute to tooling, analytics, UI components or documentation.

Needs:

- clear architecture;
- small issues;
- well-defined boundaries;
- tests;
- documentation that explains why features exist.

Ariadne helps this user by keeping the project modular and methodology-driven.

---

## 8. Jobs to be done

Ariadne should be designed around jobs users need to accomplish, not around features alone.

### 8.1 When I start an AI/ML project

**When** I start a new AI or ML project,  
**I want** a guided workspace that asks the right questions,  
**so that** I do not jump directly into modelling without understanding the data.

### 8.2 When I import a dataset

**When** I import a dataset,  
**I want** an automatic profile and a human-readable summary,  
**so that** I can quickly see missing values, variable types, suspicious columns and first risks.

### 8.3 When I inspect variables

**When** I inspect variables,  
**I want** to document their meaning, unit, source, role and methodological type,  
**so that** later transformations and models are grounded in the real meaning of the data.

### 8.4 When I observe a pattern

**When** I observe a pattern in the data,  
**I want** to record whether it is an observation, an assumption, a hypothesis or a confirmed conclusion,  
**so that** I avoid confusing exploration with validation.

### 8.5 When I prepare data

**When** I clean, encode, normalize or transform data,  
**I want** to log the reasoning behind each decision,  
**so that** the preprocessing pipeline is understandable and reviewable.

### 8.6 When I evaluate a model

**When** I evaluate a model,  
**I want** to compare training and test results and document limitations,  
**so that** I can reason about generalization instead of only reporting a metric.

### 8.7 When I finish a project phase

**When** I finish a project phase,  
**I want** to generate a report from the project history,  
**so that** the work can be shared, reviewed or archived.

---

## 9. Product pillars

Ariadne should be guided by six product pillars.

### 9.1 Methodology-first

The workflow must prioritize understanding, reasoning, documentation and evaluation.

The product should encourage this sequence:

```text
Phenomenon -> Dataset -> Variables -> Analysis -> Hypotheses -> Preprocessing -> Modelling -> Evaluation -> Report
```

The product should not push users toward model training before enough context exists.

### 9.2 Local-first

The first product must be valuable without accounts, servers or cloud services.

Local-first means:

- datasets remain on the user's machine by default;
- no hidden upload;
- no mandatory telemetry;
- no dependency on a remote AI API;
- project metadata is stored locally;
- generated reports are produced locally.

Future cloud features must be optional and explicit.

### 9.3 Evidence-based

Ariadne should help users distinguish:

```text
Raw data
Observation
Assumption
Hypothesis
Test result
Decision
Conclusion
Limitation
```

The product should make weak evidence visible rather than hiding uncertainty behind polished charts.

### 9.4 Educational but not patronizing

Ariadne should explain why each step matters, especially for learners.

However, expert users should be able to move quickly. Guidance must be helpful, not obstructive.

Good guidance:

- short explanations close to the action;
- expandable methodology notes;
- warnings when a step is risky;
- report-ready definitions.

Bad guidance:

- long mandatory tutorials;
- blocking every action with theory;
- pretending there is only one valid method;
- hiding advanced options forever.

### 9.5 Traceable by default

Important choices must become part of the project record.

Examples:

- variable classification changed from `Text` to `Discrete`;
- column ignored because it is an identifier;
- outliers reviewed but kept due to business context;
- missing values replaced by median;
- target variable selected;
- hypothesis moved from observation to confirmation;
- report generated.

Traceability should be a normal part of the workflow, not a separate administrative burden.

### 9.6 Reusable beyond MAUI

The first application is MAUI Blazor Hybrid, but the product must be shaped so that a future web version can reuse the domain, application logic and shared UI components.

This means:

- MAUI is a host, not the product core;
- UI components live in a shared Razor library;
- domain and application layers are independent;
- local storage is behind interfaces;
- cloud storage can be added later without rewriting the methodology engine.

---

## 10. The Ariadne workflow

The product should guide users through a workflow that is simple enough to understand and flexible enough to evolve.

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

### 10.1 Project

The project is the container for the user's work.

It stores:

- name;
- description;
- objective;
- project status;
- datasets;
- analyses;
- decisions;
- reports.

The project should answer:

```text
What are we trying to understand or predict?
Why does this project exist?
What would a useful outcome look like?
```

### 10.2 Dataset

The dataset step imports and registers source data.

It should answer:

```text
What data do we have?
Where is it stored?
What version is being used?
What is the shape of the dataset?
```

For the first MVP, CSV is enough.

### 10.3 Understand

This step combines dataset profiling and fundamental analysis.

It should answer:

```text
What do rows represent?
What do columns represent?
What are the units?
Who collected the data?
When and where was it collected?
How was it collected?
Why was it collected?
What context is missing?
```

This is one of the most important differentiators of Ariadne.

### 10.4 Analyze

This step contains technical analysis.

It should include, progressively:

- univariate summaries;
- categorical counts;
- numeric distributions;
- missing values;
- outlier candidates;
- simple multivariate summaries;
- correlation candidates;
- charts where useful.

The goal is not to generate many charts. The goal is to surface meaningful signals and risks.

### 10.5 Hypothesize

This step turns observations into explicit hypotheses.

Ariadne should help users separate:

- what they observed;
- what they believe;
- what they can test;
- what they have confirmed;
- what remains uncertain.

The workflow should discourage confirmation bias by making it clear when an idea was discovered and when it was tested.

### 10.6 Prepare

This step documents preprocessing decisions.

Initial focus:

- missing-value handling;
- outlier review;
- variable inclusion/exclusion;
- encoding plans;
- normalization plans;
- feature engineering notes;
- feature selection notes.

The first implementation may only plan and document preprocessing. Later versions may execute transformations.

### 10.7 Model

This step should come after the methodology foundation is stable.

Initial modelling should be simple and explainable:

- baseline models;
- regression;
- binary classification;
- multiclass classification;
- simple ML.NET integration.

Ariadne should not become AutoML-first.

### 10.8 Evaluate

Evaluation should help the user reason about generalization and applicability.

It should include:

- train metrics;
- test metrics;
- comparison between train and test;
- signs of underfitting;
- signs of overfitting;
- metric interpretation;
- error analysis notes;
- applicability boundaries;
- confidence or uncertainty notes when supported.

### 10.9 Report

The report is the product of the methodology.

It should be readable outside the application and include:

- project objective;
- dataset summary;
- variable dictionary;
- fundamental analysis;
- technical analysis;
- hypotheses;
- preprocessing decisions;
- modelling summary, when available;
- evaluation summary, when available;
- limitations;
- unresolved questions;
- decision log.

Markdown is the first target format.

---

## 11. MVP product vision

MVP v0.1 should prove that Ariadne can be useful before it trains any model.

The MVP should answer this question:

```text
Can Ariadne help a user import a dataset, understand it, document it and generate a useful methodology report locally?
```

The answer must be yes before adding advanced modelling features.

### 11.1 MVP user journey

The first complete journey should be:

```text
Launch app
Create local project
Import CSV
Preview dataset
View column profiles
Review variable classifications
Complete fundamental analysis
Add decision notes
Generate Markdown report
```

### 11.2 MVP value

The user should leave MVP v0.1 with:

- a local project workspace;
- a basic but useful dataset profile;
- a first variable dictionary;
- a documented fundamental analysis;
- a small decision log;
- a Markdown report that summarizes the work.

### 11.3 MVP emotional goal

After using the MVP, the user should feel:

```text
I understand this dataset better than before, and I have a clear record of what I know, what I assume and what remains uncertain.
```

---

## 12. Product maturity stages

Ariadne should evolve through product maturity stages.

### Stage 1 - Methodology notebook

Focus:

- project creation;
- CSV import;
- profiling;
- variable documentation;
- fundamental analysis;
- decision log;
- Markdown reports.

This is the MVP stage.

### Stage 2 - Analysis workbench

Focus:

- richer univariate analysis;
- multivariate analysis;
- charts;
- hypothesis tracking;
- basic statistical test suggestions;
- better report sections.

### Stage 3 - Preprocessing planner

Focus:

- missing-value strategies;
- outlier review workflow;
- encoding plan;
- normalization plan;
- feature engineering notes;
- preprocessing pipeline representation.

At this stage, transformations may still be declarative before becoming executable.

### Stage 4 - Modelling assistant

Focus:

- baseline models;
- simple ML.NET experiments;
- train/test split tracking;
- model comparison;
- evaluation metrics;
- overfitting/underfitting diagnostics.

### Stage 5 - Extensible methodology platform

Focus:

- plugin-like model runners;
- optional Python integration;
- reusable methodology templates;
- richer report exports;
- optional web version;
- collaboration features if commercialized.

---

## 13. Product experience principles

### 13.1 The UI should guide without overwhelming

The user should always know:

- where they are in the methodology;
- what step is recommended next;
- what is complete;
- what is missing;
- what is optional.

A progress-oriented layout is preferred over a dense dashboard.

### 13.2 Every screen should answer a question

Examples:

| Screen | Question it answers |
|---|---|
| Project overview | What is this project about? |
| Dataset import | What data are we using? |
| Dataset profile | What is inside this dataset? |
| Variable dictionary | What does each column mean? |
| Fundamental analysis | Can we trust and contextualize this data? |
| Technical analysis | What patterns or risks are visible? |
| Hypotheses | What do we believe and how can we test it? |
| Preprocessing | How will we transform the data and why? |
| Evaluation | Does the model generalize? |
| Report | What can we communicate and defend? |

### 13.3 The product should make uncertainty visible

Ariadne should never imply that a dataset profile, chart, test or metric is a final truth.

Preferred language:

```text
Potential issue
Candidate outlier
Inferred type
Needs review
Assumption
Evidence level
Limitation
```

Avoid overconfident language:

```text
Guaranteed
Correct model
Perfect dataset
Definitive conclusion
Fully automated insight
```

### 13.4 Reports should be human-first

Generated reports should be readable by people who did not use the application.

The report should explain:

- what was done;
- why it was done;
- what was found;
- what is uncertain;
- what should happen next.

### 13.5 Expert users should remain in control

Ariadne may suggest classifications, checks or next steps, but the user must be able to correct and annotate them.

The product should say:

```text
Ariadne suggests. The practitioner decides and documents.
```

---

## 14. Open-source vision

The open-source version should provide real value without requiring payment or cloud services.

The open-source core should include:

- local projects;
- dataset profiling;
- variable documentation;
- fundamental analysis;
- decision logging;
- report generation;
- deterministic analytics;
- extensible architecture;
- contributor-friendly tests and documentation.

The open-source project should build trust by avoiding artificial limitations in the local methodology workflow.

---

## 15. Future commercial vision

A future commercial version may exist, but it should build on top of the open-source core rather than replacing it.

Potential commercial features:

- hosted Blazor Web version;
- team workspaces;
- cloud synchronization;
- collaboration and comments;
- organization-level templates;
- advanced reports;
- audit trails;
- governance workflows;
- connectors to enterprise data sources;
- managed methodology libraries;
- training/team onboarding features.

Commercialization should focus on:

```text
Collaboration, hosting, governance, productivity and team scale.
```

It should not focus on crippling the local open-source tool.

---

## 16. Competitive alternatives and boundaries

Ariadne may overlap with several tool categories, but it should not try to replace them all.

| Tool category | Ariadne relationship |
|---|---|
| Jupyter notebooks | Complementary; Ariadne structures methodology and reports |
| BI dashboards | Complementary; Ariadne is not a dashboard-first tool |
| AutoML platforms | Different; Ariadne is methodology-first, not model-search-first |
| Experiment trackers | Complementary; Ariadne may later track simple experiments |
| Data catalogs | Different; Ariadne documents project-level context, not enterprise metadata at first |
| Statistical packages | Complementary; Ariadne may suggest or document tests, not replace full statistical environments |
| ML frameworks | Complementary; Ariadne may call ML.NET/Python later but should not become only a modelling wrapper |

---

## 17. Anti-goals

Ariadne should not become:

- a cloud-only SaaS in the first versions;
- an AutoML-first product;
- a generic chart builder;
- a replacement for all notebooks;
- a replacement for all statistical tools;
- a BI dashboarding platform;
- a hidden data collection product;
- a wrapper around LLM APIs;
- a complex enterprise platform before the local workflow is excellent;
- a tool that gives overconfident conclusions from weak evidence.

---

## 18. Success metrics

### 18.1 MVP success metrics

MVP v0.1 should be considered successful if:

- a user can create a project without reading technical documentation;
- a user can import a CSV and understand its structure;
- inferred column profiles are useful and reviewable;
- fundamental analysis is easy to complete;
- decision logging feels natural;
- a Markdown report is generated and readable;
- the app works without an account or server;
- the solution builds reliably;
- domain and analytics logic are unit tested;
- contributors can understand the project structure quickly.

### 18.2 Product success metrics after public release

Potential future metrics:

- GitHub stars and forks;
- number of external contributors;
- number of reported successful local projects;
- number of generated reports;
- quality of community feedback;
- repeat usage across multiple projects;
- educational adoption;
- consulting/team interest;
- requests for web/collaboration features.

### 18.3 Quality metrics

Ariadne should track product quality through:

- test coverage of domain and analytics logic;
- number of architecture boundary violations;
- number of features requiring MAUI-specific rewrites;
- report readability feedback;
- CSV import reliability;
- performance on realistic datasets;
- documentation completeness.

---

## 19. Product language

Ariadne should use precise but accessible language.

Preferred terms:

```text
Project
Dataset
Dataset version
Variable
Column profile
Variable dictionary
Observation
Hypothesis
Decision
Assumption
Limitation
Preprocessing step
Evaluation metric
Report
```

Avoid vague terms unless there is a clear reason:

```text
Magic
AI-powered insight
Smart result
Data thing
Model stuff
Helper
Manager
```

---

## 20. Voice and tone

Ariadne's tone should be:

- clear;
- calm;
- precise;
- educational;
- humble about uncertainty;
- practical;
- professional.

Ariadne should not sound:

- hype-driven;
- overconfident;
- academic for the sake of being academic;
- childish;
- sales-heavy;
- mysterious.

Example tone:

```text
This column looks numeric and has 12% missing values. Review its meaning before deciding whether to replace or remove missing values.
```

Avoid:

```text
AI has detected the best cleaning strategy for your perfect model.
```

---

## 21. User-facing conceptual model

Users should be able to understand Ariadne through this simple model:

```text
A project contains datasets.
Datasets contain variables.
Variables need context.
Analysis produces observations.
Observations can become hypotheses.
Hypotheses and preprocessing choices create decisions.
Decisions and results become a report.
```

This conceptual model should influence UI, domain naming and documentation.

---

## 22. Feature prioritization rules

When deciding what to build next, prioritize features that:

1. improve the methodology workflow;
2. reduce ambiguity in the user's reasoning;
3. make decisions more traceable;
4. improve report quality;
5. keep data local;
6. are testable without complex infrastructure;
7. strengthen future web reuse.

Deprioritize features that:

1. require cloud infrastructure too early;
2. make the app look impressive but do not improve methodology;
3. add advanced modelling before dataset understanding is strong;
4. create MAUI-specific business logic;
5. increase complexity without improving the first full user journey;
6. require external AI APIs before the local workflow is valuable.

---

## 23. Product roadmap themes

### Theme 1 - Local methodology foundation

Goal: make the local app useful and trustworthy without modelling.

Includes:

- project creation;
- CSV import;
- dataset preview;
- profiling;
- variable documentation;
- fundamental analysis;
- decision log;
- Markdown report.

### Theme 2 - Technical analysis basics

Goal: help users explore variables and relationships with appropriate summaries.

Includes:

- univariate summaries;
- value counts;
- histograms or distribution summaries;
- boxplot-ready statistics;
- simple relationships between variables;
- correlation candidates;
- outlier candidates.

### Theme 3 - Hypothesis discipline

Goal: help users separate exploration from confirmation.

Includes:

- observations;
- hypotheses;
- evidence level;
- confirmation notes;
- warnings about testing on the same data used to formulate a hypothesis.

### Theme 4 - Preprocessing traceability

Goal: represent preprocessing as a sequence of intentional decisions.

Includes:

- missing-value handling plans;
- outlier review;
- encoding choices;
- normalization choices;
- feature engineering notes;
- feature selection notes.

### Theme 5 - Modelling and evaluation

Goal: add simple, transparent modelling only after the methodology foundation is stable.

Includes:

- baseline models;
- train/test split tracking;
- ML.NET model runner;
- core metrics;
- overfitting/underfitting diagnosis;
- evaluation report sections.

### Theme 6 - Reuse and commercialization path

Goal: prepare a future web/commercial version without compromising open-source trust.

Includes:

- shared UI refinement;
- storage abstractions;
- report templates;
- optional web host;
- collaboration model exploration;
- governance/audit features later.

---

## 24. Codex product guidance

When Codex works on product-related tasks, it must respect this vision.

Codex should:

1. preserve the methodology-first positioning;
2. prefer clear workflows over flashy features;
3. avoid adding AutoML unless explicitly requested in a later phase;
4. keep local-first assumptions by default;
5. avoid remote services, telemetry or AI API dependencies unless explicitly requested;
6. use precise domain names that match the product language;
7. keep generated UI reusable outside MAUI when possible;
8. create features that strengthen the full project-to-report journey;
9. include documentation updates when product behavior changes;
10. write tests for deterministic logic.

Codex should not:

1. turn the MVP into a SaaS;
2. add authentication in the local MVP;
3. introduce PostgreSQL before a web/commercial phase;
4. put business logic in Razor components;
5. make unsupported claims about model quality;
6. generate UI text that sounds like hype marketing;
7. silently change product scope.

---

## 25. Product north star

The north star for Ariadne is:

```text
A user can produce a trustworthy, readable, evidence-based methodology report for an AI/ML project from local data.
```

Everything else is secondary until this experience works well.

---

## 26. Decision record

Current product decisions:

| Decision | Status | Notes |
|---|---|---|
| Product name | Accepted | Ariadne AI Workbench |
| Short name | Accepted | Ariadne |
| Product type | Accepted | Open-source local-first application |
| Primary host | Accepted | .NET 10 MAUI Blazor Hybrid |
| UI reuse strategy | Accepted | Shared Razor UI library |
| First report format | Accepted | Markdown |
| First dataset format | Accepted | CSV |
| First storage strategy | Open | JSON first or SQLite directly |
| License | Open | MIT or Apache-2.0 |
| First modelling integration | Deferred | ML.NET later |
| Python integration | Deferred | Optional future model runner |
| Web version | Deferred | Only after local workflow proves useful |

---

## 27. Final product intent

Ariadne AI Workbench should help users build better AI/ML projects by making the invisible parts of methodology visible.

It should slow users down at the right moments: before trusting data, before confirming hypotheses, before transforming variables, before interpreting metrics, and before presenting conclusions.

The product succeeds when users can say:

```text
I know what data I used.
I know what it means.
I know what I assumed.
I know what I changed.
I know what I tested.
I know what my model can and cannot claim.
I can explain the whole path in a report.
```
