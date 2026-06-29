using Ariadne.Domain.Common;

namespace Ariadne.Domain.Methodology;

public sealed record MethodologyProgress
{
    private static readonly MethodologyStep[] _mvpSteps =
    [
        MethodologyStep.Project,
        MethodologyStep.Dataset,
        MethodologyStep.Understand,
        MethodologyStep.Report,
    ];

    private readonly Dictionary<MethodologyStep, MethodologyStepProgress> _stepsByStep;

    public MethodologyProgress(IEnumerable<MethodologyStepProgress> steps)
    {
        ArgumentNullException.ThrowIfNull(steps);

        MethodologyStepProgress[] stepArray = [.. steps];

        if (stepArray.Length == 0)
            throw new DomainException("Methodology progress requires at least one step.");

        MethodologyStep[] duplicateSteps = [.. stepArray
            .GroupBy(progress => progress.Step)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)];

        if (duplicateSteps.Length > 0)
            throw new DomainException("Every methodology step must appear at most once.");

        _stepsByStep = stepArray.ToDictionary(progress => progress.Step);
        Steps = [.. stepArray.OrderBy(progress => progress.Step)];
    }

    public IReadOnlyCollection<MethodologyStepProgress> Steps { get; }

    public MethodologyStepProgress this[MethodologyStep step]
    {
        get
        {
            if (!_stepsByStep.TryGetValue(step, out MethodologyStepProgress? progress))
                throw new DomainException($"Methodology step '{step}' is not tracked.");

            return progress;
        }
    }

    public static MethodologyProgress CreateForMvp(DateTimeOffset now)
        => new(
            [
                new MethodologyStepProgress(MethodologyStep.Project, MethodologyStepStatus.Completed, now, now),
                new MethodologyStepProgress(MethodologyStep.Dataset, MethodologyStepStatus.NotStarted, now),
                new MethodologyStepProgress(MethodologyStep.Understand, MethodologyStepStatus.NotStarted, now),
                new MethodologyStepProgress(MethodologyStep.Analyze, MethodologyStepStatus.NotAvailable, now),
                new MethodologyStepProgress(MethodologyStep.Hypothesize, MethodologyStepStatus.NotAvailable, now),
                new MethodologyStepProgress(MethodologyStep.Prepare, MethodologyStepStatus.NotAvailable, now),
                new MethodologyStepProgress(MethodologyStep.Model, MethodologyStepStatus.NotAvailable, now),
                new MethodologyStepProgress(MethodologyStep.Evaluate, MethodologyStepStatus.NotAvailable, now),
                new MethodologyStepProgress(MethodologyStep.Report, MethodologyStepStatus.NotStarted, now),
            ]);

    public bool TryGetStep(MethodologyStep step, out MethodologyStepProgress? progress)
        => _stepsByStep.TryGetValue(step, out progress);

    public MethodologyProgress WithStatus(
        MethodologyStep step,
        MethodologyStepStatus status,
        DateTimeOffset now,
        string? notes = null)
    {
        MethodologyStepProgress current = this[step];
        if (current.Status == MethodologyStepStatus.NotAvailable && status == MethodologyStepStatus.Completed)
            throw new DomainException("Not available methodology steps cannot be marked complete.");

        MethodologyStepProgress updated = current.WithStatus(status, now, notes);

        return new MethodologyProgress(Steps.Select(progress => progress.Step == step ? updated : progress));
    }

    public static bool IsMvpStep(MethodologyStep step) => _mvpSteps.Contains(step);
}
