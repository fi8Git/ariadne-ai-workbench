# 05 - Methodology Workflow

# Ariadne AI Workbench

**Status:** Draft v0.1  
**Project type:** Open-source local-first application  
**Primary platform:** .NET 10 MAUI Blazor Hybrid  
**Future platform:** Blazor Web application, if useful and/or commercializable  
**Repository name:** `ariadne-ai-workbench`  
**Solution name:** `Ariadne.sln`  
**Root namespace:** `Ariadne`  
**Related documents:** `00-project-brief.md`, `01-product-vision.md`, `02-functional-specification.md`, `03-technical-architecture.md`, `04-domain-model.md`

---

## 1. Purpose of this document

This document defines the **methodology workflow** for **Ariadne AI Workbench**.

It translates Ariadne's product vision into a concrete, repeatable project journey that a user can follow from project creation to final reporting.

It is intended to guide:

- product behavior;
- UI navigation;
- domain workflow states;
- application use cases;
- report generation;
- Codex implementation tasks;
- open-source contribution discussions;
- future extension toward modelling, evaluation and commercial web workflows.

This document is not a screen-by-screen UI specification. UI design belongs in:

```text
07-ui-ux-guidelines.md
```

It is also not a storage specification. Persistence belongs in:

```text
06-local-first-storage.md
```

This file describes **what methodological path Ariadne enforces or encourages**, and how that path should be represented in the product.

---

## 2. Workflow thesis

Ariadne must help users avoid the most common failure mode in AI and machine learning projects:

```text
Dataset imported
→ Model trained too early
→ Metric looks acceptable
→ Context, assumptions, leakage, limitations and applicability remain unclear
```

Ariadne replaces this with a methodology-first path:

```text
Frame the project
→ Import the dataset
→ Profile the dataset
→ Review the variables
→ Document the context
→ Analyze the data
→ Formulate hypotheses
→ Prepare the data
→ Model carefully
→ Evaluate generalization
→ Report assumptions, evidence and limits
```

The first MVP implements only the beginning of this journey:

```text
Frame project
→ Import CSV
→ Preview dataset
→ Profile columns
→ Review variable dictionary
→ Complete fundamental analysis
→ Log decisions
→ Generate Markdown report
```

The future workflow extends the same path rather than replacing it.

---

## 3. Methodological principles

Ariadne's workflow must be built around the following principles.

### 3.1 A dataset is a sample, not reality

A dataset is not the full population. It is an observed sample, often incomplete, biased, outdated or shaped by the collection process.

The workflow must help the user answer:

```text
What phenomenon are we trying to understand?
What population do we care about?
What sample do we actually have?
How confident are we that the sample represents the population?
```

### 3.2 Understanding comes before modelling

Ariadne must not push the user toward model training before the dataset has been understood.

Understanding includes:

- technical profiling;
- variable meaning;
- source and collection context;
- missing values;
- outliers;
- potential leakage;
- target definition;
- representativeness;
- assumptions and limitations.

### 3.3 Technical analysis and fundamental analysis are complementary

Technical analysis answers questions such as:

```text
How many missing values are there?
What is the distribution of this column?
Are two variables correlated?
Are there outlier candidates?
```

Fundamental analysis answers questions such as:

```text
What does this variable mean?
Who collected the data?
When and where was it collected?
How was it measured?
Why was it collected?
Can the data support the intended use?
```

Ariadne must treat both as required parts of a trustworthy project.

### 3.4 Inferences are candidates

Ariadne may infer:

- primitive column types;
- methodological variable types;
- missing value severity;
- outlier candidates;
- possible target candidates;
- potential identifiers;
- possible high-cardinality categories;
- potential analysis warnings.

However, inferred results must be presented as **candidates** until reviewed by the user.

Ariadne should use language such as:

```text
Likely continuous
Potential identifier
Outlier candidate
Needs review
```

It must avoid language such as:

```text
This column is definitely continuous
This row is wrong
This variable must be removed
```

### 3.5 Unknown information is valid if explicit

A methodology report is more credible when it states what is unknown.

The workflow must allow users to mark answers as:

```text
Known
Partially known
Unknown
Not applicable
```

A required section can be considered methodologically acceptable when missing knowledge is explicitly documented as unknown, with a limitation or note where possible.

### 3.6 Decisions must be traceable

Every meaningful methodology decision should be recorded.

Examples:

```text
We treat `customer_id` as an identifier, not a feature.
We keep the `surface` column despite missing values because it is central to the problem.
We mark `price` as the target candidate.
We do not trust rows from before 2018 because the collection process changed.
We defer modelling because the data source is not yet documented.
```

Ariadne must make decision logging part of the workflow rather than an optional afterthought.

### 3.7 Avoid confirmation bias

When hypotheses are derived from observed data, Ariadne should warn against testing them on the same data without a proper confirmation strategy.

Future workflow must support:

```text
Observation data
→ hypothesis idea
→ confirmation data
→ hypothesis test
```

or:

```text
Train set exploration
→ validation/test set reserved for confirmation/evaluation
```

### 3.8 Prevent data leakage

When modelling features are implemented, Ariadne must enforce or strongly guide the following rule:

```text
Fit preprocessing rules on training data only.
Apply those rules to validation/test data without refitting.
```

Examples of leakage-prone operations:

- normalization using the whole dataset before train/test split;
- imputation statistics computed on the whole dataset;
- feature selection using the whole dataset;
- outlier thresholds computed with test data included;
- analysis of target distribution on test data before final evaluation;
- repeated model selection using the final test set.

### 3.9 Evaluation is more than a metric

Ariadne's evaluation philosophy is:

```text
Evaluation = metric + uncertainty + applicability
```

A metric alone is insufficient. A reliable evaluation should also describe:

- confidence interval or uncertainty estimate;
- train/test comparison;
- underfitting or overfitting diagnosis;
- error analysis;
- populations or situations where the model applies;
- populations or situations where the model does not apply;
- known limitations.

### 3.10 The workflow is iterative

Ariadne must not be a rigid waterfall.

The expected loop is:

```text
Analyze
→ decide
→ report
→ discover a gap
→ return to an earlier step
→ improve understanding
```

Later modelling workflows should also support:

```text
Evaluate model
→ discover weak segment
→ return to analysis
→ revise preprocessing or variables
→ re-evaluate
```

---

## 4. The Ariadne path

The product workflow is called the **Ariadne Path**.

The full Ariadne Path is:

```text
0. Project
1. Dataset
2. Understand
3. Analyze
4. Hypothesize
5. Prepare
6. Model
7. Evaluate
8. Report
```

This path maps to the `MethodologyStep` enum defined in `04-domain-model.md`:

```csharp
public enum MethodologyStep
{
    Project = 0,
    Dataset = 1,
    Understand = 2,
    Analyze = 3,
    Hypothesize = 4,
    Prepare = 5,
    Model = 6,
    Evaluate = 7,
    Report = 8
}
```

MVP active steps:

```text
Project
Dataset
Understand
Report
```

Future steps:

```text
Analyze
Hypothesize
Prepare
Model
Evaluate
```

