using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private void OnEnable()
    {
        transform.localScale = new Vector3(5, 5, 5); // 원본크기 555
        ResourceManager.Instance.Deactivate(gameObject, 1f); // 1초 뒤 스스로 파괴
        SoundManager.instance.BombEnemyAttack(); // 폭발 시 효과음 재생
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Hit();
        }
    }

}
