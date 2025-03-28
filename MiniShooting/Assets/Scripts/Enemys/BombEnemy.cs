using System.Collections;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class BombEnemy : Enemy
{
    Transform target; // ���󰡴� ������Ʈ�� ��ġ (�÷��̾�)
    float distance = 1.2f; // �÷��̾� ditance���� �Ѿư�.
    float speed = 1;
    bool isTracing = true; // ���󰡴� ��

    void Start()
    {
        target = GameManager.Instance.player.transform;
    }

    protected override void OnEnable() // �ʱ�ȭ
    {
        base.OnEnable();

        gameObject.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(1, 1, 1); // ���� �ʱ�ȭ
        isTracing = true; // ���󰡱� �ʱ�ȭ
        currentEnemyHP = 10; // TODO: 
    }

    void Update()
    {
        Trace();
    }

    void Trace()
    {
        if (isTracing)
        {
            if (Vector2.Distance(transform.position, target.position) > distance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
            else // �����Ÿ� ���� ������ ��� ���� ����
            {
                isTracing = false;
                StartCoroutine(Ignite());
                StartCoroutine(Explode());
            }
        }
    }
    IEnumerator Explode() // �����ϴ� �κ�
    {
        yield return new WaitForSeconds(1.5f); // 1.5�� �� ����
        ResourceManager.Instance.Create("explosionEffect", transform.position);
        if (currentEnemyHP > 0) // ������Ʈ Ǯ�� �ߺ����� ���� �� ����.
        {
            ResourceManager.Instance.Deactivate(gameObject);
        }
    }

    IEnumerator Ignite() // ���� �������� �κ�
    {
        float colorGB = 1;
        while (true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(1, colorGB, colorGB);
            colorGB -= Time.deltaTime * 9;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public override void Hit(float damage)
    {
        base.Hit(damage);
    }

    public void Scat()
    {
        StartCoroutine(DeleteRigidBody());
    }
    IEnumerator DeleteRigidBody()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(GetComponent<Rigidbody2D>());
    }
}
