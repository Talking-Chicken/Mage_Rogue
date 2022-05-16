using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubUpgrade : Upgrade
{
    public override void upgrade()
    {
        FindObjectOfType<PathGenerator>().addToTileTypes(TileType.Rat,1);
        FindObjectOfType<PathGenerator>().removeFromTileTypes(TileType.Enemy);
        base.upgrade();
    }
}
