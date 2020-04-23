using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
    private static Dictionary<Point, Node> nodeDict;

    private static void CreateNodes() {
        nodeDict = new Dictionary<Point, Node>();

        foreach (Tile tile in LevelManager.Instance.Tiles.Values)
        {
            nodeDict.Add(tile.GridPosition, new Node(tile));
        }
    }

    public static void GetPath(Point start)
    {
        if (nodeDict == null )
        {
            CreateNodes();
        }

        HashSet<Node> openList = new HashSet<Node>();

        HashSet<Node> closedList = new HashSet<Node>();

        Node currentNode = nodeDict[start];

        //1 Adds the start node to the openlist
        openList.Add(currentNode);

        //2 runs trhough all neighbours
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Point neighboursPosition = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);

                if (LevelManager.Instance.InBounds(neighboursPosition) &&
                    LevelManager.Instance.Tiles[neighboursPosition].walkable &&
                    neighboursPosition != currentNode.GridPosition)
                {

                    //10 cost for normal and 14 for diagonal
                    int gCost = Mathf.Abs(x-y) == 1 ? 10 : 14;

                    //3 Adds neighbours to the open list
                    Node neighbour = nodeDict[neighboursPosition];
                    //* only for debugging*//
                    //neighbour.TileRef.SpriteRenderer.color = Color.black;
                    if (!openList.Contains(neighbour))
                    {
                        openList.Add(neighbour);
                    }
                    // 4. Calculate all the values for the neighbours
                    neighbour.CalcValues(currentNode, gCost);
                }
            }
        }

        openList.Remove(currentNode);
        closedList.Add(currentNode);

        //** DEBUGGING **//
        GameObject.Find("AStarDebugger").GetComponent<AStarDebuggar>().DebugPath(openList, closedList);

    }



}
