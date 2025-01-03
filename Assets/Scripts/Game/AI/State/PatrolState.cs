using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
/// <summary>
/// Ѳ��״̬ ״̬�߼�������
/// ��Ҫ���� ������Ѳ��ʱ  ��ʲô����
/// ���ǿ��Ը����Լ������� ����Ѳ�ߵĹ���
/// һ����Ϸ���г�����Ѳ�߹���
/// 
/// 1.ԭ�ز���ĳ��������˯�������ڡ����⡢���˯�ȵȣ�
///   ������Ҫ��ӵĳ�Ա�������������ȣ���
///   �����л����ĸ���������������E_Action ��
///   
/// 2.��һ����Χ������ƶ�
///   ����ƶ��ķ�Χ���䣨Բ������ Ҳ������ �������� ����������״��  
///   Բ���뾶��       ���Σ���ߣ� 
///   Բ�ģ�Vector3��  �������ģ�Vector3��
///   
/// 3.�ڶ�������ƶ���ѭ���ƶ��������ƶ���
///   װ���ƶ���λ��������List��  
///   ��ǰĿ������������е�����λ�ã�int��
///   �ƶ���ʽ��ѭ���������� bool��int��
///   
/// ��������Ѳ�߷�ʽ����ʹ�õ��ĳ�Ա
///   Ŀ��λ�ã�Vector3��
///   ��ǰ���ڵ�Ѳ�����ͣ�int��
/// </summary>
public class PatrolState : BaseState
{
    //����Ѳ�߷�ʽ�����õ��ĳ�Ա
    //Ŀ��λ�� Ҫ�ƶ�����λ�ã�Vector3��
    private Vector3 targetPos;
    //��ǰ���ڵ�Ѳ�����ͣ�int��
    //1 ����ԭ�ز���
    //2 ��������ƶ�
    //3 �����������ƶ�
    private int type;

    //Ŀ�귶Χ���ľ��� Ŀ����������Χ�� ��Ҫ����ѭ��
    //�л��� ׷��״̬
    private float checkDis;

    //ԭ�ز���ĳ��������˯�������ڡ����⡢���˯�ȵȣ�
    //��Ҫ���ֵĶ�������
    private E_Action actionType;

    //��һ����Χ������ƶ�
    //Բ�ģ�Vector3��
    private Vector3 centerPos;
    //Բ���뾶��
    private float radius;

    //�Ƿ�Ҫ�ı�һ��Ŀ��λ��
    private bool isChangePos;

    //����Ŀ�����Ƿ�ȴ�
    private bool isWait;
    private float waitTime;

    //�ڶ�������ƶ���ѭ���ƶ��������ƶ���
    //װ���ƶ���λ��������List��  
    private List<Vector3> pointList = new List<Vector3>();
    //��ǰĿ������������е�����λ�ã�int��
    private int pointIndex = 0;
    private bool isAdd;
    //�ƶ���ʽ��ѭ���������� bool��int��
    //1 ����ѭ���ƶ�
    //2 ���������ƶ�
    private int moveType;

    public override E_AI_State AIState => E_AI_State.Patrol;

    //1.AI��ش��붼д���� �ڼ���һ����������Ӧ��AI �ﵽһ��AI����

    //2.ֱ�Ӽ���һ�������壬����AI �߲���

