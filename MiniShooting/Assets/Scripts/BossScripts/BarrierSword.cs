using UnityEngine;

public class BarrierSword : MonoBehaviour
{

    void Start()
    {

    }
    private void Update()
    {
        CheckSwordCount();
    }
    void CheckSwordCount()
    {
        if (this.gameObject.GetComponent<Boss>().currentHp <= 0)
        {
            BarrierAndSwordPattern.swordCount--;
            Destroy(gameObject);
        }
    }

}
