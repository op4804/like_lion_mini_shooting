using JetBrains.Annotations;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 5f;
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime*bulletSpeed);
    }
        private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit();

            // TODO: °üÅë?
            Destroy(gameObject);
        }
    }
}
