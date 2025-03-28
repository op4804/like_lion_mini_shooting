using System.Collections;
using UnityEngine;

public class WolfEnemy : Enemy
{

    int upDown = -1;
    bool isAttack = false;

    protected override void OnEnable() // ����� �Ǿ����� �ʱ�ȭ
    {
        base.OnEnable();
        currentEnemyHP = 50; // TODO: 
        StartCoroutine(Attack());
        isAttack = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (!isAttack)
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
        else if (transform.position.y < -2)
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
        while (true)
        {
            yield return new WaitForSeconds(4.0f);
            isAttack = true;
            StartCoroutine(Rush());
        }
    }

    IEnumerator Rush()
    {
        for (int i = 0; i < 8; i++)
        {
            transform.Translate(Vector3.left);
            yield return new WaitForSeconds(0.02f);
        }
        GameObject leftClaw = ResourceManager.Instance.Create("claw", this.transform.position);
        GameObject rightClaw = ResourceManager.Instance.Create("claw", this.transform.position);
        rightClaw.GetComponent<SpriteRenderer>().flipX = true;
        StartCoroutine(RushBack());
    }
    IEnumerator RushBack()
    {
        for (int i = 0; i < 7; i++)
        {
            transform.Translate(Vector3.right);
            yield return new WaitForSeconds(0.02f);
        }
        isAttack = false;
    }

    public override void Hit(float damage)
    {
        base.Hit(damage);
    }
}
