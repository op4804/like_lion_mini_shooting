using UnityEngine;

public class Boss : Enemy
{

    private void OnEnable()
    {
        maxEnemyHp = 1000;
        currentEnemyHP = maxEnemyHp;
        GetComponent<Collider2D>().enabled = true;
        isDead = false;
        transform.localScale = originalScale;
        transform.rotation = Quaternion.identity;
    }

    public override void Hit(float damage) 
    {

        if (isDead) return; // 죽었으면 피격되지 않음.

        //피격 사운드
        SoundManager.instance.PlayerHit();

        currentEnemyHP -= damage;
        Debug.Log($"{currentEnemyHP}", transform);
        if (currentEnemyHP <= 0) // 사망
        {

            isDead = true;

            GetComponent<Collider2D>().enabled = false;

            GameObject expParticle = Instantiate(ResourceManager.Instance.expParticle, transform.position, Quaternion.identity);
            expParticle.GetComponent<ExperienceParticle>().SetExpAmount(10f);

            // 크기 줄이기-> 줄어들면 오브젝트 파괴
            StopAllCoroutines(); // 모든 행동 중지
            StartCoroutine(RotateAndShrinkAndDie());
            SoundManager.instance.EnemyDie();

            return;
        }


    }
    public float GetMaxHp()
    {
        return maxEnemyHp;
    }
    public void setMaxHp(float hp)
    {
        maxEnemyHp = hp;
    }
    public float GetCurrentHp()
    {
        return currentEnemyHP;
    }
    public void setCurrentHp(float hp)
    {
        currentEnemyHP = hp;
    }
}
