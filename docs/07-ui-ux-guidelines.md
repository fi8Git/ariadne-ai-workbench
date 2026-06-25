# 07 - UI/UX Guidelines

# Ariadne AI Workbench

**Status:** Draft v0.1  
**Project type:** Open-source local-first application  
**Primary platform:** .NET 10 MAUI Blazor Hybrid  
**Future platform:** Blazor Web application, if useful and/or commercializable  
**Repository name:** `ariadne-ai-workbench`  
**Solution name:** `Ariadne.sln`  
**Root namespace:** `Ariadne`  
**Related documents:** `00-project-brief.md`, `01-product-vision.md`, `02-functional-specification.md`, `03-technical-architecture.md`, `04-domain-model.md`, `05-methodology-workflow.md`, `06-local-first-storage.md`

---

## 1. Purpose of this document

This document defines the **UI and UX guidelines** for **Ariadne AI Workbench**.

Ariadne is not a generic dashboard, an AutoML wizard or a notebook replacement. It is a **methodology workbench** that helps users understand, document and communicate an AI/ML project before they rush into modelling.

This document is intended to guide:

- the visual direction of the product;
- information architecture;
- navigation and workflow layout;
- screen composition;
- reusable Blazor component design;
- accessibility rules;
- responsive behavior;
- copywriting and terminology;
- local-first UX patterns;
- implementation rules for `Ariadne.SharedUi`;
- Codex tasks that create or modify UI code.

This document is **not** the functional specification. Functional requirements belong in:

```text
02-functional-specification.md
```

This document is **not** the technical architecture. Project boundaries, dependencies and hosting rules belong in:

```text
03-technical-architecture.md
```

This document is **not** the methodology workflow. Workflow states and methodology gates belong in:

```text
05-methodology-workflow.md
```

This file describes **how Ariadne should feel, look and behave from the user's point of view**.

---

## 2. UX thesis

Ariadne should make rigorous AI/ML methodology feel **calm, navigable and repeatable**.

The typical failure mode Ariadne must counter is:

```text
The user imports data.
The user jumps into modelling.
The user gets a metric.
The context, assumptions, decisions and limitations remain unclear.
```

Ariadne should replace this with:

```text
The user imports data.
The user understands what the data represents.
The user reviews variables.
The user documents context.
The user records decisions.
The user builds evidence gradually.
The user generates a report that explains what is known, unknown and still risky.
```

The UI should therefore feel less like a code editor and more like a **structured scientific notebook with guardrails**.

The interface must help users answer:

```text
Where am I in the project?
What have I already documented?
What still needs review?
What can I trust?
What is only an automated guess?
What should I do next?
What will appear in the report?
```

---

## 3. Methodological alignment

The UI must embody the methodology behind Ariadne.

The source methodology emphasizes that a dataset is only an observed sample of a broader phenomenon. A data scientist should estimate what is known, understand the confidence in that estimation, formulate hypotheses, test them and document conclusions.

The UI must therefore avoid presenting computed values as absolute truth.

### 3.1 Concepts the UI must make visible

| Methodological idea | UI implication |
|---|---|
| A dataset is a sample, not reality. | Always show row count, source, import date and reviewed/unreviewed status. |
| Variables can be discrete or continuous. | Show variable type chips and let users correct inferred types. |
| Technical analysis starts simple. | Use progressive disclosure: profile first, charts later, modelling later. |
| Fundamental analysis matters. | Provide a first-class guided section for What, Who, When, Where, How and Why. |
| Hypotheses can introduce confirmation bias. | Later hypothesis screens must distinguish observation data from confirmation data. |
| Preprocessing decisions are not neutral. | Every future preprocessing step should require a rationale or decision log entry. |
| Evaluation is metric + confidence interval + applicability. | Future evaluation screens must show limitations and applicability, not only scores. |
| Reports must mention unknowns. | Missing context should appear explicitly as report gaps, not be silently hidden. |

### 3.2 Methodology references from the PDF

The UI guidelines are aligned with these core parts of the methodology PDF:

| PDF area | UI interpretation |
|---|---|
| Pages 4-5: random phenomena, samples, uncertainty and confidence | Ariadne must show that computed profiles are observations from an imported sample. |
| Pages 6-10: discrete/continuous variables, univariate and multivariate analysis | Ariadne must make variable typing and analysis type explicit. |
| Pages 11-20: hypotheses, p-values, test conditions and confirmation bias | Future hypothesis screens must include preconditions, alpha, result interpretation and bias warnings. |
| Pages 21-22: fundamental analysis | Ariadne must treat context documentation as a required part of the workflow. |
| Pages 27, 79-80: ML pipeline | The UI must represent the project as a staged workflow, not a single modelling screen. |
| Pages 28-47: preprocessing | Future preprocessing UI must distinguish automated suggestions from user-approved decisions. |
| Pages 59-83: evaluation and confidence intervals | Future evaluation UI must combine metrics, uncertainty and applicability. |

---

## 4. Product personality

Ariadne should communicate the following qualities:

```text
Calm
Rigorous
Transparent
Local-first
Methodical
Trustworthy
Educational, but not childish
Open-source, but polished
Professional, but approachable
```

Ariadne should not feel like:

```text
A black-box AutoML product
A dark, intimidating data science IDE
A noisy analytics dashboard
A gamified learning app
A cloud-first SaaS that hides where data goes
A chatbot pretending to know more than the dataset shows
```

The user should feel guided, not constrained.

The product should say:

```text
Here is the next useful methodological step.
Here is what Ariadne computed.
Here is what still needs human review.
Here is what will be included in the report.
```

The product should not say:

```text
Your dataset is valid.
Your data is representative.
This model will work.
This preprocessing choice is correct.
This result proves the hypothesis.
```

Instead, use careful language:

```text
Suggested type
Needs review
Likely issue
Potential outlier
Missing context
Documented assumption
Evidence recorded
Ready for report
```

---

## 5. Brand direction

### 5.1 Name meaning

**Ariadne** refers to the thread that helps the user navigate a labyrinth.

The product metaphor is:

```text
AI/ML projects are labyrinths of data, assumptions, transformations, models and metrics.
Ariadne provides the thread: a structured path through the methodology.
```

### 5.2 Public product name

```text
Ariadne AI Workbench
```

### 5.3 Short product name

```text
Ariadne
```

