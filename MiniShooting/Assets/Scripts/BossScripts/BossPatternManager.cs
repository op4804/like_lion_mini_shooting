using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatternManager : MonoBehaviour
{
    public Boss boss;
    public List<MonoBehaviour> patternScripts; // 보스 패턴 리스트
    public List<IBossPattern> patterns = new List<IBossPattern>();

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
        yield return new WaitForSeconds(4f);
        StartCoroutine(patterns[0].StopPattern());
        //if(boss.currentHp <= 90 )
        //{
        //    pattern1.EndPattern();
        //}
        yield return new WaitUntil(() => patterns[0].isPatternEnd);

        StartCoroutine(patterns[1].PatternProgress());
        yield return new WaitForSeconds(4f);
        StartCoroutine(patterns[1].StopPattern());
        yield return new WaitUntil(() => patterns[1].isPatternEnd);

        StartCoroutine(patterns[2].PatternProgress());
        yield return new WaitForSeconds(4f);
        StartCoroutine(patterns[2].StopPattern());
        yield return new WaitUntil(() => patterns[2].isPatternEnd);

        StartCoroutine(patterns[3].PatternProgress());
        yield return new WaitForSeconds(4f);
        StartCoroutine(patterns[3].StopPattern());
        yield return new WaitUntil(() => patterns[3].isPatternEnd);

        StartCoroutine(patterns[4].PatternProgress());
        yield return new WaitForSeconds(4f);
        StartCoroutine(patterns[4].StopPattern());
        yield return new WaitUntil(() => patterns[4].isPatternEnd);

        StartCoroutine(patterns[5].PatternProgress());
        yield return new WaitForSeconds(4f);
        StartCoroutine(patterns[5].StopPattern());
        yield return new WaitUntil(() => patterns[5].isPatternEnd);
    }




}
