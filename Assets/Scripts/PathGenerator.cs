using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType {Empty, Experience, Enemy, Player, Health, Null}
/* represent each tile in the tile map*/
public class Unit {
    private TileType type;
    private Vector3Int pos;

    public TileType Type {get => type; set => type = value;}
    public Vector3Int Pos {get => pos; set => pos = value;}

    public Unit(Vector3Int pos) {
        Type = TileType.Empty;
        this.Pos = pos;
    }

    public void duplicate(Unit other) {
        Type = other.Type;
    }
}

public class PathGenerator : MonoBehaviour
{

    [SerializeField] private Tilemap backgroundTileMap, indicatorTileMap;
    [SerializeField] private Tile emptyTile, enemyTile, playerTile, indicatorTile, experienceTile, healthTile;
    [SerializeField, Min(0)] private int emptyFrequency, enemyFrequency, experienceFrequency, healthFrequency;
    private List<TileType> tileTypes = new List<TileType>(); //collection of TileType for randomization
    private Unit[,] map = new Unit[3,11];

    private PlayerControl player;

    //getters & setters
    public Unit[,] Map {get=>map;}
    
    void Awake()
    {
        //build the map
        Vector3Int startPos = backgroundTileMap.WorldToCell(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2 - 1,0,0)));

        for (int x = 0; x < map.GetLength(0); x++) {
            for (int y = 0; y < map.GetLength(1); y++) {
                map[x,y] = new Unit(startPos + new Vector3Int(x,y,0));
            }
        }
    }

    void Start()
    {   
        addToTileTypes(TileType.Empty, emptyFrequency);
        addToTileTypes(TileType.Enemy, enemyFrequency);
        addToTileTypes(TileType.Experience, experienceFrequency);
        addToTileTypes(TileType.Health, healthFrequency);

        //randomize beginning map
        foreach (Unit unit in Map) {
            unit.Type = tileTypes[Random.Range(0, tileTypes.Count)];
        }

        player = FindObjectOfType<PlayerControl>();
        //start from the middle bottom of the screen
        map[player.CurrentIndex.x, player.CurrentIndex.y].Type = TileType.Player;
        drawMap();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            generateRow(tileTypes[Random.Range(0, tileTypes.Count)], 
                        tileTypes[Random.Range(0, tileTypes.Count)], 
                        tileTypes[Random.Range(0, tileTypes.Count)]);
            drawMap();
        }
        drawMap();
    }

    private void addToTileTypes(TileType type, int numberCount) {
        for (int count = 0; count < numberCount; count++)
            tileTypes.Add(type);
    }

    /*draw out the whole map based on unit's tile type*/
    public void drawMap() {
        foreach (Unit unit in map) {
            Vector3Int currentCell = backgroundTileMap.WorldToCell(transform.position);
            switch (unit.Type) {
                case TileType.Empty:
                    backgroundTileMap.SetTile(unit.Pos, emptyTile);
                    break;
                case TileType.Enemy:
                    backgroundTileMap.SetTile(unit.Pos, enemyTile);
                    break;
                case TileType.Player:
                    backgroundTileMap.SetTile(unit.Pos, playerTile);
                    break;
                case TileType.Health:
                    backgroundTileMap.SetTile(unit.Pos, healthTile);
                    break;
                case TileType.Experience:
                    backgroundTileMap.SetTile(unit.Pos, experienceTile);
                    break;
                case TileType.Null:
                    backgroundTileMap.SetTile(unit.Pos, null);
                    break;
            }
        }
    }

    /* to draw the indicator on map with specified Vector3Int position that shows as indexes
       since it shows where player will move next, it will always be 1 index higher in y from position*/
    public void drawIndicator() {
        indicatorTileMap.SetTile(Map[player.MovingDestination.x, player.MovingDestination.y].Pos, indicatorTile);
    }

    /* delete indicator that drawn last time, so that there's only be one indicator */
    public void deleteIndicator(Vector3Int indicatorPos) {
        indicatorTileMap.SetTile(Map[indicatorPos.x, indicatorPos.y].Pos, null);
    }

    /* to set the unit in map with specific indexes to TileType.Empty */
    public void setUnitToEmpty(Vector3Int unitIndex) {
        Map[unitIndex.x, unitIndex.y].Type = TileType.Empty;
    }

    public void setUnitToNull(Vector3Int unitIndex) {
        Map[unitIndex.x, unitIndex.y].Type = TileType.Null;
    }

    /* replace and generate a new row at the top of the map*/
    public void generateRow(TileType column1Type, TileType column2Type, TileType column3Type) {
        shiftDownward();
        Map[0,map.GetLength(1)-1].Type = column1Type;
        Map[1,map.GetLength(1)-1].Type = column2Type;
        Map[2,map.GetLength(1)-1].Type = column3Type;
    }

    /* shift the whole map down for 1 cell*/
    public void shiftDownward()
    {
        for (int x = 0; x < Map.GetLength(0); x++)
            for (int y = 0; y < Map.GetLength(1)-1; y++)
                Map[x,y].duplicate(Map[x,y+1]);
    }

    /* check if pos is inside the map */
    public bool checkValidIndex(Vector3Int index) {
        if (index.x < 0 || index.x >= map.GetLength(0))
            return false;
        if (index.y < 0 || index.y >= map.GetLength(1))
            return false;
        return true;
    }

    /* check if destination is walkable */
    public bool checkWalkable(Vector3Int index) {
        if (checkValidIndex(index))
            if (player.CurrentIndex.x+1 >= index.x && player.CurrentIndex.x-1 <= index.x)
                if (player.CurrentIndex.y+1 >= index.y && player.CurrentIndex.y-1 <= index.y)
                    return true;
        return false;
    }
}
