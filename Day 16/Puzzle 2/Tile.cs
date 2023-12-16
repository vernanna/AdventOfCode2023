namespace Puzzle_2;

public class Tile
{
    private List<BeamDirection> usedBeamDirections = new();

    public Tile(char character, Tile? tileLeft, Tile? tileAbove)
    {
        Character = character;
        TileLeft = tileLeft;
        if (TileLeft != null)
        {
            TileLeft.TileRight = this;
        }

        TileAbove = tileAbove;
        if (TileAbove != null)
        {
            TileAbove.TileBelow = this;
        }
    }

    public char Character { get; }

    public Tile? TileLeft { get; }

    public Tile? TileRight { get; private set; }

    public Tile? TileAbove { get; }

    public Tile? TileBelow { get; private set; }

    public bool IsEnergize { get; private set; } = false;

    public void ForwardBeam(BeamDirection direction)
    {
        if (usedBeamDirections.Contains(direction))
        {
            return;
        }

        usedBeamDirections.Add(direction);
        IsEnergize = true;
        switch (Character)
        {
            case '.':
                GetTile(direction)?.ForwardBeam(direction);
                break;
            case '/':
            {
                var newDirection = direction switch
                {
                    BeamDirection.Left => BeamDirection.Down,
                    BeamDirection.Right => BeamDirection.Up,
                    BeamDirection.Up => BeamDirection.Right,
                    BeamDirection.Down => BeamDirection.Left
                };
                GetTile(newDirection)?.ForwardBeam(newDirection);
                break;
            }
            case '\\':
            {
                var newDirection = direction switch
                {
                    BeamDirection.Left => BeamDirection.Up,
                    BeamDirection.Right => BeamDirection.Down,
                    BeamDirection.Up => BeamDirection.Left,
                    BeamDirection.Down => BeamDirection.Right
                };
                GetTile(newDirection)?.ForwardBeam(newDirection);
                break;
            }
            case '-' when direction is BeamDirection.Left or BeamDirection.Right:
                GetTile(direction)?.ForwardBeam(direction);
                break;
            case '-':
                GetTile(BeamDirection.Left)?.ForwardBeam(BeamDirection.Left);
                GetTile(BeamDirection.Right)?.ForwardBeam(BeamDirection.Right);
                break;
            case '|' when direction is BeamDirection.Up or BeamDirection.Down:
                GetTile(direction)?.ForwardBeam(direction);
                break;
            case '|':
                GetTile(BeamDirection.Up)?.ForwardBeam(BeamDirection.Up);
                GetTile(BeamDirection.Down)?.ForwardBeam(BeamDirection.Down);
                break;
        }
    }

    private Tile? GetTile(BeamDirection direction) =>
        direction switch
        {
            BeamDirection.Left => TileLeft,
            BeamDirection.Right => TileRight,
            BeamDirection.Up => TileAbove,
            BeamDirection.Down => TileBelow,
            _ => null
        };

    public void Reset()
    {
        IsEnergize = false;
        usedBeamDirections.Clear();
    }
}