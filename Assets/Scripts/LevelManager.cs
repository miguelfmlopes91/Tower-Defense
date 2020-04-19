using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject[] tilePrefabs;
    [SerializeField]
    private CameraMovement cameraMovement;
    [SerializeField]
    private Transform mapParent;


    public Dictionary<Point, Tile> Tiles { get; set; }

    public float TileSize{
        get {return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    private Point BlueSpawn;
    private Point RedSpawn;

    [SerializeField]
    private GameObject BluePortal;
    [SerializeField]
    private GameObject RedPortal;


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

        Tiles = new Dictionary<Point, Tile>();

        string[] mapData = ReadFile();

        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapY; y++){
            char[] newTiles = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++){
                PlaceTile(newTiles[x].ToString(),x, y, worldStart);
            }

        }

        maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;

        cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

        SpawnPortals();
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart) {
        int tileIndex = int.Parse(tileType);
        Tile newTile = Instantiate(tilePrefabs[tileIndex-1]).GetComponent<Tile>();//martelada

        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), mapParent);

    }

    private string[] ReadFile() {

        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string data = bindData.text.Replace(Environment.NewLine, string.Empty);

        return data.Split('-');       
    }

    private void SpawnPortals() {
        BlueSpawn = new Point(0, 0);

        Instantiate(BluePortal, Tiles[BlueSpawn].GetComponent<Tile>().WorldPosition, Quaternion.identity);

        RedSpawn = new Point(16, 6);

        Instantiate(RedPortal, Tiles[RedSpawn].GetComponent<Tile>().WorldPosition, Quaternion.identity);

    }
}
