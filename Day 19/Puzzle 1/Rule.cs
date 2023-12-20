namespace Puzzle_1;

public class Rule
{
    public Rule(string rule)
    {
        var parts = rule.Split('<', '>', ':');
        if (parts.Length > 1)
        {
            Category = GetCategory(rule.First());
            RatingThreshold = int.Parse(parts.ElementAt(1));
            RatingThresholdIsUpperLimit = rule.ElementAt(1) == '<';
            NextWorkflowId = parts.ElementAt(2);
        }
        else
        {
            NextWorkflowId = rule;
        }
    }

    public Category Category { get; }

    public int? RatingThreshold { get; }

    public bool RatingThresholdIsUpperLimit { get; }

    public string NextWorkflowId { get; }

    public string? Process(Part part)
    {
        var rating = GetValue(part);
        if (RatingThreshold == null)
        {
            return Handle(part);
        }

        return RatingThresholdIsUpperLimit switch
        {
            true when rating < RatingThreshold => Handle(part),
            false when rating > RatingThreshold => Handle(part),
            _ => null
        };
    }

    private string? Handle(Part part)
    {
        if (NextWorkflowId == "A")
        {
            part.Accept();
            return null;
        }

        if (NextWorkflowId == "R")
        {
            part.Reject();
            return null;
        }

        return NextWorkflowId;
    }

    private int GetValue(Part part) =>
        Category switch
        {
            Category.Look => part.LookRating,
            Category.Musical => part.MusicalRating,
            Category.Aerodynamic => part.AerodynamicRating,
            _ => part.ShinyRating
        };

    private Category GetCategory(char category) =>
        category switch
        {
            'x' => Puzzle_1.Category.Look,
            'm' => Puzzle_1.Category.Musical,
            'a' => Puzzle_1.Category.Aerodynamic,
            _ => Category.Shiny
        };
}