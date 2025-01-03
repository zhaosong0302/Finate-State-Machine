using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ع�
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
        //����ع�״̬ʱ�ع鼴��
        stateMachine.aiObj.Move(stateMachine.aiObj.bornPos);
    }

    public override void QuitState()
    {

    }

    public override void UpdateState()
    {
        //��������Ѳ��
        if(Vector3.Distance(stateMachine.aiObj.nowPos, stateMachine.aiObj.bornPos)
            <= 0.5f)
        {
            stateMachine.ChangeState(E_AI_State.Patrol);
        }
    }
}
