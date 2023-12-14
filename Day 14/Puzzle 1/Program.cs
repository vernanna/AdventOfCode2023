using MoreLinq;

var lines = File.ReadAllLines("input.txt").ToList();

var result = lines.Transpose()
    .SelectMany(column => GetWeights(column.ToList()))
    .Sum();

Console.WriteLine(result);

IEnumerable<int> GetWeights(IReadOnlyCollection<char> spaces)
{
    var lastOccupiedSpace = -1;
    foreach (var (index, space) in spaces.Index())
    {
        if (space == '.')
        {
            continue;
        }
        if (space == '#')
        {
            lastOccupiedSpace = index;
        }else if (space == 'O')
        {
            lastOccupiedSpace += 1;
            yield return spaces.Count - lastOccupiedSpace;
        }
    }
}
        
        