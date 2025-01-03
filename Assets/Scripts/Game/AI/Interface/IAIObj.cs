using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI对象接口 用于规范 需要使用AI的对象的 行为
/// </summary>
public interface IAIObj 
{
    //所有AI对象的Transform
    public Transform objTransform
    {
        get;
    }

    //所有AI对象都应该有一个当前的位置
    public Vector3 nowPos
    {
        get;
    }

    //AI对象的目标对象所在的位置
    public Vector3 targetObjPos
    {
        get;
    }

    public float atkRange
    {
        get;
    }

    //出生点
    public Vector3 bornPos
    {
        get;
        set;
    }

    //AI对象中 应该有 移动相关的方法
    public void Move(Vector3 dirOrPos);
    //AI对象中 应该有 停止移动相关的方法
    public void StopMove();
    //AI对象中 应该有 攻击相关的方法
    public void Atk();
    //AI对象中 可能想要单独 切换指定动作
    //切换动作 应该传递一些相关参数 才能够指定切换哪个动作吧
    public void ChangeAction(E_Action action);


    //我们应该根据AI不同的状态 去提取出他们的行为合集
}