### 5.4 Taglines

Preferred English tagline:

```text
Your thread through AI methodology.
```

Preferred French tagline:

```text
Le fil conducteur de vos projets IA.
```

### 5.5 Visual metaphors

Approved metaphors:

- thread;
- path;
- steps;
- evidence trail;
- workbench;
- notebook;
- compass;
- map;
- lab;
- sample vs population;
- review gates;
- report artifacts.

Avoid metaphors that imply automation without responsibility:

- magic;
- autopilot;
- oracle;
- one-click AI;
- black-box intelligence;
- guaranteed truth.

---

## 6. Visual identity

### 6.1 General visual direction

Ariadne should use a clean, restrained interface with strong hierarchy and low visual noise.

Recommended mood:

```text
Scientific notebook + modern desktop workbench + calm productivity tool
```

The UI should have enough personality to be memorable, but not so much styling that it distracts from data review.

### 6.2 Color strategy

Use semantic design tokens. Components must not hard-code raw color values except inside token definitions.

Recommended palette direction:

| Role | Direction | Usage |
|---|---|---|
| Background | Warm neutral / slate | Main surfaces, app shell |
| Primary | Indigo / deep blue | Main actions, active step, selection |
| Accent | Muted gold | Ariadne thread, progress highlights, brand detail |
| Success | Green | Completed review, successful import |
| Warning | Amber | Missing context, needs attention |
| Danger | Red | Import errors, data integrity risk |
| Info | Blue/cyan | Automated insight, neutral information |
| Neutral | Gray/slate | Borders, secondary text, dividers |

Example CSS token names:

```css
:root {
  --ari-color-bg-app: #f7f6f2;
  --ari-color-bg-surface: #ffffff;
  --ari-color-bg-subtle: #f1f0eb;
  --ari-color-text: #1f2933;
  --ari-color-text-muted: #667085;
  --ari-color-border: #d8d6cf;

  --ari-color-primary: #28346f;
  --ari-color-primary-hover: #1f2858;
  --ari-color-primary-soft: #eef0ff;

  --ari-color-accent: #c89b3c;
  --ari-color-accent-soft: #fff4d8;

  --ari-color-success: #2f7d52;
  --ari-color-warning: #b7791f;
  --ari-color-danger: #b42318;
  --ari-color-info: #2563eb;
}
```

These values are provisional. Codex may create initial tokens, but should avoid spreading raw colors across many files.

### 6.3 Dark mode

Dark mode is allowed but not required in the MVP.

If implemented, it must be token-driven:

```css
[data-theme="dark"] {
  --ari-color-bg-app: #101828;
  --ari-color-bg-surface: #182230;
  --ari-color-bg-subtle: #202939;
  --ari-color-text: #f9fafb;
  --ari-color-text-muted: #cbd5e1;
  --ari-color-border: #344054;
}
```

Do not implement dark mode by duplicating component styles.

### 6.4 Contrast

All text and interactive elements must maintain accessible contrast.

Rules:

- do not use color alone to communicate status;
- status chips must include text labels;
- warnings must include icon + title + body text;
- disabled states must remain readable;
- focus outlines must be visible in both light and dark themes.

---

## 7. Typography

Ariadne should use system fonts by default to avoid packaging external font files.

Recommended font stack:

```css
--ari-font-sans: system-ui, -apple-system, BlinkMacSystemFont, "Segoe UI", sans-serif;
--ari-font-mono: "Cascadia Mono", "SFMono-Regular", Consolas, "Liberation Mono", monospace;
```

### 7.1 Type scale

Use a modest type scale:

| Token | Size | Usage |
|---|---:|---|
| `--ari-font-size-xs` | 0.75rem | Tags, metadata |
| `--ari-font-size-sm` | 0.875rem | Secondary text |
| `--ari-font-size-md` | 1rem | Body, form fields |
| `--ari-font-size-lg` | 1.125rem | Card titles |
| `--ari-font-size-xl` | 1.375rem | Section headings |
| `--ari-font-size-2xl` | 1.75rem | Page headings |
| `--ari-font-size-3xl` | 2.25rem | Welcome/marketing headings |

### 7.2 Writing style in the UI

Use short, direct text.

Prefer:

```text
Review variable types
Document dataset context
Generate methodology report
```

Avoid:

```text
Perform advanced analytical validation of your current tabular data asset
```

Use nouns for sections and verbs for actions:

```text
Section: Dataset profile
Action: Review columns
Action: Add decision
Action: Export report
```

---

## 8. Spacing, layout and density

### 8.1 Spacing scale

Use a consistent spacing scale:

```css
--ari-space-1: 0.25rem;
--ari-space-2: 0.5rem;
--ari-space-3: 0.75rem;
--ari-space-4: 1rem;
--ari-space-5: 1.25rem;
--ari-space-6: 1.5rem;
--ari-space-8: 2rem;
--ari-space-10: 2.5rem;
--ari-space-12: 3rem;
```

### 8.2 Layout density

Ariadne is a workbench, so information density matters. However, early screens should not overwhelm the user.

Use three density levels:

| Density | Usage |
|---|---|
| Comfortable | Welcome, onboarding, project overview, fundamental analysis |
| Standard | Most forms, report preview, decision log |
| Compact | Dataset preview, column tables, metric tables |

The dataset preview grid can be compact. The methodology guidance should be comfortable.

### 8.3 Surfaces

Use three visual surface levels:

| Surface | Purpose |
|---|---|
| App background | Gives a calm base. |
| Primary surface/card | Contains actionable content. |
| Subtle inset surface | Contains metadata, code, warnings or previews. |

Avoid excessive nested cards.

---

## 9. Application shell

### 9.1 Shell intent

The shell must help users stay oriented in a multi-step methodology.

Desktop-first shell structure:

```text
+--------------------------------------------------------------+
| Top bar: Ariadne / current project / actions / settings       |
+----------------------+---------------------------------------+
| Project navigation   | Main content                          |
| Workflow steps       |                                       |
| Dataset              | Page header                           |
| Understand           | Page body                             |
| Analyze              | Context panel / right rail optional   |
| Report               |                                       |
+----------------------+---------------------------------------+
```

### 9.2 Top bar

The top bar should contain:

- product mark/name;
- current project name;
- local storage indicator;
- primary page action if global;
- settings/about menu.

Example:

