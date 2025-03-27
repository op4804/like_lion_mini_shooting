using System.Collections;
using UnityEngine;

public class Knife : MonoBehaviour
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

    }
    void RotateTowardsPlayer()
    {
        Vector2 direction = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f; //앞방향이 오른쪽이 아니라 위라서 90도 빼줌
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    IEnumerator Move(Transform playerTransform, float moveSpeed)
    {
        Vector2 dir = (playerTransform.position - transform.position).normalized;

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
