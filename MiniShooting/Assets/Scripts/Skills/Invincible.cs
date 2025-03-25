using System.Collections;
using UnityEngine;

public class Invincible : Skill
{
    private SpriteRenderer SpriteRenderer;
    private Color playerColor;
    private bool isInvincible = false;
    public bool IsInvincible => isInvincible;
    private float duration = 3f;
    private Color InvincibleColor = Color.yellow;

    public override void InitializeSkill(Player player)
    {
        base.InitializeSkill(player);

        // 스킬 설정
        skillName = "무적";
        description = "일정 시간 무적 상태가 됩니다.";
        skillType = true;
        coolTime = 10f;
        isUnlocked = false;

        SpriteRenderer = player.GetComponent<SpriteRenderer>();
        if(SpriteRenderer != null)
        {
            playerColor = SpriteRenderer.color;
        }
    }

    public override void UseSkill()
    {
        if (!CanUse())
        {
            Debug.Log($"CoolTime : {Mathf.Max(0, coolTime - (Time.time - lastUsedTime))}");
            return;
        }
        Debug.Log($"{skillName} 스킬 사용!");
        player.StartCoroutine(InvincibleMode());
        SkillCoolTime();
    }

    IEnumerator InvincibleMode()
    {
        isInvincible = true;
        player.SetInvincibleMode(true);

        if (SpriteRenderer != null)
        {
            SpriteRenderer.color = InvincibleColor;
        }

        yield return new WaitForSeconds(duration);

        if (SpriteRenderer != null)
        {
            SpriteRenderer.color = playerColor;
        }

        player.SetInvincibleMode(false);
        isInvincible = false;
    }

    public void ActiveInvincible(float duration, Color glowColor)
    {
        if (!isInvincible)
        {
            StartCoroutine(NeonInvincibleMode(duration, glowColor));
        }
    }

    private IEnumerator NeonInvincibleMode(float duration, Color glowColor)
    {
        isInvincible = true;
        player.SetInvincibleMode(true);

        yield return StartCoroutine(NeonEffect(duration));

        player.SetInvincibleMode(false);
        isInvincible = false;
    }


    private IEnumerator NeonEffect(float duration)
    {
        float timer = 0f;
        bool isYellow = true;

        while (timer < duration)
        {
            if (SpriteRenderer != null)
            {
                SpriteRenderer.color = isYellow ? Color.yellow : Color.white;
            }

            isYellow = !isYellow;
            timer += 0.1f;

            yield return new WaitForSeconds(0.1f);
        }

        if (SpriteRenderer != null)
            SpriteRenderer.color = playerColor;
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemyBullet"))
        {
            if (isInvincible)
            {
                Debug.Log("무적 중 - 총알 제거, 데미지 없음");
                Destroy(collision.gameObject);
            }
        }
    }

}
