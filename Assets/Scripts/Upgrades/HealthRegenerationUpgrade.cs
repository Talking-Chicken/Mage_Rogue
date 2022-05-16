using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegenerationUpgrade : Upgrade
{
    public override void upgrade()
    {
        FindObjectOfType<PlayerControl>().Stats.HealthRegeneration++;
        base.upgrade();
    }
}
