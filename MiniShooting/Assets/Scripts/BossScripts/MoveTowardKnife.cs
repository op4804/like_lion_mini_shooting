using System.Collections;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MoveTowardKnife : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;
    Transform playerTransform; 

    void Awake()
    {
        playerTransform = Player.Instance.transform;
    }

    void Start()
    {
        StartCoroutine(Move(playerTransform, moveSpeed));
    }

    IEnumerator Move(Transform playerTransform, float moveSpeed)
    {
        // �÷��̾� ������ ���� ���� ���
        Vector2 dir = (playerTransform.position - transform.position).normalized;

        // �����Ӹ��� �̵�
        while (true)
        {
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hit(); 
            Destroy(gameObject);
        }
    }
}
