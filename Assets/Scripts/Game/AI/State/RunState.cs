using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回归
/// </summary>

public class RunState : BaseState
{
    public override E_AI_State AIState => E_AI_State.Run;
    public RunState(StateMachine machine) : base(machine)
    {

    }
    public override void EnterState()
    {
        Debug.Log("out of range");
        //进入回归状态时回归即可
        stateMachine.aiObj.Move(stateMachine.aiObj.bornPos);
    }

    public override void QuitState()
    {

    }

    public override void UpdateState()
    {
        //到达后进入巡逻
        if(Vector3.Distance(stateMachine.aiObj.nowPos, stateMachine.aiObj.bornPos)
            <= 0.5f)
        {
            stateMachine.ChangeState(E_AI_State.Patrol);
        }
    }
}
