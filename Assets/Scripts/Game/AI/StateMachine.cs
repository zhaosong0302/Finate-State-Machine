using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����״̬���� ����Ҫ���ڹ������״̬֮����л�
/// </summary>
public class StateMachine
{
    //��Ҫ����AI������״̬
    //��������ͨ��һ������ȥ�洢��Щ״̬
    //��Щ״̬����ʱ��ȡ���������л� �������Ҫѡ��һ��������һ�ȡ�������洢
    //key ���� ״̬����(�����޵�״̬���ͣ���ô�Ϳ�����һ��ʼ�����ģ�
    //                 ��ʹ�Ժ�߻������п� ������״̬���� �����ǸĴ��뼴�ɣ���Ϊ���������ȸ��¼��� ����Ҳû��̫���Ӱ��)
    //value ���� ������Ǵ���״̬���߼�����
    private Dictionary<E_AI_State, BaseState> stateDic = new Dictionary<E_AI_State, BaseState>();

    //��ʾ��ǰ����״̬ ���ڵ�״̬��Ҳ���Ƕ�Ӧ�Ĺ������ҵ�ǰ���ڵ�AI״̬)
    private BaseState nowState;

    //�������ai����״̬�� ����� ai���� ��ȥͨ��ai״̬����ö��� ִ�ж�Ӧ����Ϊ
    public IAIObj aiObj;

    //�ع����
    public float backDis = 15;

    //���ǵ�����״̬��������AI �����кܶ��AI״̬
    //��ô��ЩAI״̬�߼����У�����Ҫȥ���ʲô�����Ӧ��״̬�߼�
    //�������ʵ�� ��Ϸ������ҪAI�Ķ��� ���� �����ҡ����NPC�ȵ�

    //��Ȼ��Щ�����ǲ�һ���Ķ��� ���� ������������˵��Ҫ�߱���ͬ����Ϊ
    //�����ڴ���AI�߼�ʱ �Ÿ��������һЩ��Ϊ�ĵ���

    //������ʵ���Գ��� ��AIģ�����Щ������ȡ���� ��Ϊ�ӿ� ����Щ��ҪAI�Ķ��� ����Ҫʵ������ӿ� ����

    /// <summary>
    /// ��ʼ������״̬���� 
    /// </summary>
    /// <param name="aiObj">���� ai���� ����֮�����Ϊ����</param>
    public void Init(IAIObj aiObj)
    {
        this.aiObj = aiObj;
    }

    /// <summary>
    /// ���AI״̬
    /// </summary>
    public void AddState(E_AI_State state)
    {
        switch (state)
        {
            case E_AI_State.Patrol:
                stateDic.Add(state, new PatrolState(this));
                break;
            case E_AI_State.Run:
                stateDic.Add(state, new RunState(this));
                break;
            case E_AI_State.Chase:
                stateDic.Add(state, new ChaseState(this));
                break;
            case E_AI_State.Atk:
                stateDic.Add(state, new AtkState(this));
                break;
        }
    }

    /// <summary>
    /// �ı�״̬
    /// </summary>
    public void ChangeState(E_AI_State state)
    {
        //�����ǰ������һ��״̬ ���˳���״̬
        if (nowState != null)
            nowState.QuitState();

        //������ڸ�״̬���߼��������� ��ô�ͽ����״̬
        if(stateDic.ContainsKey(state))
        {
            nowState = stateDic[state];
            nowState.EnterState();
        }
    }

    /// <summary>
    /// ���µ�ǰ״̬�߼�����
    /// </summary>
    public void UpdateState()
    {
        if (nowState != null)
            nowState.UpdateState();
    }

    //����Ƿ�ع�
    public void CheckBackOrNot()
    {
        //����������ع�
        if (Vector3.Distance(this.aiObj.nowPos, this.aiObj.bornPos)
            >= backDis)
        {
            ChangeState(E_AI_State.Run);
        }
    }
}
