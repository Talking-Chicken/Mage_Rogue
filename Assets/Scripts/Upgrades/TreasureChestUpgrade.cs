using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestUpgrade : Upgrade
{
    public override void upgrade()
    {
        FindObjectOfType<PathGenerator>().addToTileTypes(TileType.Health,1);
        base.upgrade();
    }
}