Future steps can appear in the UI as locked, planned or not available, but they must not pretend to be implemented.

---

## 5. Workflow states

Every methodology step should have a status.

Use the status vocabulary from `04-domain-model.md`:

```csharp
public enum MethodologyStepStatus
{
    NotStarted = 0,
    InProgress = 1,
    NeedsReview = 2,
    Completed = 3,
    Deferred = 4,
    NotAvailable = 5
}
```

### 5.1 Status semantics

| Status | Meaning |
|---|---|
| `NotStarted` | The step exists but no meaningful work has been done. |
| `InProgress` | The user or system has started the step. |
| `NeedsReview` | The system has generated inferred or incomplete results that require user review. |
| `Completed` | The step has enough explicit information to be included as completed in the report. |
| `Deferred` | The step is intentionally postponed with a reason. |
| `NotAvailable` | The feature is not implemented in the current version. |

### 5.2 Completion does not mean perfection

A completed step does not mean all information is known or all issues are solved.

It means:

```text
The user has explicitly reviewed the step,
critical required items are either answered or marked unknown/not applicable,
and unresolved limitations are visible.
```

This is important for fundamental analysis. A user may not know who collected the dataset, but the answer can still be completed if the limitation is explicitly documented.

### 5.3 Deferred steps must include a reason

When a user defers a step, Ariadne must capture a reason.

Examples:

```text
Hypothesis testing deferred because MVP does not support statistical tests yet.
Modelling deferred because dataset documentation is incomplete.
Evaluation deferred because no target variable has been selected.
```

The report must include deferred steps and reasons.

---

## 6. Stage 0 - Project

### 6.1 Goal

The Project step defines the purpose and boundaries of the work.

A project should answer:

```text
What are we trying to understand, predict or decide?
Why does this project exist?
Who will use the result?
What would make the project useful?
What risks should be tracked from the start?
```

### 6.2 MVP user flow

```text
User launches Ariadne
→ clicks Create Project
→ enters name and optional description
→ confirms local project creation
→ project appears in dashboard
```

### 6.3 Inputs

Required:

- project name.

Optional in MVP:

- description;
- objective;
- project tags;
- notes;
- data sensitivity label;
- intended audience;
- expected decision or output.

### 6.4 Outputs

- `AiProject` aggregate;
- initial `MethodologyProgress`;
- first optional `DecisionLogEntry` if the user records a project assumption or objective.

### 6.5 Completion criteria

The Project step is `Completed` when:

- project name exists;
- project has been created successfully;
- project status is `Active`.

The Project step is `InProgress` when:

- project exists but core metadata is incomplete.

### 6.6 Recommended prompts

Ariadne may later guide users with prompts such as:

```text
What is the main objective of this project?
Is this an analysis project, a prediction project or a learning exercise?
Who is the intended user of the final report?
What decision could be made from the results?
What would make the project unreliable or unacceptable?
```

### 6.7 Codex implementation guidance

For the MVP:

- implement project creation before dataset import;
- keep project metadata simple;
- do not add user accounts;
- do not add cloud sync;
- do not add project templates unless explicitly requested;
- every created project should initialize methodology progress.

---

## 7. Stage 1 - Dataset

### 7.1 Goal

The Dataset step captures the data artifact that the project will analyze.

The user should know:

```text
Which file was imported?
What format was detected?
How many rows and columns exist?
Were parsing warnings found?
What is the initial preview?
What dataset version is being analyzed?
```

### 7.2 MVP user flow

```text
Open project
→ Import CSV
→ select local file
→ parse file
→ show preview
→ show import warnings if any
→ create dataset version
→ mark Dataset step as NeedsReview or Completed
```

### 7.3 Inputs

Required:

- local CSV file.

Detected or provided:

- file name;
- file size;
- delimiter;
- encoding;
- header presence;
- row count;
- column count;
- parsing warnings;
- content hash;
- import timestamp.

### 7.4 Outputs

- `Dataset` aggregate;
- `DatasetVersion` entity;
- `DatasetFileReference`;
- dataset preview model;
- import warnings;
- optional decision log entries for warnings or limitations.

### 7.5 Completion criteria

The Dataset step is `Completed` when:

- a dataset version has been imported;
- row count and column count are known;
- the preview is available;
- critical parsing errors are absent.

The Dataset step is `NeedsReview` when:

- parsing warnings exist;
- headers are missing or ambiguous;
- column count is inconsistent;
- row parsing was partially successful;
- duplicate column names were detected and renamed;
- import assumptions were made automatically.

The Dataset step remains `InProgress` when:

- import has started but not completed;
- parsing is pending;
- file metadata exists but no usable dataset version exists.

### 7.6 Import rules

Ariadne must not silently discard data during import.

If any rows or columns are skipped, the import process must record:

```text
What was skipped?
How many items were affected?
Why did it happen?
Can the user still continue?
```

### 7.7 Dataset versioning rule

A dataset import creates a version.

A future transformed dataset should not overwrite the original imported dataset.

Expected version flow:

```text
Raw import version
→ cleaned version
→ prepared version
→ modelling version
```

Only the raw import version is required in the MVP.

### 7.8 Codex implementation guidance

For the MVP:

- support CSV only;
- do not add Excel or Parquet until later;
- do not store large raw rows in domain aggregates;
- domain stores metadata and references only;
- infrastructure handles file storage and parsing;
- application use cases coordinate import;
- UI displays preview and warnings.

---

## 8. Stage 2 - Understand

### 8.1 Goal

The Understand step is the heart of the MVP.

It answers:

```text
What columns exist?
What types do they appear to have?
Which variables are discrete, continuous, identifiers, dates, text or targets?
What is known about their meaning?
What is unknown about their source, units and context?
Can the dataset be trusted enough to analyze further?
```

The Understand step includes three major activities:

```text
Dataset profiling
Variable dictionary review
Fundamental analysis
```

### 8.2 MVP user flow

```text
Dataset imported
→ run profiling
→ show column profile table
→ generate variable dictionary from profile
→ user reviews variable definitions
→ user completes fundamental analysis groups
→ user logs important assumptions and limitations
→ Understand step becomes Completed or NeedsReview
```

### 8.3 Substep A - Dataset profiling

#### Goal

Profiling creates a technical snapshot of the dataset.

It should identify:

- number of rows;
- number of columns;
- column names;
- inferred primitive type;
- inferred methodological type;
- missing value counts and ratios;
- distinct value counts;
- numeric statistics;
- top values for categorical/text-like columns;
- outlier candidates for numeric columns;
- warnings.

#### MVP column profile fields

Recommended profile fields:

```text
ColumnName
PrimitiveDataType
MethodologicalVariableType candidate
NonNullCount
MissingCount
MissingRatio
DistinctCount
DistinctRatio
Min
Max
Mean
Median
StandardDeviation
Q1
Q3
IQR
OutlierCandidateCount
TopValues
Warnings
```

#### Inference guidelines

Ariadne may infer:

