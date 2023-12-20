using Puzzle_1;

var lines = File.ReadAllLines("input.txt");

var workFlows = lines
    .TakeWhile(line => !string.IsNullOrWhiteSpace(line))
    .Select(line => new Workflow(line))
    .ToDictionary(workflow => workflow.Id);

var parts = lines
    .Reverse()
    .TakeWhile(line => !string.IsNullOrWhiteSpace(line))
    .Select(line => new Part(line))
    .ToList();

foreach (var part in parts)
{
    var currentWorkflowId = "in";
    while (currentWorkflowId != null)
    {
        currentWorkflowId = workFlows[currentWorkflowId].Process(part);
    }
}

var sumOfAcceptedRatings = parts
    .Where(part => part.IsAccepted)
    .Select(part => part.LookRating + part.MusicalRating + part.AerodynamicRating + part.ShinyRating)
    .Sum();

Console.WriteLine(sumOfAcceptedRatings);

