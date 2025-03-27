using UnityEngine;
using UnityEngine.UI;

public class BossHpbar : MonoBehaviour
{
    public Image currentHpImage;
    public GameObject go;

    void Start()
    {
        
    }

    void Update()
    {
        currentHpImage.fillAmount = go.GetComponent<Boss>().currentHp / go.GetComponent<Boss>().maxHp;
    }
}
