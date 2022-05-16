using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
                ironDummyTile;
    
    public int enemyDamage,
               zombieDamage,
               ratDamage,
               ironDummyDamage;
    
    public int[] levelUpRequirements;
}
