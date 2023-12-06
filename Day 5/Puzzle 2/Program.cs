// See https://aka.ms/new-console-template for more information

using Puzzle_1;
using Range = Puzzle_1.Range;

var lines = File.ReadAllLines("input.txt").ToList();

var seedRanges = lines
    .PopFirst()
    .Split(" ")
    .Skip(1)
    .Select(Int128.Parse)
    .Chunk(2)
    .Select(startAndEnd => new Range(startAndEnd[0], startAndEnd[0] + startAndEnd[1]))
    .ToList();

lines.RemoveWhile(string.IsNullOrWhiteSpace);

var criteriaMaps = new Dictionary<string, CriteriaMap>();
while (lines.Any())
{
    var criteriaMappingLines = lines.PopWhile(line => !string.IsNullOrWhiteSpace(line));
    var criteriaMap = CriteriaMap.Create(criteriaMappingLines);
    criteriaMaps.Add(criteriaMap.SourceCriterion, criteriaMap);

    lines.RemoveWhile(string.IsNullOrWhiteSpace);
}

var minimumLocation = seedRanges
    .AsParallel()
    .SelectMany(seedRange => GetLocationRanges(seedRange, "seed"))
    .Min(rang => rang.Start);

Console.WriteLine(minimumLocation);

IEnumerable<DestinationRange> GetLocationRanges(Range sourceRange, string sourceCriterion)
{
    var map = criteriaMaps[sourceCriterion];
    var destinationRanges = map.GetDestinationRanges(sourceRange).ToList();
    foreach (var destinationRange in destinationRanges)
    {
        if (destinationRange.DestinationCriterion == "location")
        {
            Console.WriteLine($"{destinationRanges.Count} destination ranges found");
            yield return destinationRange;
        }
        else
        {
            Console.WriteLine($"Looking fir {destinationRanges.Count} ranges of criterion {destinationRange.DestinationCriterion}");
            foreach (var locationRange in GetLocationRanges(destinationRange, destinationRange.DestinationCriterion))
            {
                yield return locationRange;
            }
        }
    }
    
    
    // var destinationRanges = GetDestinationRanges(seedRange, "seed");
    //
    // foreach (var destinationRange in destinationRanges)
    // {
    //     if (destinationRange.DestinationCriterion == "location")
    //     {
    //         yield return destinationRange;
    //     }
    //     else
    //     {
    //         foreach (var VARIABLE in GetDestinationRanges(destinationRange, destinationRange.DestinationCriterion))
    //         {
    //             
    //         }
    //     }
    // }
}

// IEnumerable<DestinationRange> GetDestinationRanges(Range sourceRange, string sourceCriterion)
// {
//     var map = criteriaMaps[sourceCriterion];
//     return map.GetDestinationRanges(sourceRange);
// }