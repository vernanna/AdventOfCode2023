var numberOfRequiredTilts = 1000000000;

var lines = File.ReadAllLines("input.txt").Select(line => line.ToList()).ToList();
var spaces = new char[lines.Count, lines.First().Count];
for (var row = 0; row < lines.Count; row++)
{
    for (var column = 0; column < lines[row].Count; column++)
    {
        spaces[row, column] = lines[row][column];
    }
}

var knownPatterns = new Dictionary<string, long>();

for (long i = 0; i < numberOfRequiredTilts; i++)
{
    var cacheKey = GetHash(spaces);
    if (knownPatterns.TryGetValue(cacheKey, out var index))
    {
        var loopSize = i - index;
        var remainingTilts = numberOfRequiredTilts - i;
        if (remainingTilts % loopSize == 0)
        {
            break;
        }
    }
    else
    {
        knownPatterns.Add(cacheKey, i);
    }

    Tilt(spaces);
}

PrintSpaces(spaces);

var sumOfWeights = GetSumOfWeights(spaces);

Console.WriteLine(sumOfWeights);

void Tilt(char[,] spaces)
{
    TiltNorth(spaces);
    TiltWest(spaces);
    TiltSouth(spaces);
    TiltEast(spaces);
}

void TiltNorth(char[,] spaces)
{
    for (var column = 0; column < spaces.GetLength(1); column++)
    {
        var lastOccupiedSpace = -1;
        for (var row = 0; row < spaces.GetLength(0); row++)
        {
            var space = spaces[row, column];
            if (space == '.')
            {
                continue;
            }

            if (space == '#')
            {
                lastOccupiedSpace = row;
            }
            else if (space == 'O')
            {
                spaces[row, column] = '.';
                lastOccupiedSpace += 1;
                spaces[lastOccupiedSpace, column] = space;
            }
        }
    }
}

void TiltSouth(char[,] spaces)
{
    for (var column = 0; column < spaces.GetLength(1); column++)
    {
        var lastOccupiedSpace = spaces.GetLength(0);
        for (var row = spaces.GetLength(0) - 1; row >= 0; row--)
        {
            var space = spaces[row, column];
            if (space == '.')
            {
                continue;
            }

            if (space == '#')
            {
                lastOccupiedSpace = row;
            }
            else if (space == 'O')
            {
                spaces[row, column] = '.';
                lastOccupiedSpace -= 1;
                spaces[lastOccupiedSpace, column] = space;
            }
        }
    }
}

void TiltWest(char[,] spaces)
{
    for (var row = 0; row < spaces.GetLength(0); row++)
    {
        var lastOccupiedSpace = -1;
        for (var column = 0; column < spaces.GetLength(1); column++)
        {
            var space = spaces[row, column];
            if (space == '.')
            {
                continue;
            }

            if (space == '#')
            {
                lastOccupiedSpace = column;
            }
            else if (space == 'O')
            {
                spaces[row, column] = '.';
                lastOccupiedSpace += 1;
                spaces[row, lastOccupiedSpace] = space;
            }
        }
    }
}

void TiltEast(char[,] spaces)
{
    for (var row = 0; row < spaces.GetLength(0); row++)
    {
        var lastOccupiedSpace = spaces.GetLength(1);
        for (var column = spaces.GetLength(1) - 1; column >= 0; column--)
        {
            var space = spaces[row, column];
            if (space == '.')
            {
                continue;
            }

            if (space == '#')
            {
                lastOccupiedSpace = column;
            }
            else if (space == 'O')
            {
                spaces[row, column] = '.';
                lastOccupiedSpace -= 1;
                spaces[row, lastOccupiedSpace] = space;
            }
        }
    }
}

int GetSumOfWeights(char[,] spaces)
{
    var sum = 0;
    var numberOfRows = spaces.GetLength(0);
    for (var row = 0; row < spaces.GetLength(0); row++)
    {
        for (var column = 0; column < spaces.GetLength(1); column++)
        {
            if (spaces[row, column] == 'O')
            {
                sum += numberOfRows - row;
            }
        }
    }

    return sum;
}

string GetHash(char[,] spaces)
{
    var s = "";
    for (var row = 0; row < spaces.GetLength(0); row++)
    {
        for (var column = 0; column < spaces.GetLength(1); column++)
        {
            s += spaces[row, column];
        }
    }

    return s;
}

void PrintSpaces(char[,] spaces)
{
    for (var row = 0; row < spaces.GetLength(0); row++)
    {
        for (var column = 0; column < spaces.GetLength(1); column++)
        {
            Console.Write(spaces[row, column]);
        }

        Console.WriteLine();
    }

    Console.WriteLine();
}