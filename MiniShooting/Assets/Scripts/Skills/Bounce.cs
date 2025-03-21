using UnityEngine;

public class Bounce : Skill
{
    public GameObject bouncingBulletPrefab;

    private int maxBounces = 3; //�ִ� ƨ��� Ƚ��
    private float bounceRadius = 10f; //ƨ��� ����

    public override void InitializeSkill(Player player)
    {
        base.InitializeSkill(player);

        //��ų ����
        skillName = "�ٿ";
        description = "�Ѿ��� ���� ���̸� ƨ��ϴ�.";
        skillType = false;
        isUnlocked = false;
    }

    public override void ApplyEffect()
    {
        if (!isUnlocked) return;
        Debug.Log("Bounce ��ų �����");

        SkillManager.Instance.SetBulletPrefab(bouncingBulletPrefab);

        SkillManager.Instance.AddBulletModifier((bullet) =>
        {
            var bouncingBullet = bullet.AddComponent<BouncingBullet>();
            bouncingBullet.SetBounceValues(maxBounces, bounceRadius);
        });
    }
}
