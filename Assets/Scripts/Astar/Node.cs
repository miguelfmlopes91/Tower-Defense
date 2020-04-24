using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public Point GridPosition { get; private set; }

    public Tile TileRef { get; private  set; }

    public Node Parent { get; private set; }

    public int G { get; set; }

    public int H { get; set; }

    public int F { get; set; }

    public Node(Tile tileRef)
    {
        TileRef = tileRef;
        GridPosition = TileRef.GridPosition;
    }

    public void CalcValues(Node parent, Node goal ,int gCost)
    {
        Parent = parent;
        G = parent.G + gCost;
        H = ((Mathf.Abs(GridPosition.X - goal.GridPosition.X)) + Mathf.Abs((goal.GridPosition.Y - GridPosition.Y))) * 10;
        F = G + H;
    }
}
