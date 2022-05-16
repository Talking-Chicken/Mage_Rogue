using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossUpgrade : Upgrade
{
    public override void upgrade()
    {
        FindObjectOfType<PlayerControl>().Stats.ZombieResistence += 1;
        base.upgrade();
    }
}
