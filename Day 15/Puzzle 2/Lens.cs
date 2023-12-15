namespace Puzzle_2;

public class Lens
{
    public Lens(int focalLength, string label)
    {
        FocalLength = focalLength;
        Label = label;
    }

    public string Label { get; }

    public int FocalLength { get; }
}