using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*It increases player's damage resistance from iron dummy enemy.*/
public class ShieldUpgrade : Upgrade
{
    public override void upgrade()
    {
        FindObjectOfType<PlayerControl>().Stats.IronDummyResistence += 1;
        base.upgrade();
    }
}
