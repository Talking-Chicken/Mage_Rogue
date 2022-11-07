using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/* Fixed Data is a scriptable object that stores data that will not change during the game, such as enemy damages and references of each tile.*/
[CreateAssetMenu(fileName = "FixedData", menuName = "ScriptableObjects/FixedData", order = 1)]
public class FixedData : ScriptableObject
{
    public Tile emptyTile, 
                enemyTile, 
                playerTile, 
                indicatorTile, 
                experienceTile, 
                healthTile, 
                zombieTile, 
                ratTile, 
                ironDummyTile,
                fogTile,
                bossTile;
    
    public int enemyDamage,
               zombieDamage,
               ratDamage,
               ironDummyDamage;
    
    public int[] levelUpRequirements;
    public int[] bossAppearLevel;
}
