using System.Collections;
using UnityEngine;

public class EnemyClaw : MonoBehaviour
{
    float duration = 1.0f;

    private void OnEnable()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(1, 1, 1); // 색상 초기화
        gameObject.GetComponent<SpriteRenderer>().flipX = false; // 좌우 반전 초기화

        StartCoroutine(FadeOut());
        StartCoroutine(Delete());
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().Hit();
        }
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(duration);
        ResourceManager.Instance.Deactivate(gameObject);
    }

    IEnumerator FadeOut()
    {
        float alpha = 1.0f;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, alpha);
            alpha -= Time.deltaTime * 20;
        }
    }

}
