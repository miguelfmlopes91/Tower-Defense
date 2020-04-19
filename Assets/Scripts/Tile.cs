using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public Point GridPosition { get; set; }

    public Vector2 WorldPosition
    {
        get
        {
            return GetComponent<SpriteRenderer>().bounds.center;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.TowerClickButton != null)
        {
            PlaceTower();
        }
    }

   

    private void PlaceTower() {
        GameObject tower = Instantiate(GameManager.Instance.TowerClickButton.TowerButtonPrefab, transform.position, Quaternion.identity);

        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        tower.transform.SetParent(transform);

        GameManager.Instance.BuyTower();

    }
}
