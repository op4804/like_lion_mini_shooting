using JetBrains.Annotations;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed; //발사체 속도
    [SerializeField]
    private float bulletAttack;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit(bulletAttack);

            // TODO: 관통?
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<BossStatus>().Damaged(1);//나중에 탄환데미지로변경 테스트로 1넣음

        }
    }

    public void SetBulletAttack(float bulletAttack)
    {
        this.bulletAttack = bulletAttack;
    }
    public void SetBulletSpeed(float bulletSpeed)
    {
        this.bulletSpeed = bulletSpeed;
    }
}
