using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    public override E_AI_State AIState => E_AI_State.Chase;

    //计数,隔几针调用一次
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
        //就可以通过管理者对象 得到控制的 ai对象 对它进行操作 即可
        //stateMachine.aiObj
        //追逐目标

        if(timer % 10 == 0)
            stateMachine.aiObj.Move(stateMachine.aiObj.targetObjPos);

        ++timer;

        if (Vector3.Distance(stateMachine.aiObj.nowPos, stateMachine.aiObj.targetObjPos)
            <= stateMachine.aiObj.atkRange)
        {
            stateMachine.ChangeState(E_AI_State.Atk);
        }

        //超出最大距离回归
        stateMachine.CheckBackOrNot();
    }
}
