/// <summary>
/// Defines a maze using a dictionary. The dictionary is provided by the
/// user when the Maze object is created. The dictionary will contain the
/// following mapping:
///
/// (x,y) : [left, right, up, down]
///
/// 'x' and 'y' are integers and represents locations in the maze.
/// 'left', 'right', 'up', and 'down' are boolean are represent valid directions
///
/// If a direction is false, then we can assume there is a wall in that direction.
/// If a direction is true, then we can proceed.  
///
/// If there is a wall, then throw an InvalidOperationException with the message "Can't go that way!".  If there is no wall,
/// then the 'currX' and 'currY' values should be changed.
/// </summary>
public class Maze
{
    private readonly Dictionary<ValueTuple<int, int>, bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<ValueTuple<int, int>, bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    /// <summary>
    /// Check to see if you can move left.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveLeft()
    {
        // Check if current position exists in maze
        if (!_mazeMap.TryGetValue((_currX, _currY), out bool[] directions))
        {
            throw new InvalidOperationException($"Position ({_currX}, {_currY}) not in maze!");
        }

        // Check if left is valid (index 0 = left)
        if (!directions[0])
        {
            throw new InvalidOperationException("Can't go that way!");
        }

        // Move left (decrease x by 1)
        _currX--;
    }

    /// <summary>
    /// Check to see if you can move right.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    public void MoveRight()
    {
        // Check if current position exists in maze
        if (!_mazeMap.TryGetValue((_currX, _currY), out bool[] directions))
        {
            throw new InvalidOperationException($"Position ({_currX}, {_currY}) not in maze!");
        }

        // Check if right is valid (index 1 = right)
        if (!directions[1])
        {
            throw new InvalidOperationException("Can't go that way!");
        }

        // Move right (increase x by 1)
        _currX++;
    }

    /// <summary>
    /// Check to see if you can move up.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
 public void MoveUp()
{
    if (!_mazeMap.TryGetValue((_currX, _currY), out bool[] directions))
    {
        throw new InvalidOperationException($"Position ({_currX}, {_currY}) not in maze!");
    }
    if (!directions[2])
    {
        throw new InvalidOperationException("Can't go that way!");
    }
    _currY--;   // was _currY++
}   
    public void MoveDown()
{
    if (!_mazeMap.TryGetValue((_currX, _currY), out bool[] directions))
    {
        throw new InvalidOperationException($"Position ({_currX}, {_currY}) not in maze!");
    }
    if (!directions[3])
    {
        throw new InvalidOperationException("Can't go that way!");
    }
    _currY++;   // was _currY--
}

    /// <summary>
    /// Check to see if you can move down.  If you can, then move.  If you
    /// can't move, throw an InvalidOperationException with the message "Can't go that way!".
    /// </summary>
    

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}