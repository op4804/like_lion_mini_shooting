using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float expAmount = 10f; //적 처치시 경험치
    private float enemySpeed = 1f; //적 속도

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * enemySpeed);
    }

    public void Hit()
    {
        GameObject expParticle = Instantiate(ResourceManager.Instance.expParticle, transform.position, Quaternion.identity);
        expParticle.GetComponent<ExperienceParticle>().SetExpAmount(expAmount);
        Destroy(gameObject);
    }
}
