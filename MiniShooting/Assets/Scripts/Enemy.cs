using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float expAmount = 10f; //�� óġ�� ����ġ
    private float enemySpeed = 1f; //�� �ӵ�

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
