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
    [SerializeField] private Tile floor, door, wall;
    [SerializeField] private Tilemap backgroundTileMap;

    private class Room {
        private int width, height, doorCount;
        private RectInt rect;
        private List<Vector3Int> bound;

        //getters & setters
        public RectInt Rect {get => rect; set => rect = value;}
        public List<Vector3Int> Bound {get => bound; private set => bound = value;}

        public Room(int width, int height, Vector3Int startPos) {
           this.width = width;
           this.height = height;
           rect = new RectInt(startPos.x, startPos.y, width, height);
           Bound = getBounds();
        }

        public Vector3Int randomDoorPos() {
            return getBoundsButNotConors()[Random.Range(0, bound.Count)];
        }

        public List<Vector3Int> getBounds() {
            List<Vector3Int> bound = new List<Vector3Int>(); 
            for (int x = Rect.xMin; x < Rect.xMax; x++) {
                bound.Add(new Vector3Int(x, Rect.yMin));
                bound.Add(new Vector3Int(x, Rect.yMax-1));
            }

            for (int y = Rect.yMin; y < Rect.yMax; y++) {
                bound.Add(new Vector3Int(Rect.xMin, y));
                bound.Add(new Vector3Int(Rect.xMax-1, y));
            }

            return bound;
        }

        /* return all vector3Int positions that lying on the perimeter of the rectangle, but not including four conors */
        public List<Vector3Int> getBoundsButNotConors() {
            List<Vector3Int> boundWithoutConors = Bound;
            Vector3Int[] cornorsPos = new Vector3Int[] {new Vector3Int(Rect.xMin, Rect.yMin),
                                                     new Vector3Int(Rect.xMax-1, Rect.yMin),
                                                     new Vector3Int(Rect.xMin, Rect.yMax-1),
                                                     new Vector3Int(Rect.xMax-1, Rect.yMax-1)};

            List<Vector3Int> cornors = new List<Vector3Int>();

            //remove all cornors
            foreach (Vector3Int cell in boundWithoutConors) {
                foreach (Vector3Int cornor in cornorsPos)
                    if (cell.Equals(cornor))
                        cornors.Add(cornor);
            }
            foreach (Vector3Int cornor in cornors) {
                boundWithoutConors.Remove(cornor);
            }

            return boundWithoutConors;
        }
    }

    void Start()
    {
        Vector3Int currentCell = backgroundTileMap.WorldToCell(transform.position);
        Room r = new Room(4,6, backgroundTileMap.WorldToCell(transform.position));
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
        foreach (Vector3Int walls in  room.getBounds()) {
            backgroundTileMap.SetTile(walls, wall);
        }

        //build the door
        backgroundTileMap.SetTile(room.randomDoorPos(), door);
    }

    private void drawMap() {
        
    }
}
