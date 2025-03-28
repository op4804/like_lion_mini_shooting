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
            Debug.Log("모든 검 파괴됨, 마법진도 사라짐");
            boss.GetComponent<BarrierAndSwordPattern>().StopPattern(); // 보스에게 알림
            Destroy(gameObject); // 마법진 파괴
        }
    }
}
