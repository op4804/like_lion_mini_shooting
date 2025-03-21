using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class MoveShootPattern : MonoBehaviour, IBossPattern
{
    public int repeatCount = 3; // 반복할 횟수
    private int currentRepeat = 0;

    public GameObject knifePrefab;// 던질 칼
    public Transform knifeSpawnTransform;// 칼 던지는 위치

    public float moveSpeed = 5f;// 보스 이동 속도
    public float moveRange = 5f;// 보스 가동 범위
    public float makeInterval = 0.25f;// 칼 만드는 딜레이
    public float throwInterval = 0.25f;// 칼 던지는 딜레이
    public int knifeCount = 5; // 생성할 칼 개수
    public float radius = 2f; // 원의 반지름
    public float knifeSpeed = 5f; // 칼의 속도

    private List<GameObject> knifetList;
    private Vector2 startPos;
    private Vector2 top;
    private Vector2 bottom;
    private bool isActive = false;

    void Awake()
    {
        startPos = transform.position; //위치초기화
        knifetList = new List<GameObject>(); //생성된 칼을 저장할 리스트 초기화

        top = startPos + Vector2.up * moveRange;
        bottom = startPos - Vector2.up * moveRange;
    }


    public void StartPattern()
    {
        isActive = true;
        StartCoroutine(PatternCoroutine());
    }

    public void StopPattern()
    {
        isActive = false;
        StopAllCoroutines();
        knifetList.Clear();
    }

    IEnumerator MoveUpDown()
    {
        // 현재 → 1번
        yield return StartCoroutine(MoveToPosition(top));

        // 현재 → 2번 (즉, point1 → 2번)
        yield return StartCoroutine(MoveToPosition(bottom));

        // 현재 → 1번 (즉, point2 → 1번)
        yield return StartCoroutine(MoveToPosition(top));

    }
    IEnumerator MoveToPosition(Vector3 targetPos)
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < moveRange)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed / moveRange);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
    }

    IEnumerator PatternCoroutine() // 코루틴을 이용한 총알 발사
    {
        while (true)
        {
            StartCoroutine(MoveUpDown());
            yield return StartCoroutine(MakeKnifeCircle());
            if(BossPatternManager.timeStoped == false)
            {
                yield return StartCoroutine(throwKnife());     
            }
        }

    }

     IEnumerator MakeKnifeCircle()
     {

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
        yield return new WaitForSeconds(makeInterval); // 만드는텀
    }

    IEnumerator throwKnife()
    {
        foreach (GameObject knife in knifetList)
        {
            Rigidbody2D kniferigidbody = knife.GetComponent<Rigidbody2D>();
            float angle = kniferigidbody.rotation;
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            kniferigidbody.linearVelocity = direction * knifeSpeed;
        }

        knifetList.Clear(); //칼 리스트 초기화
        yield return new WaitForSeconds(throwInterval); // 던지는텀
    }

}