```text
Integer → potentially discrete or continuous
Decimal → likely continuous
Text with low cardinality → likely discrete
Text with high cardinality → likely text or identifier
Date-like string → likely date/date-time
Unique column values → potential identifier
Column name such as price/target/label → target candidate only, never final target
```

#### Warnings

Examples of warnings:

```text
High missing value ratio
All values missing
All values identical
Potential identifier
Potential target leakage
High cardinality category
Numeric column with very few distinct values
Outlier candidates detected
Mixed parse types
```

#### Completion criteria

Profiling is complete when:

- every imported column has a `ColumnProfile`;
- profiling timestamp is recorded;
- warnings are persisted or available for report generation.

Profiling does not require user review. Review belongs to the variable dictionary.

### 8.4 Substep B - Variable dictionary review

#### Goal

The variable dictionary turns technical columns into user-reviewed methodological variables.

The user should clarify:

```text
What does this variable mean?
What is its unit?
Is it discrete or continuous?
Is it a feature, target, identifier, metadata or ignored?
Should the inferred type be corrected?
Is the variable trustworthy?
```

#### Required fields per variable

Minimum MVP fields:

```text
ColumnName
DisplayName
Description
MethodologicalType
Role
UnitOfMeasure
ReviewStatus
Notes
```

#### Variable roles

Use the `VariableRole` enum from `04-domain-model.md`:

```text
Unknown
Feature
Target
Identifier
Metadata
Ignored
```

#### Review rules

A variable begins as `NeedsReview`.

A variable can become `Reviewed` when:

- methodological type is confirmed or corrected;
- role is confirmed or corrected;
- important notes are added where needed.

A variable can become `Ignored` when:

- the user explicitly chooses to ignore it;
- a reason should be captured in notes or decision log.

#### Important methodological rule

A numeric variable is not automatically continuous.

Examples:

```text
age_in_years can be integer but continuous-like for modelling.
number_of_children is numeric but discrete.
postal_code is numeric-looking but categorical/identifier-like.
customer_id is numeric-looking but should not be treated as a continuous feature.
```

The UI should help users correct these cases.

#### Completion criteria

Variable dictionary is complete when:

- every column has a `VariableDefinition`;
- every variable is `Reviewed` or `Ignored`;
- at least one variable has a meaningful role unless the dataset is purely exploratory.

If many variables remain `NeedsReview`, the Understand step should be `NeedsReview`.

### 8.5 Substep C - Fundamental analysis

#### Goal

Fundamental analysis documents the context around the dataset.

It complements technical profiling by asking six groups of questions:

```text
What?
Who?
When?
Where?
How?
Why?
```

#### What

Purpose:

```text
Understand what the variables and rows represent.
```

Default questions:

```text
what.variables
what.units
what.entities
what.target_problem
```

Recommended prompts:

```text
What does each important variable represent?
What units are used?
What does one row represent: an individual, transaction, event, product, time period or something else?
What problem or phenomenon is the dataset supposed to represent?
```

#### Who

Purpose:

```text
Understand who collected, owns, manages and benefits from the data.
```

Default questions:

```text
who.collected_data
who.owns_data
who.benefits
who.subjects
```

Recommended prompts:

```text
Who collected the data?
Who owns or maintains it?
Who will benefit from the analysis?
Who are the people, organizations or entities represented in the data?
```

#### When

Purpose:

```text
Understand the temporal context and relevance of the data.
```

Default questions:

```text
when.collection_period
when.update_frequency
when.temporal_relevance
```

Recommended prompts:

```text
When was the data collected?
Is the dataset updated continuously, periodically or only once?
Could the data be obsolete?
Is there a temporal split that matters for modelling?
```

#### Where

Purpose:

```text
Understand geographic and contextual scope.
```

Default questions:

```text
where.geographic_origin
where.entity_location
where.generalization_scope
```

Recommended prompts:

```text
Where does the data come from geographically?
Are entities associated with locations?
Can conclusions generalize outside this location or context?
```

#### How

Purpose:

```text
Understand collection method, measurement process and data quality.
```

Default questions:

```text
how.collection_method
how.collection_protocol
how.quality_controls
how.tools_used
```

Recommended prompts:

```text
How was the data collected?
What protocol was followed?
What quality controls exist?
What tools or systems generated the data?
Could the collection method introduce bias?
```

#### Why

Purpose:

```text
Understand the motivation behind the dataset and possible bias from its original purpose.
```

Default questions:

```text
why.initial_purpose
why.intended_use
why.available
why.potential_bias
```

Recommended prompts:

```text
Why was the dataset originally collected?
Was it collected for the same purpose as this project?
Why is it available?
Could the original motivation introduce bias?
```

#### Knowledge status rules

Each answer should use:

```text
Unknown
PartiallyKnown
Known
NotApplicable
```

A `Known` answer must include answer text.

An `Unknown` answer should include a limitation where possible.

A `NotApplicable` answer should include a short explanation if the reason is not obvious.

#### Completion criteria

Fundamental analysis can be completed when:

- all six groups exist;
- all required questions are answered, marked unknown or marked not applicable;
- incomplete or unknown items are visible;
- the user has explicitly reviewed the sections.

A section should not require perfect knowledge.

### 8.6 Understand step completion criteria

The Understand step is `Completed` when:

- dataset profile exists;
- variable dictionary exists;
- variables are reviewed or ignored;
- fundamental analysis has all six groups reviewed;
- unresolved unknowns or limitations are explicitly documented.

The Understand step is `NeedsReview` when:

- profile exists but variable dictionary is incomplete;
- many variables remain `NeedsReview`;
- fundamental analysis contains unanswered required items;
- critical warnings exist without a decision log entry or notes.

The Understand step is `InProgress` when:

- profiling or variable review has started but is incomplete.

### 8.7 Codex implementation guidance

For the MVP:

- implement profiling before technical charts;
- implement variable dictionary before modelling;
- implement fundamental analysis before advanced EDA;
- use validation results for incomplete knowledge rather than throwing exceptions everywhere;
- ensure generated reports include missing knowledge.

---

## 9. Stage 3 - Analyze

### 9.1 Status in MVP

`Analyze` is a future step.

For the first MVP, set it to:

```text
NotAvailable
```

or leave it visible as a planned locked step.

### 9.2 Goal

The Analyze step performs technical analysis of variables and relationships.

It covers:

```text
Univariate analysis
Multivariate analysis
Exploratory visualizations
Descriptive statistics
Statistical test planning
```

### 9.3 Univariate analysis

Univariate analysis studies variables one by one.

#### Discrete variables

For discrete variables, Ariadne should support:

```text
Value counts
Frequency ratios
Bar chart
Top values
Rare category detection
Missing value distribution
```

Questions to answer:

```text
Which categories exist?
Are classes balanced?
Are there rare categories?
Are there unexpected values?
Are missing values concentrated in this variable?
```

#### Continuous variables

For continuous variables, Ariadne should support:

```text
Mean
Median
Variance
Standard deviation
Minimum
Maximum
Q1
Q3
IQR
Histogram
Box plot
Outlier candidates
```

Questions to answer:

```text
What is the distribution shape?
Is it skewed?
Are outliers present?
Does the unit make sense?
Are values plausible from a fundamental perspective?
```

