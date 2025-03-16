using UnityEngine;

public class ExperienceParticle : MonoBehaviour
{

    private float expAmount = 0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetExpAmount(float amount)
    {
        expAmount = amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().GetExpParticle(expAmount);
            Destroy(gameObject);
        }
    }
}