```text
Ariadne    House Prices Study       Local project     Export report     Settings
```

### 9.3 Side navigation

The side navigation should show workflow sections, not technical implementation modules.

MVP navigation:

```text
Project overview
Dataset
Variable dictionary
Fundamental analysis
Decision log
Report
```

Future navigation:

```text
Technical analysis
Hypotheses
Preprocessing
Modelling
Evaluation
```

Each navigation item should support status:

```text
Not started
In progress
Needs review
Completed
Blocked
```

### 9.4 Breadcrumbs

Use breadcrumbs inside deep views, especially variable detail screens.

Example:

```text
Projects / House Prices Study / Dataset / surface_m2
```

### 9.5 Right-side context rail

On large screens, some pages may include a right-side context rail showing:

- methodology tip;
- current step status;
- report impact;
- related decisions;
- next action.

Do not show a right rail on small screens.

---

## 10. Information architecture

### 10.1 MVP information architecture

```text
Ariadne
├── Welcome / recent projects
├── Project
│   ├── Overview
│   ├── Dataset
│   │   ├── Import CSV
│   │   ├── Preview
│   │   └── Profile
│   ├── Variable dictionary
│   │   ├── Column list
│   │   └── Variable detail
│   ├── Fundamental analysis
│   │   ├── What
│   │   ├── Who
│   │   ├── When
│   │   ├── Where
│   │   ├── How
│   │   └── Why
│   ├── Decision log
│   └── Report
└── Settings / About
```

### 10.2 Future information architecture

```text
Project
├── Technical analysis
│   ├── Univariate
│   ├── Multivariate
│   └── Findings
├── Hypotheses
│   ├── Hypothesis registry
│   ├── Test selection
│   ├── Test conditions
│   └── Results
├── Preprocessing
│   ├── Missing values
│   ├── Outliers
│   ├── Encoding
│   ├── Feature engineering
│   ├── Feature selection
│   └── Normalization
├── Modelling
│   ├── Problem type
│   ├── Candidate models
│   ├── Training runs
│   └── Hyperparameters
├── Evaluation
│   ├── Metrics
│   ├── Confidence intervals
│   ├── Error analysis
│   └── Applicability
└── Final report
```

---

## 11. Workflow progress design

### 11.1 Workflow rail

Ariadne should visualize methodology progress as a **workflow rail**.

MVP workflow rail:

```text
1. Project frame
2. Dataset import
3. Dataset profile
4. Variable review
5. Fundamental analysis
6. Decision log
7. Report
```

Each step should show:

- label;
- status;
- completion count where relevant;
- blockers;
- primary next action.

Example:

```text
Variable review
12 / 18 columns reviewed
Needs attention
```

### 11.2 Status model

Use consistent statuses:

| Status | Meaning | Visual treatment |
|---|---|---|
| `NotStarted` | No meaningful work done | Neutral outline |
| `InProgress` | Some work done | Primary/active |
| `NeedsReview` | Automated or incomplete content requires human review | Warning |
| `Blocked` | User cannot proceed until issue is resolved | Danger |
| `Completed` | Required work is done | Success |
| `Optional` | Step is available but not required for current MVP | Muted |

### 11.3 Methodology gates

A gate is a point where the user is encouraged to complete a methodological step before continuing.

MVP gates should be soft, not hard, unless data integrity is at risk.

Example soft gate:

```text
You can generate a report now, but 6 variables still need review.
The report will mark them as unreviewed.
```

Example hard gate:

```text
No dataset has been imported. Dataset profile cannot be generated.
```

### 11.4 Progress must not imply scientific validity

Avoid labels like:

```text
Dataset validated
Analysis approved
Model ready
```

Prefer:

```text
Dataset imported
Profile computed
Variables reviewed
Context documented
Report generated
```

---

## 12. Core screens - MVP

### 12.1 Welcome / recent projects

Purpose:

- introduce Ariadne;
- open recent local projects;
- create a new project;
- make local-first behavior clear.

Primary content:

```text
Ariadne AI Workbench
Your thread through AI methodology.

Create a local project, import a dataset, document context and generate a methodology report.
```

Required elements:

- Create project button;
- Open project button;
- Recent projects list;
- Local-first note;
- Link to documentation/about.

Empty state:

```text
No local projects yet.
Create your first project to start documenting an AI/ML workflow.
```

Do not show modelling, metrics or advanced analytics on the welcome screen.

### 12.2 Project creation

Purpose:

- capture basic project framing;
- create a local project folder/database;
- guide the user to dataset import.

Fields:

| Field | Required | Notes |
|---|---:|---|
| Project name | Yes | Human-readable name. |
| Objective | Recommended | Short statement of the project goal. |
| Domain | Optional | Healthcare, finance, education, etc. |
| Owner/author | Optional | Useful for reports. |
| Project location | Optional in MVP | Default managed workspace. |

UX rules:

- Keep the form short.
- Explain that details can be edited later.
- After creation, direct user to dataset import.

### 12.3 Project overview

Purpose:

- summarize project state;
- show methodology progress;
- surface next recommended action.

Sections:

```text
Project summary
Methodology progress
Dataset summary
Open issues
Recent decisions
Report readiness
```

Example next action card:

```text
Next recommended step
Import a CSV dataset to begin profiling variables.
```

### 12.4 Dataset import

Purpose:

- import a CSV file;
- capture parsing options;
- explain local storage behavior.

Required UI elements:

- file picker/drop zone;
- delimiter option;
- encoding option if feasible;
- header row detection;
- decimal separator option if feasible;
- import preview;
- import button;
- privacy/local-first note.

Local-first copy:

```text
Your dataset stays on this device. Ariadne does not upload it by default.
```

Error cases:

| Case | Message style |
|---|---|
| File not found | Clear, actionable. |
| Unsupported format | Explain supported formats. |
| Empty file | Explain that no rows were detected. |
| Parse error | Show row/column if available. |
| Very large file | Warn that profiling may take time. |

### 12.5 Dataset preview

Purpose:

- let the user inspect the imported sample;
- confirm that parsing worked;
- provide quick dataset metadata.

Required content:

```text
Rows
Columns
Imported file name
Imported date
Dataset version
Hash/status if implemented
First N rows
Column headers
Detected data types
```

Grid behavior:

- read-only in MVP;
- horizontal scroll allowed;
- sticky header preferred;
- row numbers shown;
- null values visibly distinct;
- long text truncated with tooltip/details.

