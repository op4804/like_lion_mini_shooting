using UnityEngine;

public class SkillBook : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
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
