using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IAIObj
{
    //����Ѱ·���
    private NavMeshAgent navMeshAgent;

    //����Ҫʹ��AIģ��Ķ����� ����һ�� AI״̬������ ���ڿ���AI����
    private StateMachine aiStateMachine;

    private Vector3 nowObjPos;
    //����ǰ��λ��
    public Vector3 nowPos {
        get
        {
            nowObjPos = this.transform.position;
            //Ϊ�˺�����AIģ��Ķ�λ������ͬ û�п��� Y�ϵ�λ�� ��Ҫ����xzƽ�����λ��
            nowObjPos.y = 0;
            return nowObjPos;
        }
    }

    //������
    public Vector3 bornPos
    {
        get;
        set;
    }

    //AI������Ҫ����ȡTS
    public Transform objTransform => this.transform;

    Vector3 IAIObj.targetObjPos
    {
        get
        {
            return GameObject.Find("Player").transform.position;
        }
    }

    //Ŀǰ����д����һ��ͨ�����ñ�
    public float atkRange => 2;

    //�����������ڻ�����ȥ���� Ŀ�� ��������һ��Ŀ��λ��
    //public Vector3 targetObjPos;

    private void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();

        //֮���԰�AI����Ҫ��ʼ�� �ŵ������൱�� ��Ҫԭ��
        //����Ϊ��ͬ���� ���ܻ���ڲ�ͬ��AI״̬����ͬ����ʼ״̬
        //��Щ��������Ϸ�� �������ñ������õ� ����һ��д�ڹ��ﴴ����

        //ע�⣺
        //���������� ����� ����������еĴ�������ķ����У���������Ŀǰû����ƹ��������
        //��ˣ����ǰ���һ����� ������ ����������������ں����� Ҳ����Start�У�Ҳ���Է���Awake)

        //��ʼ��AIģ�������״̬������
        aiStateMachine = new StateMachine();
        //��ai�����Լ� �������н��г�ʼ��
        aiStateMachine.Init(this);

        //����ҪʲôAI״̬ �Ͷ�̬��ӣ��Ժ�һ������� ��ͨ�����ñ������ȥ��ӣ�
        //ΪAI���Ѳ��״̬
        aiStateMachine.AddState(E_AI_State.Patrol);
        aiStateMachine.AddState(E_AI_State.Chase);
        aiStateMachine.AddState(E_AI_State.Atk);
        aiStateMachine.AddState(E_AI_State.Run);

        //��ʼ��������AI״̬�� �Ǿ���Ҫһ����ǰ��AI״̬
        //Ŀǰһ��ʼ���ö���ʱһ��Ѳ��״̬
        aiStateMachine.ChangeState(E_AI_State.Patrol);

        //�����㸳ֵ
        bornPos = this.transform.position;
    }

    private void Update()
    {
        //ai��صĸ��� ���� ai����� ֡���º��� ����� 
        aiStateMachine.UpdateState();
    }


    public void Atk()
    {
        //��ʱ��д ֮��д������AIʱ ��ȥд��
        print("attack");
    }

    public void ChangeAction(E_Action action)
    {
        print(action);
    }

    public void Move(Vector3 dirOrPos)
    {
        //����ֹͣ�ƶ�
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(dirOrPos);
    }

    public void StopMove()
    {
        //�÷�����ʱ��
        //navMeshAgent.Stop();
        //ֹͣ�ƶ�
        navMeshAgent.isStopped = true;
    }
}