### 9.4 Multivariate analysis

Multivariate analysis studies relationships between variables.

#### Discrete-discrete

Recommended outputs:

```text
Contingency table
Stacked/grouped bar chart
Chi-square independence test candidate
```

Questions:

```text
Are two categorical variables associated?
Are some combinations unexpectedly frequent or rare?
```

#### Discrete-continuous

Recommended outputs:

```text
Group-by summary
Grouped box plot
Grouped histogram
Student test candidate for two groups
Welch test candidate if variances differ
ANOVA candidate for more than two groups
```

Questions:

```text
Does the continuous variable distribution differ by category?
Are group means meaningfully different?
Are group variances comparable?
```

#### Continuous-continuous

Recommended outputs:

```text
Scatter plot
Correlation matrix
Pearson correlation candidate
Spearman correlation candidate
Outlier-sensitive warning
```

Questions:

```text
Is there a visible relationship?
Is it linear?
Is it monotonic?
Are outliers driving the correlation?
```

### 9.5 Analysis rules

Ariadne should follow these rules:

1. Start with simple summaries.
2. Do not generate too many charts at once.
3. Explain what a chart can and cannot prove.
4. Always link technical findings to variable definitions and fundamental context.
5. Allow users to convert an observation into a decision log entry or hypothesis idea.
6. For future modelling, analysis should be performed on train data only after a split exists.

### 9.6 Outputs

Future artifacts:

```text
TechnicalAnalysisRun
UnivariateAnalysisResult
MultivariateAnalysisResult
ChartSpec
AnalysisObservation
```

MVP alternative:

- use profiling results only;
- allow decision log entries with `RelatedStep = Analyze` even if the step is not implemented.

### 9.7 Completion criteria

Future `Analyze` step can be completed when:

- selected variables have univariate summaries;
- important relationships have been reviewed;
- significant observations have been logged;
- obvious data quality issues have either been resolved, documented or deferred.

---

## 10. Stage 4 - Hypothesize

### 10.1 Status in MVP

`Hypothesize` is a future step.

For the first MVP, set it to:

```text
NotAvailable
```

Decision log entries of type `HypothesisIdea` can still exist in the MVP.

### 10.2 Goal

The Hypothesize step helps users turn observations into explicit, testable hypotheses.

It should distinguish:

```text
Observation
Hypothesis idea
Null hypothesis H0
Alternative hypothesis H1
Test conditions
Confirmation strategy
Conclusion
```

### 10.3 Hypothesis lifecycle

Recommended future lifecycle:

```text
Draft
→ ReadyForReview
→ Planned
→ Tested
→ Confirmed
→ Rejected
→ Inconclusive
→ Deprecated
```

The precise enum belongs in a future domain extension.

### 10.4 Hypothesis sources

A hypothesis can originate from:

```text
Business/domain assumption
Technical analysis observation
Fundamental analysis concern
Data quality issue
Model error analysis
User intuition
External documentation
```

The source matters because hypotheses created after observing the data need stronger confirmation controls.

### 10.5 Frequentist hypothesis workflow

The standard workflow is:

```text
1. Define H0
2. Define H1 or expected deviation
3. Choose alpha threshold
4. Confirm test conditions
5. Run the test
6. Interpret p-value
7. Record conclusion and limitations
```

Ariadne should avoid reducing this to "p-value below threshold = truth".

The report must describe:

- what was tested;
- why the test was chosen;
- whether assumptions were satisfied;
- alpha threshold;
- result;
- interpretation;
- limitations.

### 10.6 Test selection guide

Future Ariadne should suggest tests based on variable types.

| Situation | Possible test | Purpose |
|---|---|---|
| One discrete variable vs theoretical proportion | Binomial test | Compare observed proportion to expected proportion. |
| One discrete variable vs theoretical distribution | Chi-square goodness-of-fit | Compare observed frequencies to expected frequencies. |
| One continuous variable vs theoretical mean | One-sample Student test | Compare observed mean to expected mean. |
| Two discrete variables | Chi-square independence | Test whether variables appear independent. |
| One discrete variable with two groups + one continuous variable | Student two-sample test | Compare two group means. |
| One discrete variable with two groups + one continuous variable with unequal variances | Welch test | Compare means when equal variance assumption is not met. |
| One discrete variable with more than two groups + one continuous variable | ANOVA | Compare multiple group means. |
| Two continuous variables | Pearson correlation | Test linear correlation. |
| Two continuous variables with monotonic/non-normal relation | Spearman correlation | Test rank correlation. |

### 10.7 Test condition checks

Ariadne should help users check assumptions.

Examples:

```text
Are observations independent?
Are samples from the same population?
Are groups large enough?
Do distributions approximately follow required assumptions?
Are variances comparable?
Is the target leakage-free?
Was the hypothesis formulated before or after observing this data?
```

### 10.8 Confirmation bias guard

If a hypothesis is created from the current dataset, Ariadne should warn:

```text
This hypothesis appears to be generated from observed data.
Testing it on the same data may create confirmation bias.
Use a confirmation split, validation set, new dataset or clearly mark the conclusion as exploratory.
```

### 10.9 Outputs

Future artifacts:

```text
Hypothesis
StatisticalTestPlan
StatisticalTestRun
HypothesisConclusion
```

MVP artifacts:

```text
DecisionLogEntry with Type = HypothesisIdea
```

### 10.10 Completion criteria

Future `Hypothesize` step can be completed when:

- hypotheses have explicit H0/H1 where applicable;
- test strategy is documented;
- confirmation bias risk is addressed;
- unresolved hypotheses are marked open or deferred.

---

## 11. Stage 5 - Prepare

### 11.1 Status in MVP

`Prepare` is a future step.

For the first MVP, set it to:

```text
NotAvailable
```

### 11.2 Goal

The Prepare step designs and documents preprocessing.

Preprocessing should be treated as part of modelling, not as a hidden utility.

It includes:

```text
Missing value handling
Outlier handling
Encoding
Feature engineering
Feature selection
Normalization or standardization
Train/test-safe transformer fitting
```

### 11.3 Recommended preprocessing sequence

Default sequence:

```text
1. Missing value diagnosis and handling
2. Outlier diagnosis and handling
3. Encoding
4. Feature engineering
5. Feature selection
6. Final normalization or standardization
```

Ariadne should allow exceptions but require documentation.

Example exception:

```text
Normalize before feature engineering because the feature transformation assumes centered data.
Then normalize again at the end of the pipeline.
```

### 11.4 Missing value workflow

Ariadne should guide the user through:

```text
How many missing values exist?
Where are they located?
Are they random, row-based, column-based or clustered?
Can they be recovered from source/context?
Should rows or columns be removed?
Is imputation justified?
What bias may be introduced?
```

Default philosophy:

```text
Prefer understanding and elimination before inventing values.
Use imputation as a documented fallback when necessary.
```

### 11.5 Outlier workflow

Ariadne should distinguish:

```text
Statistical outlier candidate
Fundamental outlier
Valid rare case
Data error
Out-of-scope population member
```

