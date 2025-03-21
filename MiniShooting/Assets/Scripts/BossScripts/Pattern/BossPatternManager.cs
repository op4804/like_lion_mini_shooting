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
    public float patternDuration = 3f; // ���� ���� �ð�
    public float cooldown = 1f; // ���� ���� ����

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
                currentPattern.StopPattern(); // ���� ���� ����
            }

            yield return new WaitForSeconds(cooldown); // ���� ���� �� ��ٿ�
        }
    }

    void ChooseRandomPattern()
    {
        if (patternScripts.Count == 0) return;

        // ���� ���� ����
        if (currentPattern != null)
        {
            currentPattern.StopPattern();
        }

        // �������� ���� ����
        int index = Random.Range(0, patternScripts.Count);
        currentPattern = patternScripts[index] as IBossPattern;
        currentPattern.StartPattern();
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
