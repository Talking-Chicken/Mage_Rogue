using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*It makes cult enemy appears less frenquently, but increases the number of rats */
public class ClubUpgrade : Upgrade
{
    public override void upgrade()
    {
        FindObjectOfType<PathGenerator>().addToTileTypes(TileType.Rat,1);
        FindObjectOfType<PathGenerator>().removeFromTileTypes(TileType.Enemy);
        base.upgrade();
    }
}
