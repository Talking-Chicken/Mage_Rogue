using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthUpgrade : Upgrade
{
    public override void upgrade()
    {
        FindObjectOfType<PlayerControl>().Stats.MaxHealth += 2;
        FindObjectOfType<PlayerControl>().Stats.Health += 2;
        base.upgrade();
    }
}
