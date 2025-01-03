using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AI����ӿ� ���ڹ淶 ��Ҫʹ��AI�Ķ���� ��Ϊ
/// </summary>
public interface IAIObj 
{
    //����AI�����Transform
    public Transform objTransform
    {
        get;
    }

    //����AI����Ӧ����һ����ǰ��λ��
    public Vector3 nowPos
    {
        get;
    }

    //AI�����Ŀ��������ڵ�λ��
    public Vector3 targetObjPos
    {
        get;
    }

    public float atkRange
    {
        get;
    }

    //������
    public Vector3 bornPos
    {
        get;
        set;
    }

    //AI������ Ӧ���� �ƶ���صķ���
    public void Move(Vector3 dirOrPos);
    //AI������ Ӧ���� ֹͣ�ƶ���صķ���
    public void StopMove();
    //AI������ Ӧ���� ������صķ���
    public void Atk();
    //AI������ ������Ҫ���� �л�ָ������
    //�л����� Ӧ�ô���һЩ��ز��� ���ܹ�ָ���л��ĸ�������
    public void ChangeAction(E_Action action);


    //����Ӧ�ø���AI��ͬ��״̬ ȥ��ȡ�����ǵ���Ϊ�ϼ�
}