Do not perform data editing in the preview grid for the MVP.

### 12.6 Dataset profile

Purpose:

- show computed observations about the dataset;
- make automated inference reviewable.

Content:

```text
Dataset-level profile
Column-level profile list
Quality flags
Review progress
```

Column profile list should include:

| Item | Description |
|---|---|
| Column name | Original dataset column name. |
| Inferred type | Continuous, discrete, text, date, unknown. |
| User-reviewed type | Empty until reviewed. |
| Missing values | Count and percentage. |
| Unique values | Count. |
| Numeric stats | Min, max, mean, median when relevant. |
| Quality flags | High missingness, constant column, potential ID, etc. |
| Review status | Needs review / reviewed. |

### 12.7 Variable dictionary

Purpose:

- transform raw columns into documented variables;
- capture user interpretation.

Variable list should show:

```text
Column name
Display name
Inferred type
Reviewed type
Role
Unit
Description status
Missingness
Review status
```

Variable detail should include:

| Field | Purpose |
|---|---|
| Display name | Human-friendly label. |
| Description | What the variable represents. |
| Variable type | Discrete, continuous, text, date, identifier, target, unknown. |
| Unit | kg, EUR, seconds, m², etc. |
| Role | Feature, target, identifier, ignored, unknown. |
| Measurement notes | How the value was measured or collected. |
| Known limitations | Bias, missingness, suspected error, ambiguity. |
| Review status | Reviewed / needs review. |

UX rule:

```text
A variable is not considered reviewed until the user explicitly confirms it.
```

### 12.8 Fundamental analysis

Purpose:

- document the context surrounding the dataset;
- support the report;
- make unknowns explicit.

The screen should be organized around six sections:

```text
What
Who
When
Where
How
Why
```

Each section should include:

- short explanation;
- guided questions;
- free text answer;
- status: unanswered / partial / answered / unknown;
- optional notes and evidence/source field.

Example question groups:

#### What

```text
What does each row represent?
What does each important variable mean?
What units are used?
What is the target phenomenon?
```

#### Who

```text
Who collected the data?
Who maintains it?
Who will use the analysis?
Who might be affected by decisions based on it?
```

#### When

```text
When was the data collected?
How frequently is it updated?
Could it be outdated?
```

#### Where

```text
Where does the data come from?
Does geography affect representativeness?
Is the dataset limited to a region or market?
```

#### How

```text
How was the data collected?
Which tools, processes or sensors were used?
What quality controls existed?
Could the collection process introduce bias?
```

#### Why

```text
Why was the data originally collected?
Was it collected for this project or another purpose?
Could the initial motivation introduce bias?
```

Important UX rule:

```text
Unknown is a valid answer.
```

The UI should allow users to mark a field as unknown and explain why.

### 12.9 Decision log

Purpose:

- create a visible trail of methodological decisions.

Decision log item fields:

| Field | Required | Notes |
|---|---:|---|
| Title | Yes | Short decision name. |
| Category | Yes | Import, variable review, context, preprocessing, modelling, evaluation, report. |
| Decision | Yes | What was decided. |
| Rationale | Yes | Why it was decided. |
| Evidence | Optional | Link to profile, variable, context answer or note. |
| Consequence | Optional | What changes because of this decision. |
| Date | Auto | Stored locally. |

UI behavior:

- show most recent decisions first;
- allow filtering by category;
- show linked artifact when possible;
- allow edit/delete in MVP if no audit immutability has been implemented yet;
- future versions may support immutable decision history.

### 12.10 Report preview

Purpose:

- show what Ariadne will export;
- make missing sections visible;
- allow export to Markdown.

Report screen sections:

```text
Report readiness summary
Preview
Included sections
Missing context
Export actions
```

Readiness example:

```text
Report readiness
- Dataset imported: completed
- Dataset profile: completed
- Variables reviewed: 12 / 18
- Fundamental analysis: 4 / 6 sections answered
- Decision log: 3 decisions recorded
```

Export warning example:

```text
The report can be generated, but it will mention that some variables and context sections remain unreviewed.
```

### 12.11 Settings / About

MVP settings should be minimal:

```text
Workspace location
Theme preference if implemented
About Ariadne
Version
Open-source license
Documentation links
```

Avoid complex settings before the core workflow is useful.

---

## 13. Future screen guidelines

### 13.1 Technical analysis screens

Future technical analysis screens should map to the five analysis cases:

```text
Discrete variable alone
Continuous variable alone
Discrete × discrete
Discrete × continuous
Continuous × continuous
```

UX rules:

- start with a simple selector: choose variable(s);
- show appropriate statistics and charts;
- include interpretation prompts;
- allow saving a finding to the decision log or report;
- avoid overwhelming the user with every possible chart.

### 13.2 Hypothesis screens

Hypothesis screens should guide users through:

```text
Observation
Hypothesis H0
Alternative hypothesis H1
Data split / confirmation data
Test choice
Test conditions
Alpha threshold
p-value / result
Interpretation
Decision
```

UX rules:

- always show that p-value is conditional on H0;
- require the user to document the hypothesis before running or recording the test;
- warn when hypothesis and test are based on the same observed data;
- distinguish statistical significance from practical importance.

### 13.3 Preprocessing screens

Preprocessing screens should be structured by transformation type:

```text
Missing values
Outliers
Encoding
Feature engineering
Feature selection
Normalization
```

UX rules:

- every suggested transformation must be reviewable;
- destructive transformations must create a new dataset version;
- every accepted transformation should be loggable as a decision;
- show before/after summaries;
- make train/test leakage risks explicit in future modelling workflows.

### 13.4 Modelling screens

Modelling screens should be conservative and educational.

Recommended progression:

```text
Problem type
Target variable
Feature set
Baseline model
Simple candidate models
Training run
Comparison
Selection rationale
```

UX rules:

- prefer simple baseline first;
- show train/test split clearly;
- do not imply a model is good until evaluated;
- explain model family in plain language;
- store every run as an experiment artifact.

### 13.5 Evaluation screens

Future evaluation screens must follow:

```text
Evaluation = metric + confidence interval + applicability
```

UI rules:

- show metric values;
- show uncertainty/confidence intervals when implemented;
- show applicability limits;
- compare train and test performance;
- display overfitting/underfitting diagnosis;
- allow error analysis notes.

