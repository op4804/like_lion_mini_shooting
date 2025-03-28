using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;
using System;

public class oneEyeEliteEnemyBullet : MonoBehaviour
{
    private GameObject player;
    private GameManager gm;

    private void Start()
    {
        gm = GameManager.Instance;
        player = gm.player;
    }
    private void OnEnable()
    {
        SoundManager.instance.OneEyeEliteAttack(); // ����Ʈ ���� �� ȿ���� ���
        StartCoroutine(FollowPlayer());
    }

    private void Update()
    {
        DestroyOutOfBoundary();
    }
    internal void Init(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    IEnumerator FollowPlayer() // ������ ����
    {
        for (int i = 0; i < 180; i++)
        {
            yield return new WaitForFixedUpdate();
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 2f * Time.deltaTime);
        }
        SoundManager.instance.OneEyeEliteSpecial(); // SpecialAttack() ȿ���� ���
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

    private void DestroyOutOfBoundary()
    {
        if (transform.position.x > gm.maxBounds.x || transform.position.x < gm.minBounds.x
            || transform.position.y > gm.maxBounds.y || transform.position.y < gm.minBounds.y)
        {
            ResourceManager.Instance.Deactivate(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hit();
            ResourceManager.Instance.Deactivate(gameObject);
            SoundManager.instance.OneEyeEnemyAttack(); // ���Ÿ� ���� ��� ȿ����
        }
    }


}
