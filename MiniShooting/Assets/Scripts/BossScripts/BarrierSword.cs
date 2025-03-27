using UnityEngine;

public class BarrierSword : MonoBehaviour
{
    private bool hasStart = false;

    void Start()
    {
        InvokeRepeating(nameof(CheckSwordCount), 0f, 1f); // 1초마다 체크
    }

    void CheckSwordCount()
    {
        if (transform.GetComponent<Boss>().currentHp <= 0 && !hasStart)
        {
            hasStart = true;
            //보스한테 자신이 파괴 되었다는걸 알리고 스스로 파괴
        }
    }

}
