using UnityEngine;

public class BossKnife : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    //[SerializeField]
    //private float damage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.left * Time.deltaTime * speed);
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * speed;
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
