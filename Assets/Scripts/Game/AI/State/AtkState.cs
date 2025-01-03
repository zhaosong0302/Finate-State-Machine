using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkState : BaseState
{
    public override E_AI_State AIState => E_AI_State.Atk;

    //攻击时间 等待攻击时间
    private float nextAtkTime;
    private float waitTime = 2f;

    public AtkState(StateMachine machine):base(machine)
    {

    }

    public override void EnterState()
    {
        Debug.Log("atk enter");
        //进入攻击状态立刻攻击
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

        //如果目标距离过远 切换到追逐状态
        if(Vector3.Distance(stateMachine.aiObj.nowPos, stateMachine.aiObj.targetObjPos)
            > stateMachine.aiObj.atkRange)
        {
            stateMachine.ChangeState(E_AI_State.Chase);
        }

        //利用向量四元数让AI对象朝向目标
        stateMachine.aiObj.objTransform.LookAt(stateMachine.aiObj.targetObjPos +
            Vector3.up * 0.5f);

        //超出最大距离回归
        stateMachine.CheckBackOrNot();
    }
}
