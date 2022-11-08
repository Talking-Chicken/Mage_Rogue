using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*It increases player's max health by 2.*/
public class MaxHealthUpgrade : Upgrade
{
    public override void upgrade()
    {
        FindObjectOfType<PlayerControl>().Stats.MaxHealth += 2;
        FindObjectOfType<PlayerControl>().Stats.Health += 2;
        base.upgrade();
    }
}
