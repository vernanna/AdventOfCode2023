using System.Text.RegularExpressions;
using Puzzle_2;

var lines = File.ReadAllLines("input.txt").ToList();

var regex = new Regex("\\d+");

var time = double.Parse(string.Join("", regex.Matches(lines[0]).Select(match => match.Value)));
var distance = double.Parse(string.Join("", regex.Matches(lines[1]).Select(match => match.Value)));

var race = new Race(time, distance);

var speedGainRange = race.GetSpeedGainRangeToWin();
var rangeLength = speedGainRange.End - speedGainRange.Start + 1;

Console.WriteLine(rangeLength);