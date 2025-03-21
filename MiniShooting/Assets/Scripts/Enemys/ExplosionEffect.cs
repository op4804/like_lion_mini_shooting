using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, 1f); // 1�� �� ������ �ı�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Hit();
        }
    }

}
