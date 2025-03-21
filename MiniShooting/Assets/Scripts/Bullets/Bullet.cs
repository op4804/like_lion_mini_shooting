using JetBrains.Annotations;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed; // �߻�ü �ӵ�
    [SerializeField]
    private float bulletAttack;

    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
    }

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed);
        DestroyOutOfBoundary(); // ȭ�� ������ ������ ������� �κ�
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit(bulletAttack);

            // TODO: ����?
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<BossStatus>().Damaged(1);//���߿� źȯ�������κ��� �׽�Ʈ�� 1����

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

    private void DestroyOutOfBoundary()
    {
        if (transform.position.x > gm.maxBounds.x || transform.position.x < gm.minBounds.x
            || transform.position.y > gm.maxBounds.y || transform.position.y < gm.minBounds.y)
        {
            ResourceManager.Instance.Deactivate(gameObject);
        }
    }


}


