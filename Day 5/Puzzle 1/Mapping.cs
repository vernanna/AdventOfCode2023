namespace Puzzle_1;

public record Mapping(Int128 SourceStart, Int128 SourceEnd, Int128 DestinationStart)
{
    public bool IsFor(Int128 sourceId) => SourceStart <= sourceId && sourceId <= SourceEnd;

    public Int128 DestinationFor(Int128 sourceId) => DestinationStart + sourceId - SourceStart;
}