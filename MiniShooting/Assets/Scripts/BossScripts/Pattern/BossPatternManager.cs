using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BossPatternManager : MonoBehaviour
{
    [SerializeField]
    public static bool timeStoped = false;

    public List<MonoBehaviour> patternScripts; // 보스 패턴 리스트
    private IBossPattern currentPattern;
    public float waitTime = 1f; // 쉼 간격

    void Start()
    {
        StartCoroutine(PatternLoop());
    }

    IEnumerator PatternLoop()
    {
        while (true)
        {
            ChooseRandomPattern();
            yield return currentPattern.StartPattern();
        }
    }

    void ChooseRandomPattern()
    {
        // 랜덤으로 패턴 선택
        int index = Random.Range(0, patternScripts.Count);
        currentPattern = patternScripts[index] as IBossPattern;
    }

    void Timestop()
    {
        timeStoped = true;
    }
    void TimeResume()
    {
        timeStoped = false;
    }
}
