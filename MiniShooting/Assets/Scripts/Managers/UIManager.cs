using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public static UIManager Instance = null;

    private UpgradeMenu upgradeMenu;

    public Text expText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        PlayerHpBar.Instance.UpdateLife();
    }

    public void ToggleUpgradeMenu()
    {
        UpgradeMenu.Instance.ToggleMenu();
    }

    //레벨, 경험치 표기
    public void ViewExp(float playerExp, int playerLevel)
    {
        expText.text = $"Level : {playerLevel} \nExp : {playerExp}";
    }
}
