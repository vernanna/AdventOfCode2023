using Puzzle_1;

var lines = File.ReadAllLines("input.txt").Select(line => line.ToList()).ToList();
var tiles = new Tile[lines.Count, lines.First().Count];
for (var row = 0; row < lines.Count; row++)
{
    for (var column = 0; column < lines[row].Count; column++)
    {
        var tileLeft = column > 0 ? tiles[row, column - 1] : null;
        var tileAbove = row > 0 ? tiles[row - 1, column] : null;
        tiles[row, column] = new Tile(lines[row][column], tileLeft, tileAbove);
    }
}

tiles[0, 0].ForwardBeam(BeamDirection.Right);

var numberOfEnergizedTiles = tiles.OfType<Tile>().Count(tile => tile.IsEnergize);

Console.WriteLine(numberOfEnergizedTiles);