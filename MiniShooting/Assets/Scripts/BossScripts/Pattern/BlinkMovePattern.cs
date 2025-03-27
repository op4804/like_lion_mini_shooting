using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class BlinkMovePattern : MonoBehaviour, IBossPattern
{
    public GameObject knifeCircleSpawner;// 소환할거
    public Transform spawnPos, spawnPos1, spawnPos2;// 소환 위치

    public float moveSpeed = 10f;// 보스 이동속도
    public List<Transform> movePoss;//보스 이동 좌표
    public Transform startPos;

    public SpriteRenderer spriteRenderer;//알파값 조정을 위한 스프라이트 렌더러
    public float fadeSpeed = 10f; // 알파 변화 속도


    public bool isPatternEnd { get; set; }
    public bool isPatternStop { get; set; }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            yield return StartCoroutine(FadeTo(0));
            int randomIndex = Random.Range(0, movePoss.Count);
            Transform randomPos = movePoss[randomIndex]; //랜덤으로 movePoss중 하나를 선택
            yield return StartCoroutine(Move(randomPos, moveSpeed));
            yield return StartCoroutine(FadeTo(1));
            yield return StartCoroutine(Spawn(knifeCircleSpawner, spawnPos));
            yield return StartCoroutine(Spawn(knifeCircleSpawner, spawnPos1));
            yield return StartCoroutine(Spawn(knifeCircleSpawner, spawnPos2));

            if (isPatternStop == true)
            {
                break;
            }
        }
        isPatternEnd = true;
    }
    IEnumerator FadeTo(float targetAlpha)
    {
        float currentAlpha = spriteRenderer.color.a;

        while (!Mathf.Approximately(currentAlpha, targetAlpha))
        {
            currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);

            Color newColor = spriteRenderer.color;
            newColor.a = currentAlpha;
            spriteRenderer.color = newColor;

            yield return null;
        }
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

}
