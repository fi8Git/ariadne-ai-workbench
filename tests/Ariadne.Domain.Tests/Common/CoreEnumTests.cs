using Ariadne.Domain.Datasets;
using Ariadne.Domain.Decisions;
using Ariadne.Domain.FundamentalAnalysis;
using Ariadne.Domain.Methodology;
using Ariadne.Domain.Profiling;
using Ariadne.Domain.Projects;
using Ariadne.Domain.Reports;
using Ariadne.Domain.Variables;

namespace Ariadne.Domain.Tests.Common;

public class CoreEnumTests
{
    [Fact]
    public void CoreEnumsKeepDocumentedDefaultValues()
    {
        Assert.Equal(0, (int)ProjectStatus.Active);
        Assert.Equal(0, (int)MethodologyStep.Project);
        Assert.Equal(0, (int)MethodologyStepStatus.NotStarted);
        Assert.Equal(0, (int)PrimitiveDataType.Unknown);
        Assert.Equal(0, (int)MethodologicalVariableType.Unknown);
        Assert.Equal(0, (int)VariableRole.Unknown);
        Assert.Equal(0, (int)VariableReviewStatus.NeedsReview);
        Assert.Equal(0, (int)DataSourceKind.Unknown);
        Assert.Equal(0, (int)DecisionEntryType.Note);
        Assert.Equal(0, (int)DecisionStatus.Open);
        Assert.Equal(0, (int)KnowledgeStatus.Unknown);
        Assert.Equal(0, (int)FundamentalQuestionGroup.What);
        Assert.Equal(0, (int)ReportFormat.Markdown);
    }
}
