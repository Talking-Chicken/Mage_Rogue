using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* represent each tile in the tile map*/
public enum TileType {Empty, Experience, Enemy, Player, Health, Zombie, Rat, IronDummy, Boss, Null}

/*Unit Class is a representation of each "Tile" in the game. It stores tile's type and position, having a funtion to duplicate other units.*/
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

/* Path Generator Class generate a map the beginning of the game. Generate rows by rows during the game when player moves.
   It has functions of setting individual units, show indicator of player's destination, and shifting the whole map downwards.*/
public class PathGenerator : MonoBehaviour
{

    [SerializeField] private Tilemap backgroundTileMap, indicatorTileMap, fogTileMap;
    [SerializeField] private FixedData mapData;
    private PlayerStats playerStats;
    private List<TileType> tileTypes = new List<TileType>(); //collection of TileType for randomization
    private Unit[,] map = new Unit[3,11];

    private PlayerControl player;
    private Boss boss;

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
        playerStats = player.Stats;
        boss = FindObjectOfType<Boss>();

        addToTileTypes(TileType.Empty, playerStats.EmptyFrequency);
        addToTileTypes(TileType.Enemy, playerStats.EnemyFrequency);
        addToTileTypes(TileType.Experience, playerStats.ExperienceFrequency);
        addToTileTypes(TileType.Health, playerStats.HealthFrequency);
        addToTileTypes(TileType.Zombie, playerStats.ZombieFrequency);
        addToTileTypes(TileType.Rat, playerStats.RatFrequency);
        addToTileTypes(TileType.IronDummy, playerStats.IronDummyFrequency);

        //randomize beginning map
        foreach (Unit unit in Map) {
            unit.Type = tileTypes[Random.Range(0, tileTypes.Count)];
        }

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
            if (boss.BossYIndex < 0) {
                drawBoss();
            }
        }
        drawMap();
    }

    public void addToTileTypes(TileType type, int numberCount) {
        for (int count = 0; count < numberCount; count++)
            tileTypes.Add(type);
    }

    public void removeFromTileTypes(TileType type) {
        if (tileTypes.Contains(type))
            tileTypes.Remove(type);
    }

    /*draw out the whole map based on unit's tile type
      Then, draw fogs above them*/
    public void drawMap() {
        foreach (Unit unit in map) {
            Vector3Int currentCell = backgroundTileMap.WorldToCell(transform.position);
            switch (unit.Type) {
                case TileType.Empty:
                    backgroundTileMap.SetTile(unit.Pos, mapData.emptyTile);
                    break;
                case TileType.Enemy:
                    backgroundTileMap.SetTile(unit.Pos, mapData.enemyTile);
                    break;
                case TileType.Player:
                    backgroundTileMap.SetTile(unit.Pos, mapData.playerTile);
                    break;
                case TileType.Health:
                    backgroundTileMap.SetTile(unit.Pos, mapData.healthTile);
                    break;
                case TileType.Experience:
                    backgroundTileMap.SetTile(unit.Pos, mapData.experienceTile);
                    break;
                case TileType.Zombie:
                    backgroundTileMap.SetTile(unit.Pos, mapData.zombieTile);
                    break;
                case TileType.Rat:
                    backgroundTileMap.SetTile(unit.Pos, mapData.ratTile);
                    break;
                case TileType.IronDummy:
                    backgroundTileMap.SetTile(unit.Pos, mapData.ironDummyTile);
                    break;
                case TileType.Boss:
                    backgroundTileMap.SetTile(unit.Pos, mapData.bossTile);
                    break;
                case TileType.Null:
                    backgroundTileMap.SetTile(unit.Pos, null);
                    break;
            }
        }

        //set fog tiles
        for (int y = Map.GetLength(1)-1; y > playerStats.Sight; y--)
            for (int x = 0; x < Map.GetLength(0); x++)
                fogTileMap.SetTile(Map[x,y].Pos, mapData.fogTile);
        for (int y = playerStats.Sight; y > 0; y--)
            for (int x = 0; x < Map.GetLength(0); x++)
                fogTileMap.SetTile(Map[x,y].Pos, null);
    }

    /* to draw the indicator on map with specified Vector3Int position that shows as indexes
       This is the position that player will move to*/
    public void drawIndicator() {
        indicatorTileMap.SetTile(Map[player.MovingDestination.x, player.MovingDestination.y].Pos, mapData.indicatorTile);
    }

    /* delete indicator that drawn last time, so that there's only be one indicator */
    public void deleteIndicator(Vector3Int indicatorPos) {
        indicatorTileMap.SetTile(Map[indicatorPos.x, indicatorPos.y].Pos, null);
    }

    /* replace a tile to boss tile according to boss appear position */
    public void drawBoss() {
        Vector3Int bossPos = boss.bossPosition();
        boss.BossYIndex = bossPos.y;
        Map[bossPos.x, bossPos.y].Type = TileType.Boss;
        backgroundTileMap.SetTile(Map[bossPos.x, bossPos.y].Pos, mapData.bossTile);
    }

    /* to set the unit in map with specific indexes to TileType.Empty */
    public void setUnitToEmpty(Vector3Int unitIndex) {
        Map[unitIndex.x, unitIndex.y].Type = TileType.Empty;
    }

    public void setUnitToNull(Vector3Int unitIndex) {
        Map[unitIndex.x, unitIndex.y].Type = TileType.Null;
    }

    /* replace and generate a new row at the top of the map 
       This triggers whenever player moves on step*/
    public void generateRow(TileType column1Type, TileType column2Type, TileType column3Type) {
        shiftDownward();
        Map[0,map.GetLength(1)-1].Type = column1Type;
        Map[1,map.GetLength(1)-1].Type = column2Type;
        Map[2,map.GetLength(1)-1].Type = column3Type;
    }

    /* shift the whole map down for 1 row, and track the y position of boss (if there's one) 
       It's the same as player move up 1 step*/
    public void shiftDownward()
    {
        for (int x = 0; x < Map.GetLength(0); x++)
            for (int y = 0; y < Map.GetLength(1)-1; y++)
                Map[x,y].duplicate(Map[x,y+1]);
        
        if (!boss.IsBossEliminated)
            boss.BossYIndex--;
    }

    /* check if index is inside the map */
    public bool checkValidIndex(Vector3Int index) {
        if (index.x < 0 || index.x >= map.GetLength(0))
            return false;
        if (index.y < 0 || index.y >= map.GetLength(1))
            return false;
        return true;
    }

    /* check if destination is in range of player reachable distance (+1/-1 unit) */
    public bool checkWalkable(Vector3Int index) {
        if (checkValidIndex(index))
            if (player.CurrentIndex.x+1 >= index.x && player.CurrentIndex.x-1 <= index.x)
                return true;
        return false;
    }
}
