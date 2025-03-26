using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class ExplosionBullet : MonoBehaviour
{
    private float explosionRadius = 3f; //���� �ݰ�
    private float explosionDamageMultiplier = 0.5f; //���� ���� ����
    private string effectKey; //Ű �� ����

    private Vector3 originalScale;

    private GameObject effectPrefab; //����Ʈ ����

    private void OnEnable()
    {
        if (!string.IsNullOrEmpty(effectKey))
        {
            //Debug.Log($"{GetInstanceID()}�� {effectKey}ȿ�� ���", this);
            SkillManager.Instance.RegisterBulletEffect(gameObject, effectKey);
        }
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
            }
        }

        //����Ʈ �������Դϴ�. ��ų�Ŵ����� ������� ������ ����, �����ؾ��մϴ�.
        if (effectPrefab != null)
        {
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            float scale = 0.7f;
            effect.transform.localScale = new Vector3(scale, scale, 1f);

            Animator anim = effect.GetComponent<Animator>();
            float animDuration = 0.2f; // �⺻��

            if (anim != null && anim.runtimeAnimatorController != null)
            {
                AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
                if (clips.Length > 0)
                {
                    animDuration = clips[0].length/2;
                }
            }

            Destroy(effect, animDuration);
        }

        SkillManager.Instance.NotifyEffectComplete(gameObject, effectKey);
    }

    public void SetEffectKey(string keyName)
    {
        effectKey = keyName;

        if (SkillManager.Instance != null && gameObject.activeInHierarchy)
        {
            SkillManager.Instance.RegisterBulletEffect(gameObject, effectKey);
        }
    }
}
