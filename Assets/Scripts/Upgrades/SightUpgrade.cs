using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*It increases player's sight by 2 (can see 2 more rows).*/
public class SightUpgrade : Upgrade
{
    public override void upgrade()
    {
        FindObjectOfType<PlayerControl>().Stats.Sight+=2;
        base.upgrade();
    }
}
