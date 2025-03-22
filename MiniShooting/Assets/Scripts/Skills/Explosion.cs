using UnityEngine;

public class Explosion : Skill
{
    private float explosionRadius = 3f; //폭발 반경
    private float explosionDamageMultiplier = 0.5f; //폭발 피해 배율

    public GameObject effectPrefab; //하이어라키에서 이펙트를 삽입

    public override void InitializeSkill(Player player)
    {
        base.InitializeSkill(player);
        skillName = "폭발";
        description = "적을 맞추면 폭발하여 주변 적에게 피해를 줍니다.";
        skillType = false; // 패시브 스킬
        isUnlocked = true;
    }

    public override void ApplyEffect()
    {
        if (!isUnlocked) return;

        Debug.Log("Explosion 스킬 적용됨");

        SkillManager.Instance.AddBulletModifier((bullet) =>
        {
            if (!bullet.GetComponent<ExplosionBullet>()) 
            {
                var explosionBullet = bullet.AddComponent<ExplosionBullet>();
                                
                explosionBullet.SetExplosionValues(explosionRadius, explosionDamageMultiplier, effectPrefab);
            }
        });
    }
}