The system may detect outlier candidates using IQR in early versions.

The user must decide what they mean.

Important rule:

```text
Do not remove an outlier only because it is statistically distant.
Confirm whether it is outside the phenomenon being modelled.
```

### 11.6 Encoding workflow

Ariadne should guide users to distinguish:

```text
Ordinal categories
Nominal categories
Identifiers
Free text
High-cardinality categories
```

Encoding suggestions:

```text
Ordinal encoding for ordered categories.
One-hot encoding for nominal categories.
Ignore identifiers unless there is a specific, justified feature engineering strategy.
Consider dropping one one-hot column where multicollinearity matters.
```

### 11.7 Feature engineering workflow

Feature engineering should be guided by:

- domain knowledge;
- observed relationships;
- variable dictionary;
- modelling need;
- risk of overfitting.

Ariadne should warn when feature generation creates many variables without a rationale.

### 11.8 Feature selection workflow

Ariadne should encourage starting with variables the user understands.

Recommended principle:

```text
Start with a small, interpretable feature set.
Add features deliberately when they improve validated performance or explain a documented phenomenon.
```

### 11.9 Normalization workflow

Ariadne should ask:

```text
Does the chosen model use distances or gradients?
Are variable scales very different?
Are outliers present?
Should Min-Max scaling or standardization be used?
Is the target being normalized, and why?
```

### 11.10 Leakage prevention rule

For future implementation:

```text
Any preprocessing step with learned parameters must be fitted on the training set only.
Validation and test sets must use transform only.
```

Examples:

- imputation mean;
- standardization mean and standard deviation;
- Min-Max scaling min and max;
- one-hot category mapping;
- feature selection scores;
- outlier thresholds.

### 11.11 Outputs

Future artifacts:

```text
PreprocessingPipeline
PreprocessingStep
PreprocessingDecision
TransformationParameterSnapshot
PreparedDatasetVersion
```

### 11.12 Completion criteria

Future `Prepare` step can be completed when:

- preprocessing steps are ordered;
- each step has a rationale;
- train/test leakage rules are satisfied;
- outputs are reproducible;
- risks and approximations are documented.

---

## 12. Stage 6 - Model

### 12.1 Status in MVP

`Model` is a future step.

For the first MVP, set it to:

```text
NotAvailable
```

### 12.2 Goal

The Model step trains or records model candidates only after sufficient understanding and preparation.

Ariadne should support a methodology where simple models are preferred first.

### 12.3 Problem type selection

Before modelling, Ariadne should identify:

```text
Is there a target variable?
Is the target continuous or discrete?
Is this regression, classification, clustering, anomaly detection, dimension reduction or reinforcement learning?
Is the dataset supervised or unsupervised?
```

Initial implementation should focus on tabular supervised learning only when modelling is added.

### 12.4 Recommended first model families

For early modelling support, prioritize simple, interpretable baselines:

```text
Regression:
- linear regression
- decision tree regressor
- KNN regressor

Classification:
- logistic regression
- decision tree classifier
- KNN classifier
```

Do not add advanced modelling before the methodology workflow is useful.

### 12.5 Model run records

Each model run should record:

```text
Dataset version
Train/test split
Preprocessing pipeline
Target variable
Feature set
Model type
Hyperparameters
Training timestamp
Random seed
Metrics
Warnings
Notes
```

### 12.6 Hyperparameter strategy

Ariadne should guide users to avoid over-optimizing too early.

Future workflow:

```text
Train baseline
→ evaluate train/test
→ diagnose underfitting/overfitting
→ try simple improvements
→ use validation/cross-validation for hyperparameters
→ reserve final test set
```

### 12.7 Outputs

Future artifacts:

```text
ExperimentRun
ModelCandidate
ModelConfiguration
HyperparameterSet
ModelArtifactReference
```

### 12.8 Completion criteria

Future `Model` step can be completed when:

- at least one model candidate has been trained or documented;
- model configuration is reproducible;
- features, target and preprocessing are linked;
- hyperparameter selection process is documented.

---

## 13. Stage 7 - Evaluate

### 13.1 Status in MVP

`Evaluate` is a future step.

For the first MVP, set it to:

```text
NotAvailable
```

### 13.2 Goal

The Evaluate step determines whether a model generalizes and where it can be applied.

The guiding formula is:

```text
Evaluation = Metric + Confidence Interval + Applicability
```

### 13.3 Regression metrics

For regression, Ariadne should support:

```text
MAE
MSE
RMSE
R2
Error distribution
Residual analysis
```

Recommended interpretation:

- MAE is easiest to explain to non-technical stakeholders.
- R2 should not be used alone.
- Error distribution can reveal segments where the model fails.

### 13.4 Classification metrics

For classification, Ariadne should support:

```text
Confusion matrix
Accuracy
Precision
Recall
F1 score
Precision-recall curve
ROC curve
AUC
```

Ariadne should warn that accuracy can be misleading on imbalanced datasets.

### 13.5 Train/test diagnosis

Ariadne should compare train and test performance.

Basic diagnosis:

| Train performance | Test performance | Likely diagnosis |
|---|---|---|
| Poor | Poor | Underfitting or data/problem issue. |
| Good | Poor | Overfitting or leakage/shift issue. |
| Good | Good | Potentially good fit; still check applicability and uncertainty. |
| Poor | Good | Possible bug, split issue or unstable metric. |

### 13.6 Confidence intervals

Ariadne should present uncertainty around metrics.

For regression:

```text
Mean error ± confidence interval
Bootstrap or analytical interval where appropriate
```

For classification:

```text
Proportion metric ± confidence interval
```

The user should be able to see that a metric computed on a small test set may be highly uncertain.

### 13.7 Applicability analysis

Ariadne must help the user describe:

```text
Where does this model apply?
Where should it not be used?
Which population was represented in test data?
Which segments have weak performance?
What assumptions must remain true in production?
```

Examples:

```text
Applies to apartment listings in Paris from 2018-2024.
Does not apply to rural houses or agricultural buildings.
Performance is weaker for luxury properties above 2M EUR.
The model assumes the source system keeps the same measurement protocol.
```

### 13.8 Error analysis

Future Ariadne should help inspect errors by:

- variable ranges;
- categories;
- geography;
- time period;
- target magnitude;
- missing value patterns;
- outlier status.

### 13.9 Outputs

Future artifacts:

```text
EvaluationSummary
MetricResult
ConfidenceInterval
ConfusionMatrix
ErrorAnalysisResult
ApplicabilityStatement
```

### 13.10 Completion criteria

Future `Evaluate` step can be completed when:

- appropriate metrics are computed;
- train/test comparison is available;
- uncertainty is estimated or explicitly deferred;
- applicability scope is written;
- limitations are documented.

---

## 14. Stage 8 - Report

### 14.1 Goal

The Report step turns project work into a human-readable methodology report.

The report should not only show results. It should show evidence, assumptions, unknowns, limitations and decisions.

### 14.2 MVP report flow

```text
User opens Report section
→ Ariadne checks available project artifacts
→ user selects Markdown report
→ Ariadne generates report snapshot
→ file is saved locally
→ report metadata is stored
```

