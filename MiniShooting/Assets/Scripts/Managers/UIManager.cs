using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public static UIManager Instance = null;

    [SerializeField]
    private StatusMenu statusMenu;

    public string IsToggle { get; set; } = null;

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
        if (IsToggle == "UpgradeMenu" || IsToggle == null)
        {
            UpgradeMenu.Instance.ToggleMenu();
        }
    }

    public void ToggleStatusMenu()
    {
        if (IsToggle == "StatusMenu" || IsToggle == null)
        {
            statusMenu.ToggleMenu();
        }
    }

    //����, ����ġ ǥ��
    public void ViewExp(float playerExp, int playerLevel)
    {
        expText.text = $"Level : {playerLevel} \nExp : {playerExp}";
    }
}
