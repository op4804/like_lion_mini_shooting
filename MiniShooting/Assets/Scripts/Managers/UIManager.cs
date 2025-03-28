using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup fadePanel;

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

    public void ActivateEliteEnemyHpBar(EliteEnemy eliteEnemy)
    {
        eliteEnemyHpBar.gameObject.SetActive(true);
        eliteEnemyHpBar.GetComponent<EliteEnemyHpBar>().SetEliteEnemy(eliteEnemy);
    }
    public void DeactivateEliteEnemyHpBar()
    {
        eliteEnemyHpBar.gameObject.SetActive(false);
    }




    public IEnumerator FadeScreen(float from, float to, float duration)
    {
        CanvasGroup fade = fadePanel; // 검정 패널 연결돼 있어야 함
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            fade.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }
        fade.alpha = to;
    }
}


