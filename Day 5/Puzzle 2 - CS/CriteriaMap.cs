using System.Text.RegularExpressions;

namespace Puzzle_1;

public class CriteriaMap
{
    public string SourceCriteria { get; }
    public string DestinationCriteria { get; }
    public IReadOnlyCollection<Mapping> Mappings { get; }

    public Int128 GetDestinationId(Int128 sourceId) =>
        Mappings.SingleOrDefault(mapping => mapping.IsForSourceId(sourceId))?.DestinationFor(sourceId) ?? sourceId;
    
    public List<Int128> GetSourceIds(Int128 destinationId)
    {
        var sources = Mappings.Where(mapping => mapping.IsForDestinationId(destinationId)).Select(mapping => mapping.SourceFor(destinationId)).ToList();

        return sources.Any() ? sources : new List<Int128> { destinationId };
    }

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
                    var numbers = line.Split(" ").Select(Int128.Parse).ToList();
                    var destinationRangeStart = numbers.PopFirst();
                    var sourceRangeStart = numbers.PopFirst();
                    var rangeLength = numbers.PopFirst();

                    return new Mapping(sourceRangeStart, sourceRangeStart + rangeLength, destinationRangeStart, destinationRangeStart + rangeLength);
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