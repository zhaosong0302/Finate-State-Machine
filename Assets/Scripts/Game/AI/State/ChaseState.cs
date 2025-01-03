using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    public override E_AI_State AIState => E_AI_State.Chase;

    //����,���������һ��
    private int timer;


    public ChaseState(StateMachine machine) : base(machine)
    {

    }

    public override void EnterState()
    {
        Debug.Log("chase enter success");
    }

    public override void QuitState()
    {

    }

    public override void UpdateState()
    {
        //�Ϳ���ͨ�������߶��� �õ����Ƶ� ai���� �������в��� ����
        //stateMachine.aiObj
        //׷��Ŀ��

        if(timer % 10 == 0)
            stateMachine.aiObj.Move(stateMachine.aiObj.targetObjPos);

        ++timer;

        if (Vector3.Distance(stateMachine.aiObj.nowPos, stateMachine.aiObj.targetObjPos)
            <= stateMachine.aiObj.atkRange)
        {
            stateMachine.ChangeState(E_AI_State.Atk);
        }

        //����������ع�
        stateMachine.CheckBackOrNot();
    }
}
