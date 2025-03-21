using System.Collections;
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
        currentEnemyHP = 10; // TODO: 
    }

    void Update()
    {
        Trace();
    }

    void Trace()
    {
        if(isTracing)
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
        Instantiate(ResourceManager.Instance.explosionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator Ignite() // ���� �������� �κ�
    {
        float colorGB = 1;
        while(true)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, colorGB, colorGB);            
            colorGB -= Time.deltaTime * 9;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
