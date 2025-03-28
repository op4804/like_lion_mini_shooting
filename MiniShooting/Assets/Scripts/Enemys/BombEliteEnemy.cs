using UnityEngine;
using System.Collections;

public class BombEliteEnemy : EliteEnemy
{

    float colorGB = 1;

    protected override void OnEnable()
    {
        base.OnEnable();
        maxEnemyHp = 50;
        currentEnemyHP = maxEnemyHp;
        StartCoroutine(MoveFoward()); // 앞으로 가기
    }

    IEnumerator MoveFoward()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForFixedUpdate();
            transform.Translate(Vector2.left * Time.deltaTime * 2f);
        }    
    }

    public override void Hit(float damage)
    {
        if (isDead) return; // 죽었으면 피격되지 않음.

        currentEnemyHP -= damage;

        if (currentEnemyHP <= 0) // 사망
        {
            isDead = true;
            GetComponent<Collider2D>().enabled = false;

            // TODO: 특성 아이템 생성

            StartCoroutine(ClusterExplosion());
            UIManager.Instance.DeactivateEliteEnemyHpBar();
            GameObject go = Instantiate(ResourceManager.Instance.explosionEffect, transform.position, Quaternion.identity);
            go.transform.localScale = new Vector3(10, 10, 10);
            GameObject Skill = Instantiate(ResourceManager.Instance.skillBook, transform.position, Quaternion.identity);            
            ResourceManager.Instance.Deactivate(gameObject, 0.1f);
            return;
        }

        colorGB -= 0.1f;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, colorGB, colorGB);
           
    }
IEnumerator ClusterExplosion()
    {
    float angle = 360 / 10;    
    for (int i = 0; i < 10; i++)
        {
            float x = Mathf.Cos(angle * Mathf.Deg2Rad * i);
            float y = Mathf.Sin(angle * Mathf.Deg2Rad * i);
            float randForce = Random.Range(1.0f, 2.5f);
            GameObject go = Instantiate(ResourceManager.Instance.bombEnemy, transform.position, Quaternion.identity);
            go.AddComponent<Rigidbody2D>();
            go.GetComponent<Rigidbody2D>().gravityScale = 0;
            go.GetComponent<Rigidbody2D>().AddForce(new Vector2(x,y) * randForce, ForceMode2D.Impulse);
            go.GetComponent<BombEnemy>().Scat();
        }
        yield return new WaitForSeconds(0);
    }
}
