using System.Collections;
using UnityEngine;

public class OneEyeEnemy : Enemy
{
    // 몬스터의 공격
    GameObject enemybullet;
    float attackDelay;

    void Start()
    {
        enemybullet = ResourceManager.Instance.enemybullet1;
        currentEnemyHP = 20; // TODO: 
        attackDelay = 2.0f;

        StartCoroutine(FireBullet());
    }

    void Update()
    {
        Move();
    }

    IEnumerator FireBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackDelay);
            Vector2 newPos = new Vector2(transform.position.x - 1.0f, transform.position.y);
            Instantiate(enemybullet, newPos, Quaternion.identity);
        }
    }
    private void Move()
    {
        transform.Translate(Vector3.left * Time.deltaTime);
    }

    public void Hit()
    {
        Debug.Log("자식");
    }
}
