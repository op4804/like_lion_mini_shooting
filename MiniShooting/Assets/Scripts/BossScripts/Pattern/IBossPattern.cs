using System.Collections;
using UnityEngine;

public interface IBossPattern
{
    //���Ͽ� ����� ź�� ������Ʈ��
    //���Ͽ� ����� ��ȯ,������ǥ ������Ʈ��
    //����Ÿ�� ȸ�� or ����
    //ȸ�������� ���Ͻð��� ������ �ڵ����� ������������ �Ѿ��
    //���������� ������ ���ӵ� ���� ���� hp�� ������ġ��ŭ ��� ������ �Ѿ��.

    IEnumerator StartPattern(); // ���� ����
    //IEnumerator Move(); // ���� �ڷ�ƾ
    //IEnumerator Spawn(GameObject go ,Transform pos); // ��ȯ �ڷ�ƾ

}
