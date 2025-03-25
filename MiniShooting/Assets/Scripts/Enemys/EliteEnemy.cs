using UnityEngine;
using System.Collections;

public class EliteEnemy : Enemy
{

    // TODO: 따라다니는 HP만들기
    public override void Hit(float damage)
    {
        if (isDead) return; // 죽었으면 피격되지 않음.

        currentEnemyHP -= damage;

        if (currentEnemyHP <= 0) // 사망
        {
            isDead = true;

            GetComponent<Collider2D>().enabled = false;

            // TODO: 특성 아이템 생성
            // GameObject expParticle = Instantiate(ResourceManager.Instance.expParticle, transform.position, Quaternion.identity);
            
            // 크기 줄이기-> 줄어들면 오브젝트 파괴
            StopAllCoroutines(); // 모든 행동 중지
            StartCoroutine(RotateAndShrinkAndDie());

            return;
        }

        gameObject.GetComponent<Animator>().SetTrigger("hit");
    }
}
