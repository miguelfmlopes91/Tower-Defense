using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tile;

    public float TileSize{
        get {return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }


    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateLevel() {

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int j = 0; j < 10; j++){
            for (int i = 1; i < 10; i++){
                PlaceTile(i, j, worldStart);
            }

        }
    }

    private void PlaceTile(int x, int y, Vector3 worldStart) {
        GameObject newTile = Instantiate(tile);
        newTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0);
    }
}
