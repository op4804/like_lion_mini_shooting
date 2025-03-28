using UnityEngine;

public class smallCloud : MonoBehaviour
{
    private GameManager gm;

    private float speed = 2.0f;
    private void Awake()
    {
        gm = GameManager.Instance;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
        DestroyOutOfBoundary();
    }

    private void DestroyOutOfBoundary()
    {
        if (transform.position.x > gm.maxBounds.x + 10f || transform.position.x < gm.minBounds.x - 10f
            || transform.position.y > gm.maxBounds.y || transform.position.y < gm.minBounds.y)
        {
            ResourceManager.Instance.Deactivate(gameObject);
        }
    }
}
