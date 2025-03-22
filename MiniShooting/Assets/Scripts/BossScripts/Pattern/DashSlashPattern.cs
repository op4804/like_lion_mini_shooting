using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DashPattern : MonoBehaviour, IBossPattern
{
    public float dashSpeed = 20f;
    public float dashWaringTime = 2f;
    public float movetime = 1f;
    public float dashWaitTime = 2f;

    public float Waringwidth = 2f;
    private Vector2 startPos;

    private LineRenderer BosslineRenderer;
    private Transform playerTransform;
    private TrailRenderer BossTrailRenderer;


    void Awake()
    {
        startPos = transform.position;
        playerTransform = Player.Instance.transform;

        BosslineRenderer = GetComponent<LineRenderer>(); // 라인 렌더러 가져오기
        BossTrailRenderer = GetComponent<TrailRenderer>(); // 트레일 렌더러 가져오기
        BossTrailRenderer.enabled = false;
        BosslineRenderer.enabled = false; // 기본적으로 비활성화
        BosslineRenderer.startWidth = Waringwidth;
        BosslineRenderer.endWidth = Waringwidth;
        BosslineRenderer.material = new Material(Shader.Find("Sprites/Default")); // 기본 머티리얼 적용
        BosslineRenderer.startColor = Color.red;
        BosslineRenderer.endColor = Color.red;
        Color newStartColor = BosslineRenderer.startColor;
        Color newEndColor = BosslineRenderer.endColor;
        newStartColor.a = 0.5f;
        newEndColor.a = 0.5f;
        BosslineRenderer.startColor = newStartColor;
        BosslineRenderer.endColor = newEndColor;

    }

    IEnumerator IBossPattern.StartPattern()
    {
        yield return StartCoroutine(MakeWarningCoroutine());
    }

    IEnumerator MakeWarningCoroutine()
    {
        if (playerTransform == null)
            yield break;

        BosslineRenderer.enabled = true;

        // 라인 렌더러의 시작점과 끝점을 설정하여 플레이어 방향으로 빨간 선을 그림
        BosslineRenderer.SetPosition(0, transform.position);
        BosslineRenderer.SetPosition(1, playerTransform.position);

        yield return new WaitForSeconds(dashWaringTime); // 대시 전 경고 시간

        BosslineRenderer.enabled = false;
        StartCoroutine(Move());

    }
    IEnumerator Move()
    {

        if (playerTransform == null)
            yield break;

        float elapsedTime = 0f; // 경과 시간
        Vector2 startPos = transform.position; // 시작 위치
        Vector2 targetPos = playerTransform.position; // 목표 위치
        Vector2 direction = (targetPos - startPos).normalized; // 이동 방향
        BossTrailRenderer.enabled = true; // 대쉬 이펙트 활성화

        while (elapsedTime < movetime)
        {
            transform.position += (Vector3)(direction * dashSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null; // 다음 프레임까지 대기
        }
        BossTrailRenderer.enabled = false;// 대쉬 이펙트 비활성화

        // 이동이 끝나면 SlashCoroutine 실행
        yield return StartCoroutine(SlashCoroutine());
    }

    IEnumerator SlashCoroutine()
    {

        // 칼 이펙트 생성 (여기서는 간단한 파티클 효과)
        GameObject slashEffect = new GameObject("SlashEffect");
        SpriteRenderer renderer = slashEffect.AddComponent<SpriteRenderer>();
        renderer.color = Color.red; // 붉은색 효과
        slashEffect.transform.position = transform.position;
        Destroy(slashEffect, 0.5f); // 0.5초 후 제거

        // 범위 내 플레이어가 있으면 피해 입히기
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, Waringwidth / 2f);
        foreach (var hit in hitObjects)
        {
            if (hit.CompareTag("Player"))
            {
                hit.gameObject.GetComponent<Player>().Hit();
            }
        }
        yield return null;

        this.transform.position = startPos;
    }

}
