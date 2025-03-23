using UnityEngine;

public class Frag : MonoBehaviour
{
    private float fragSpeed;
    private float fragDuration;
    private float fragDamage;
    private Vector2 dir;
    private void OnEnable()
    {
        Destroy(gameObject, fragDuration);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * fragSpeed * Time.deltaTime);
    }

    public void SetDir(Vector2 dir)
    {
        this.dir = dir;
    }
    public void SetDefaultValue(float fragSpeed, float fragDuration, float fragDamage)
    {
        this.fragSpeed = fragSpeed;
        this.fragDuration = fragDuration;
        this.fragDamage = fragDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit(fragDamage);
            // ResourceManager.Instance.Deactivate(gameObject);
        }
    }
}
