namespace Puzzle_1;

public class Mapping(Int128 sourceStart, Int128 sourceEnd, Int128 destinationStart, Int128 destinationEnd)
{
    public Int128 SourceStart => sourceStart;
    
    public Int128 SourceEnd => sourceEnd;
    
    public Int128 DestinationStart => destinationStart;
    
    public Int128 DestinationEnd => destinationEnd;
    
    public bool IsForSourceId(Int128 sourceId) => sourceStart <= sourceId && sourceId <= sourceEnd;

    public bool IsForDestinationId(Int128 destinationId) =>
        destinationStart <= destinationId && destinationId <= destinationEnd;

    public Int128 DestinationFor(Int128 sourceId) => destinationStart + sourceId - sourceStart;

    public Int128 SourceFor(Int128 destinationId) => sourceStart + destinationId - destinationStart;
}