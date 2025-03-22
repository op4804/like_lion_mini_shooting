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

        BosslineRenderer = GetComponent<LineRenderer>(); // ���� ������ ��������
        BossTrailRenderer = GetComponent<TrailRenderer>(); // Ʈ���� ������ ��������
        BossTrailRenderer.enabled = false;
        BosslineRenderer.enabled = false; // �⺻������ ��Ȱ��ȭ
        BosslineRenderer.startWidth = Waringwidth;
        BosslineRenderer.endWidth = Waringwidth;
        BosslineRenderer.material = new Material(Shader.Find("Sprites/Default")); // �⺻ ��Ƽ���� ����
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

        // ���� �������� �������� ������ �����Ͽ� �÷��̾� �������� ���� ���� �׸�
        BosslineRenderer.SetPosition(0, transform.position);
        BosslineRenderer.SetPosition(1, playerTransform.position);

        yield return new WaitForSeconds(dashWaringTime); // ��� �� ��� �ð�

        BosslineRenderer.enabled = false;
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
        BossTrailRenderer.enabled = true; // �뽬 ����Ʈ Ȱ��ȭ

        while (elapsedTime < movetime)
        {
            transform.position += (Vector3)(direction * dashSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null; // ���� �����ӱ��� ���
        }
        BossTrailRenderer.enabled = false;// �뽬 ����Ʈ ��Ȱ��ȭ

        // �̵��� ������ SlashCoroutine ����
        yield return StartCoroutine(SlashCoroutine());
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
        yield return null;

        this.transform.position = startPos;
    }

}
