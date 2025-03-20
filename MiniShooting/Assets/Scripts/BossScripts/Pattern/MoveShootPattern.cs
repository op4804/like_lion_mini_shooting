using UnityEngine;
using System.Collections;

public class MoveShootPattern : MonoBehaviour
{
    public static bool timeStoped = false;

    public GameObject knifePrefab;// 던질 칼
    public Transform knifeSpawnTransform;// 칼 던지는 위치
    public Transform playerTransform;// 플레이어 위치

    public float moveSpeed = 10f;// 보스 이동 속도
    public float yRange = 4f;// 보스 가동 범위
    public float shootInterval = 0.25f;// 칼 던지는 딜레이

    public int knifeCount = 8; // 생성할 칼 개수
    public float radius = 2f; // 원의 반지름
    public float bulletSpeed = 8f; // 칼의 속도

    private Vector2 startPos;
    private int direction = 1;

    void Start()
    {
        startPos = transform.position; //발사위치초기화

        StartCoroutine(ShootingCoroutine()); // 코루틴 실행
    }

    void Update()
    {
        MoveVertical();
    }

    void MoveVertical() // 위아래 이동
    {
        transform.Translate(Vector2.up * direction * moveSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.y - startPos.y) > yRange)
        {
            direction *= -1;
        }
    }

    IEnumerator ShootingCoroutine() // 코루틴을 이용한 총알 발사
    {
        while (true)
        {
            //SpawnCircularKnives();
            ShootCircularKnives();
            yield return new WaitForSeconds(shootInterval); // 일정 시간 대기 후 반복
        }
    }

    public void ShootCircularKnives()
    {
        for (int i = 0; i < knifeCount; i++)
        {
            // 원형 배치를 위한 각도 계산
            float angle = (360f / knifeCount) * i;
            Vector2 spawnPosition = (Vector2)transform.position + new Vector2(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius
            );

            // 칼 생성
            GameObject knife = Instantiate(knifePrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D rb = knife.GetComponent<Rigidbody2D>();

            if (rb != null && playerTransform != null)
            {
                // 플레이어 방향 계산
                Vector2 direction = ((Vector2)playerTransform.position - (Vector2)knife.transform.position).normalized;
                rb.velocity = direction * bulletSpeed;
            }
        }
    }
}
