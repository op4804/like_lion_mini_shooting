using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour
{
    // 몬스터 공격 간격
    private float attackDelay = 2.0f;

    // 몬스터 체력
    [SerializeField]
    private float currentEnemyHP;    
    private float MaxEnemyHp = 100; // TODO: 적당한 값으로 재설정.

    // 몬스터의 공격
    GameObject enemybullet;
    
    void Start()
    {
        enemybullet = ResourceManager.Instance.enemybullet1;
        currentEnemyHP = MaxEnemyHp;
        StartCoroutine(FireBullet());        
    }    

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime);
    }   

    public void Hit(float damage)
    {
        currentEnemyHP -= damage;
        if (currentEnemyHP <= 0) // 사망
        {
            Destroy(gameObject);
            GameObject expParticle = Instantiate(ResourceManager.Instance.expParticle, transform.position, Quaternion.identity);
            expParticle.GetComponent<ExperienceParticle>().SetExpAmount(10f);
        }        
    }

    IEnumerator FireBullet()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackDelay);
            Vector2 newPos = new Vector2(transform.position.x - 1.0f, transform.position.y);
            Instantiate(enemybullet, newPos, Quaternion.identity);
        }
    }
}
