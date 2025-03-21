using System.Collections;
using UnityEngine;

public class OneEyeEnemy : Enemy
{
    // 몬스터의 공격
    GameObject enemyBullet;
    float attackDelay;

    GameManager gm;

    void Start()
    {
        enemyBullet = ResourceManager.Instance.enemyBullet;
        currentEnemyHP = 20; // TODO: 
        attackDelay = 2.0f;

        gm = GameManager.Instance;

        StartCoroutine(FireBullet());
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
