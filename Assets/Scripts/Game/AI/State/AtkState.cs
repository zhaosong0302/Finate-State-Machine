using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkState : BaseState
{
    public override E_AI_State AIState => E_AI_State.Atk;

    //����ʱ�� �ȴ�����ʱ��
    private float nextAtkTime;
    private float waitTime = 2f;

    public AtkState(StateMachine machine):base(machine)
    {

    }

    public override void EnterState()
    {
        Debug.Log("atk enter");
        //���빥��״̬���̹���
        nextAtkTime = Time.time;
    }

    public override void QuitState()
    {
        
    }

    public override void UpdateState()
    {
        if (Time.time >= nextAtkTime)
        {
            stateMachine.aiObj.Atk();
            nextAtkTime = Time.time + waitTime;
        }

        //���Ŀ������Զ �л���׷��״̬
        if(Vector3.Distance(stateMachine.aiObj.nowPos, stateMachine.aiObj.targetObjPos)
            > stateMachine.aiObj.atkRange)
        {
            stateMachine.ChangeState(E_AI_State.Chase);
        }

        //����������Ԫ����AI������Ŀ��
        stateMachine.aiObj.objTransform.LookAt(stateMachine.aiObj.targetObjPos +
            Vector3.up * 0.5f);

        //����������ع�
        stateMachine.CheckBackOrNot();
    }
}
