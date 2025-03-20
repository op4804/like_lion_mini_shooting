using System.Collections;
using UnityEngine;

public class WolfEnemy : Enemy
{

    int upDown = -1;
    bool isAttack = false;
    void Start()
    {
        currentEnemyHP = 50; // TODO: 
        StartCoroutine(Attack());
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAttack)
        {
            Move();
        }
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

    IEnumerator Attack()
    {
        while(true)
        {
            yield return new WaitForSeconds(4.0f);
            isAttack = true;
            StartCoroutine(Rush());
        }
    }

    IEnumerator Rush()
    {
        for(int i = 0; i < 8; i++)
        {
            transform.Translate(Vector3.left);
            yield return new WaitForSeconds(0.02f);
        }
        GameObject leftClaw = Instantiate(ResourceManager.Instance.claw, this.transform.position, Quaternion.identity);
        GameObject rightClaw = Instantiate(ResourceManager.Instance.claw, this.transform.position, Quaternion.identity);
        rightClaw.GetComponent<SpriteRenderer>().flipX = true;
        StartCoroutine(RushBack());
    }
    IEnumerator RushBack()
    {
        for(int i = 0; i < 7; i++)
        {
            transform.Translate(Vector3.right);
            yield return new WaitForSeconds(0.02f);
        }
        isAttack = false;
    }
}
