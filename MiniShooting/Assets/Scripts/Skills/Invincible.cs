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
        coolTime = 10f;
        description = "���� �ð� ���� ���°� �˴ϴ�.";
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
        Debug.Log($"{skillName} ��ų ���!");
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
                Debug.Log("���� �� - �Ѿ� ����, ������ ����");
                Destroy(collision.gameObject);
            }
        }
    }

}
