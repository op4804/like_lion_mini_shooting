using UnityEngine;

// @ 프리펩 enemybullet에 연결된 스크립트.
public class EnemyBullet : MonoBehaviour
{
    private float bulletSpeed = 5f;
    private GameManager gm;

    void Update()
    {
        gm = GameManager.Instance;
        transform.Translate(Vector3.left * Time.deltaTime * bulletSpeed);

        DestroyOutOfBoundary();
        // 화면 밖으로 나가면 사라지는 부분

    }

    private void DestroyOutOfBoundary()
    {
        if(transform.position.x > gm.maxBounds.x || transform.position.x < gm.minBounds.x
            || transform.position.y > gm.maxBounds.y || transform.position.y < gm.minBounds.y)
        {
            Destroy(gameObject);
        }

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
