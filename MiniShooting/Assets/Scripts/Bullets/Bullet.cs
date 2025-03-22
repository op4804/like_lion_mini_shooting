using JetBrains.Annotations;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Bullet : MonoBehaviour
{

    private GameManager gm;
    private Rigidbody2D rb;

    private void Start()
    {
        gm = GameManager.Instance;
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * Player.Instance.GetbulletSpeed();
    }
    private void OnEnable()
    {
        if (gm == null)
            gm = GameManager.Instance;

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity = transform.right * Player.Instance.GetbulletSpeed();
        SkillManager.Instance.RegisterBulletEffect(gameObject, name);
    }

    void Update()
    {
        DestroyOutOfBoundary(); // 화면 밖으로 나가면 사라지는 부분
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SkillManager.Instance.IsBulletHaveEffect(gameObject, name))
        {
            return;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit(Player.Instance.GetAttack());

            // TODO: 관통?
            SkillManager.Instance.NotifyEffectComplete(gameObject, name);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossStatus>().Hit(Player.Instance.GetAttack());
            SkillManager.Instance.NotifyEffectComplete(gameObject, name);
        }
    }

    public bool NotifyEffectOutOfScreen()
    {
        return true;
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


