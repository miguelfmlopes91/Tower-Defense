using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public Point GridPosition { get; private set; }

    public Tile TileRef { get; private  set; }

    public Node Parent { get; private set; }

    public Node(Tile tileRef)
    {
        TileRef = tileRef;
        GridPosition = TileRef.GridPosition;
    }

    public void CalcValues(Node parent)
    {
        Parent = parent;
    }
}
