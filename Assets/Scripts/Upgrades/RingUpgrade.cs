using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*It makes experience appears more.*/
public class RingUpgrade : Upgrade
{
    public override void upgrade()
    {
        FindObjectOfType<PathGenerator>().addToTileTypes(TileType.Experience, 1);
        base.upgrade();
    }
}
