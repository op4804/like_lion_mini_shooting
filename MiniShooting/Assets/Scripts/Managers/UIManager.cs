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

    [SerializeField]
    private GameObject eliteEnemyHpBar;

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
        ViewExp();
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
    public void ViewExp()
    {
        Debug.Log($"Level : {Player.Instance.GetPlayerLevel()}");
        expText.text = $"Level : {Player.Instance.GetPlayerLevel()}";
    }

    public void ActivateEliteEnemyHpBar(EliteEnemy eliteEnemy)
    {
        eliteEnemyHpBar.gameObject.SetActive(true);
        eliteEnemyHpBar.GetComponent<EliteEnemyHpBar>().SetEliteEnemy(eliteEnemy);
    }
    public void DeactivateEliteEnemyHpBar()
    {
        eliteEnemyHpBar.gameObject.SetActive(false);
    }
}
