namespace Puzzle_2;

public class Box
{
    private List<Lens> lenses = new();

    public Box(int id)
    {
        Id = id;
    }

    public int Id { get; }

    public void AddLens(Lens lens)
    {
        var existingLensIndex = lenses.FindIndex(l => l.Label == lens.Label);
        if (existingLensIndex != -1)
        {
            lenses[existingLensIndex] = lens;
        }
        else
        {
            lenses.Add(lens);
        }
    }

    public void RemoveLens(string label)
    {
        lenses.RemoveAll(lens => lens.Label == label);
    }

    public int GetSumOfFocusingPower()
    {
        return lenses.Select((lens, index) => (1 + Id) * (index + 1) * lens.FocalLength).Sum();
    }
}