using MoreLinq.Extensions;
using Puzzle_2;

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

var maximumNumberOfEnergizedTiles = 0;

for (var column = 0; column < tiles.GetLength(1); column++)
{
    tiles[0, column].ForwardBeam(BeamDirection.Down);
    var numberOfEnergizedTiles = tiles.OfType<Tile>().Count(tile => tile.IsEnergize);
    maximumNumberOfEnergizedTiles = Math.Max(maximumNumberOfEnergizedTiles, numberOfEnergizedTiles);
    tiles.OfType<Tile>().ForEach(tile => tile.Reset());

    tiles[tiles.GetUpperBound(0), column].ForwardBeam(BeamDirection.Up);
    numberOfEnergizedTiles = tiles.OfType<Tile>().Count(tile => tile.IsEnergize);
    maximumNumberOfEnergizedTiles = Math.Max(maximumNumberOfEnergizedTiles, numberOfEnergizedTiles);
    tiles.OfType<Tile>().ForEach(tile => tile.Reset());
}

for (var row = 0; row < tiles.GetLength(0); row++)
{
    tiles[row, 0].ForwardBeam(BeamDirection.Right);
    var numberOfEnergizedTiles = tiles.OfType<Tile>().Count(tile => tile.IsEnergize);
    maximumNumberOfEnergizedTiles = Math.Max(maximumNumberOfEnergizedTiles, numberOfEnergizedTiles);
    tiles.OfType<Tile>().ForEach(tile => tile.Reset());

    tiles[row, tiles.GetUpperBound(1)].ForwardBeam(BeamDirection.Left);
    numberOfEnergizedTiles = tiles.OfType<Tile>().Count(tile => tile.IsEnergize);
    maximumNumberOfEnergizedTiles = Math.Max(maximumNumberOfEnergizedTiles, numberOfEnergizedTiles);
    tiles.OfType<Tile>().ForEach(tile => tile.Reset());
}

Console.WriteLine(maximumNumberOfEnergizedTiles);