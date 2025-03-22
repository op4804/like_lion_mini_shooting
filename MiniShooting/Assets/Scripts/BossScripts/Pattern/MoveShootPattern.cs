using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class MoveShootPattern : MonoBehaviour, IBossPattern
{
    public GameObject knifePrefab;// 던질 칼
    public Transform knifeSpawnTransform;// 칼 던지는 위치

    public float moveDuration = 1f;      // 이동하는 데 걸리는 시간
    public float moveRange = 5f;// 보스 이동 범위

    public float makeInterval = 0.25f;// 칼 만드는 딜레이
    public float throwInterval = 0.25f;// 칼 던지는 딜레이
    public int knifeCount = 5; // 생성할 칼 개수
    public float radius = 2f; // 원의 반지름
    public float knifeSpeed = 5f; // 칼의 속도

    private List<GameObject> knifetList;
    private Vector2 startPos;
    private Vector2 upPos;
    private Vector2 downPos;
    private bool isMoveEnd = false;

    void Awake()
    {
        knifetList = new List<GameObject>(); //생성된 칼을 저장할 리스트 초기화

        startPos = this.transform.position;
        upPos = startPos + Vector2.up * moveRange;
        downPos = startPos + Vector2.down * moveRange;
    }


    IEnumerator IBossPattern.StartPattern()
    {
        yield return StartCoroutine(PatternCoroutine());
    }

    IEnumerator PatternCoroutine() // 코루틴을 이용한 총알 발사
    {
        StartCoroutine(MoveSequence());

        yield return StartCoroutine(MakeKnifeCircle());
        if (BossPatternManager.timeStoped == false)
        {
            yield return StartCoroutine(throwKnife());
        }
        yield return StartCoroutine(MakeKnifeCircle());
        if (BossPatternManager.timeStoped == false)
        {
            yield return StartCoroutine(throwKnife());
        }
        yield return StartCoroutine(MakeKnifeCircle());
        if (BossPatternManager.timeStoped == false)
        {
            yield return StartCoroutine(throwKnife());
        }
        yield return StartCoroutine(MakeKnifeCircle());
        if (BossPatternManager.timeStoped == false)
        {
            yield return StartCoroutine(throwKnife());
        }
        yield return StartCoroutine(MakeKnifeCircle());
        if (BossPatternManager.timeStoped == false)
        {
            yield return StartCoroutine(throwKnife());
        }
        yield return new WaitUntil(() => isMoveEnd);
    }

    IEnumerator MoveSequence()
    {
        isMoveEnd = false; 
        // 위로 왕복
        yield return StartCoroutine(MoveToPosition(upPos));
        yield return StartCoroutine(MoveToPosition(startPos));

        // 아래로 왕복
        yield return StartCoroutine(MoveToPosition(downPos));
        yield return StartCoroutine(MoveToPosition(startPos));
        isMoveEnd = true;
    }
    IEnumerator MoveToPosition(Vector2 targetPos)
    {
        Vector2 initialPos = transform.position;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / moveDuration;

            transform.position = Vector2.Lerp(initialPos, targetPos, t);
            yield return null;
        }

        transform.position = targetPos; // 정확하게 위치 맞추기
    }




    IEnumerator MakeKnifeCircle()
     {
        yield return new WaitForSeconds(makeInterval); // 만드는텀
        for (int i = 0; i < knifeCount; i++)
        {
            // 원형 배치를 위한 각도 계산
            float angle = (360f / knifeCount) * i;
            Vector2 spawnPosition = (Vector2)transform.position + new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius
            );

            // 칼 생성및 방향 설정
            GameObject knife = Instantiate(knifePrefab, spawnPosition, Quaternion.identity);

            Rigidbody2D kniferigidbody = knife.GetComponent<Rigidbody2D>();
            Vector2 direction = ((Vector2)Player.Instance.transform.position - (Vector2)knife.transform.position).normalized;
            float angle2 = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            kniferigidbody.SetRotation(angle2);

            //칼 리스트에 추가
            knifetList.Add(knife);
        }
        yield return null;
    }
    IEnumerator throwKnife()
    {
        yield return new WaitForSeconds(throwInterval); // 던지는텀
        foreach (GameObject knife in knifetList)
        {
            Rigidbody2D kniferigidbody = knife.GetComponent<Rigidbody2D>();
            float angle = kniferigidbody.rotation;
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            kniferigidbody.linearVelocity = direction * knifeSpeed;
        }

        knifetList.Clear(); //칼 리스트 초기화
        yield return null;
    }

}
