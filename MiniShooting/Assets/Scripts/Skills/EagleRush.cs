using System.Collections;
using UnityEngine;

public class EagleRush : Skill
{
    [Header("Eagle Rush Settings")]
    [SerializeField] private string eagleKey = "Eagle";
    [SerializeField] private AudioSource eagleSound;

     private int eagleCount = 5; //��ȯ ��ü��
     private float eagleSpacing = 0.2f; //��ȯ ����
     private float eagleSpeed = 40f; //�������� �ӵ�
     private float yRange = 2f; //��ȯ ����

    private bool isUsingSkill = false;

    public override void InitializeSkill(Player player)
    {
        base.InitializeSkill(player);

        skillName = "�̱� ����";
        description = "���� ������ �������� �÷��̾� �ڿ��� ���ư��� ���� �����մϴ�.";
        damage = 100;
        skillType = true;
        coolTime = 5.0f;
        isUnlocked = true;
    }

    public override void UseSkill()
    {
        if (!CanUse())
        {
            Debug.Log($"CoolTime : {Mathf.Max(0, coolTime - (Time.time - lastUsedTime))}");
            return;
        }

        if (isUsingSkill) return;

        SkillCoolTime();

        if (eagleSound != null)
        {
            eagleSound.enabled = true;
            eagleSound.Play();
        }

        player.StartCoroutine(FireEagles());
    }
    private IEnumerator FireEagles()
    {
        isUsingSkill = true;

        Camera cam = Camera.main;
        float leftEdgeX = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x-5;
        Vector2 direction = Vector2.right;

        for (int i = 0; i < eagleCount; i++)
        {
            float randomY = Random.Range(-yRange, yRange);
            Vector2 spawnPos = new Vector2(leftEdgeX, player.transform.position.y + randomY);

            GameObject eagle = ResourceManager.Instance.Create(eagleKey, spawnPos);
            Rigidbody2D rb = eagle.GetComponent<Rigidbody2D>();

            if (rb != null)
                rb.linearVelocity = direction * eagleSpeed;

            ActiveSkillHit hit = eagle.GetComponent<ActiveSkillHit>();
            if (hit != null)
                hit.Initialize(damage);

            ResourceManager.Instance.Deactivate(eagle, 2f);

            yield return new WaitForSeconds(eagleSpacing);
        }

        isUsingSkill = false;
    }
}
