// See https://aka.ms/new-console-template for more information

using Puzzle_1;

var lines = File.ReadAllLines("input.txt").ToList();

var seedIds = lines.PopFirst().Split(" ").Skip(1).Select(long.Parse).ToList();

lines.RemoveWhile(string.IsNullOrWhiteSpace);

var criteriaMaps = new List<CriteriaMap>();
while (lines.Any())
{
    var criteriaMappingLines = lines.PopWhile(line => !string.IsNullOrWhiteSpace(line));
    var criteriaMap = CriteriaMap.Create(criteriaMappingLines);
    criteriaMaps.Add(criteriaMap);

    lines.RemoveWhile(string.IsNullOrWhiteSpace);
}

var sourceCriteria = "seed";
var currentIds = seedIds.AsEnumerable();

while (sourceCriteria != "location")
{
    var criteriaMap = criteriaMaps.Single(map => map.SourceCriteria == sourceCriteria);
    currentIds = currentIds.Select(sourceId => criteriaMap.GetDestinationId(sourceId));
    sourceCriteria = criteriaMap.DestinationCriteria;
}

Console.WriteLine(currentIds.Min());