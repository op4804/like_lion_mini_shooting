using UnityEngine;
using System.Collections;

public class BombEliteEnemy : EliteEnemy
{
    protected override void OnEnable()
    {
        currentEnemyHP = 10;
        StartCoroutine(MoveFoward()); // ������ ����
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
        if (isDead) return; // �׾����� �ǰݵ��� ����.

        currentEnemyHP -= damage;

        if (currentEnemyHP <= 0) // ���
        {
            isDead = true;
            GetComponent<Collider2D>().enabled = false;

            // TODO: Ư�� ������ ����
            // GameObject expParticle = Instantiate(ResourceManager.Instance.expParticle, transform.position, Quaternion.identity);

            StartCoroutine(ClusterExplosion());

            return;
        }
    }

    IEnumerator ClusterExplosion()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject go = Instantiate(ResourceManager.Instance.bombEnemy, transform.position, Quaternion.identity);
            go.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 10f, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0);
    }
}
