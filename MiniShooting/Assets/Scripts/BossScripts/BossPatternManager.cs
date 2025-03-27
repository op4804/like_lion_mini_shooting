using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternManager : MonoBehaviour
{
    public Boss boss;
    public List<MonoBehaviour> patternScripts; // ���� ���� ����Ʈ
    public List<IBossPattern> patterns = new List<IBossPattern>();
    //������
    //1.��ũĮ������
    //2.�Դٰ��� ����Į���� ������
    //3.�ڷκ����ٰ� �������� Į�ε�������
    //4.S�ڷ� �����̸鼭 Į ���� �� �����
    //5.����� ���� �˱� ������������ ������ ��� ������

    //������ȯ����
    //1.+������� ����� ��������ȯ
    //2. o������� ����� ǥâ ������

    //��������
    //1.�������� �����ǰ�4����ȯ, �����ǰ� ������ ������ ȸ��

    //����������
    //1.�н�����ȯ�Ͽ� 4���� ������ ���������� �˱� ������
    private void Awake()
    {
        foreach (var script in patternScripts)
        {
            IBossPattern pattern = script as IBossPattern;
            patterns.Add(pattern);
        }


    }
    void Start()
    {
        StartCoroutine(PatternProgress());
    }

    IEnumerator PatternProgress()
    {
        StartCoroutine(patterns[0].PatternProgress());
        yield return new WaitForSeconds(2f);
        StartCoroutine(patterns[0].StopPattern());
        yield return new WaitUntil(() => patterns[0].isPatternEnd);

        StartCoroutine(patterns[1].PatternProgress());
        yield return new WaitForSeconds(2f);
        StartCoroutine(patterns[1].StopPattern());
        yield return new WaitUntil(() => patterns[1].isPatternEnd);

        StartCoroutine(patterns[2].PatternProgress());
        yield return new WaitForSeconds(2f);
        StartCoroutine(patterns[2].StopPattern());
        yield return new WaitUntil(() => patterns[2].isPatternEnd);

        StartCoroutine(patterns[3].PatternProgress());
        yield return new WaitForSeconds(2f);
        StartCoroutine(patterns[3].StopPattern());
        yield return new WaitUntil(() => patterns[3].isPatternEnd);

        StartCoroutine(patterns[4].PatternProgress());
        yield return new WaitForSeconds(2f);
        StartCoroutine(patterns[4].StopPattern());
        yield return new WaitUntil(() => patterns[4].isPatternEnd);

        StartCoroutine(patterns[5].PatternProgress());
        yield return new WaitForSeconds(2f);
        StartCoroutine(patterns[5].StopPattern());
        yield return new WaitUntil(() => patterns[5].isPatternEnd);

        StartCoroutine(patterns[6].PatternProgress());
        yield return new WaitForSeconds(2f);
        StartCoroutine(patterns[6].StopPattern());
        yield return new WaitUntil(() => patterns[6].isPatternEnd);

        StartCoroutine(patterns[7].PatternProgress());
        yield return new WaitUntil(() => patterns[7].isPatternEnd);
    }




}
