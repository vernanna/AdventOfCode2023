namespace Puzzle_2;

public record Race(double Time, double RecordDistance)
{
    public (int Start, int End) GetSpeedGainRangeToWin()
    {
        var (x1, x2) = SolveQuadraticEquation(Time, RecordDistance);

        var start = x1 < x2 ? x1 : x2;
        var end = x2 > x1 ? x2 : x1;

        return new ((int)start + 1, (int)end);
    }

    private (double X1, double X2) SolveQuadraticEquation(double p, double q)
    {
        var x1 = (p / 2) + Math.Sqrt(Math.Pow(p / 2, 2) - q);
        var x2 = (p / 2) - Math.Sqrt(Math.Pow(p / 2, 2) - q);

        return (X1: x1, X2: x2);
    }
}