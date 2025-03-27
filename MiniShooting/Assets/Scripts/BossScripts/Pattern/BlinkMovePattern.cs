using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class BlinkMovePattern : MonoBehaviour, IBossPattern
{
    public GameObject knifeCircleSpawner;// ��ȯ�Ұ�
    public Transform spawnPos, spawnPos1, spawnPos2;// ��ȯ ��ġ

    public float moveSpeed = 10f;// ���� �̵��ӵ�
    public List<Transform> movePoss;//���� �̵� ��ǥ
    public Transform startPos;

    public SpriteRenderer spriteRenderer;//���İ� ������ ���� ��������Ʈ ������
    public float fadeSpeed = 10f; // ���� ��ȭ �ӵ�


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
    public IEnumerator PatternProgress() // �ڷ�ƾ�� �̿��� �Ѿ� �߻�
    {
        isPatternStop = false;
        yield return StartCoroutine(MoveToStart());
        while (true)
        {
            yield return StartCoroutine(FadeTo(0));
            int randomIndex = Random.Range(0, movePoss.Count);
            Transform randomPos = movePoss[randomIndex]; //�������� movePoss�� �ϳ��� ����
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
