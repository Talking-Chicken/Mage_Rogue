using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMove : PlayerStateBase
{
    public override void EnterState(PlayerControl player) {
        player.upgrade();
        player.react(player.MovingDestination);

        //I set PathGenerator script run after PlayerControl script in Script Excution Order, so the tilemap will shift down only after player moved
        player.move();

        player.summonBoss();
        player.changeState(player.statePrepare);
    }
    public override void UpdateState(PlayerControl player) {}
    public override void LeaveState(PlayerControl player) {}
}
