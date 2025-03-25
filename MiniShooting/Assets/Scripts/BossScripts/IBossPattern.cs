using NUnit.Framework;
using System.Collections;
using UnityEngine;

public interface IBossPattern
{
    bool isPatternStop { get; set; }
    bool isPatternEnd { get; set; }

    //���Ͽ� ����� ź�� ������Ʈ��
    //���Ͽ� ����� ��ȯ,������ǥ ������Ʈ��

    IEnumerator StopPattern();//���� �� ���α׷����ڷ�ƾ���� ���� ����
    IEnumerator PatternProgress(); // ���� ������ ���� �ϴ� �ڷ�ƾ Move(),Spawn()�� �ð� ��� ��� ���� ����
    IEnumerator Move(Transform pos, float moveSpeed); // ���� �ڷ�ƾ ��������
    IEnumerator Spawn(GameObject go, Transform pos); // ��ȯ �ڷ�ƾ
}
