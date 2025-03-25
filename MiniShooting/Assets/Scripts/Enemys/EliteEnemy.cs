using UnityEngine;
using System.Collections;

public class EliteEnemy : Enemy
{

    // TODO: ����ٴϴ� HP�����
    public override void Hit(float damage)
    {
        if (isDead) return; // �׾����� �ǰݵ��� ����.

        currentEnemyHP -= damage;

        if (currentEnemyHP <= 0) // ���
        {
            isDead = true;

            GetComponent<Collider2D>().enabled = false;

            // TODO: Ư�� ������ ����
            // GameObject expParticle = Instantiate(ResourceManager.Instance.expParticle, transform.position, Quaternion.identity);
            
            // ũ�� ���̱�-> �پ��� ������Ʈ �ı�
            StopAllCoroutines(); // ��� �ൿ ����
            StartCoroutine(RotateAndShrinkAndDie());

            return;
        }

        gameObject.GetComponent<Animator>().SetTrigger("hit");
    }
}
