using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ״̬���� ����״̬�඼��̳���
/// </summary>
public abstract class BaseState
{
    //����״̬��ʵ�ֵ�AI�е� ��Щ ״̬�� ���ı��� ����������˵ ������ʲô��
    //�߼���������������AI���������ô�����ʲô�������� �����ڽ����߼�����

    //AI״̬���л� 
    //�л������ ����ζ��
    // ״̬1 ����> ״̬2

    //����������� ����ȥʵ������״̬���е� ���롢�뿪������״̬����Ϊ��������������
    //������Щ������ �����ǻ��࣬û����ȷ������״̬��Ҳ����ζ�ŷ����в���д����
    //��ô ����д���ݵĺ��� �������뵽ʲô��
    //1.����ǽӿ� ����ôֱ������
    //2.������࣬��ô���Կ��ǳ��󷽷�����һ���ǳ�����

    //�����Լ�������״̬������
    protected StateMachine stateMachine;


    /// <summary>
    /// ��ʼ��״̬��ʱ  �������ߴ��� ���м�¼
    /// </summary>
    /// <param name="machine"></param>
    public BaseState(StateMachine machine)
    {
        stateMachine = machine;
    }

    //��ǰ״̬������
    public virtual E_AI_State AIState
    {
        get;
    }


    // 1.�뿪״̬ʱ ��ʲô
    public abstract void QuitState();

    // 2.����״̬ʱ ��ʲô
    public abstract void EnterState();

    // 3.����״̬ʱ ��ʲô�������߼�����
    public abstract void UpdateState();
}
