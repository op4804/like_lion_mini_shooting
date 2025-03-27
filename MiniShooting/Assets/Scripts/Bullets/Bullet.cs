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

        //SkillManager.Instance.ReRegisterEffects(gameObject);
    }

    void Update()
    {
        DestroyOutOfBoundary(); // ȭ�� ������ ������ ������� �κ�
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (SkillManager.Instance.IsBulletHaveEffect(gameObject, name))
    //    {
    //        return;
    //    }

    //    if (collision.gameObject.CompareTag("Enemy"))
    //    {
    //        collision.gameObject.GetComponent<Enemy>().Hit(Player.Instance.GetAttack());

    //        // TODO: ����?
    //        SkillManager.Instance.NotifyEffectComplete(gameObject, name);
    //        SoundManager.instance.PlayerHit(); // �÷��̾ �� �ǰ� �� �Ҹ� ���
    //        Debug.Log("�÷��̾� ��Ʈ ���� ����!");
    //    }
    //    else if (collision.gameObject.CompareTag("Boss"))
    //    {
    //        collision.gameObject.GetComponent<BossStatus>().Hit(Player.Instance.GetAttack());
    //        SkillManager.Instance.NotifyEffectComplete(gameObject, name);
    //    }
    //}

    // enemy���� bullet�� �ǰݵǸ� �Ҹ� ����� ���� Trigger ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool hasEffect = SkillManager.Instance.IsBulletHaveEffect(gameObject);
        if (hasEffect)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit(Player.Instance.GetAttack());

            SkillManager.Instance.NotifyEffectComplete(gameObject);

            SoundManager.instance.PlayerHit();
            //Debug.Log("�÷��̾� ��Ʈ ���� ����!");
        }

        else if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<Boss>().Hit(Player.Instance.GetAttack());
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
            SkillManager.Instance.NotifyAllEffectsComplete(gameObject);
            ResourceManager.Instance.Deactivate(gameObject);
        }
    }
}


