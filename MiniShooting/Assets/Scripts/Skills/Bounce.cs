using UnityEngine;

public class Bounce : Skill
{
    public GameObject bouncingBulletPrefab;

    private int maxBounces = 3; //ÃÖ´ë Æ¨±â´Â È½¼ö
    private float bounceRadius = 10f; //Æ¨±â´Â ¹üÀ§

    public override void InitializeSkill(Player player)
    {
        base.InitializeSkill(player);

        //½ºÅ³ ¼³Á¤
        skillName = "¹Ù¿î½º";
        description = "ÃÑ¾ËÀÌ Àûµé »çÀÌ¸¦ Æ¨±é´Ï´Ù.";
        skillType = false;
        isUnlocked = false;
    }

    public override void ApplyEffect()
    {
        if (!isUnlocked) return;
        Debug.Log("Bounce ½ºÅ³ Àû¿ëµÊ");

        SkillManager.Instance.SetBulletPrefab(bouncingBulletPrefab);

        SkillManager.Instance.AddBulletModifier((bullet) =>
        {
            var bouncingBullet = bullet.AddComponent<BouncingBullet>();
            bouncingBullet.SetBounceValues(maxBounces, bounceRadius);
        });
    }
}
