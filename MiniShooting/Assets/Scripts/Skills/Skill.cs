using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected string skillName { get; set; }//��ų �̸�
    protected string description { get; set; } //��ų ����
    protected float coolTime { get; set; }  // ��Ÿ��
    protected float lastUsedTime { get; set; } = -10f; //������ ��� �ð�

    protected bool skillType { get; set; } // false�� passive, true�� active
    public bool IsActiveSkill => skillType;
    protected bool isUnlocked { get; set; } = false; //��ų Ȱ��ȭ ����

    protected Player player;

    public virtual void InitializeSkill(Player player)
    {
        this.player = player;
    }

    public void UnlockSkill()
    {
        isUnlocked = true;
        ApplyEffect();
    }

    public virtual void UseSkill()
    {
        if (!isUnlocked || skillType || !CanUse()) return;
        SkillCoolTime();
    }

    public virtual void ApplyEffect()
    {
        if (!isUnlocked || !skillType) return;
    }

    protected bool CanUse()
    {
        return Time.time - lastUsedTime >= coolTime;
    }

    protected void SkillCoolTime()
    {
        lastUsedTime = Time.time;
    }

    public bool GetSkillType()
    {
        return skillType;
    }
}
