using System;
using System.Collections;
using UnityEngine;

public class SpinSword : Knife
{
    public float rotationSpeed = 1800f; // ȸ���ӵ�
    private void Start()
    {
        StartCoroutine(AutoDestroy());
    }
    void Update()
    {
        // Z���� �������� ȸ�� (2D���)
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
    IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(1f); // 1�� ��ٸ�

        Destroy(gameObject); // �ڱ� �ڽ� �ı�
    }

}
