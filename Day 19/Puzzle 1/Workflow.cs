namespace Puzzle_1;

public class Workflow
{
    private List<Rule> _rules;

    public Workflow(string workflow)
    {
        var parts = workflow.Split('{', ',', '}');
        Id = parts.First();
        _rules = parts.Skip(1).Select(part => new Rule(part)).ToList();
    }
    
    public string Id { get; }
    
    public string? Process(Part part)
    {
        foreach (var rule in _rules)
        {
            var nextWorkflowId = rule.Process(part);
            if (nextWorkflowId != null || !part.RequiredProcessing)
            {
                return nextWorkflowId;
            }
        }

        return null;
    }
}