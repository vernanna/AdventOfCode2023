var sumOfHashes = string.Join("", File.ReadAllLines("input.txt"))
    .Split(",")
    .Select(step => step.Select(character => (int)character))
    .Sum(step => step.Aggregate(0, (sum, s) => (sum + s) * 17 % 256));

Console.WriteLine(sumOfHashes);