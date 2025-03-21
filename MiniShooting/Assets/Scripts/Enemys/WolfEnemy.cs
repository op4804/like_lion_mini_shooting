using System.Collections;
using UnityEngine;

public class WolfEnemy : Enemy
{

    int upDown = -1;
    bool isAttack = false;

    GameManager gm;
    void Start()
    {
        gm = GameManager.Instance;
    }

    private void OnEnable() // 재생성 되었을때 초기화
    {
        currentEnemyHP = 50; // TODO: 
        StartCoroutine(Attack());
        isAttack = false;
    }


    // Update is called once per frame
    void Update()
    {
        if(!isAttack)
        {
            Move();
            DestroyOutOfBoundary();
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

    private void DestroyOutOfBoundary()
    {
        if (transform.position.x > gm.maxBounds.x || transform.position.x < gm.minBounds.x
            || transform.position.y > gm.maxBounds.y || transform.position.y < gm.minBounds.y)
        {
            ResourceManager.Instance.Deactivate(gameObject);
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
