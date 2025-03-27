using UnityEngine;

public class BarrierSword : MonoBehaviour
{
    private bool hasStart = false;

    void Start()
    {
        InvokeRepeating(nameof(CheckSwordCount), 0f, 1f); // 1�ʸ��� üũ
    }

    void CheckSwordCount()
    {
        if (transform.GetComponent<Boss>().currentHp <= 0 && !hasStart)
        {
            hasStart = true;
            //�������� �ڽ��� �ı� �Ǿ��ٴ°� �˸��� ������ �ı�
        }
    }

}
