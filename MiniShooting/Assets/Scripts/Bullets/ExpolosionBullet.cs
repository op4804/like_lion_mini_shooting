using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class ExplosionBullet : MonoBehaviour
{
    private float explosionRadius = 3f; //���� �ݰ�
    private float explosionDamageMultiplier = 0.5f; //���� ���� ����

    private Vector3 originalScale;

    private GameObject effectPrefab; //����Ʈ ����

    private void OnEnable()
    {
        transform.localScale = originalScale;
    }

    private void Awake()
    {
        originalScale = transform.localScale;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    public void SetExplosionValues(float explosionRadius, float explosionDamageMultiplier, GameObject effectPrefab)
    {
        this.explosionRadius = explosionRadius;
        this.explosionDamageMultiplier = explosionDamageMultiplier;
        this.effectPrefab = effectPrefab;
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                float playerAttack = Player.Instance.GetAttack() * explosionDamageMultiplier;
                hit.GetComponent<Enemy>().Hit(playerAttack);

                transform.localScale = Vector3.one * explosionRadius * 2f;
            }
        }

        //����Ʈ �������Դϴ�. ��ų�Ŵ����� ������� ������ ����, �����ؾ��մϴ�.
        if (effectPrefab != null)
        {
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            float scale = explosionRadius * 2f;
            effect.transform.localScale = new Vector3(scale, scale, 1f);

            Destroy(effect, 0.2f);
        }

        SkillManager.Instance.NotifyEffectComplete(gameObject, name);
    }
}
