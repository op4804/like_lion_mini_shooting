using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class SRoadPattern : MonoBehaviour, IBossPattern
{
    public GameObject knife;// 소환할거
    public Transform spawnPos1,spawnPos2;// 소환 위치

    public float moveSpeed = 10f;// 보스 이동속도
    public Transform startPos;//보스 이동 처음시작좌표
    public Transform upPos,downPos;//보스 이동 좌표

    public bool isPatternEnd { get; set; }
    public bool isPatternStop { get; set; }

    void Start()
    {
    }

    public IEnumerator StopPattern()
    {
        isPatternStop = true;
        yield return null;
    }
    public IEnumerator PatternProgress() // 코루틴을 이용한 총알 발사
    {
        isPatternStop = false;
        yield return StartCoroutine(MoveToStart());
        while (true)
        {
            Coroutine myCo1 = StartCoroutine(Spawning(knife, spawnPos1));
            Coroutine myCo2 = StartCoroutine(Spawning(knife, spawnPos2));
            yield return StartCoroutine(Move(upPos, moveSpeed));
            yield return StartCoroutine(Move(downPos, moveSpeed));
            StopCoroutine(myCo1);
            StopCoroutine(myCo2);

            if (isPatternStop == true)
            {
                break;
            }

        }
        isPatternEnd = true;
    }
    public IEnumerator MoveToStart()
    {
        while (Vector2.Distance(transform.position, startPos.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos.position, 10 * Time.deltaTime);
            yield return null;
        }
    }
    public IEnumerator Move(Transform targetPos, float moveSpeed)
    {
        while (Vector2.Distance(transform.position, targetPos.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator Spawn(GameObject go, Transform pos)
    {
        Instantiate(go, pos.position, Quaternion.identity);
        yield return null;
    }

    public IEnumerator Spawning(GameObject go, Transform pos)
    {
        while(true)
        {
            yield return Spawn(go,pos);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
