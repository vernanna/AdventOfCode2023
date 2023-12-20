using System.Text.RegularExpressions;

namespace Puzzle_1;

public class Part
{
    public Part(string part)
    {
        var parts = Regex.Matches(part, "\\d+").Select(match => int.Parse(match.Value)).ToList();
        LookRating = parts.ElementAt(0);
        MusicalRating = parts.ElementAt(1);
        AerodynamicRating = parts.ElementAt(2);
        ShinyRating = parts.ElementAt(3);
    }

    public int LookRating { get; }

    public int MusicalRating { get; }

    public int AerodynamicRating { get; }

    public int ShinyRating { get; }

    public bool IsAccepted { get; private set; }

    public bool IsRejected { get; private set; }

    public bool RequiredProcessing => !IsAccepted && !IsRejected;

    public void Accept() => IsAccepted = true;

    public void Reject() => IsRejected = true;
}