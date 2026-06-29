using Ariadne.Domain.Common;
using Ariadne.Domain.Methodology;

namespace Ariadne.Domain.Tests.Methodology;

public class MethodologyProgressTests
{
    private static readonly DateTimeOffset Now = new(2026, 6, 26, 12, 0, 0, TimeSpan.Zero);

    [Fact]
    public void CreateForMvpInitializesAllDocumentedSteps()
    {
        MethodologyProgress progress = MethodologyProgress.CreateForMvp(Now);

        Assert.Equal(9, progress.Steps.Count);
        Assert.Equal(MethodologyStepStatus.Completed, progress[MethodologyStep.Project].Status);
        Assert.Equal(Now, progress[MethodologyStep.Project].CompletedAtUtc);
        Assert.Equal(MethodologyStepStatus.NotStarted, progress[MethodologyStep.Dataset].Status);
        Assert.Equal(MethodologyStepStatus.NotStarted, progress[MethodologyStep.Understand].Status);
        Assert.Equal(MethodologyStepStatus.NotStarted, progress[MethodologyStep.Report].Status);
        Assert.Equal(MethodologyStepStatus.NotAvailable, progress[MethodologyStep.Analyze].Status);
        Assert.Equal(MethodologyStepStatus.NotAvailable, progress[MethodologyStep.Hypothesize].Status);
        Assert.Equal(MethodologyStepStatus.NotAvailable, progress[MethodologyStep.Prepare].Status);
        Assert.Equal(MethodologyStepStatus.NotAvailable, progress[MethodologyStep.Model].Status);
        Assert.Equal(MethodologyStepStatus.NotAvailable, progress[MethodologyStep.Evaluate].Status);
    }

    [Fact]
    public void WithStatusReturnsUpdatedCopyWithoutMutatingOriginal()
    {
        DateTimeOffset updatedAt = Now.AddMinutes(5);
        MethodologyProgress original = MethodologyProgress.CreateForMvp(Now);

        MethodologyProgress updated = original.WithStatus(
            MethodologyStep.Dataset,
            MethodologyStepStatus.Completed,
            updatedAt,
            " reviewed ");

        Assert.Equal(MethodologyStepStatus.NotStarted, original[MethodologyStep.Dataset].Status);
        Assert.Equal(MethodologyStepStatus.Completed, updated[MethodologyStep.Dataset].Status);
        Assert.Equal(updatedAt, updated[MethodologyStep.Dataset].UpdatedAtUtc);
        Assert.Equal(updatedAt, updated[MethodologyStep.Dataset].CompletedAtUtc);
        Assert.Equal("reviewed", updated[MethodologyStep.Dataset].Notes);
    }

    [Fact]
    public void CreateWithDuplicateStepsThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => new MethodologyProgress(
                [
                    new MethodologyStepProgress(MethodologyStep.Project, MethodologyStepStatus.NotStarted, Now),
                    new MethodologyStepProgress(MethodologyStep.Project, MethodologyStepStatus.Completed, Now, Now),
                ]));
    }

    [Fact]
    public void CompleteUnavailableMvpStepThrowsDomainException()
    {
        MethodologyProgress progress = MethodologyProgress.CreateForMvp(Now);

        Assert.Throws<DomainException>(
            () => progress.WithStatus(MethodologyStep.Model, MethodologyStepStatus.Completed, Now.AddMinutes(1)));
    }

    [Fact]
    public void CompletedStepRequiresCompletionTimestamp()
    {
        Assert.Throws<DomainException>(
            () => new MethodologyStepProgress(
                MethodologyStep.Project,
                MethodologyStepStatus.Completed,
                Now));
    }
}
