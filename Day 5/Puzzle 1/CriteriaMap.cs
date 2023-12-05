using System.Text.RegularExpressions;

namespace Puzzle_1;

public class CriteriaMap
{
    public string SourceCriteria { get; }
    public string DestinationCriteria { get; }
    public IReadOnlyCollection<Mapping> Mappings { get; }

    public long GetDestinationId(long sourceId) =>
        Mappings.SingleOrDefault(mapping => mapping.IsFor(sourceId))?.DestinationFor(sourceId) ?? sourceId;

    public static CriteriaMap Create(List<string> lines)
    {
        var headerLine = lines.PopFirst();
        var headerMatch = new Regex(@"(?<sourceCriteria>\S+)-to-(?<destinationCriteria>\S+)").Matches(headerLine)
            .Single();
        var sourceCriteria = headerMatch.Groups["sourceCriteria"].Value;
        var destinationCriteria = headerMatch.Groups["destinationCriteria"].Value;

        var mapping = lines.Select(
                line =>
                {
                    var numbers = line.Split(" ").Select(long.Parse).ToList();
                    var destinationRangeStart = numbers.PopFirst();
                    var sourceRangeStart = numbers.PopFirst();
                    var rangeLength = (int)numbers.PopFirst();

                    return new Mapping(sourceRangeStart, sourceRangeStart + rangeLength, destinationRangeStart);
                })
            .ToList();

        return new CriteriaMap(sourceCriteria, destinationCriteria, mapping);
    }

    private CriteriaMap(string sourceCriteria, string destinationCriteria, IReadOnlyCollection<Mapping> mappings)
    {
        SourceCriteria = sourceCriteria;
        DestinationCriteria = destinationCriteria;
        Mappings = mappings;
    }
}