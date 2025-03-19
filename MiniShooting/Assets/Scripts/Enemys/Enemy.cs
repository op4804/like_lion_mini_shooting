using System.Collections;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour
{
    // 몬스터 공격 간격
    protected float attackDelay;

    // 몬스터 체력
    [SerializeField]
    protected float currentEnemyHP;
    private float MaxEnemyHp;

   
    void Start()
    {
           
    }    

    void Update()
    {
        
    }   

    public void Hit(float damage)
    {
        currentEnemyHP -= damage;
        if (currentEnemyHP < 0) // 사망
        {
            Destroy(gameObject);
            GameObject expParticle = Instantiate(ResourceManager.Instance.expParticle, transform.position, Quaternion.identity);
            expParticle.GetComponent<ExperienceParticle>().SetExpAmount(10f);
        }        
    }
}
