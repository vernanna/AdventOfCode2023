namespace Puzzle_1;

public record Mapping(Int128 SourceStart, Int128 SourceEnd, Int128 DestinationStart, Int128 DestinationEnd)
{
    public bool IsForSourceId(Int128 sourceId) => SourceStart <= sourceId && sourceId <= SourceEnd;

    public bool IsForDestinationId(Int128 destinationId) =>
        DestinationStart <= destinationId && destinationId <= DestinationEnd;

    public Int128 DestinationFor(Int128 sourceId) => DestinationStart + sourceId - SourceStart;

    public Int128 SourceFor(Int128 destinationId) => SourceStart + destinationId - DestinationStart;
}