---

## 14. Reusable UI components

Reusable components belong in:

```text
src/Ariadne.SharedUi/
```

They must not depend directly on MAUI, SQLite, file system APIs or infrastructure services.

### 14.1 Component naming

Use clear names prefixed by product/domain meaning when helpful.

Recommended components:

```text
AppShell.razor
TopBar.razor
SideNavigation.razor
WorkflowRail.razor
WorkflowStepItem.razor
PageHeader.razor
SectionHeader.razor
StatusBadge.razor
ReviewBadge.razor
InfoCallout.razor
WarningCallout.razor
ErrorCallout.razor
InsightCard.razor
EvidenceCard.razor
MethodologyTip.razor
EmptyState.razor
LoadingState.razor
LocalFirstNotice.razor
DataPreviewGrid.razor
ColumnProfileTable.razor
ColumnProfileCard.razor
VariableTypeBadge.razor
VariableRoleBadge.razor
DecisionLogList.razor
DecisionLogItem.razor
FundamentalQuestionCard.razor
ReportReadinessPanel.razor
MarkdownPreview.razor
```

### 14.2 Component responsibilities

| Component | Responsibility |
|---|---|
| `AppShell` | Overall layout, navigation slots, responsive structure. |
| `WorkflowRail` | Shows methodology progress, not business logic. |
| `StatusBadge` | Displays status consistently. |
| `DataPreviewGrid` | Shows read-only table preview. |
| `ColumnProfileTable` | Displays column profiles and review status. |
| `VariableTypeBadge` | Displays variable type with label and accessible text. |
| `FundamentalQuestionCard` | Displays one fundamental analysis prompt group. |
| `DecisionLogItem` | Displays a decision and its metadata. |
| `ReportReadinessPanel` | Shows report completeness. |

Components should receive data via parameters. They should not load data directly.

### 14.3 Component anti-patterns

Do not create components that:

- call repositories directly;
- parse CSV files;
- compute column statistics;
- write to SQLite;
- know MAUI platform details;
- include large business logic branches;
- hide important state transitions inside event handlers.

Use application services and view models outside reusable components.

---

## 15. Forms and input design

### 15.1 Form principles

Forms in Ariadne should support thoughtful documentation.

Rules:

- use clear labels, not placeholder-only inputs;
- group related fields;
- provide helper text where methodology context matters;
- show validation errors near the relevant input;
- preserve user input if an error occurs;
- autosave only if clearly indicated;
- avoid large modal forms for complex documentation.

### 15.2 Required vs recommended fields

Distinguish required fields from recommended fields.

Example:

```text
Project name *
Objective recommended
```

Do not block report generation because recommended fields are missing. Instead, show missing sections as report gaps.

### 15.3 Long-form documentation fields

Fundamental analysis and decision rationale fields may be long.

Use:

- multiline text areas;
- autosave or explicit save indicators;
- markdown support later if useful;
- character counts only if necessary;
- prompt examples.

### 15.4 Unknown state

For context questions, users must be able to mark an answer as unknown.

Example UI:

```text
[ ] Mark as unknown
Reason: __________________________
```

This is important because unknowns should be documented, not hidden.

---

## 16. Tables and grids

### 16.1 Dataset preview grid

The dataset preview grid should prioritize readability and performance.

Rules:

- show a limited number of rows by default;
- make the limit explicit;
- show row and column counts outside the grid;
- support horizontal scroll;
- do not freeze the UI for large files;
- distinguish empty string, null and zero if possible;
- truncate long values but allow full value inspection.

Example note:

```text
Showing first 100 rows of 12,482.
```

### 16.2 Column profile table

Column profile tables should support scanning and review.

Columns:

```text
Column
Inferred type
Reviewed type
Missing
Unique
Stats
Flags
Status
Action
```

Use status badges and flags sparingly.

### 16.3 Sorting and filtering

MVP should support at least basic filtering if feasible:

```text
All columns
Needs review
High missingness
Numeric
Categorical
Potential identifiers
```

Sorting should be stable and obvious.

### 16.4 Empty and edge states

Examples:

```text
No dataset imported yet.
No columns need review.
No quality flags were detected.
This column has no numeric statistics because it was inferred as text.
```

---

## 17. Data visualization guidelines

The MVP can avoid complex charts. When charts are introduced, they must serve methodology, not decoration.

### 17.1 Chart principles

Rules:

- every chart needs a title;
- every axis needs a label;
- units should be visible when known;
- chart type should match variable type;
- avoid 3D charts;
- avoid misleading truncated axes unless clearly marked;
- show sample size;
- include missing value context where relevant;
- allow saving chart interpretation as a finding later.

### 17.2 Recommended chart mapping

| Analysis type | Recommended view |
|---|---|
| Discrete variable | Bar chart / frequency table |
| Continuous variable | Histogram / boxplot / summary stats |
| Discrete × discrete | Contingency table / grouped bar chart |
| Discrete × continuous | Grouped summary table / boxplot by category |
| Continuous × continuous | Scatter plot / correlation summary |

### 17.3 Visual uncertainty

Where uncertainty matters, future charts should visualize it:

- confidence intervals;
- error bars;
- sample size warnings;
- train/test separation;
- validation fold summaries.

Do not show a single metric as if it were certainty.

---

## 18. Status, badges and callouts

### 18.1 Status badges

Use badges for compact state representation.

Recommended labels:

```text
Imported
Profile computed
Needs review
Reviewed
Unknown
Missing context
Ready for report
Blocked
```

Avoid vague labels:

```text
Valid
Correct
Approved
Safe
Optimal
```

### 18.2 Callout types

Use callouts for important guidance.

| Type | Usage |
|---|---|
| Info | General methodology guidance. |
| Tip | Optional helpful guidance. |
| Warning | Risk, missing context, possible bias. |
| Error | Blocking problem. |
| Local-first | Privacy/storage note. |

Example warning:

```text
This column has 82% missing values. Removing it may be reasonable, but the decision should be justified using the dataset context.
```

### 18.3 Methodology tips

Methodology tips should be concise and contextual.

Example:

```text
A discrete variable is defined by countable values, not merely by being a category label.
```

Example:

```text
Unknown context is still useful context when it is documented explicitly.
```

---

## 19. Local-first UX

Ariadne's local-first nature must be visible and reassuring.

