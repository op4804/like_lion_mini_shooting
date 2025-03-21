using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Enemy : MonoBehaviour
{
    // 몬스터 체력
    [SerializeField]
    protected float currentEnemyHP;
    private float MaxEnemyHp; 

    public void Hit(float damage)
    {
        currentEnemyHP -= damage;
        if (currentEnemyHP <= 0) // 사망
        {
            GameObject expParticle = Instantiate(ResourceManager.Instance.expParticle, transform.position, Quaternion.identity);
            expParticle.GetComponent<ExperienceParticle>().SetExpAmount(10f);
            Destroy(gameObject);
            return;
        }

        gameObject.GetComponent<Animator>().SetTrigger("hit");
        transform.Translate(Vector3.right * 0.1f);
    }
}
