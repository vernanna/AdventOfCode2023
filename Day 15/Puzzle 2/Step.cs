namespace Puzzle_2;

public class Step
{
    public Step(string step)
    {
        var parts = step.Split('=', '-');
        LensLabel = parts.First();
        BoxId = LensLabel.Aggregate(0, (sum, s) => (sum + s) * 17 % 256);
        FocalLength = step.Contains('=') ? int.Parse(parts[1]) : -1;
    }

    public int BoxId { get; }

    public string LensLabel { get; }

    public int FocalLength { get; }

    public bool IsRemoveStep => FocalLength == -1;
}