using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatePrepare : PlayerStateBase
{
    public override void EnterState(PlayerControl player) {}
    public override void UpdateState(PlayerControl player) {
        player.selectMovingDestination();
        player.drawIndicator();
    }
    public override void LeaveState(PlayerControl player) {}
}