### 14.3 MVP report sections

The MVP Markdown report should include:

```text
1. Project summary
2. Dataset summary
3. Import warnings
4. Dataset profile summary
5. Variable dictionary
6. Fundamental analysis
7. Decision log
8. Known limitations
9. Deferred methodology steps
10. Next recommended steps
```

### 14.4 Future report sections

Future reports should add:

```text
Technical analysis
Hypotheses and statistical tests
Preprocessing pipeline
Model candidates
Evaluation metrics
Confidence intervals
Applicability statement
Error analysis
Experiment history
```

### 14.5 Report status

A report should include section completeness status.

Examples:

```text
Dataset: Completed
Understand: Needs review
Analyze: Not available
Hypothesize: Deferred
Report: Completed
```

This is why `MethodologyReportSnapshot` should store `ReportSectionStatus` values.

### 14.6 Missing knowledge rule

The report must show missing knowledge.

Example:

```text
Who collected the data?
Status: Unknown
Limitation: The source CSV does not include provider metadata.
Impact: The reliability of the dataset cannot be fully assessed.
```

Do not hide unknowns to make the report look cleaner.

### 14.7 Decision log inclusion

The report should include decision log entries grouped by type or methodology step.

Recommended grouping:

```text
Assumptions
Observations
Decisions
Limitations
Risks
Open questions
Hypothesis ideas
```

### 14.8 Completion criteria

The Report step is `Completed` when:

- a report snapshot has been generated;
- the report has a file reference;
- section statuses are recorded;
- generation timestamp is recorded.

The Report step is `NeedsReview` when:

- report generation succeeded but important sections are incomplete;
- report contains warnings that the user has not acknowledged.

---

## 15. Methodology gates

Ariadne should use **methodology gates** to guide, not punish, users.

A gate answers:

```text
Is it methodologically reasonable to move to the next step?
```

A gate should provide:

- status;
- blocking issues;
- warnings;
- recommended actions;
- option to continue with documented limitation where appropriate.

### 15.1 Gate severity

Recommended gate severity levels:

```text
Pass
Warning
Blocked
NotAvailable
```

### 15.2 Gate A - Dataset readiness

Before Understand.

Pass when:

- dataset version exists;
- row/column counts exist;
- no fatal import errors.

Warnings:

- parsing warnings;
- missing headers;
- duplicate headers;
- very small dataset;
- suspicious column count.

Blocked when:

- no dataset imported;
- import failed;
- no usable rows.

### 15.3 Gate B - Understanding readiness

Before Analyze.

Pass when:

- profile exists;
- variable dictionary is reviewed;
- fundamental analysis is reviewed;
- unknowns are explicit.

Warnings:

- important variables lack description;
- many unknown fundamental answers;
- target not selected;
- high missing values;
- potential identifiers still marked as features;
- potential leakage candidates.

Blocked when:

- no profile exists;
- no variable dictionary exists;
- no variables reviewed.

### 15.4 Gate C - Analysis readiness

Before Hypothesize or Prepare.

Future pass when:

- relevant univariate/multivariate analyses exist;
- important observations are logged;
- analysis limitations are explicit.

Warnings:

- analysis performed on full dataset after split should have been restricted to train set;
- correlations may be outlier-driven;
- distributions are heavily imbalanced;
- group sizes too small.

### 15.5 Gate D - Modelling readiness

Before Model.

Future pass when:

- target is selected for supervised learning;
- feature candidates are selected;
- train/test strategy exists;
- preprocessing pipeline exists;
- leakage checks pass.

Warnings:

- no validation strategy;
- high-dimensional feature set;
- many imputed values;
- target distribution imbalance;
- outliers removed without fundamental justification.

Blocked when:

- no target for supervised modelling;
- no train/test split;
- preprocessing fitted on full dataset;
- target leakage detected and unresolved.

### 15.6 Gate E - Evaluation readiness

Before final report or model acceptance.

Future pass when:

- test metrics exist;
- train/test comparison exists;
- uncertainty estimate exists or is explicitly deferred;
- applicability statement exists.

Warnings:

- small test set;
- wide confidence interval;
- poor performance on critical segment;
- only accuracy reported for imbalanced classification;
- R2 reported without error metric.

Blocked when:

- model evaluated on training data only;
- final test set was used for model selection without warning;
- no metric appropriate to problem type exists.

---

## 16. Methodology progress rules

### 16.1 Initial progress

When a project is created:

```text
Project = Completed or InProgress
Dataset = NotStarted
Understand = NotStarted
Analyze = NotAvailable
Hypothesize = NotAvailable
Prepare = NotAvailable
Model = NotAvailable
Evaluate = NotAvailable
Report = NotStarted
```

Alternative: if project metadata beyond name is required later, `Project` can start as `InProgress`.

### 16.2 After dataset import

When a dataset is imported successfully:

```text
Dataset = Completed or NeedsReview
Understand = NotStarted
Report = NotStarted
```

If import warnings exist:

```text
Dataset = NeedsReview
```

### 16.3 After profiling

When profiling is generated:

```text
Understand = NeedsReview
```

Reason: generated profiles require user review.

### 16.4 After variable dictionary review

If all variables are reviewed or ignored, and fundamental analysis is not complete:

```text
Understand = InProgress
```

If critical variable issues remain:

```text
Understand = NeedsReview
```

### 16.5 After fundamental analysis review

If profile exists, variables are reviewed, and fundamental analysis is explicit:

```text
Understand = Completed
```

If unknowns exist but are documented:

```text
Understand = Completed
```

If unknowns exist without review:

```text
Understand = NeedsReview
```

### 16.6 After report generation

When a report is generated:

```text
Report = Completed
```

If report contains warnings:

```text
Report = NeedsReview
```

This depends on product choice. Recommended MVP behavior:

```text
Report = Completed
with report warnings shown inside the snapshot.
```

---

## 17. Decision logging workflow

### 17.1 Goal

Decision logging records the reasoning behind the project.

It should answer:

```text
What did we decide?
Why did we decide it?
What evidence supports it?
What is the impact?
Which methodology step is affected?
Is the decision still valid?
```

### 17.2 Entry types

Use `DecisionEntryType` from `04-domain-model.md`:

```text
Note
Observation
Assumption
Decision
Limitation
Risk
Question
HypothesisIdea
```

### 17.3 Recommended user actions

From most workflow screens, the user should be able to:

```text
Add decision log entry
Convert warning to decision
Convert observation to hypothesis idea
Mark limitation
Resolve open question
Supersede previous decision
```

### 17.4 Examples

#### Variable decision

```text
Type: Decision
Title: Treat customer_id as identifier
Related step: Understand
Related artifact: Variable / customer_id
Rationale: The column is unique per row and has no meaningful numerical ordering.
Impact: High
Status: Confirmed
```

#### Fundamental limitation

```text
Type: Limitation
Title: Unknown collection protocol
Related step: Understand
Related artifact: FundamentalQuestion / how.collection_protocol
Rationale: The CSV was exported without documentation.
Impact: Medium
Status: Open
```