    public PatrolState(StateMachine machine) : base(machine)
    {
        //�ڹ��캯���� �� Ѳ����ص����ݽ��г�ʼ��
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
            //�����ԭ�ز���ĳ������ʱ ���뿪Ѳ��״̬ʱ ���Բ�����һ��������صĶ���
        }
        else
        {
            //�л�״̬֮ǰ ֹͣ�ƶ�
            stateMachine.aiObj.StopMove();
            Debug.Log("patrol exit");
        }
    }


    //����Ѳ��״̬ʱ ��ʲô�������߼�����
    public override void UpdateState()
    {
        Debug.Log("patrolling");
        //���е�AI״̬����
        //�������ڴ��ڸ�״̬ʱ ����һЩ�����ж�
        //������״̬�л�������ʱ  Ӧ�û�ȥ����״̬���л�
        //1 ����ԭ�ز���
        //2 ��������ƶ�
        //3 �����������ƶ�

        //3��Ѳ�߱��ֶ����������Ѳ��״̬���߼�����
        //��ô���ǿ���ѡ�����ִ���ʽ
        //1.ͨ�ô���
        //  ������Ѳ��״̬�������һ��ʱ ���磺����ҽ��뵽�Լ��Ķ��ٷ�Χ�� ������Ѳ��

        //2.���������
        //  2-1��˯��ʱ����ʹ��ҽ��뷶ΧҲ�����룬һ��Ҫ����ҹ���������
        //  2-2: ����ƶ�ʱ����Һ��ҵľ��뵽��һ����Χ�ھ�����
        //  2-3: ����ƶ���������Ұ��Χ�ڣ�������

        // ���ͶƱ���� ͨ�ô����ɣ���Ϊ���������ʱ �߼����

        switch (type)
        {
            //ԭ�ز��Ŷ��� ����
            case 1:
                //��Ҫ����AI���� ִ�в��Ŷ�������Ϊ
                //ֻ��Ҫȥ��ȡ��AI���� ͨ������״̬��������洢�� �������� ��������
                //����������ĳһ������
                stateMachine.aiObj.ChangeAction(actionType);
                break;
            //��Բ��Χ������ƶ��ı���
            case 2:
            //�ڶ�����������ƶ�����ѭ���ƶ��ı���
            case 3:
                //�ȴ�ʱ��Ϊ0 ��ִ��������߼�
                if(waitTime <= 0)
                {
                    //�ж�ʲôʱ�����λ��
                    if (isChangePos)
                    {
                        if(type == 2)
                        {
                            //������������Ԫ����� ���������������ת �ٳ��԰뾶 �ټ���Բ�ĵ� �൱�ھ��ǵõ�Բ�����ϵ�ĳһ���� 
                            targetPos = Quaternion.Euler(0, Random.Range(0f, 359f), 0) * Vector3.forward * radius + centerPos;
                            isChangePos = false;
                        }
                        else if(type == 3)
                        {
                            //�Ӷ�����������ȡ��һ����
                            targetPos = pointList[pointIndex];
                            //Ȼ�� �������λ��Ҫ�仯
                            //1 ����ѭ���ƶ�
                            //2 ���������ƶ�
                            if (moveType == 1)
                            {
                                ++pointIndex;
                                //ѭ���ƶ�ʱ �����������
                                if (pointIndex == pointList.Count)
                                    pointIndex = 0;
                            }
                            else
                            {
                                //�������ǰѡ�� isAdd����true �������false
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
                    //AI����Ӧ�ò�ͣ�ĳ���Ŀ���ȥ�ƶ�
                    stateMachine.aiObj.Move(targetPos);
                    //�ж�ʲôʱ�������
                    //AI���󵽴�Ŀ���ʱ ����� 
                    //AI���󲻿��ܸպòȵ�Ŀ��� �������ǽ���Ŀ���֮��ľ����ж�
                    if (Vector3.Distance(targetPos, stateMachine.aiObj.nowPos) < 0.2f)
                    {
                        isChangePos = true;
                        //����Ŀ���� ֹͣ�ƶ�
                        stateMachine.aiObj.StopMove();
                        //�ȴ�һ��ʱ��� ���ƶ�
                        waitTime = 0;
                    }
                }
                else
                    waitTime -= Time.deltaTime;
                break;
        }

        //����Ѳ���ж�
        //ͨ�ô��� ����������һ�������ھͽ���Ѳ��״̬����
        if(Vector3.Distance(stateMachine.aiObj.nowPos, stateMachine.aiObj.targetObjPos) < checkDis)
        {
            stateMachine.ChangeState(E_AI_State.Chase);
        }
    }
}
