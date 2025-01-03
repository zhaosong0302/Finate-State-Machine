using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI状态类型 是由策划决定的 一开始就置顶好的规则
/// </summary>
public enum E_AI_State 
{
    /// <summary>
    /// 睡眠状态
    /// </summary>
    Sleep,
    /// <summary>
    /// 巡逻状态
    /// </summary>
    Patrol,
    /// <summary>
    /// 聊天状态
    /// </summary>
    Chat,
    /// <summary>
    /// 逃跑状态
    /// </summary>
    Run,
    /// <summary>
    /// 追逐玩家状态
    /// </summary>
    Chase,
    /// <summary>
    /// 攻击玩家的状态
    /// </summary>
    Atk,
    /// <summary>
    /// 警觉状态
    /// </summary>
    Alertness,
}
