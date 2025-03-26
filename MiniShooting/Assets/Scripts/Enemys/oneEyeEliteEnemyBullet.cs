using UnityEngine;
using System.Collections;

public class oneEyeEliteEnemyBullet : MonoBehaviour
{

    private void OnEnable()
    {
        StartCoroutine(MoveFoward());
    }

    Vector3 dir;

    float angle;


    public void Setdir(Vector3 direction)
    {
        this.dir = direction;
    }

    public void SetRot(float angle)
    {
        this.angle = angle;
    }

    IEnumerator MoveFoward() // 앞으로 가기
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForFixedUpdate();
            transform.Translate(Vector2.left * Time.deltaTime * 2f);
        }

        transform.rotation = Quaternion.Euler(0, 0, angle);
        StartCoroutine(SpecialAttack());
    }

    IEnumerator SpecialAttack()
    {

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForFixedUpdate();
            transform.Translate(Vector3.left * Time.deltaTime * 2f);
        }

        Destroy(gameObject);
    }
}
