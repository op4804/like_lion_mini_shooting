using UnityEngine;

public class WolfEnemy : Enemy
{

    int upDown = -1;
    void Start()
    {
        currentEnemyHP = 50; // TODO: 
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.left * Time.deltaTime);
        transform.Translate(Vector3.up * upDown * Time.deltaTime);
        if (transform.position.y > 2)
        {
            upDown *= -1;
        }
        else if(transform.position.y < -2)
        {
            upDown *= -1;
        }
    }
}
