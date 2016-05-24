using UnityEngine;
using System.Collections;

public class Node
{
    public bool walkable;
    public Vector3 worldPosition;

    public int gridX, gridY;

    public int gCost, hCost;

    public Node parent;

    public Node(bool _walkable, Vector3 _worldPosition, int _gridX, int _gridY)
    {
        walkable = _walkable;
        worldPosition = _worldPosition;

        gridX = _gridX;
        gridY = _gridY;
    }

    public int FCost
    {
        get{
            return gCost + hCost;
        }
    }
}
