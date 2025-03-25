using UnityEngine;

public class Explosion : Skill
{
    private float explosionRadius = 3f; //���� �ݰ�
    private float explosionDamageMultiplier = 0.5f; //���� ���� ����

    public GameObject effectPrefab; //���̾��Ű���� ����Ʈ�� ����

    public override void InitializeSkill(Player player)
    {
        base.InitializeSkill(player);
        skillName = "����";
        effectKey = "Explosion";
        description = "���� ���߸� �����Ͽ� �ֺ� ������ ���ظ� �ݴϴ�.";
        skillType = false; // �нú� ��ų
        isUnlocked = false;
    }

    public override void ApplyEffect()
    {
        if (!isUnlocked) return;

        Debug.Log($"{effectKey} ��ų �����");

        SkillManager.Instance.AddBulletModifier((bullet) =>
        {
            if (!bullet.GetComponent<ExplosionBullet>()) 
            {
                var explosionBullet = bullet.AddComponent<ExplosionBullet>();
                                
                explosionBullet.SetExplosionValues(explosionRadius, explosionDamageMultiplier, effectPrefab);
                explosionBullet.SetEffectKey(effectKey); // Ű ���� �������ݴϴ�.
            }
        });
    }
}
