namespace Puzzle_1;

public record Mapping(long SourceStart, long SourceEnd, long DestinationStart)
{
    public bool IsFor(long sourceId) => SourceStart <= sourceId && sourceId <= SourceEnd;

    public long DestinationFor(long sourceId) => DestinationStart + sourceId - SourceStart;
}