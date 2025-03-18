using UnityEngine;
using UnityEngine.UI;

public class BossHpbar : MonoBehaviour
{
    public Image currentHpImage;
    public BossStatus bossStatus;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentHpImage.fillAmount = bossStatus.getCurrentHp() / bossStatus.getMaxHp();
    }
}
