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

        Node currentNode = nodeDict[start];

        openList.Add(currentNode);

        //** DEBUGGING **//
        GameObject.Find("AStarDebugger").GetComponent<AStarDebuggar>().DebugPath(openList);

    }



}
