using System.Text.RegularExpressions;

namespace Puzzle_1;

public class CriteriaMap
{
    public string SourceCriterion { get; }
    public string DestinationCriterion { get; }
    public IReadOnlyCollection<Mapping> Mappings { get; }

    public IEnumerable<DestinationRange> GetDestinationRanges(Range sourceRange)
    {
        var ranges = Mappings.Where(mapping =>
            (sourceRange.Start >= mapping.SourceStart && sourceRange.Start <= mapping.SourceEnd) ||
            (sourceRange.End >= mapping.SourceStart && sourceRange.End <= mapping.SourceEnd) ||
            (mapping.SourceStart >= sourceRange.Start && mapping.SourceStart <= sourceRange.End) ||
            (mapping.SourceEnd >= sourceRange.Start && mapping.SourceEnd <= sourceRange.End));

        foreach (var range in ranges)
        {
            var start = range.SourceStart < sourceRange.Start
                ? range.DestinationFor(sourceRange.Start)
                : range.DestinationStart;
            var end = range.SourceEnd > sourceRange.End
                ? range.DestinationFor(sourceRange.End)
                : range.DestinationEnd;

            yield return new DestinationRange(start, end, DestinationCriterion);
        }
    }

    public Int128 GetDestinationId(Int128 sourceId) =>
        Mappings.SingleOrDefault(mapping => mapping.IsForSourceId(sourceId))?.DestinationFor(sourceId) ?? sourceId;

    public List<Int128> GetSourceIds(Int128 destinationId)
    {
        var sources = Mappings.Where(mapping => mapping.IsForDestinationId(destinationId))
            .Select(mapping => mapping.SourceFor(destinationId)).ToList();

        return sources.Any() ? sources : new List<Int128> { destinationId };
    }

    public static CriteriaMap Create(List<string> lines)
    {
        var headerLine = lines.PopFirst();
        var headerMatch = new Regex(@"(?<sourceCriteria>\S+)-to-(?<destinationCriteria>\S+)").Matches(headerLine)
            .Single();
        var sourceCriteria = headerMatch.Groups["sourceCriteria"].Value;
        var destinationCriteria = headerMatch.Groups["destinationCriteria"].Value;

        var explicitMappings = lines.Select(
                line =>
                {
                    var numbers = line.Split(" ").Select(Int128.Parse).ToList();
                    var destinationRangeStart = numbers.PopFirst();
                    var sourceRangeStart = numbers.PopFirst();
                    var rangeLength = numbers.PopFirst();

                    return new Mapping(sourceRangeStart, sourceRangeStart + rangeLength - 1, destinationRangeStart,
                        destinationRangeStart + rangeLength - 1);
                })
            .OrderBy(mapping => mapping.SourceStart)
            .ToList();

        var mappings = new List<Mapping>();
        var firstExplicitMapping = explicitMappings.First();
        if (firstExplicitMapping.SourceStart != 0)
        {
            mappings.Add(new Mapping(0, firstExplicitMapping.SourceEnd - 1, 0, firstExplicitMapping.SourceEnd - 1));
        }

        mappings.Add(firstExplicitMapping);

        Mapping lastMapping;
        foreach (var explicitMapping in explicitMappings.Skip(1))
        {
            lastMapping = mappings.Last();
            if (lastMapping.SourceEnd + 1 != explicitMapping.SourceStart)
            {
                mappings.Add(new Mapping(lastMapping.SourceEnd + 1, explicitMapping.SourceStart - 1,
                    lastMapping.SourceEnd + 1, explicitMapping.SourceStart - 1));
            }

            mappings.Add(explicitMapping);
        }

        lastMapping = mappings.Last();
        if (mappings.Last().SourceEnd != Int128.MaxValue)
        {
            mappings.Add(new Mapping(lastMapping.SourceEnd + 1, Int128.MaxValue, lastMapping.SourceEnd + 1,
                Int128.MaxValue));
        }

        return new CriteriaMap(sourceCriteria, destinationCriteria, mappings);
    }

    private CriteriaMap(string sourceCriterion, string destinationCriterion, IReadOnlyCollection<Mapping> mappings)
    {
        SourceCriterion = sourceCriterion;
        DestinationCriterion = destinationCriterion;
        Mappings = mappings;
    }
}