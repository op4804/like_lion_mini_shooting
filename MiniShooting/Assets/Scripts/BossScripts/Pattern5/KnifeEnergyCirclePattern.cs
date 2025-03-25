using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class KnifeEnergyCirclePattern : MonoBehaviour, IBossPattern
{
    public GameObject knife;// 소환할거
    public Transform spawnPos1;// 소환 위치

    public float moveSpeed = 10f;// 보스 이동속도
    public Transform midPos;//보스 이동 좌표

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
        yield return StartCoroutine(Move(midPos, moveSpeed));
        while (true)
        {
            StartCoroutine(Spawn(knife, spawnPos1));
            yield return new WaitForSeconds(0.05f);
            if (isPatternStop == true)
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

    public float knifeSpeed = 5f; // 칼이 날아가는 속도

    public IEnumerator Spawn(GameObject go, Transform pos)
    {
        GameObject obj = Instantiate(go, pos.position, Quaternion.identity);

        // 랜덤 방향 생성 (단위 벡터)
        Vector2 randomDir = Random.insideUnitCircle.normalized;

        obj.transform.right = randomDir; // ← 핵심! 회전 방향 지정

        // Rigidbody2D가 있다면 힘을 줌
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = randomDir * knifeSpeed;
        }

        yield return null;
    }

}
