using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossPatternManager : MonoBehaviour
{
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


    private void OnEnable()
    {
        foreach (var script in patternScripts)
        {
            IBossPattern pattern = script as IBossPattern;
            patterns.Add(pattern);
        }
        StartCoroutine(PatternProgress());

    }

    IEnumerator PatternProgress()
    {
        //yield return StartCoroutine(Test(5f));
        //���� �׽�Ʈ

        while(true)
        {
            yield return StartCoroutine(MaveAndShoot(5f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= this.GetComponent<Boss>().GetMaxHp() / 2) { break; }
            yield return StartCoroutine(Sroad(5f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= this.GetComponent<Boss>().GetMaxHp() / 2) { break; }
            yield return StartCoroutine(Rampage(5f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= this.GetComponent<Boss>().GetMaxHp() / 2) { break; }
            yield return StartCoroutine(DashSlash(3f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= this.GetComponent<Boss>().GetMaxHp() / 2) { break; }
            yield return StartCoroutine(SpinSword(5f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= this.GetComponent<Boss>().GetMaxHp() / 2) { break; }
            yield return StartCoroutine(Lazer(5f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= this.GetComponent<Boss>().GetMaxHp() / 2) { break; }
            yield return StartCoroutine(BlinkMove(5f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= this.GetComponent<Boss>().GetMaxHp() / 2) { break; }
        }
        yield return StartCoroutine(HealSword());
        while (true)
        {
            yield return StartCoroutine(SpinSwordStart());
            yield return StartCoroutine(MaveAndShoot(5f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= 0) { break; }
            yield return StartCoroutine(Sroad(5f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= 0) { break; }
            yield return StartCoroutine(Rampage(5f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= 0) { break; }
            yield return StartCoroutine(DashSlash(3f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= 0) { break; }
            yield return StartCoroutine(BlinkMove(5f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= 0) { break; }
            yield return StartCoroutine(SpinSwordStop());

            yield return StartCoroutine(LazerStart());
            yield return StartCoroutine(MaveAndShoot(5f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= 0) { break; }
            yield return StartCoroutine(Sroad(5f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= 0) { break; }
            yield return StartCoroutine(Rampage(5f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= 0) { break; }
            yield return StartCoroutine(DashSlash(3f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= 0) { break; }
            yield return StartCoroutine(BlinkMove(5f));
            if (this.GetComponent<Boss>().GetCurrentHp() <= 0) { break; }
            yield return StartCoroutine(LazerStop());
        }
        Destroy(gameObject);

        StartCoroutine(patterns[5].PatternProgress());
        //���ϳ־��ֱ�
        StartCoroutine(patterns[5].StopPattern());
        yield return new WaitUntil(() => patterns[5].isPatternEnd);

        StartCoroutine(patterns[6].PatternProgress());
        //���ϳ־��ֱ�
        StartCoroutine(patterns[6].StopPattern());
        yield return new WaitUntil(() => patterns[5].isPatternEnd);
    }











    IEnumerator SpinSwordStart()
    {
        StartCoroutine(patterns[5].PatternProgress());
        yield return null;
    }
    IEnumerator SpinSwordStop()
    {
        StartCoroutine(patterns[5].StopPattern());
        yield return new WaitUntil(() => patterns[5].isPatternEnd);
        //ȸ���˷�����ȯ
    }
    IEnumerator LazerStart()
    {
        StartCoroutine(patterns[6].PatternProgress());
        yield return null;
    }
    IEnumerator LazerStop()
    {
        StartCoroutine(patterns[6].StopPattern());
        yield return new WaitUntil(() => patterns[6].isPatternEnd);
    }

    IEnumerator Test(float seconds)
    {
        StartCoroutine(patterns[0].PatternProgress());
        yield return new WaitForSeconds(seconds);
        StartCoroutine(patterns[0].StopPattern());
        yield return new WaitUntil(() => patterns[0].isPatternEnd);
    }
    IEnumerator MaveAndShoot(float seconds)
    {
        StartCoroutine(patterns[1].PatternProgress());
        yield return new WaitForSeconds(seconds);
        StartCoroutine(patterns[1].StopPattern());
        yield return new WaitUntil(() => patterns[1].isPatternEnd);
        //���꿣��
    }
    IEnumerator DashSlash(float seconds)
    {
        StartCoroutine(patterns[2].PatternProgress());
        yield return new WaitForSeconds(seconds);
        StartCoroutine(patterns[2].StopPattern());
        yield return new WaitUntil(() => patterns[2].isPatternEnd);
        //�뽬 ������
    }
    IEnumerator Sroad(float seconds)
    {
        StartCoroutine(patterns[3].PatternProgress());
        yield return new WaitForSeconds(seconds);
        StartCoroutine(patterns[3].StopPattern());
        yield return new WaitUntil(() => patterns[3].isPatternEnd);
        //s�ε�
    }
    IEnumerator Rampage(float seconds)
    {
        StartCoroutine(patterns[4].PatternProgress());
        yield return new WaitForSeconds(seconds);
        StartCoroutine(patterns[4].StopPattern());
        yield return new WaitUntil(() => patterns[4].isPatternEnd);
        //�˱ⷣ������
    }

    IEnumerator SpinSword(float seconds)
    {
        StartCoroutine(patterns[5].PatternProgress());
        yield return new WaitForSeconds(seconds);
        StartCoroutine(patterns[5].StopPattern());
        yield return new WaitUntil(() => patterns[5].isPatternEnd);
        //ȸ���˷�����ȯ
    }
    IEnumerator Lazer(float seconds)
    {
        StartCoroutine(patterns[6].PatternProgress());
        yield return new WaitForSeconds(seconds);
        StartCoroutine(patterns[6].StopPattern());
        yield return new WaitUntil(() => patterns[6].isPatternEnd);
        //������������ȯ
    }
    IEnumerator HealSword()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(patterns[7].PatternProgress());
        yield return new WaitUntil(() => patterns[7].isPatternEnd);
        //���ΰ˼�ȯ,�νǶ�����ȸ��
    }
    IEnumerator BlinkMove(float seconds)
    {
        StartCoroutine(patterns[8].PatternProgress());
        yield return new WaitForSeconds(seconds);
        StartCoroutine(patterns[8].StopPattern());
        yield return new WaitUntil(() => patterns[6].isPatternEnd);
        //��󠺴ٰ� ��Ÿ���� ���ݺ�
    }
}
