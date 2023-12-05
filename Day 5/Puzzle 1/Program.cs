// See https://aka.ms/new-console-template for more information

using Puzzle_1;

var lines = File.ReadAllLines("input.txt").ToList();

var seedIds = lines.PopFirst().Split(" ").Skip(1).Select(Int128.Parse).Chunk(2).Select(chunk => (Start: chunk[0], End: chunk[0] + chunk[1]));
var maximumSeedId = seedIds.Max(seedId => seedId.End);

lines.RemoveWhile(string.IsNullOrWhiteSpace);

var criteriaMaps = new List<CriteriaMap>();
while (lines.Any())
{
    var criteriaMappingLines = lines.PopWhile(line => !string.IsNullOrWhiteSpace(line));
    var criteriaMap = CriteriaMap.Create(criteriaMappingLines);
    criteriaMaps.Add(criteriaMap);

    lines.RemoveWhile(string.IsNullOrWhiteSpace);
}

Int128 smallestLocation = Int128.MaxValue;
for (Int128 seed = 0; seed <= maximumSeedId; seed++)
{
    var sourceCriteria = "seed";
    var sourceId = seed;
    
    while (sourceCriteria != "location")
    {
        var criteriaMap = criteriaMaps.Single(map => map.SourceCriteria == sourceCriteria);
        sourceId = criteriaMap.GetDestinationId(sourceId);
        sourceCriteria = criteriaMap.DestinationCriteria;
    }

    if (smallestLocation > sourceId)
    {
        smallestLocation = sourceId;
    }
}

Console.WriteLine(smallestLocation);