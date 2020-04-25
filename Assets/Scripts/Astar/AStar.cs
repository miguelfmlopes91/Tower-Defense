using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public static void GetPath(Point start, Point goal)
    {
        if (nodeDict == null )
        {
            CreateNodes();
        }

        HashSet<Node> openList = new HashSet<Node>();

        HashSet<Node> closedList = new HashSet<Node>();

        Stack<Node> finalPath = new Stack<Node>();//for debugging final path

        Node currentNode = nodeDict[start];

        //1 Adds the start node to the openlist
        openList.Add(currentNode);

        while (openList.Count > 0)
        {
            //2 runs trhough all neighbours
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Point neighboursPosition = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);

                    if (LevelManager.Instance.InBounds(neighboursPosition) &&
                        LevelManager.Instance.Tiles[neighboursPosition].Walkable &&
                        neighboursPosition != currentNode.GridPosition)
                    {

                        //10 cost for normal and 14 for diagonal
                        int gCost;
                        if (Mathf.Abs(x - y) == 1) {
                            gCost = 10;
                        }
                        else
                        {
                            if (!ConnectedDiagonally(currentNode, nodeDict[neighboursPosition]))
                            {
                                continue;
                            }
                            gCost = 14;
                        }

                        //3 Adds neighbours to the open list
                        Node neighbour = nodeDict[neighboursPosition];


                        if (openList.Contains(neighbour))
                        {
                            if (currentNode.G + gCost < neighbour.G)
                            {
                                //Step 9.4 check if this is a better parent
                                neighbour.CalcValues(currentNode, nodeDict[goal], gCost);
                            }
                        }
                        else if (!closedList.Contains(neighbour))
                        {
                            openList.Add(neighbour);
                            // 4. Calculate all the values for the neighbours
                            neighbour.CalcValues(currentNode, nodeDict[goal], gCost);
                        }
                    }
                }
            }
            //5 Move the current node from the open list to the closed list
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            //6 sort the open list by F value
            if (openList.Count >0)
            {
                currentNode = openList.OrderBy(n => n.F).First();
            }

            if (currentNode == nodeDict[goal])
            {
                while (currentNode.GridPosition != start)
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.Parent;
                }
                break;
            }
        }//while

        //** DEBUGGING **//
        GameObject.Find("AStarDebugger").GetComponent<AStarDebuggar>().DebugPath(openList, closedList, finalPath);

    }

    private static bool ConnectedDiagonally(Node currentNode, Node neighbour)
    {
        Point direction = currentNode.GridPosition - neighbour.GridPosition;

        //check first diagonal (one on the left/right)
        Point first = new Point(currentNode.GridPosition.X + direction.X, currentNode.GridPosition.Y);
        //check second diagonal (one on the top/bottom)
        Point second = new Point(currentNode.GridPosition.X, currentNode.GridPosition.Y + direction.Y);

        if (LevelManager.Instance.InBounds(first) && !LevelManager.Instance.Tiles[first].Walkable)
        {
            return false;
        }
        if (LevelManager.Instance.InBounds(second) && !LevelManager.Instance.Tiles[second].Walkable)
        {
            return false;
        }

        return true;
    }
}
