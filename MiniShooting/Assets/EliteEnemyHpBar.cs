using UnityEngine;
using UnityEngine.UI;

public class EliteEnemyHpBar : MonoBehaviour
{
    public Image currentHpImage;
    private EliteEnemy em;

    void Start()
    {

    }

    void Update()
    {
        if (em != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(em.transform.position + new Vector3(0, 2f, 0));
            currentHpImage.fillAmount = em.GetHpRatio();
        }
    }
    public void SetEliteEnemy(EliteEnemy em)
    {
        this.em = em;
    }
}




