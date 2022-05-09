using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum CellType {Floor, Door, Wall}

public class MapCell {
    private Vector3Int pos;
    private CellType type;

    public Vector3Int Pos {get => pos; set => pos = value;}
    public CellType Type {get => type; set => type = value;}

    public MapCell (Vector3Int pos, CellType type) {
        Pos = pos;
        Type = type;
    }

    public MapCell (int posX, int posY, CellType type) {
        Pos = new Vector3Int(posX, posY);
        Type = type;
    }
}

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private Tile floor, door, leftVerticalWallTile, rightVerticalWallTile, horizontalWallTile,cornorTile;
    [SerializeField] private Tilemap backgroundTileMap;

        void Start()
    {
        Vector3Int currentCell = backgroundTileMap.WorldToCell(transform.position);
        Room r = new Room(4,6, currentCell);
        build(r);
    }

    
    void Update()
    {
        
    }

    /* build this room in tilemap from the position startPos
           @param startPos the position of the lower left cornor of the room */
    private void build(Room room) {
        for (int x = room.Rect.xMin; x < room.Rect.xMax; x++) {
            for (int y = room.Rect.yMin; y < room.Rect.yMax; y++) {
                backgroundTileMap.SetTile(new Vector3Int(x,y), floor);
            }
        }

        //build the wall
        foreach (Vector3Int leftVerticalWall in  room.LeftBound) {
            backgroundTileMap.SetTile(leftVerticalWall, leftVerticalWallTile);
        }
        foreach (Vector3Int rightVerticalWall in room.RightBound) {
            backgroundTileMap.SetTile(rightVerticalWall, rightVerticalWallTile);
        }
        foreach (Vector3Int horizontalWall in room.HorizontalWalls) {
            backgroundTileMap.SetTile(horizontalWall, horizontalWallTile);
        }
        foreach (Vector3Int corner in room.Corners) {
            backgroundTileMap.SetTile(corner, cornorTile);
        }

        //build the door
        backgroundTileMap.SetTile(room.randomDoorPos(), door);
    }

    private void drawMap() {
        
    }

    
}
