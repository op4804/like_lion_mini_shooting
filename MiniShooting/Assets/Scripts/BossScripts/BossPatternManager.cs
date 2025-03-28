using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternManager : MonoBehaviour
{
    public Boss boss;
    public List<MonoBehaviour> patternScripts; // 보스 패턴 리스트
    public List<IBossPattern> patterns = new List<IBossPattern>();
    //주패턴
    //1.블링크칼던지기
    //2.왔다갔다 원형칼무리 던지기
    //3.뒤로빠졋다가 연속으로 칼로돌진베기
    //4.S자로 움직이면서 칼 결계로 길 만들기
    //5.가운데로 가서 검기 랜덤방향으로 빠르게 계속 날리기

    //보조소환패턴
    //1.+모양으로 경고후 레이저소환
    //2. o모양으로 경고후 표창 돌리기

    //반피패턴
    //1.마법진과 봉인의검4개소환, 봉인의검 깨지기 전까지 회복

    //마지막패턴
    //1.분신을소환하여 4개로 나뉜뒤 죽을때까지 검기 날리기
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
        //yield return StartCoroutine(Test(5f));
        //패턴 테스트

        yield return StartCoroutine(MaveAndShoot(5f));
        yield return StartCoroutine(Sroad(5f));
        yield return StartCoroutine(Rampage(5f));
        yield return StartCoroutine(DashSlash(3f));
        yield return StartCoroutine(SpinSword(5f));
        yield return StartCoroutine(Lazer(5f));
        yield return StartCoroutine(BlinkMove(5f));
        yield return StartCoroutine(HealSword());


        StartCoroutine(patterns[5].PatternProgress());
        //패턴넣어주기
        StartCoroutine(patterns[5].StopPattern());
        yield return new WaitUntil(() => patterns[5].isPatternEnd);

        StartCoroutine(patterns[6].PatternProgress());
        //패턴넣어주기
        StartCoroutine(patterns[6].StopPattern());
        yield return new WaitUntil(() => patterns[5].isPatternEnd);
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
        //무브엔슛
    }
    IEnumerator DashSlash(float seconds)
    {
        StartCoroutine(patterns[2].PatternProgress());
        yield return new WaitForSeconds(seconds);
        StartCoroutine(patterns[2].StopPattern());
        yield return new WaitUntil(() => patterns[2].isPatternEnd);
        //대쉬 슬래시
    }
    IEnumerator Sroad(float seconds)
    {
        StartCoroutine(patterns[3].PatternProgress());
        yield return new WaitForSeconds(seconds);
        StartCoroutine(patterns[3].StopPattern());
        yield return new WaitUntil(() => patterns[3].isPatternEnd);
        //s로드
    }
    IEnumerator Rampage(float seconds)
    {
        StartCoroutine(patterns[4].PatternProgress());
        yield return new WaitForSeconds(seconds);
        StartCoroutine(patterns[4].StopPattern());
        yield return new WaitUntil(() => patterns[4].isPatternEnd);
        //검기랜덤난사
    }

    IEnumerator SpinSword(float seconds)
    {
        StartCoroutine(patterns[5].PatternProgress());
        yield return new WaitForSeconds(seconds);
        StartCoroutine(patterns[5].StopPattern());
        yield return new WaitUntil(() => patterns[5].isPatternEnd);
        //회전검랜덤소환
    }
    IEnumerator Lazer(float seconds)
    {
        StartCoroutine(patterns[6].PatternProgress());
        yield return new WaitForSeconds(seconds);
        StartCoroutine(patterns[6].StopPattern());
        yield return new WaitUntil(() => patterns[6].isPatternEnd);
        //레이저랜덤소환
    }
    IEnumerator HealSword()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(patterns[7].PatternProgress());
        yield return new WaitUntil(() => patterns[7].isPatternEnd);
        //봉인검소환,부실때까지회복
    }
    IEnumerator BlinkMove(float seconds)
    {
        StartCoroutine(patterns[8].PatternProgress());
        yield return new WaitForSeconds(seconds);
        StartCoroutine(patterns[8].StopPattern());
        yield return new WaitUntil(() => patterns[6].isPatternEnd);
        //사라졋다가 나타나서 쏘기반복
    }
}
