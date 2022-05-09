using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType {Empty, PowerUp, Enemy, Obstacle, Door}
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

    [SerializeField] private Tilemap backgroundTileMap;
    [SerializeField] private Tile emptyTile, enemyTile;
    private Unit[,] map = new Unit[3,11];
    
    void Start()
    {
        //start from the middle bottom of the screen
        Vector3Int startPos = backgroundTileMap.WorldToCell(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2 - 1,0,0)));

        for (int x = 0; x < map.GetLength(0); x++) {
            for (int y = 0; y < map.GetLength(1); y++) {
                map[x,y] = new Unit(startPos + new Vector3Int(x,y,0));
            }
        }

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
            }
        }
    }

    /* replace and generate a new row at the top of the map*/
    public void generateRow(TileType column1Type, TileType column2Type, TileType column3Type) {
        shiftDownward();
        map[0,map.GetLength(1)-1].Type = column1Type;
        map[1,map.GetLength(1)-1].Type = column2Type;
        map[2,map.GetLength(1)-1].Type = column3Type;
    }

    /* shift the whole map down for 1 cell*/
    public void shiftDownward()
    {
        for (int x = 0; x < map.GetLength(0); x++)
            for (int y = 0; y < map.GetLength(1)-1; y++)
                map[x,y].duplicate(map[x,y+1]);
    }
}
