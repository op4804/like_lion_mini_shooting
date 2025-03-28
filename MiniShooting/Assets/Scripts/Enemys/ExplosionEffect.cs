using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = new Vector3(5, 5, 5); // ����ũ�� 555
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
