using UnityEngine;

public class Bullet : MonoBehaviour
{

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime);
    }

    // TODO: 화면 밖으로 나가면 사라지기. 혹은 일정 시간 뒤에 사라지기.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit();


            // TODO: 관통?
            Destroy(gameObject);
        }
    }
}
