using UnityEngine;

public class ActiveSkillHit : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private string[] targetTags = new[] { "Enemy" };

    // �ܺο��� �������� Ÿ�� �±� ���� ����
    public void Initialize(float dmg, string[] validTags = null)
    {
        damage = dmg;
        if (validTags != null)
            targetTags = validTags;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var tag in targetTags)
        {
            if (collision.CompareTag(tag))
            {
                Enemy enemy = collision.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Hit(damage);
                }
            }
        }
    }
}
