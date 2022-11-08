using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*It increases player's damage resistance from zombie enemy.*/
public class CrossUpgrade : Upgrade
{
    public override void upgrade()
    {
        FindObjectOfType<PlayerControl>().Stats.ZombieResistence += 1;
        base.upgrade();
    }
}
