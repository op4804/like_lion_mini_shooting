using UnityEngine;

public class BossKnife : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private Vector2 tempVelocity;
    //[SerializeField]
    //private float damage;

    void Start()
    {
        
    }

    void Update()
    {
        //transform.Translate(Vector2.left * Time.deltaTime * speed);
        //GetComponent<Rigidbody2D>().linearVelocity = Vector2.left * speed;

    }

    void TimeStop()
    {
        if (MoveShootPattern.timeStoped = true)
        {
            tempVelocity = GetComponent<Rigidbody2D>().linearVelocity;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
    void TimeResume()
    {
        if (MoveShootPattern.timeStoped = false)
        {
            GetComponent<Rigidbody2D>().linearVelocity = tempVelocity;
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
