using JetBrains.Annotations;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * Player.Instance.GetbulletSpeed();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
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

            // TODO: °üÅë?
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
}
