using System.Collections;
using UnityEngine;

public class OneEyeEnemy : Enemy
{
    // 몬스터의 공격
    float attackDelay = 2.0f;

    GameManager gm;

    void Start()
    {
        gm = GameManager.Instance;
    }

    protected override void OnEnable() // 처음, 재생성되었을때 초기화
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

    public override void Hit(float damage)
    {
        if (isDead) return;

        currentEnemyHP -= damage;

        if (currentEnemyHP <= 0)
        {
            SoundManager.instance.OneEyeEnemyDie();

            isDead = true;
            GetComponent<Collider2D>().enabled = false;

            GameObject expParticle = Instantiate(ResourceManager.Instance.expParticle, transform.position, Quaternion.identity);
            expParticle.GetComponent<ExperienceParticle>().SetExpAmount(10f);

            StopAllCoroutines();
            StartCoroutine(RotateAndShrinkAndDie());

            return;
        }

        gameObject.GetComponent<Animator>().SetTrigger("hit");
        transform.Translate(Vector3.right * 0.1f);
    }
}
