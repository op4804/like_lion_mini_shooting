using UnityEngine;

// @ ������ enemybullet�� ����� ��ũ��Ʈ.
public class EnemyBullet : MonoBehaviour
{
    private float bulletSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * bulletSpeed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hit();
            Destroy(gameObject);
        }
    }
}
