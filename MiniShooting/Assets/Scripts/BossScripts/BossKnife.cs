using UnityEngine;

public class BossKnife : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnBecameInvisible()//ȭ�鿡�Ⱥ��̸����
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)//�÷��̾�� �ε����� �������ְ� ����
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hit(); //�ȿ����ڷ� damage�־��ٰ�
            Destroy(gameObject);
        }
    }
}
