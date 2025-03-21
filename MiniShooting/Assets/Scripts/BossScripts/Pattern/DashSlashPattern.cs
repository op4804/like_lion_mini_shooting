using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DashPattern : MonoBehaviour, IBossPattern
{
    public float dashSpeed = 20f;
    public float dashWaringTime = 2f;
    public float Waringwidth = 2f;
    public float movetime = 1f;
    public float dashWaitTime = 2f;

    private Vector2 startPos;
    private bool isActive = false;

    private LineRenderer lineRenderer;
    private Transform playerTransform;


    void Awake()
    {
        startPos = transform.position;

        lineRenderer = GetComponent<LineRenderer>(); // ���� ������ ��������
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>(); // ������ �߰�
        }
        lineRenderer.startWidth = Waringwidth;
        lineRenderer.endWidth = Waringwidth;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // �⺻ ��Ƽ���� ����
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        Color newStartColor = lineRenderer.startColor;
        Color newEndColor = lineRenderer.endColor;
        newStartColor.a = 0.5f;
        newEndColor.a = 0.5f;
        lineRenderer.startColor = newStartColor;
        lineRenderer.endColor = newEndColor;
        lineRenderer.enabled = false; // �⺻������ ��Ȱ��ȭ

        playerTransform = Player.Instance.transform;
    }

    public void StartPattern()
    {
        isActive = true;
        StartCoroutine(MakeWarningCoroutine());
    }

    public void StopPattern()
    {
        isActive = false;
        StopAllCoroutines();
    }

    IEnumerator MakeWarningCoroutine()
    {
        if (playerTransform == null)
            yield break;

        lineRenderer.enabled = true;

        // ���� �������� �������� ������ �����Ͽ� �÷��̾� �������� ���� ���� �׸�
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, playerTransform.position);

        yield return new WaitForSeconds(dashWaringTime); // ��� �� ��� �ð�

        lineRenderer.enabled = false;
        StartCoroutine(Move());

    }
    IEnumerator Move()
    {
        if (playerTransform == null)
            yield break;

        float elapsedTime = 0f; // ��� �ð�
        Vector2 startPos = transform.position; // ���� ��ġ
        Vector2 targetPos = playerTransform.position; // ��ǥ ��ġ
        Vector2 direction = (targetPos - startPos).normalized; // �̵� ����

        while (elapsedTime < movetime)
        {
            transform.position += (Vector3)(direction * dashSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null; // ���� �����ӱ��� ���
        }

        // �̵��� ������ SlashCoroutine ����
        StartCoroutine(SlashCoroutine());
    }

    IEnumerator SlashCoroutine()
    {

        // Į ����Ʈ ���� (���⼭�� ������ ��ƼŬ ȿ��)
        GameObject slashEffect = new GameObject("SlashEffect");
        SpriteRenderer renderer = slashEffect.AddComponent<SpriteRenderer>();
        renderer.color = Color.red; // ������ ȿ��
        slashEffect.transform.position = transform.position;
        Destroy(slashEffect, 0.5f); // 0.5�� �� ����

        // ���� �� �÷��̾ ������ ���� ������
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, Waringwidth / 2f);
        foreach (var hit in hitObjects)
        {
            if (hit.CompareTag("Player"))
            {
                hit.gameObject.GetComponent<Player>().Hit();
            }

        }
        yield return new WaitForSeconds(dashWaitTime);

        this.transform.position = startPos;
        StartPattern();
    }




}
