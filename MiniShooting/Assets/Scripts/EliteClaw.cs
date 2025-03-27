using System.Collections;
using UnityEngine;

public class EliteClaw : MonoBehaviour
{

    Vector3 originScale;

    float randomRange = 0.8f;


    private void Awake()
    {
        originScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = originScale;
        StartCoroutine(Enlarge());
    }

    IEnumerator Enlarge()
    {
        Vector3 tempScale = new Vector3(0.5f, 0.5f, 0.5f);
        while (2.5f > transform.localScale.x)
        {
            yield return new WaitForFixedUpdate();
            transform.localScale = tempScale;
            tempScale += new Vector3(0.1f, 0.1f, 0.1f) * Time.deltaTime * 8f;
        }
        StartCoroutine(CreateClaws());
        ResourceManager.Instance.Deactivate(gameObject, 0.1f);
    }

    IEnumerator CreateClaws()
    {
        for (int i = 0; i < 4; i++)
        {
            float randX = Random.Range(-randomRange, randomRange);
            float randY = Random.Range(-randomRange, randomRange);
            GameObject go = Instantiate(ResourceManager.Instance.claw, transform.position + new Vector3(randX, randY, 0), Quaternion.identity);
            go.GetComponent<SpriteRenderer>().color = new Color(0.57f, 0.74f, 1, 1);
            if (i % 2 == 0)
            {
                go.GetComponent<SpriteRenderer>().flipX = true;
            }
            yield return new WaitForSeconds(0);
        }
    }
}
