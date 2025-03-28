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
        if (isDead) return; // 죽었으면 피격되지 않음.

        currentEnemyHP -= damage;

        if (currentEnemyHP <= 0) // 사망
        {
            isDead = true;

            GetComponent<Collider2D>().enabled = false;
            GameObject Skill = Instantiate(ResourceManager.Instance.skillBook, transform.position, Quaternion.identity);
            // 크기 줄이기-> 줄어들면 오브젝트 파괴
            StopAllCoroutines(); // 모든 행동 중지
            UIManager.Instance.DeactivateEliteEnemyHpBar();
            StartCoroutine(RotateAndShrinkAndDie());

            return;
        }

        gameObject.GetComponent<Animator>().SetTrigger("hit");
    }

    public float GetHpRatio()
    {
        return currentEnemyHP / maxEnemyHp;
    }
}
