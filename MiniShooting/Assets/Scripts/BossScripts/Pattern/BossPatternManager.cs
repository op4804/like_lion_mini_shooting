using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossPatternManager : MonoBehaviour
{
    public static bool timeStoped = false;

    public List<MonoBehaviour> patternScripts; // 보스 패턴 리스트
    private IBossPattern currentPattern;
    public float patternDuration = 3f; // 패턴 지속 시간
    public float cooldown = 1f; // 패턴 변경 간격

    void Start()
    {
        StartCoroutine(PatternLoop());
    }

    IEnumerator PatternLoop()
    {
        while (true)
        {
            ChooseRandomPattern();
            yield return new WaitForSeconds(patternDuration);

            if (currentPattern != null)
            {
                currentPattern.StopPattern(); // 기존 패턴 중지
            }

            yield return new WaitForSeconds(cooldown); // 다음 패턴 전 쿨다운
        }
    }

    void ChooseRandomPattern()
    {
        if (patternScripts.Count == 0) return;

        // 이전 패턴 중지
        if (currentPattern != null)
        {
            currentPattern.StopPattern();
        }

        // 랜덤으로 패턴 선택
        int index = Random.Range(0, patternScripts.Count);
        currentPattern = patternScripts[index] as IBossPattern;
        currentPattern.StartPattern();
    }
}
