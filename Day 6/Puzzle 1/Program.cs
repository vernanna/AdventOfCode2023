using System.Text.RegularExpressions;
using Puzzle_1;

var lines = File.ReadAllLines("input.txt").ToList();

var regex = new Regex("\\d+");

var times = regex.Matches(lines[0]).Select(match => int.Parse(match.Value));
var distances = regex.Matches(lines[1]).Select(match => int.Parse(match.Value));

var races = times
    .Zip(distances)
    .Select(timeAndDistance => new Race(timeAndDistance.First, timeAndDistance.Second))
    .ToList();

var productOfWaysToWin = 1;
foreach (var race in races)
{
    var speedGainRange = race.GetSpeedGainRangeToWin();
    var rangeLength = speedGainRange.End - speedGainRange.Start + 1;
    productOfWaysToWin *= rangeLength;

}

Console.WriteLine(productOfWaysToWin);