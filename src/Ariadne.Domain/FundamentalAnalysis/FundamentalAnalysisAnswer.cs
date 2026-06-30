using Ariadne.Domain.Common;

namespace Ariadne.Domain.FundamentalAnalysis;

public sealed class FundamentalAnalysisAnswer : Entity<string>
{
    private FundamentalAnalysisAnswer(
        FundamentalQuestionGroup questionGroup,
        string questionKey,
        string prompt,
        DateTimeOffset createdAtUtc)
        : base(NormalizeRequiredText(questionKey, "Question key is required."))
    {
        createdAtUtc = DomainGuard.EnsureUtc(createdAtUtc, nameof(createdAtUtc));

        EnsureDefined(questionGroup, "Fundamental question group is not defined.");

        QuestionGroup = questionGroup;
        Prompt = NormalizeRequiredText(prompt, "Question prompt is required.");
        KnowledgeStatus = KnowledgeStatus.Unknown;
        CreatedAtUtc = createdAtUtc;
    }

    public string QuestionKey => Id;

    public FundamentalQuestionGroup QuestionGroup { get; }

    public string Prompt { get; private set; }

    public string? AnswerText { get; private set; }

    public KnowledgeStatus KnowledgeStatus { get; private set; }

    public string? Notes { get; private set; }

    public DateTimeOffset CreatedAtUtc { get; }

    public DateTimeOffset? UpdatedAtUtc { get; private set; }

    public bool IsAddressed => UpdatedAtUtc is not null;

    public bool IsExplicitlyUnknown => KnowledgeStatus == KnowledgeStatus.Unknown && IsAddressed;

    public static FundamentalAnalysisAnswer Create(
        FundamentalQuestionGroup questionGroup,
        string questionKey,
        string prompt,
        DateTimeOffset now)
        => new(questionGroup, questionKey, prompt, now);

    public void ChangePrompt(string prompt, DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        Prompt = NormalizeRequiredText(prompt, "Question prompt is required.");
        Touch(now);
    }

    public void UpdateAnswer(
        KnowledgeStatus knowledgeStatus,
        string? answerText,
        string? notes,
        DateTimeOffset now)
    {
        EnsureUpdateTimestamp(now);
        EnsureDefined(knowledgeStatus, "Knowledge status is not defined.");

        string? normalizedAnswer = NormalizeOptionalText(answerText);

        if (knowledgeStatus is KnowledgeStatus.Known or KnowledgeStatus.PartiallyKnown && normalizedAnswer is null)
            throw new DomainException("Known or partially known answers require answer text.");

        KnowledgeStatus = knowledgeStatus;
        AnswerText = knowledgeStatus is KnowledgeStatus.Known or KnowledgeStatus.PartiallyKnown ? normalizedAnswer : null;
        Notes = NormalizeOptionalText(notes);
        Touch(now);
    }

    public void MarkKnown(string answerText, string? notes, DateTimeOffset now)
        => UpdateAnswer(KnowledgeStatus.Known, answerText, notes, now);

    public void MarkPartiallyKnown(string answerText, string? notes, DateTimeOffset now)
        => UpdateAnswer(KnowledgeStatus.PartiallyKnown, answerText, notes, now);

    public void MarkUnknown(string? notes, DateTimeOffset now)
        => UpdateAnswer(KnowledgeStatus.Unknown, null, notes, now);

    public void MarkNotApplicable(string? notes, DateTimeOffset now)
        => UpdateAnswer(KnowledgeStatus.NotApplicable, null, notes, now);

    private void Touch(DateTimeOffset now)
        => UpdatedAtUtc = now;

    private void EnsureUpdateTimestamp(DateTimeOffset now)
    {
        DomainGuard.EnsureUtc(now, nameof(now));

        DateTimeOffset lastUpdate = UpdatedAtUtc ?? CreatedAtUtc;

        if (now < lastUpdate)
            throw new DomainException("Updated timestamp cannot be before the last fundamental answer update.");
    }

    private static string NormalizeRequiredText(string value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(message);

        return value.Trim();
    }

    private static string? NormalizeOptionalText(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value.Trim();

    private static void EnsureDefined<TEnum>(TEnum value, string message) where TEnum : struct, Enum
    {
        if (!Enum.IsDefined(value))
            throw new DomainException(message);
    }
}
