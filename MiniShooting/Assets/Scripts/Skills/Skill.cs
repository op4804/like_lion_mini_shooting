using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    protected string skillName { get; set; }//스킬 이름
    protected string effectKey { get; set; } // 내부 시스템용 키 (영문/고유)
    protected string description { get; set; } //스킬 설명

    protected float damage { get; set; } //데미지
    protected float coolTime { get; set; }  // 쿨타임
    protected float lastUsedTime { get; set; } = -10f; //마지막 사용 시간
    protected bool skillType { get; set; } // false면 passive, true면 active
    public bool IsActiveSkill => skillType;
    protected bool isUnlocked { get; set; } = false; //스킬 활성화 여부

    protected Player player;

    public virtual void InitializeSkill(Player player)
    {
        this.player = player;
    }

    public bool IsUnlockSkill()
    {
        return isUnlocked;
    }

    public void LockSkill()
    {
        isUnlocked = false;
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

    public virtual bool HasEffectComponent(GameObject bullet)
    {
        return false;
    }

    public string GetEffectKey()
    {
        return effectKey;
    }
}
