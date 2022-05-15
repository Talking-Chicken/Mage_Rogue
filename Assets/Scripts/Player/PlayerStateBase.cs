using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerStateBase
{
    public virtual void EnterState(PlayerControl player) {}
    public virtual void UpdateState(PlayerControl player) {}
    public virtual void LeaveState(PlayerControl player) {}
}
