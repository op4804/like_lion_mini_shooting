using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class EliteEnemy : Enemy
{

    protected override void OnEnable()
    {
        base.OnEnable();       
        UIManager.Instance.ActivateEliteEnemyHpBar(this);
        Debug.Log("EliteEnemy OnEnable");
    }

    public override void Hit(float damage)
    {
        if (isDead) return; // �׾����� �ǰݵ��� ����.

        //�ǰ� ����
        SoundManager.instance.PlayerHit();

        currentEnemyHP -= damage;

        if (currentEnemyHP <= 0) // ���
        {
            isDead = true;

            GetComponent<Collider2D>().enabled = false;
            GameObject Skill = Instantiate(ResourceManager.Instance.skillBook, transform.position, Quaternion.identity);
            // ũ�� ���̱�-> �پ��� ������Ʈ �ı�
            StopAllCoroutines(); // ��� �ൿ ����
            UIManager.Instance.DeactivateEliteEnemyHpBar();
            StartCoroutine(RotateAndShrinkAndDie());
            SoundManager.instance.EliteDie();

            return;
        }

        gameObject.GetComponent<Animator>().SetTrigger("hit");
    }

    public float GetHpRatio()
    {
        return currentEnemyHP / maxEnemyHp;
    }
}
