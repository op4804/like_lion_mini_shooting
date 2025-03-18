using UnityEngine;

// @ 프리펩 enemybullet에 연결된 스크립트.
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
