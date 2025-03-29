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

        if (isDead) return; // �׾����� �ǰݵ��� ����.

        //�ǰ� ����
        SoundManager.instance.PlayerHit();

        currentEnemyHP -= damage;
        Debug.Log($"{currentEnemyHP}", transform);
        if (currentEnemyHP <= 0) // ���
        {

            isDead = true;

            GetComponent<Collider2D>().enabled = false;

            GameObject expParticle = Instantiate(ResourceManager.Instance.expParticle, transform.position, Quaternion.identity);
            expParticle.GetComponent<ExperienceParticle>().SetExpAmount(10f);

            // ũ�� ���̱�-> �پ��� ������Ʈ �ı�
            StopAllCoroutines(); // ��� �ൿ ����
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