#### Hypothesis idea

```text
Type: HypothesisIdea
Title: Larger surface may be associated with higher price
Related step: Analyze
Related artifacts: Variable / surface, Variable / price
Rationale: Initial scatter plot suggests positive relationship.
Impact: Medium
Status: Open
```

### 17.5 Report behavior

Decision log entries must be included in reports.

Open risks and limitations should be especially visible.

---

## 18. Workflow UI model

### 18.1 Ariadne Path component

The UI should expose the methodology path as a persistent navigation element.

Suggested component:

```text
AriadnePath
```

It shows:

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

Each step displays:

- label;
- status;
- warning count;
- completion percentage if useful;
- lock/not available state.

### 18.2 Stage home page pattern

Each methodology step page should follow a consistent pattern:

```text
Step title
Why this step matters
Current status
Required actions
Warnings / blockers
Artifacts created in this step
Decision log entries related to this step
Next recommended action
```

### 18.3 Review-first UI

When Ariadne generates inferred data, the UI should require or encourage review.

Examples:

```text
Generated profile → Review variable dictionary
Detected outliers → Confirm whether they are true outliers
Generated report → Review warnings and missing knowledge
```

### 18.4 Empty states

Empty states should teach the methodology.

Example for no dataset:

```text
No dataset imported yet.
Ariadne starts by preserving your raw data and creating a dataset version.
Import a CSV to begin profiling and documentation.
```

Example for future Analyse step:

```text
Technical analysis is planned for a future version.
For now, use profiling and the decision log to document observations.
```

### 18.5 Warning language

Warnings should be precise and non-alarmist.

Good:

```text
This column has 92% missing values. Document whether it is still useful before modelling.
```

Avoid:

```text
This column is bad.
```

Good:

```text
This hypothesis was created after observing the dataset. Testing it on the same data may create confirmation bias.
```

Avoid:

```text
This hypothesis is invalid.
```

---

## 19. Application use cases

This section lists use cases that should exist or eventually exist in `Ariadne.Application`.

### 19.1 MVP use cases

```text
CreateProject
UpdateProjectMetadata
ArchiveProject
ImportCsvDataset
GetDatasetPreview
RunDatasetProfiling
CreateVariableDictionaryFromProfile
UpdateVariableDefinition
MarkVariableReviewed
CreateDefaultFundamentalAnalysis
AnswerFundamentalQuestion
MarkFundamentalQuestionUnknown
CompleteFundamentalSection
AddDecisionLogEntry
UpdateDecisionLogEntry
ResolveDecisionLogEntry
GetMethodologyProgress
ValidateMethodologyGate
GenerateMarkdownReport
```

### 19.2 Future use cases

```text
RunUnivariateAnalysis
RunMultivariateAnalysis
CreateHypothesis
PlanStatisticalTest
RunStatisticalTest
DefineTrainTestSplit
CreatePreprocessingPipeline
AddPreprocessingStep
ValidatePreprocessingLeakage
RunModelExperiment
CompareExperiments
EvaluateModel
GenerateConfidenceIntervals
CreateApplicabilityStatement
```

### 19.3 Use case design rules

- Use cases return explicit result objects.
- Validation warnings should be structured, not plain strings only.
- Use cases should not directly access UI components.
- Use cases should not depend on MAUI APIs.
- Use cases should not depend directly on SQLite details.
- Long-running operations should support `CancellationToken`.

---

## 20. Workflow validation model

Ariadne should use validation results for methodology checks.

Recommended concepts:

```text
MethodologyGateResult
MethodologyValidationIssue
MethodologyIssueSeverity
MethodologyRecommendation
```

### 20.1 Suggested severity enum

```csharp
public enum MethodologyIssueSeverity
{
    Info = 0,
    Warning = 1,
    Blocking = 2
}
```

This enum does not need to be in MVP domain unless implementation requires it. It may live in Application initially.

### 20.2 Suggested validation issue fields

```text
Code
Severity
Title
Description
RelatedStep
RelatedArtifact
SuggestedAction
CanContinueWithLimitation
```

Example:

```text
Code: UNDERSTAND_VARIABLES_NEED_REVIEW
Severity: Warning
Title: Some variables still need review
Description: 7 variables have not been reviewed or ignored.
RelatedStep: Understand
SuggestedAction: Review the variable dictionary or mark the variables as ignored.
CanContinueWithLimitation: true
```

### 20.3 Blocking vs warning

A blocking issue prevents the workflow from producing a meaningful artifact.

Example:

```text
No dataset imported → cannot run profiling.
```

A warning allows continuation but must be visible.

Example:

```text
Dataset source is unknown → report can be generated but must include limitation.
```

---

## 21. Report-driven workflow

Ariadne is report-driven.

This means the workflow should always consider:

```text
How will this appear in the final report?
```

### 21.1 Every step should produce reportable artifacts

| Step | Reportable artifact |
|---|---|
| Project | Project summary and objective. |
| Dataset | Dataset import summary and warnings. |
| Understand | Profile, variable dictionary and fundamental analysis. |
| Analyze | Charts, statistics and observations. |
| Hypothesize | Hypotheses, tests and conclusions. |
| Prepare | Preprocessing pipeline and rationale. |
| Model | Model candidates and configurations. |
| Evaluate | Metrics, intervals, diagnostics and applicability. |
| Report | Snapshot metadata and completeness. |

### 21.2 Reports should be reproducible

A report snapshot should record:

```text
Project ID
Dataset version ID
Profile run ID
Variable dictionary ID
Fundamental analysis ID
Decision log entries included
Generated file path/reference
Generated timestamp
Section statuses
```

### 21.3 Reports should include next actions

The MVP report should end with recommendations such as:

```text
Review remaining variables.
Document unknown data source.
Investigate high missing values in column X.
Decide whether column Y is an identifier or feature.
Proceed to technical analysis when available.
```

---

## 22. Open-source workflow considerations

Ariadne is open-source first.

The methodology workflow should be understandable to contributors who are not professional data scientists.

### 22.1 Contributor-friendly design

Every methodology concept should have:

```text
Clear name
Short explanation
Examples
Acceptance criteria
Tests
```

### 22.2 Avoid hidden magic

The application should avoid "smart" hidden automation that is hard to inspect.

Good:

```text
Ariadne inferred this column as Discrete because it has 4 unique values across 10,000 rows.
```

Bad:

```text
Ariadne classified this variable automatically.
```

### 22.3 Enable progressive enhancement

The workflow should allow simple implementations first.

Example:

```text
MVP: IQR outlier candidate count.
Later: visual box plot.
Later: multivariate anomaly detection.
Later: LOF/Isolation Forest integration.
```

---

## 23. Commercial/web future compatibility

The workflow should work locally first but remain compatible with a future web/commercial version.

### 23.1 Local-first assumptions

MVP:

```text
Single user
Local project files
Local SQLite database later
Local report generation
No authentication
No cloud sync
```

### 23.2 Future web assumptions

Potential commercial version:

