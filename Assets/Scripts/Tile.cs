using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public Point GridPosition { get; set; }

    public bool IsEmpty { get; private set; }

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

    // Start is called before the first frame update
    void Start()
    {
        IsEmpty = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Point gridPos, Vector3 worldPosition, Transform parent) {
        this.GridPosition = gridPos;
        transform.position = worldPosition;
        transform.SetParent(parent);
        LevelManager.Instance.Tiles.Add(gridPos, this);

    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.TowerClickButton != null)
        {
            if (IsEmpty)
            {
                ColorTile(emptyColor);
            }

            if (!IsEmpty)
            {
                ColorTile(fullColor);

            }
            else if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
    }

    private void OnMouseExit()
    {
        ColorTile(Color.white);   
    }



    private void PlaceTower() {
        GameObject tower = Instantiate(GameManager.Instance.TowerClickButton.TowerButtonPrefab, transform.position, Quaternion.identity);

        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        tower.transform.SetParent(transform);

        IsEmpty = false;

        ColorTile(Color.white);

        GameManager.Instance.BuyTower();

    }


    private void ColorTile(Color32 newColor)
    {
        spriteRenderer.color = newColor;
    }
}
