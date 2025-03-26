using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public static UIManager Instance = null;

    [SerializeField]
    private StatusMenu statusMenu;

    [SerializeField]
    private UpgradeMenu upgradeMenu;

    [SerializeField]
    private SkillMenu skillMenu;

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
            upgradeMenu.ToggleMenu();
        }
    }

    public void ToggleStatusMenu()
    {
        if (IsToggle == "StatusMenu" || IsToggle == null)
        {
            statusMenu.ToggleMenu();
        }
    }

    public void ToggleSkillMenu()
    {
        if (IsToggle == "SkillMenu" || IsToggle == null)
        {
            skillMenu.ToggleMenu();
        }
    }

    //레벨, 경험치 표기
    public void ViewExp(float playerExp, int playerLevel)
    {
        expText.text = $"Level : {playerLevel} \nExp : {playerExp}";
    }
}
