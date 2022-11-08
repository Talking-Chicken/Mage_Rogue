using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*It makes player heal more when pick up health potion.*/
public class HealthRegenerationUpgrade : Upgrade
{
    public override void upgrade()
    {
        FindObjectOfType<PlayerControl>().Stats.HealthRegeneration++;
        base.upgrade();
    }
}
