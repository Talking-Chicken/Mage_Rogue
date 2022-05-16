using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMove : PlayerStateBase
{
    public override void EnterState(PlayerControl player) {
        player.upgrade();
        player.react(player.MovingDestination);
        player.move();
        player.changeState(player.statePrepare);
    }
    public override void UpdateState(PlayerControl player) {}
    public override void LeaveState(PlayerControl player) {}
}
