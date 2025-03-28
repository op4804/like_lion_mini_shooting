using System.Collections;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    public int totalSwords;
    public int destroyedSwords;
    public GameObject boss;

    void Awake()
    {
        totalSwords = 4;
        destroyedSwords = 0;
    }

    public void NotifySwordDestroyed()
    {
        destroyedSwords++;
        if (destroyedSwords >= totalSwords)
        {
            Debug.Log("��� �� �ı���, �������� �����");
            boss.GetComponent<BarrierAndSwordPattern>().StopPattern(); // �������� �˸�
            Destroy(gameObject); // ������ �ı�
        }
    }
}
