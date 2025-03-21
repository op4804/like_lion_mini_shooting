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
        coolTime = 10f;
        description = "일정 시간 무적 상태가 됩니다.";
        skillType = true;

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
        if (!isInvincible)
        {
            player.StartCoroutine(InvincibleMode());
        }
    }

    IEnumerator InvincibleMode()
    {
        isInvincible = true;
        if (SpriteRenderer != null)
        {
            SpriteRenderer.color = InvincibleColor;
        }

        yield return new WaitForSeconds(duration);

        if (SpriteRenderer != null)
        {
            SpriteRenderer.color = playerColor;
        }
        isInvincible = false;
    }

    public void ActiveInvincible(float duration, Color glowColor)
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibleMode(duration, glowColor));
        }
    }

    IEnumerator InvincibleMode(float duration, Color glowColor)
    {
        isInvincible = true;

        if (SpriteRenderer != null)
        {
            SpriteRenderer.color = glowColor;
        }

        yield return new WaitForSeconds(duration);

        if (SpriteRenderer != null)
        {
            SpriteRenderer.color = playerColor;
        }

        isInvincible = false;

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
