namespace Puzzle_1;

public class DestinationRange(Int128 start, Int128 end, string destinationCriterion) : Range(start, end)
{
    public string DestinationCriterion { get; } = destinationCriterion;
}