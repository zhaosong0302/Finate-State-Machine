using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
/// <summary>
/// 巡逻状态 状态逻辑处理类
/// 主要处理 对象在巡逻时  有什么表现
/// 我们可以根据自己的需求 来定巡逻的规则
/// 一般游戏当中常见的巡逻规则
/// 
/// 1.原地播放某个动作（睡觉、放哨、烤肉、打瞌睡等等）
///   分析需要添加的成员（变量、方法等）：
///   具体切换到哪个动作（动作类型E_Action ）
///   
/// 2.在一定范围内随机移动
///   随机移动的范围区间（圆形区间 也可以是 矩形区间 或者其他形状）  
///   圆（半径）       矩形（宽高） 
///   圆心（Vector3）  矩形中心（Vector3）
///   
/// 3.在多个点上移动（循环移动、来回移动）
///   装载移动点位的容器（List）  
///   当前目标点在容器当中的索引位置（int）
///   移动方式（循环还是来回 bool或int）
///   
/// 可能三种巡逻方式都会使用到的成员
///   目标位置（Vector3）
///   当前处于的巡逻类型（int）
/// </summary>
public class PatrolState : BaseState
{
    //所有巡逻方式都会用到的成员
    //目标位置 要移动到的位置（Vector3）
    private Vector3 targetPos;
    //当前处于的巡逻类型（int）
    //1 代表原地播放
    //2 代表随机移动
    //3 代表多个点上移动
    private int type;

    //目标范围检测的距离 目标进入这个范围内 就要脱离循环
    //切换到 追逐状态
    private float checkDis;

    //原地播放某个动作（睡觉、放哨、烤肉、打瞌睡等等）
    //想要保持的动作类型
    private E_Action actionType;

    //在一定范围内随机移动
    //圆心（Vector3）
    private Vector3 centerPos;
    //圆（半径）
    private float radius;

    //是否要改变一个目标位置
    private bool isChangePos;

    //到达目标点后是否等待
    private bool isWait;
    private float waitTime;

    //在多个点上移动（循环移动、来回移动）
    //装载移动点位的容器（List）  
    private List<Vector3> pointList = new List<Vector3>();
    //当前目标点在容器当中的索引位置（int）
    private int pointIndex = 0;
    private bool isAdd;
    //移动方式（循环还是来回 bool或int）
    //1 代表循环移动
    //2 代表来回移动
    private int moveType;

    public override E_AI_State AIState => E_AI_State.Patrol;

    //1.AI相关代码都写完了 在加入一个立方体来应用AI 达到一个AI表现

    //2.直接加入一个立方体，边做AI 边测试

    public PatrolState(StateMachine machine) : base(machine)
    {
        //在构造函数中 对 巡逻相关的数据进行初始化
        type = 3;
        actionType = E_Action.Sleep;

        centerPos = Vector3.zero;
        radius = 5;

        pointList.Add(Vector3.right*2);
        pointList.Add(new Vector3(1, 0, 1)*2);
        pointList.Add(new Vector3(0, 0, 1)*2);
        pointList.Add(new Vector3(0, 0, 0)*2);


        pointIndex = 2;

        moveType = 1;

        isChangePos = true;
        waitTime = 0;

        checkDis = 5;
    }

    public override void EnterState()
    {
        Debug.Log("patrol enter success");
    }

    public override void QuitState()
    {
        if (type == 1)
        {
            //如果是原地播放某个动作时 当离开巡逻状态时 可以播放另一个脱离相关的动作
        }
        else
        {
            //切换状态之前 停止移动
            stateMachine.aiObj.StopMove();
            Debug.Log("patrol exit");
        }
    }


    //处于巡逻状态时 做什么（核心逻辑处理）
    public override void UpdateState()
    {
        Debug.Log("patrolling");
        //所有的AI状态对象
        //都可能在处于该状态时 进行一些条件判断
        //当满足状态切换的条件时  应该会去进行状态的切换
        //1 代表原地播放
        //2 代表随机移动
        //3 代表多个点上移动

        //3中巡逻表现都会存在脱离巡逻状态的逻辑处理
        //那么我们可以选择两种处理方式
        //1.通用处理：
        //  适用于巡逻状态脱离表现一致时 比如：当玩家进入到自己的多少范围内 就脱离巡逻

        //2.分情况处理：
        //  2-1：睡觉时，即使玩家进入范围也不脱离，一定要被玩家攻击才脱离
        //  2-2: 随机移动时，玩家和我的距离到达一定范围内就脱离
        //  2-3: 玩家移动到我们视野范围内，再脱离

        // 大家投票决定 通用处理即可，因为分情况处理时 逻辑差不多

        switch (type)
        {
            //原地播放动画 表现
            case 1:
                //想要控制AI对象 执行播放动画的行为
                //只需要去获取到AI对象 通过有限状态基类里面存储的 对象引用 来命令它
                //命令它播放某一个动画
                stateMachine.aiObj.ChangeAction(actionType);
                break;
            //在圆范围内随机移动的表现
            case 2:
            //在多个点上来回移动或者循环移动的表现
            case 3:
                //等待时间为0 才执行里面的逻辑
                if(waitTime <= 0)
                {
                    //判断什么时候随机位置
                    if (isChangePos)
                    {
                        if(type == 2)
                        {
                            //利用向量和四元数相乘 对向量进行随机旋转 再乘以半径 再加上圆心点 相当于就是得到圆轮廓上的某一个点 
                            targetPos = Quaternion.Euler(0, Random.Range(0f, 359f), 0) * Vector3.forward * radius + centerPos;
                            isChangePos = false;
                        }
                        else if(type == 3)
                        {
                            //从多个点的容器中取出一个点
                            targetPos = pointList[pointIndex];
                            //然后 点的索引位置要变化
                            //1 代表循环移动
                            //2 代表来回移动
                            if (moveType == 1)
                            {
                                ++pointIndex;
                                //循环移动时 索引走完归零
                                if (pointIndex == pointList.Count)
                                    pointIndex = 0;
                            }
                            else
                            {
                                //如果是往前选点 isAdd会是true 否则会是false
                                pointIndex = isAdd ? pointIndex + 1 : pointIndex - 1;
                                if (pointIndex == pointList.Count)
                                {
                                    isAdd = false;
                                    --pointIndex;
                                }

                                else if (pointIndex < 0)
                                {
                                    isAdd = true;
                                    ++pointIndex;
                                }
                            }
                            isChangePos = false;
                        }

                    }
                    //AI对象应该不停的朝向目标点去移动
                    stateMachine.aiObj.Move(targetPos);
                    //判断什么时候再随机
                    //AI对象到达目标点时 就随机 
                    //AI对象不可能刚好踩到目标点 所以我们进行目标点之间的距离判断
                    if (Vector3.Distance(targetPos, stateMachine.aiObj.nowPos) < 0.2f)
                    {
                        isChangePos = true;
                        //到达目标点后 停止移动
                        stateMachine.aiObj.StopMove();
                        //等待一定时间后 再移动
                        waitTime = 0;
                    }
                }
                else
                    waitTime -= Time.deltaTime;
                break;
        }

        //脱离巡逻判断
        //通用处理 当玩家里怪物一定距离内就进行巡逻状态脱离
        if(Vector3.Distance(stateMachine.aiObj.nowPos, stateMachine.aiObj.targetObjPos) < checkDis)
        {
            stateMachine.ChangeState(E_AI_State.Chase);
        }
    }
}
