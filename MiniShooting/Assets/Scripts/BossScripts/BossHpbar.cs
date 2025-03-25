using UnityEngine;
using UnityEngine.UI;

public class BossHpbar : MonoBehaviour
{
    public Image currentHpImage;
    public Boss boss;

    void Start()
    {
        
    }

    void Update()
    {
        currentHpImage.fillAmount = boss.currentHp / boss.maxHp;
    }
}
