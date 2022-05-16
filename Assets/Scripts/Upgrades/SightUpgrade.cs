using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightUpgrade : Upgrade
{
    public override void upgrade()
    {
        FindObjectOfType<PlayerControl>().Stats.Sight++;
        base.upgrade();
    }
}
