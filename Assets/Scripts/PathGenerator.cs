using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType {Empty, PowerUp, Enemy, Obstacle, Door, Player}
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
    [SerializeField] private Tile emptyTile, enemyTile, playerTile, indicatorTile;
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
        player = FindObjectOfType<PlayerControl>();
        //start from the middle bottom of the screen
        map[player.CurrentIndexX, player.CurrentIndexY].Type = TileType.Player;
        drawMap();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            generateRow(TileType.Enemy, TileType.Empty, TileType.Enemy);
            drawMap();
        }
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
            }
        }
    }

    /* to draw the indicator on map with specified Vector3Int position that shows as indexes
       since it shows where player will move next, it will always be 1 index higher in y from position*/
    public void drawIndicator() {
        indicatorTileMap.SetTile(Map[player.MovingDestination.x, player.MovingDestination.y+1].Pos, indicatorTile);
    }

    /* delete indicator that drawn last time, so that there's only be one indicator */
    public void deleteIndicator(Vector3Int indicatorPos) {
        indicatorTileMap.SetTile(Map[indicatorPos.x, indicatorPos.y+1].Pos, null);
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
    public bool checkValidIndex(int indexX, int indexY) {
        if (indexX < 0 || indexX >= map.GetLength(0))
            return false;
        if (indexY < 0 || indexY >= map.GetLength(1))
            return false;
        return true;
    }

    /* check if destination is walkable */
    public bool checkWalkable(int indexX, int indexY) {
        if (checkValidIndex(indexX, indexY))
            if (player.CurrentIndexX+1 >= indexX && player.CurrentIndexX-1 <= indexX)
                if (player.CurrentIndexY+1 >= indexY && player.CurrentIndexY-1 <= indexY)
                    return true;
        return false;
    }
}
