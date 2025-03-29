using UnityEngine;
using UnityEngine.UI;

public class BarrierSwordHpbar : MonoBehaviour
{
    public Image currentHpImage;
    public GameObject go;

    void Start()
    {     
    }
    void Update()
    {
        currentHpImage.fillAmount = go.GetComponent<BarrierSword>().GetCurrentHp() / go.GetComponent<BarrierSword>().GetMaxHp();
    }


}
