using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class MoveShootPattern : MonoBehaviour, IBossPattern
{
    public GameObject knifeCircleSpawner;// 소환할거
    public Transform spawnPos;// 소환 위치

    public float moveSpeed = 10f;// 보스 이동속도
    public Transform upPos, startPos, downPos;//보스 이동 좌표

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
        yield return StartCoroutine(Move(startPos, moveSpeed));
        while (true)
        {
            yield return StartCoroutine(Move(upPos, moveSpeed));
            yield return StartCoroutine(Spawn(knifeCircleSpawner, spawnPos));
            yield return StartCoroutine(Move(downPos, moveSpeed));
            yield return StartCoroutine(Spawn(knifeCircleSpawner, spawnPos));
            if (isPatternStop  == true)
            {
                break;
            }
        }
        isPatternEnd = true;
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

}
