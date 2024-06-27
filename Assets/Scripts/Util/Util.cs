using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Util
{
    public static Vector3 DirToVector(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up: return Vector3.up;
            case Direction.Right: return Vector3.right;
            case Direction.Down: return Vector3.down;
            case Direction.Left: return Vector3.left;
            default: return Vector3.zero;
        }
    }

    public static T GetRandomEnum<T>()
    {
        Array enumValues = Enum.GetValues(typeof(T));
        System.Random random = new System.Random();
        T randomEnum = (T) enumValues.GetValue(random.Next(enumValues.Length));
        return randomEnum;
    }

    public static List<Direction> OrthogonalDirection(Direction dir)
    {
        switch (dir)
        {
            case Direction.Up:
            case Direction.Down:
                return new List<Direction>() {Direction.Left, Direction.Right};
            case Direction.Left:
            case Direction.Right:
                return new List<Direction>() {Direction.Up, Direction.Down};
            default:
                Debug.LogError("Invalid direction.");
                return new List<Direction>() {Direction.Left, Direction.Right};
        }
    }

    public static Vector3 GetRandomDirection()
    {
        Direction dir = GetRandomEnum<Direction>();
        Direction orthaDir = OrthogonalDirection(dir)[Random.Range(0, 2)];
        Vector3 dirVector = DirToVector(dir) + DirToVector(orthaDir) * Random.Range(0f, 1f);
        dirVector.Normalize();
        return dirVector;
    }
}

public enum Direction
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}

[Serializable]
public struct MinMax
{
    public int Min;
    public int Max;
}