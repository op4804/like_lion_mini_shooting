using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;


    [Header("이동칼던지기")]

    [Header("돌진")]
    [Header("")]
    [Header("")]
    public float fireRate = 0.1f;
    public float spiralSpeed = 10f;
    public float bulletSpeed = 5f;
    [Header("")]
    private float timer;
    private float angle;
    void start()
    {
        PatternMove patternmove = GetComponent<PatternMove>();
        //PatternMove = GetComponent<PatternMove>();
        //PatternMove = GetComponent<PatternMove>();
        //PatternMove = GetComponent<PatternMove>();
        //PatternMove = GetComponent<PatternMove>();
    }
    void Update()
    {
        
    }

    //void Pattern1()
    //{
    //    timer += Time.deltaTime;
    //    if (timer >= fireRate)
    //    {
    //        timer = 0;

    //        for (int i = 0; i < 2; i++)
    //        {
    //            float shootAngle = angle + i * 180;
    //            SpawnBullet(shootAngle);
    //        }
    //        angle += spiralSpeed;
    //    }
    //}
    //void SpawnBullet(float degree)//
    //{
    //    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, degree));
    //    float rad = degree * Mathf.Deg2Rad;
    //    Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    //    bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * bulletSpeed;
    //}spiral pattern

}
