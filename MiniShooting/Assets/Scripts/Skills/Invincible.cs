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

        // ��ų ����
        skillName = "����";
        description = "���� �ð� ���� ���°� �˴ϴ�.";
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
        Debug.Log($"{skillName} ��ų ���!");
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
                Debug.Log("���� �� - �Ѿ� ����, ������ ����");
                Destroy(collision.gameObject);
            }
        }
    }

}
