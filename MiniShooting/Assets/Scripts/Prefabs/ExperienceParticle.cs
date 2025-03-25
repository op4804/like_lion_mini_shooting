using UnityEngine;

public class ExperienceParticle : MonoBehaviour
{

    private float moveSpeed = 5f; // �÷��̾�� ���ư��� �ӵ�
    private float attractionRange = 3f; // �÷��̾�� ���ư��� �����ϴ� �Ÿ�

    private float expAmount = 0f;
    private Transform player;
    private bool isAttracted = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        
        // �÷��̾ Ư�� �Ÿ� �ȿ� ������ �ڵ����� ���󰡵��� ����
        if (distance < attractionRange)
        {
            isAttracted = true;
        }

        if (isAttracted)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position += new Vector3(-0.5f * Time.deltaTime, 0,0);
        }
    }

    public void SetExpAmount(float amount)
    {
        expAmount = amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().GetExpParticle(expAmount);
            Destroy(gameObject);
        }
    }
}
