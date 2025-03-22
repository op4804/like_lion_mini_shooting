using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BossPatternManager : MonoBehaviour
{
    [SerializeField]
    public static bool timeStoped = false;

    public List<MonoBehaviour> patternScripts; // ���� ���� ����Ʈ
    private IBossPattern currentPattern;
    public float waitTime = 1f; // �� ����

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
        // �������� ���� ����
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
