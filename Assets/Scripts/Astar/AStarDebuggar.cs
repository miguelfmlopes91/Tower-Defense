﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarDebuggar : MonoBehaviour
{
    private Tile goal;
    private Tile start;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClickTile();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AStar.GetPath(start.GridPosition);
        }
    }

    private void ClickTile()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),Vector2.zero);

            if (hit.collider!= null)
            {
                Tile temp = hit.collider.GetComponent<Tile>();
                if (temp != null)
                {
                    if (start == null)
                    {
                        start = temp;
                        start.SpriteRenderer.color = Color.green;
                        start.Debugging = true;
                    }
                    else if(goal == null)
                    {
                        goal = temp;
                        goal.SpriteRenderer.color = new Color32(255, 0, 0, 255);
                        goal.Debugging = true;
                    }
                }
            }
        }
    }
    public void DebugPath(HashSet<Node> openList)
    {
        foreach (Node node in openList)
        {
            node.TileRef.SpriteRenderer.color = Color.cyan;

        }
    }
}
