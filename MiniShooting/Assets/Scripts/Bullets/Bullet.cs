using JetBrains.Annotations;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Bullet : MonoBehaviour
{

    GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * Player.Instance.GetbulletSpeed();
    }

    void Update()
    {
        DestroyOutOfBoundary(); // 화면 밖으로 나가면 사라지는 부분
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (SkillManager.Instance.IsBulletHaveEffect(gameObject))
        {
            return;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit(Player.Instance.GetAttack());

            // TODO: 관통?
            SkillManager.Instance.NotifyEffectComplete(gameObject);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {            
            collision.gameObject.GetComponent<BossStatus>().Hit(Player.Instance.GetAttack());
            SkillManager.Instance.NotifyEffectComplete(gameObject);
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