### 19.1 Local storage indicator

The shell should include a small local status indicator for projects.

Examples:

```text
Local project
Stored on this device
No cloud sync enabled
```

### 19.2 File location clarity

When useful, show project location:

```text
Project folder: /Users/julien/Ariadne/Projects/HousePrices
```

Do not overload every screen with path details.

### 19.3 Import privacy notice

During dataset import, display:

```text
Ariadne works locally by default. This file is not uploaded.
```

### 19.4 External services

If a future feature uses an external service, the UI must require explicit user consent and must clearly explain:

- what data is sent;
- where it is sent;
- why it is sent;
- how to disable it.

MVP rule:

```text
No remote AI or cloud service is required.
```

---

## 20. Accessibility guidelines

Ariadne must be usable by keyboard and assistive technologies.

### 20.1 General accessibility rules

Rules:

- every interactive element must be keyboard reachable;
- focus order must follow visual order;
- focus state must be visible;
- buttons must have accessible names;
- icon-only buttons must include accessible labels;
- form inputs must have labels;
- validation errors must be associated with inputs;
- status changes should be announced when important;
- color must not be the only status indicator;
- tables must use semantic headers;
- headings must follow a logical hierarchy.

### 20.2 Keyboard behavior

Minimum expected behavior:

| Interaction | Expected behavior |
|---|---|
| Tab | Move to next interactive element. |
| Shift+Tab | Move to previous interactive element. |
| Enter | Activate focused primary action. |
| Space | Toggle checkbox/button where appropriate. |
| Escape | Close menu/dialog if open. |
| Arrow keys | Navigate menus where implemented. |

### 20.3 Screen reader labels

Examples:

```html
<button aria-label="Review variable surface_m2">Review</button>
<span aria-label="Needs review">Needs review</span>
```

Do not rely on color-only icons.

### 20.4 Reduced motion

Avoid unnecessary animation. If animation is used:

- keep it short;
- respect reduced motion preferences where feasible;
- never animate core data tables in a way that disrupts reading.

### 20.5 Touch targets

For MAUI/mobile/tablet contexts, interactive controls should have comfortable touch targets.

Recommended minimum:

```text
44 × 44 CSS pixels for primary touch interactions
```

---

## 21. Responsive behavior

Ariadne is desktop-first for the MVP but should not break on smaller screens.

### 21.1 Breakpoint strategy

Suggested breakpoints:

```css
--ari-breakpoint-sm: 640px;
--ari-breakpoint-md: 768px;
--ari-breakpoint-lg: 1024px;
--ari-breakpoint-xl: 1280px;
```

### 21.2 Desktop layout

Desktop should show:

- persistent side navigation;
- wide main content;
- optional context rail;
- data grids with horizontal scroll when needed.

### 21.3 Tablet layout

Tablet should:

- collapse right rail;
- keep side navigation if width allows;
- use stacked cards for dense panels;
- keep dataset preview usable with horizontal scroll.

### 21.4 Mobile layout

Mobile is not the MVP priority.

Mobile behavior should be functional:

- navigation collapses into a drawer or menu;
- workflow rail becomes horizontal or stacked;
- data grids become scrollable;
- long forms remain usable;
- report preview stacks vertically.

Do not optimize advanced data exploration for small phones in the MVP.

---

## 22. Cross-platform MAUI Blazor considerations

Ariadne's UI is primarily Razor components hosted in MAUI Blazor Hybrid.

### 22.1 Shared UI principle

Reusable UI belongs in `Ariadne.SharedUi` so it can later be reused by `Ariadne.Web`.

The host-specific projects should only provide:

- startup/bootstrap;
- platform services;
- file picker integration;
- storage location selection;
- native window behavior;
- host-specific CSS or resources when unavoidable.

### 22.2 Avoid MAUI dependencies in shared Razor components

`Ariadne.SharedUi` must not directly depend on:

```text
Microsoft.Maui.*
SQLite implementation details
Local file system APIs
Platform-specific dialogs
```

Use abstractions from `Ariadne.Application` and host-level service registration.

### 22.3 CSS isolation

Use component-level CSS isolation for reusable components when it reduces collisions.

Recommended convention:

```text
Component.razor
Component.razor.css
```

Use global styles only for:

- tokens;
- layout primitives;
- typography;
- reset/base rules;
- utility classes if intentionally adopted.

### 22.4 Static assets

Shared assets should live in the RCL when they are part of shared UI.

Examples:

```text
Ariadne.SharedUi/wwwroot/images/
Ariadne.SharedUi/wwwroot/styles/
```

Host-specific icons and splash resources belong in the MAUI project.

---

## 23. Design tokens and CSS architecture

### 23.1 Token files

Recommended file organization:

```text
src/Ariadne.SharedUi/wwwroot/styles/
  ari-theme.css
  ari-tokens.css
  ari-base.css
  ari-layout.css
  ari-components.css
```

MVP can start with fewer files, but tokens should remain centralized.

### 23.2 CSS naming convention

Use predictable class names. Recommended prefix:

```text
ari-
```

Examples:

```css
.ari-shell
.ari-topbar
.ari-sidebar
.ari-page
.ari-card
.ari-status-badge
.ari-workflow-rail
.ari-data-grid
.ari-callout
```

### 23.3 Utility classes

Use utilities sparingly.

Acceptable:

```css
.ari-sr-only
.ari-text-muted
.ari-stack
.ari-cluster
```

Avoid creating an uncontrolled utility framework inside the project.

### 23.4 Component style rules

Do:

- use CSS variables;
- keep selectors simple;
- scope component-specific styles;
- test with long text;
- test with narrow layouts.

Do not:

- hard-code colors repeatedly;
- rely on pixel-perfect fragile layouts;
- hide overflow in ways that clip content;
- create inaccessible custom controls when native controls would work.

---

## 24. Interaction patterns

### 24.1 Primary actions

Each screen should have one obvious primary action.

Examples:

| Screen | Primary action |
|---|---|
| Welcome | Create project |
| Project overview | Continue next step |
| Dataset import | Import dataset |
| Dataset profile | Review variables |
| Variable detail | Mark as reviewed |
| Fundamental analysis | Save section |
| Report | Generate Markdown report |

Secondary actions should be visually quieter.

### 24.2 Confirmation dialogs

Use confirmation dialogs for destructive or hard-to-reverse actions.

Examples:

