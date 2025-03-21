using UnityEngine;
using System.Collections.Generic;

public class BouncingBullet : MonoBehaviour
{
    private int maxBounces = 3; //�ִ� ƨ��� Ƚ��
    private int currentBounce = 0; //ƨ�� Ƚ�� ī��Ʈ
    private float bounceRadius = 100f; //ƨ��� ����
    private float bulletSpeed = 15f; //�߻�ü �ӵ�

    private Rigidbody2D rb;

    private List<GameObject> alreadyHit = new List<GameObject>(); //������ �ߺ� ����

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity = transform.right * bulletSpeed;
    }

    public void SetBounceValues(int maxBounces, float bounceRadius)
    {
        this.maxBounces = maxBounces;
        this.bounceRadius = bounceRadius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemy = collision.gameObject;

            if (alreadyHit.Contains(enemy)) return;

            enemy.GetComponent<Enemy>().Hit(Player.Instance.GetAttack());

            alreadyHit.Add(enemy);

            if (currentBounce < maxBounces)
            {
                currentBounce++;
                NextTarget(enemy.transform.position);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void NextTarget(Vector3 currentPosition)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(currentPosition, bounceRadius);
        GameObject nextTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy") && !alreadyHit.Contains(hit.gameObject))
            {
                float distance = Vector3.Distance(currentPosition, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    nextTarget = hit.gameObject;
                }
            }
        }

        if (nextTarget != null)
        {
            Vector3 direction = (nextTarget.transform.position - transform.position).normalized;
            rb.linearVelocity = direction * bulletSpeed; 
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
