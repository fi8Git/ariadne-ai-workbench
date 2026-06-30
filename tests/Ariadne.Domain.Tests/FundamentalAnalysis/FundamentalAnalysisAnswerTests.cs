using Ariadne.Domain.Common;
using Ariadne.Domain.FundamentalAnalysis;

namespace Ariadne.Domain.Tests.FundamentalAnalysis;

public class FundamentalAnalysisAnswerTests
{
    private static readonly DateTimeOffset CreatedAt = new(2026, 6, 30, 10, 0, 0, TimeSpan.Zero);

    [Fact]
    public void CreateInitializesUnansweredQuestion()
    {
        FundamentalAnalysisAnswer answer = FundamentalAnalysisAnswer.Create(
            FundamentalQuestionGroup.What,
            "  what.variables  ",
            "  What do the variables represent?  ",
            CreatedAt);

        Assert.Equal("what.variables", answer.QuestionKey);
        Assert.Equal(FundamentalQuestionGroup.What, answer.QuestionGroup);
        Assert.Equal("What do the variables represent?", answer.Prompt);
        Assert.Equal(KnowledgeStatus.Unknown, answer.KnowledgeStatus);
        Assert.False(answer.IsAddressed);
        Assert.False(answer.IsExplicitlyUnknown);
        Assert.Null(answer.AnswerText);
        Assert.Null(answer.Notes);
        Assert.Equal(CreatedAt, answer.CreatedAtUtc);
        Assert.Null(answer.UpdatedAtUtc);
    }

    [Theory]
    [InlineData(null, "Prompt")]
    [InlineData("", "Prompt")]
    [InlineData("what.variables", null)]
    [InlineData("what.variables", "")]
    public void CreateWithMissingRequiredTextThrowsDomainException(string? questionKey, string? prompt)
    {
        Assert.Throws<DomainException>(
            () => FundamentalAnalysisAnswer.Create(
                FundamentalQuestionGroup.What,
                questionKey!,
                prompt!,
                CreatedAt));
    }

    [Fact]
    public void CreateWithUndefinedQuestionGroupThrowsDomainException()
    {
        Assert.Throws<DomainException>(
            () => FundamentalAnalysisAnswer.Create(
                (FundamentalQuestionGroup)999,
                "what.variables",
                "What do the variables represent?",
                CreatedAt));
    }

    [Fact]
    public void MarkKnownRequiresAnswerText()
    {
        FundamentalAnalysisAnswer answer = CreateAnswer();

        Assert.Throws<DomainException>(() => answer.MarkKnown(" ", null, CreatedAt.AddMinutes(1)));
    }

    [Fact]
    public void MarkKnownStoresAnswerAndNotes()
    {
        FundamentalAnalysisAnswer answer = CreateAnswer();
        DateTimeOffset updatedAt = CreatedAt.AddMinutes(1);

        answer.MarkKnown("  Rows are property sales.  ", "  Source documentation reviewed.  ", updatedAt);

        Assert.Equal(KnowledgeStatus.Known, answer.KnowledgeStatus);
        Assert.Equal("Rows are property sales.", answer.AnswerText);
        Assert.Equal("Source documentation reviewed.", answer.Notes);
        Assert.True(answer.IsAddressed);
        Assert.False(answer.IsExplicitlyUnknown);
        Assert.Equal(updatedAt, answer.UpdatedAtUtc);
    }

    [Fact]
    public void MarkPartiallyKnownRequiresAnswerText()
    {
        FundamentalAnalysisAnswer answer = CreateAnswer();

        Assert.Throws<DomainException>(
            () => answer.MarkPartiallyKnown(null!, "Only partial metadata is available.", CreatedAt.AddMinutes(1)));
    }

    [Fact]
    public void MarkUnknownAllowsMissingAnswerText()
    {
        FundamentalAnalysisAnswer answer = CreateAnswer();
        DateTimeOffset updatedAt = CreatedAt.AddMinutes(1);

        answer.MarkUnknown(null, updatedAt);

        Assert.Equal(KnowledgeStatus.Unknown, answer.KnowledgeStatus);
        Assert.Null(answer.AnswerText);
        Assert.Null(answer.Notes);
        Assert.True(answer.IsAddressed);
        Assert.True(answer.IsExplicitlyUnknown);
        Assert.Equal(updatedAt, answer.UpdatedAtUtc);
    }

    [Fact]
    public void MarkNotApplicableStoresNotesWithoutAnswerText()
    {
        FundamentalAnalysisAnswer answer = CreateAnswer();
        DateTimeOffset updatedAt = CreatedAt.AddMinutes(1);

        answer.MarkNotApplicable("  No geographic dimension in this dataset.  ", updatedAt);

        Assert.Equal(KnowledgeStatus.NotApplicable, answer.KnowledgeStatus);
        Assert.Null(answer.AnswerText);
        Assert.Equal("No geographic dimension in this dataset.", answer.Notes);
        Assert.True(answer.IsAddressed);
        Assert.Equal(updatedAt, answer.UpdatedAtUtc);
    }

    [Fact]
    public void UpdateAnswerRejectsUndefinedKnowledgeStatus()
    {
        FundamentalAnalysisAnswer answer = CreateAnswer();

        Assert.Throws<DomainException>(
            () => answer.UpdateAnswer((KnowledgeStatus)999, "Invalid.", null, CreatedAt.AddMinutes(1)));
    }

    [Fact]
    public void ChangePromptTrimsPromptAndUpdatesTimestamp()
    {
        FundamentalAnalysisAnswer answer = CreateAnswer();
        DateTimeOffset updatedAt = CreatedAt.AddMinutes(1);

        answer.ChangePrompt("  What does each row represent?  ", updatedAt);

        Assert.Equal("What does each row represent?", answer.Prompt);
        Assert.Equal(updatedAt, answer.UpdatedAtUtc);
    }

    [Fact]
    public void UpdateBeforeLastUpdateThrowsDomainExceptionAndLeavesStateUnchanged()
    {
        FundamentalAnalysisAnswer answer = CreateAnswer();
        answer.MarkKnown("Rows are property sales.", null, CreatedAt.AddMinutes(2));

        Assert.Throws<DomainException>(() => answer.MarkUnknown("Older update.", CreatedAt.AddMinutes(1)));

        Assert.Equal(KnowledgeStatus.Known, answer.KnowledgeStatus);
        Assert.Equal("Rows are property sales.", answer.AnswerText);
        Assert.Equal(CreatedAt.AddMinutes(2), answer.UpdatedAtUtc);
    }

    private static FundamentalAnalysisAnswer CreateAnswer()
        => FundamentalAnalysisAnswer.Create(
            FundamentalQuestionGroup.What,
            "what.variables",
            "What do the variables represent?",
            CreatedAt);
}
