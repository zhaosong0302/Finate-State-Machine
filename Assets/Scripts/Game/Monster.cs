using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IAIObj
{
    //网格寻路组件
    private NavMeshAgent navMeshAgent;

    //在需要使用AI模块的对象当中 声明一个 AI状态机对象 用于开启AI功能
    private StateMachine aiStateMachine;

    private Vector3 nowObjPos;
    //对象当前的位置
    public Vector3 nowPos {
        get
        {
            nowObjPos = this.transform.position;
            //为了和我们AI模块的定位规则相同 没有考虑 Y上的位置 主要是在xz平面进行位移
            nowObjPos.y = 0;
            return nowObjPos;
        }
    }

    //出生点
    public Vector3 bornPos
    {
        get;
        set;
    }

    //AI对象需要被获取TS
    public Transform objTransform => this.transform;

    Vector3 IAIObj.targetObjPos
    {
        get
        {
            return GameObject.Find("Player").transform.position;
        }
    }

    //目前可以写死，一般通过配置表
    public float atkRange => 2;

    //由于我们现在还不用去考虑 目标 所以随便给一个目标位置
    //public Vector3 targetObjPos;

    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        //之所以把AI的重要初始化 放到对象类当中 主要原因
        //是因为不同对象 可能会存在不同的AI状态，不同的起始状态
        //这些往往在游戏中 都是配置表当中配置的 所以一般写在怪物创建处

        //注意：
        //大多数情况下 会放在 怪物管理器中的创建怪物的方法中，但是我们目前没有设计怪物管理器
        //因此，我们把这一块代码 放在了 怪物出生的生命周期函数中 也就是Start中（也可以放在Awake)

        //初始化AI模块的有限状态机对象
        aiStateMachine = new StateMachine();
        //把ai对象自己 传入其中进行初始化
        aiStateMachine.Init(this);

        //你需要什么AI状态 就动态添加（以后一般情况下 是通过配置表的配置去添加）
        //为AI添加巡逻状态
        aiStateMachine.AddState(E_AI_State.Patrol);
        aiStateMachine.AddState(E_AI_State.Chase);
        aiStateMachine.AddState(E_AI_State.Atk);
        aiStateMachine.AddState(E_AI_State.Run);

        //初始化完所有AI状态后 那就需要一个当前的AI状态
        //目前一开始就让对象时一个巡逻状态
        aiStateMachine.ChangeState(E_AI_State.Patrol);

        //出生点赋值
        bornPos = this.transform.position;
    }

    private void Update()
    {
        //ai相关的更新 是由 ai对象的 帧更新函数 发起的 
        aiStateMachine.UpdateState();
    }


    public void Atk()
    {
        //暂时不写 之后写到攻击AI时 再去写它
        print("attack");
    }

    public void ChangeAction(E_Action action)
    {
        print(action);
    }

    public void Move(Vector3 dirOrPos)
    {
        //结束停止移动
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(dirOrPos);
    }

    public void StopMove()
    {
        //该方法过时了
        //navMeshAgent.Stop();
        //停止移动
        navMeshAgent.isStopped = true;
    }
}
