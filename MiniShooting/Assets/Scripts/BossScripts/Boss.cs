using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;

    public float fireRate = 0.1f;
    public float spiralSpeed = 10f;
    public float bulletSpeed = 5f;

    private float timer;
    private float angle;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            timer = 0;

            for (int i = 0; i < 2; i++)
            {
                float shootAngle = angle + i * 180;
                SpawnBullet(shootAngle);
            }
            angle += spiralSpeed; 
        }
    }

    void SpawnBullet(float degree)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        float rad = degree * Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
        bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * bulletSpeed;
    }


}
