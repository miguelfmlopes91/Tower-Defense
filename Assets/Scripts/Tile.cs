using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public Point GridPosition { get; set; }

    public bool IsEmpty { get; set; }

    public Vector2 WorldPosition
    {
        get
        {
            return GetComponent<SpriteRenderer>().bounds.center;
        }
    }


    private Color32 fullColor   = new Color32(255, 118, 118, 255);
    private Color32 emptyColor  = new Color32(96, 255, 90, 255);


    private SpriteRenderer spriteRenderer;
    private Range myTower;


    public bool Walkable { get; set; }

    public bool Debugging { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Point gridPos, Vector3 worldPosition, Transform parent) {
        Walkable = true;
        IsEmpty = true;
        GridPosition = gridPos;
        transform.position = worldPosition;
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(gridPos, this);

    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.TowerClickButton != null)
        {
            if (IsEmpty && !Debugging)
            {
                ColorTile(emptyColor);
            }

            if (!IsEmpty && !Debugging)
            {
                ColorTile(fullColor);

            }
            else if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
        else if(!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.TowerClickButton == null && Input.GetMouseButtonDown(0))
        {
            if (myTower!=null)
            {
                GameManager.Instance.SelectTower(myTower);
            }
            else
            {
                GameManager.Instance.DeselectTower();
            }
        }
    }

    private void OnMouseExit()
    {
        if (!Debugging)
        {
            ColorTile(Color.white);
        }
    }



    private void PlaceTower() {
        GameObject tower = Instantiate(GameManager.Instance.TowerClickButton.TowerButtonPrefab, transform.position, Quaternion.identity);

        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        tower.transform.SetParent(transform);

        myTower = tower.transform.GetChild(0).GetComponent<Range>();

        IsEmpty = false;

        ColorTile(Color.white);

        myTower.Price = GameManager.Instance.TowerClickButton.Price;

        GameManager.Instance.BuyTower();

        Walkable = false;

    }


    private void ColorTile(Color32 newColor)
    {
        spriteRenderer.color = newColor;
    }
}