```text
Remove dataset version
Delete project
Delete decision log entry
Overwrite generated report
```

Dialogs must clearly describe what will happen.

### 24.3 Save behavior

MVP can use explicit save buttons.

Where autosave is implemented, show state:

```text
Saving...
Saved locally
Unsaved changes
Save failed
```

Do not silently lose long-form notes.

### 24.4 Undo

Undo is not required in the MVP.

For destructive actions, prefer confirmation and clear consequences.

---

## 25. Error handling UX

### 25.1 Error tone

Errors should be calm and actionable.

Bad:

```text
Import failed.
```

Good:

```text
Ariadne could not parse this CSV file. Check the delimiter or encoding, then try again.
```

### 25.2 Error structure

Use:

```text
What happened
Why it may have happened
What the user can do next
Technical details if useful
```

Example:

```text
CSV import failed
Ariadne could not detect a consistent number of columns.
Try selecting a different delimiter or checking row 124.
```

### 25.3 Recoverable errors

Recoverable errors should keep the user's work intact.

Examples:

- parsing option error;
- failed save;
- report export path unavailable;
- file moved after import if stored as reference.

### 25.4 Technical details

Technical details can be hidden behind a disclosure element:

```text
Show technical details
```

This keeps the UI approachable while helping developers and open-source contributors diagnose problems.

---

## 26. Empty states and onboarding

### 26.1 Empty state principles

Empty states should:

- explain what is missing;
- explain why it matters;
- provide the next action;
- avoid shame or blame.

Example:

```text
No dataset imported yet
Import a CSV file to compute a first dataset profile and start documenting variables.
```

### 26.2 Onboarding depth

Avoid a long onboarding wizard before the user can create a project.

Preferred approach:

```text
Short welcome
Create project
Contextual tips inside each step
```

### 26.3 Educational guidance

Ariadne may include methodology tips, but they should not turn screens into a course.

Keep tips:

- contextual;
- dismissible or unobtrusive;
- linked to the action at hand.

---

## 27. Report-oriented UX

Ariadne should constantly make clear that actions contribute to a final report.

### 27.1 Report impact indicator

On important screens, show how the current work affects the report.

Examples:

```text
This answer will appear in the Fundamental analysis section of the report.
```

```text
Unreviewed variables will be listed as report gaps.
```

### 27.2 Report readiness

Report readiness should not be a fake quality score.

Avoid:

```text
Report quality: 87%
```

Prefer:

```text
Report readiness
4 of 6 context sections answered
12 of 18 variables reviewed
3 decisions recorded
```

### 27.3 Missing information as report value

Ariadne should treat unknowns as honest methodology artifacts.

Example report warning:

```text
The data collection method is unknown. This limits the ability to assess measurement bias.
```

---

## 28. Copywriting guidelines

### 28.1 Voice

Ariadne's voice should be:

```text
Clear
Calm
Specific
Methodological
Honest about uncertainty
```

### 28.2 Avoid overclaiming

Do not write:

```text
Ariadne validated your dataset.
This model generalizes well.
This variable is irrelevant.
This outlier should be removed.
```

Write:

```text
Ariadne computed a dataset profile.
This model should be evaluated on unseen data.
This variable has not yet been reviewed.
This value may be an outlier and should be checked against context.
```

### 28.3 Use consistent terms

Preferred terms:

| Concept | Preferred UI term |
|---|---|
| Project | Project |
| Dataset file | Dataset |
| Dataset version | Version |
| Column | Column |
| Interpreted column | Variable |
| Automated type | Inferred type |
| User-confirmed type | Reviewed type |
| Missing context | Missing context |
| Unknown answer | Unknown |
| Methodological decision | Decision |
| Final artifact | Report |

Avoid switching between too many synonyms.

### 28.4 English first, localization-ready

The MVP UI can be English first.

However:

- keep text centralized where feasible;
- avoid hard-coding long strings inside deeply nested components;
- prepare for French localization later;
- avoid idioms that are hard to translate.

---

## 29. Accessibility and inclusivity in language

Ariadne should not assume every user is an expert data scientist.

Prefer accessible explanations:

```text
A continuous variable is usually measured on a numeric scale.
```

Instead of:

```text
This feature is continuous over an uncountable domain.
```

However, the product can include technical detail when needed.

Use progressive disclosure:

```text
What does this mean?
```

---

## 30. Performance UX

Large files and profiling can take time. The UI should make this understandable.

### 30.1 Loading states

Use specific loading messages:

```text
Reading CSV file...
Detecting columns...
Computing column profiles...
Saving project locally...
Generating Markdown report...
```

Avoid generic spinners without context.

### 30.2 Long-running work

For longer work:

- show progress if measurable;
- show current stage if not measurable;
- allow cancellation when safe;
- do not block the entire app unnecessarily;
- keep partial results only if consistent.

### 30.3 Large dataset warning

Example:

```text
This file contains 1.2M rows. Ariadne may profile a sample first to keep the interface responsive.
```

Sampling must be clearly disclosed.

---

## 31. Security and privacy UX

The UI should reinforce user trust.

### 31.1 No hidden upload behavior

The MVP must not suggest or imply cloud upload.

If future cloud features exist, the UI must explicitly ask before sending data.

### 31.2 Sensitive data reminder

During import, optional warning:

```text
Before importing, make sure you are allowed to store and analyze this dataset on this device.
```

### 31.3 Report export warning

If reports may include dataset metadata, context notes or variable names, make it clear.

Example:

```text
The exported report may include dataset name, column names, context notes and decision rationale.
```

---

## 32. Screen specification template

When adding a new screen, document it using this template:

```md
## Screen: <Name>

Purpose:
- ...

Primary user goal:
- ...

Entry points:
- ...

Primary action:
- ...

Secondary actions:
- ...

Required data:
- ...

Empty state:
- ...

Loading state:
- ...

Error states:
- ...

Accessibility notes:
- ...

Report impact:
- ...

Acceptance criteria:
- ...
```

Codex should use this template before implementing large new screens.

---

## 33. Implementation guidance for Codex

### 33.1 General rules

Codex must follow these UI implementation rules:

```text
Use Ariadne.SharedUi for reusable components.
Do not put business logic in Razor components.
Do not call repositories directly from components.
Do not put MAUI dependencies in SharedUi.
Use design tokens for colors and spacing.
Use accessible labels and semantic HTML.
Implement empty, loading and error states.
Keep screens methodology-first.
Keep UI copy careful about uncertainty.
```

