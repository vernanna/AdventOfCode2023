// See https://aka.ms/new-console-template for more information

using Puzzle_1;

var lines = File.ReadAllLines("input.txt").ToList();

var existingSeedIds = lines
    .PopFirst()
    .Split(" ")
    .Skip(1)
    .Select(Int128.Parse)
    .Chunk(2)
    .Select(startAndEnd => (Start: startAndEnd[0], End: startAndEnd[1]))
    .ToList();

lines.RemoveWhile(string.IsNullOrWhiteSpace);

var criteriaMaps = new Dictionary<string, CriteriaMap>();
while (lines.Any())
{
    var criteriaMappingLines = lines.PopWhile(line => !string.IsNullOrWhiteSpace(line));
    var criteriaMap = CriteriaMap.Create(criteriaMappingLines);
    criteriaMaps.Add(criteriaMap.DestinationCriteria, criteriaMap);

    lines.RemoveWhile(string.IsNullOrWhiteSpace);
}

for (Int128 location = 0;; location++)
{
    var seedIds = GetSourceIds(location, "location");

    if (seedIds.Any(seedId => existingSeedIds.Any(existingSeedId => existingSeedId.Start <= seedId && seedId <= existingSeedId.End)))
    {
        Console.WriteLine(location);
        break;
    }
}

IEnumerable<Int128> GetSourceIds(Int128 destinationId, string destinationCriteria)
{
    var criteriaMap = criteriaMaps[destinationCriteria];
    var sourceIds = criteriaMap.GetSourceIds(destinationId);
    var sourceCriteria = criteriaMap.SourceCriteria;
    if (sourceCriteria == "seed")
    {
        foreach (var s in sourceIds)
        {
            yield return s;
        }
    }
    else
    {
        foreach (var sourceId in sourceIds)
        {
            foreach (var s in GetSourceIds(sourceId, sourceCriteria))
            {
                yield return s;
            }
        }
    }
}