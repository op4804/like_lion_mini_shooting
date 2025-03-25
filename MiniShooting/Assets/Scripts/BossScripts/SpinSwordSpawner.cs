using System.Collections;
using UnityEngine;

public class SpinSwordSpawner : MonoBehaviour
{
    public GameObject spawn; // ����� �� ��ȯ�� ������Ʈ

    private void Start()
    {
        StartCoroutine(AutoDestroy());
    }

    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(1f); // 1�� ��ٸ�

        if (spawn != null)
        {
            Instantiate(spawn, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // �ڱ� �ڽ� �ı�
    }
}