```text
Multiple users
Workspaces
Project sharing
Team decision log
Cloud storage
PostgreSQL
Authentication
Role-based access
Report collaboration
Templates
```

### 23.3 Workflow compatibility rule

Do not encode local-only assumptions into the core domain workflow.

Good:

```text
ProjectId owns methodology progress.
DecisionLogEntry has ProjectId and timestamps.
```

Bad:

```text
DecisionLogEntry assumes one local machine user forever.
```

Future collaboration can add authorship, comments and review approvals without changing the methodology path.

---

## 24. MVP workflow acceptance criteria

The first MVP should satisfy the following end-to-end acceptance scenario.

### 24.1 Happy path

```text
Given Ariadne is installed locally
When the user creates a project named "Housing price analysis"
And imports a CSV dataset
And views the dataset preview
And runs profiling
And reviews variables
And completes fundamental analysis
And adds at least one decision log entry
And generates a Markdown report
Then the project should contain a completed local methodology trail
And the report should include dataset, profile, variables, fundamental analysis and decisions
```

### 24.2 Dataset warning path

```text
Given a CSV import produces parsing warnings
When the import completes with usable rows
Then the Dataset step should be NeedsReview
And the warnings should be visible in the UI
And the warnings should appear in the report
```

### 24.3 Variable review path

```text
Given profiling inferred variable types
When the variable dictionary is created
Then all variables should start as NeedsReview
When the user reviews or ignores every variable
Then the variable dictionary should be considered reviewed
```

### 24.4 Fundamental unknown path

```text
Given the user does not know who collected the data
When the user marks who.collected_data as Unknown with a limitation
Then the fundamental analysis can still be completed
And the report should include the unknown status and limitation
```

### 24.5 Report path with incomplete sections

```text
Given the user generates a report before completing all sections
Then the report should still be generated if enough core data exists
And incomplete sections should be marked as NotStarted, NeedsReview or Deferred
And the report should not pretend the methodology is complete
```

---

## 25. Testing strategy for workflow logic

### 25.1 Domain tests

Test:

```text
MethodologyProgress initializes all MVP and future steps correctly.
MethodologyProgress prevents duplicate steps.
AiProject initializes progress on creation.
VariableDefinition starts as NeedsReview.
FundamentalAnalysis contains six groups.
DecisionLogEntry requires a title and project ID.
```

### 25.2 Application tests

Test:

```text
ImportCsvDataset creates dataset version metadata.
RunDatasetProfiling creates one ColumnProfile per column.
CreateVariableDictionaryFromProfile creates one VariableDefinition per profile column.
ValidateMethodologyGate returns blocking issue when no dataset exists.
ValidateMethodologyGate returns warnings for incomplete variable review.
GenerateMarkdownReport includes incomplete/unknown sections.
```

### 25.3 UI behavior tests

Where practical, test:

```text
AriadnePath displays step statuses.
Dataset page shows import warnings.
Variable dictionary page shows NeedsReview variables.
Fundamental analysis page supports Known/Unknown/NotApplicable statuses.
Report page shows generated file reference.
```

### 25.4 Golden file tests for reports

Report generation should use golden file tests when stable.

Example:

```text
Given sample project fixture
When Markdown report is generated
Then output matches expected Markdown structure
```

Avoid brittle tests for timestamps and generated IDs.

---

## 26. Codex implementation rules

Codex must follow these rules when implementing workflow-related features.

### 26.1 General rules

- Do not skip methodology steps for convenience.
- Do not add modelling before MVP workflow is implemented.
- Do not put workflow logic in Razor components.
- Do not put domain decisions in infrastructure.
- Do not persist raw dataset rows inside domain aggregates.
- Do not mark inferred results as final without user review.
- Do not hide missing knowledge from reports.

### 26.2 Status rules

- Use `MethodologyStep` and `MethodologyStepStatus` consistently.
- Future steps should be `NotAvailable` until implemented.
- `NeedsReview` should be used whenever generated or incomplete information requires user validation.
- `Completed` should mean explicit review, not perfect data.

### 26.3 MVP scope rules

For MVP, Codex should implement:

```text
Project
Dataset
Understand
Report
```

Codex should not implement unless explicitly requested:

```text
Technical charts
Statistical tests
Train/test split
Preprocessing execution
ML.NET model training
Python integration
Cloud sync
User accounts
Commercial licensing
```

### 26.4 Report rules

Generated reports must:

- include section statuses;
- include unknowns and limitations;
- include decision log entries;
- include dataset warnings;
- avoid claiming unsupported conclusions.

### 26.5 Naming rules

Use these names consistently:

```text
Ariadne Path
MethodologyStep
MethodologyProgress
DecisionLogEntry
VariableDictionary
FundamentalAnalysis
DatasetProfile
MethodologyReportSnapshot
```

Do not introduce synonyms such as:

```text
WorkflowStage
ProjectPhase
StudyStep
```

unless there is a strong reason and the domain model is updated accordingly.

---

## 27. Future extensions

The workflow is designed for the following future extensions.

### 27.1 Technical analysis module

Add:

```text
Univariate analysis
Multivariate analysis
Chart specs
Analysis observations
Correlation matrix
Contingency tables
Grouped statistics
```

### 27.2 Hypothesis testing module

Add:

```text
Hypothesis backlog
H0/H1 editor
Test recommendation
Assumption checks
p-value interpretation
Confirmation bias warnings
Conclusion recording
```

### 27.3 Preprocessing module

Add:

```text
Pipeline builder
Missing value decisions
Outlier decisions
Encoding decisions
Feature engineering
Feature selection
Normalization
Leakage validation
```

### 27.4 Modelling module

Add:

```text
ML.NET baseline models
Experiment runner
Model configuration tracking
Hyperparameter comparison
Validation strategy
```

### 27.5 Evaluation module

Add:

```text
Regression metrics
Classification metrics
Confidence intervals
Train/test diagnostics
Error analysis
Applicability statement
```

### 27.6 Web/commercial module

Add:

```text
Shared workspaces
Team review
Report comments
Cloud projects
Premium templates
Export formats
Advanced connectors
```

---

## 28. Non-goals

Ariadne's methodology workflow is not intended to be:

- a fully automatic AutoML system;
- a replacement for domain expertise;
- a guarantee that a dataset is unbiased;
- a guarantee that a model is production-ready;
- a statistical testing engine in the MVP;
- a notebook environment in the MVP;
- a cloud collaboration platform in the MVP;
- a black-box AI assistant that makes decisions for the user.

Ariadne should guide, structure and document the work. The user remains responsible for the final interpretation.

---

## 29. Summary

The Ariadne methodology workflow is a guided path from uncertainty to documented reasoning.

The full path is:

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

The MVP implements:

```text
Project
→ Dataset
→ Understand
→ Report
```

The key design requirement is that Ariadne must make methodology visible:

```text
Data is sampled.
Types are inferred.
Variables need review.
Context matters.
Unknowns must be documented.
Decisions require rationale.
Reports must show limitations.
Models come later.
```

If implemented well, Ariadne will not merely help users build AI/ML projects faster. It will help them build projects that are easier to trust, explain, reproduce and improve.
