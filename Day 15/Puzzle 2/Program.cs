using Puzzle_2;

var steps = string.Join("", File.ReadAllLines("input.txt"))
    .Split(",")
    .Select(step => new Step(step));

var boxes = Enumerable.Range(0, 256).Select(index => new Box(index)).ToList();

foreach (var step in steps)
{
    var box = boxes.Single(b => b.Id == step.BoxId);
    if (step.IsRemoveStep)
    {
        box.RemoveLens(step.LensLabel);
    }
    else
    {
        box.AddLens(new Lens(step.FocalLength, step.LensLabel));
    }
}

var sumOfFocusingPower = boxes.Sum(box => box.GetSumOfFocusingPower());

Console.WriteLine(sumOfFocusingPower);