### 33.2 Component creation rules

When creating a reusable component, Codex should include:

- `.razor` file;
- optional `.razor.css` file;
- parameters with clear names;
- no hidden infrastructure calls;
- basic null/empty state handling;
- accessible markup;
- a simple usage example in a page if needed.

### 33.3 Page creation rules

When creating a page, Codex should:

- add a clear page title;
- add a page-level primary action;
- include loading state;
- include empty state;
- include error state;
- include accessibility-friendly form labels;
- avoid fake data unless explicitly requested;
- use application services through dependency injection.

### 33.4 Styling rules

Codex should:

- create or reuse tokens;
- avoid inline styles unless absolutely necessary;
- use CSS isolation for component-specific styles;
- avoid global style leaks;
- test long project names and column names;
- test narrow layouts.

### 33.5 Copywriting rules for Codex

Codex should use language like:

```text
Inferred
Suggested
Needs review
Potential issue
Documented assumption
Missing context
```

Codex should avoid:

```text
Validated
Correct
Guaranteed
Optimal
Proven
```

---

## 34. UI testing strategy

### 34.1 Component tests

Use component tests where feasible for reusable components.

Test examples:

```text
StatusBadge renders the correct label.
WorkflowRail renders all steps in order.
EmptyState renders action when provided.
ColumnProfileTable renders missingness percentage.
FundamentalQuestionCard renders unknown state.
```

### 34.2 Accessibility checks

Manual and automated checks should verify:

- heading hierarchy;
- keyboard navigation;
- focus visibility;
- form labels;
- table headers;
- color-independent status indicators;
- dialog focus management when dialogs are introduced.

### 34.3 Responsive checks

At minimum, inspect:

```text
1280px desktop
1024px laptop/tablet landscape
768px tablet
390px mobile narrow fallback
```

### 34.4 Data stress checks

Use test data with:

- long project names;
- long column names;
- many columns;
- many missing values;
- special characters;
- empty strings;
- very wide CSV files.

---

## 35. MVP design acceptance criteria

The MVP UI is acceptable when:

- the user can create a local project;
- the user can import a CSV without confusion about local storage;
- the user can preview imported data;
- the user can understand profiling results at a glance;
- the user can review variable definitions;
- the user can complete What/Who/When/Where/How/Why context sections;
- the user can add decisions with rationale;
- the user can see report readiness;
- the user can generate a Markdown report;
- navigation clearly shows where the user is in the methodology;
- empty, loading and error states are implemented;
- the UI does not imply false certainty;
- reusable UI lives in `Ariadne.SharedUi`;
- core screens remain usable on desktop and do not break on tablet/mobile sizes;
- keyboard navigation is functional for the main workflow.

---

## 36. Non-goals for the MVP UI

The MVP UI should not attempt to implement:

- advanced charting library integration;
- drag-and-drop workflow builders;
- notebook execution;
- AutoML model comparison dashboards;
- collaborative comments;
- cloud workspace administration;
- account/profile management;
- real-time sync status;
- complex mobile-first data exploration;
- full dark mode if it delays core methodology workflow;
- polished marketing pages inside the app.

These can be added later if the open-source product gains usage.

---

## 37. Open design questions

These questions can remain open during early development:

1. Should the app UI be English-only for MVP or bilingual English/French?
2. Should Ariadne support Markdown input in long-form context answers?
3. Should generated reports be previewed as rendered Markdown or as raw Markdown first?
4. Should decision log entries become immutable after report generation?
5. Should project overview use cards, a timeline or both?
6. Should mobile be read-only first, with desktop as the primary editing experience?
7. Should the product include built-in methodology examples or keep examples in documentation?
8. Should visual identity include a custom Ariadne thread icon in the MVP?

---

## 38. Suggested initial UI implementation order

Recommended Codex implementation sequence:

```text
1. Create SharedUi base styles and design tokens.
2. Create AppShell, TopBar and SideNavigation.
3. Create StatusBadge, EmptyState, LoadingState and Callout components.
4. Create Welcome page.
5. Create Project overview layout.
6. Create Dataset import page skeleton.
7. Create Dataset preview grid component.
8. Create Column profile table component.
9. Create Variable dictionary screens.
10. Create Fundamental analysis cards.
11. Create Decision log UI.
12. Create Report readiness and Markdown preview UI.
```

Do not build advanced analytics screens before the MVP workflow is usable end-to-end.

---

## 39. External implementation references

These references should guide implementation details when needed:

| Topic | Reference |
|---|---|
| .NET MAUI `BlazorWebView` hosting model | https://learn.microsoft.com/en-us/dotnet/maui/user-interface/controls/blazorwebview?view=net-maui-10.0 |
| MAUI Blazor Hybrid + Blazor Web App with shared RCL | https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/tutorials/maui-blazor-web-app?view=aspnetcore-10.0 |
| RCL best practices for Blazor Hybrid and Web | https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/class-libraries-best-practices?view=aspnetcore-10.0 |
| Sharing assets across web and native clients with RCL | https://learn.microsoft.com/en-us/aspnet/core/blazor/hybrid/class-libraries?view=aspnetcore-10.0 |
| Blazor CSS isolation | https://learn.microsoft.com/en-us/aspnet/core/blazor/components/css-isolation?view=aspnetcore-10.0 |
| Razor components | https://learn.microsoft.com/en-us/aspnet/core/blazor/components/?view=aspnetcore-10.0 |
| .NET MAUI accessibility semantic properties | https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/accessibility?view=net-maui-10.0 |
| .NET MAUI theming and system theme changes | https://learn.microsoft.com/en-us/dotnet/maui/user-interface/theming?view=net-maui-10.0 |

---

## 40. Final guidance

Ariadne's UI must constantly reinforce one idea:

```text
Good AI work is not only about getting a model to run.
Good AI work is about understanding what the data means,
what assumptions were made,
what evidence exists,
what remains uncertain,
and what can responsibly be concluded.
```

The interface should therefore guide users through a visible thread:

```text
Project
→ Dataset
→ Variables
→ Context
→ Decisions
→ Report
```

The MVP should be simple, useful and honest.

Do not build a flashy AI dashboard. Build the first version of a trustworthy methodology workbench.
