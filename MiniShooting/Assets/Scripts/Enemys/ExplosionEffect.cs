using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private void OnEnable()
    {
        ResourceManager.Instance.Deactivate(gameObject, 1f); // 1�� �� ������ �ı�
        SoundManager.instance.BombEnemyAttack(); // ���� �� ȿ���� ���
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Hit();
        }
    }

}
