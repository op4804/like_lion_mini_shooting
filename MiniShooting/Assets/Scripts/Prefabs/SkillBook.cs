using UnityEngine;

public class SkillBook : MonoBehaviour
{
    public float speed = 1.0f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //skill UI ¿ÀÇÂ
            UIManager.Instance.ToggleSkillMenu();
            Destroy(gameObject);
        }
    }
}
