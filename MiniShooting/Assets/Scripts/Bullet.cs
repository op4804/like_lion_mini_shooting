using UnityEngine;

public class Bullet : MonoBehaviour
{

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime);
    }

    // TODO: ȭ�� ������ ������ �������. Ȥ�� ���� �ð� �ڿ� �������.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit();


            // TODO: ����?
            Destroy(gameObject);
        }
    }
}
