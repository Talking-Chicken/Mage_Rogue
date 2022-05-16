using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatePrepare : PlayerStateBase
{
    public override void EnterState(PlayerControl player) {}
    public override void UpdateState(PlayerControl player) {
        player.selectMovingDestination();
        player.drawIndicator();

        player.navigatingUpgrades();
        player.levelUp();

        if (Input.GetKeyDown(KeyCode.Space))
            player.changeState(player.stateMove);
    }
    public override void LeaveState(PlayerControl player) {}
}
