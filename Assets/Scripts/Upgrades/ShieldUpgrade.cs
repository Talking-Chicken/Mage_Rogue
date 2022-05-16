using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUpgrade : Upgrade
{
    public override void upgrade()
    {
        FindObjectOfType<PlayerControl>().Stats.IronDummyResistence += 1;
        base.upgrade();
    }
}
