using System.Collections;
using UnityEngine;

public class OneEyeEnemy : Enemy
{
    // ������ ����
    float attackDelay = 2.0f;

    GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;
    }

    protected override void OnEnable() // ó��, ������Ǿ����� �ʱ�ȭ
    {
        base.OnEnable();

        StartCoroutine(FireBullet());
        currentEnemyHP = 20; // TODO: 
    }

    void Update()
    {
        Move();
        DestroyOutOfBoundary();
    }

    IEnumerator FireBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackDelay);
            Vector2 newPos = new Vector2(transform.position.x - 1.0f, transform.position.y);
            ResourceManager.Instance.Create("enemyBullet", newPos);
        }
    }
    private void Move()
    {
        transform.Translate(Vector3.left * Time.deltaTime);
    }
    private void DestroyOutOfBoundary()
    {
        if (transform.position.x > gm.maxBounds.x || transform.position.x < gm.minBounds.x
            || transform.position.y > gm.maxBounds.y || transform.position.y < gm.minBounds.y)
        {
            ResourceManager.Instance.Deactivate(gameObject);
        }
    }
}
