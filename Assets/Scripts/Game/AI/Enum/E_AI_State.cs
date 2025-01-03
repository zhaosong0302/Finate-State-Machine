using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI״̬���� ���ɲ߻������� һ��ʼ���ö��õĹ���
/// </summary>
public enum E_AI_State 
{
    /// <summary>
    /// ˯��״̬
    /// </summary>
    Sleep,
    /// <summary>
    /// Ѳ��״̬
    /// </summary>
    Patrol,
    /// <summary>
    /// ����״̬
    /// </summary>
    Chat,
    /// <summary>
    /// ����״̬
    /// </summary>
    Run,
    /// <summary>
    /// ׷�����״̬
    /// </summary>
    Chase,
    /// <summary>
    /// ������ҵ�״̬
    /// </summary>
    Atk,
    /// <summary>
    /// ����״̬
    /// </summary>
    Alertness,
}
