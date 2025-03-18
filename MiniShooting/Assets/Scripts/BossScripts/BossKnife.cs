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

    private void OnBecameInvisible()//화면에안보이면삭제
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)//플레이어랑 부딪히면 데미지주고 삭제
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hit(); //안에인자로 damage넣어줄것
            Destroy(gameObject);
        }
    }
}
