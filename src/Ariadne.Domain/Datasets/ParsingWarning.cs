using Ariadne.Domain.Common;

namespace Ariadne.Domain.Datasets;

public sealed record ParsingWarning
{
    public ParsingWarning(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw new DomainException("Parsing warning message is required.");

        Message = message.Trim();
    }

    public string Message { get; }

    public override string ToString() => Message;
}
