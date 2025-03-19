using UnityEngine;

public class StatusMenu : MonoBehaviour
{
    public GameObject playerImg;
    public GameObject statusPanel;

    public bool isMenuActive = false;

    private float startX = -160f;

    void Start()
    {
        statusPanel.SetActive(false);
        ViewPlayer();
    }

    public void ToggleMenu()
    {
        isMenuActive = !isMenuActive;

        statusPanel.SetActive(isMenuActive);

        if (isMenuActive)
        {
            Time.timeScale = 0f;
            UIManager.Instance.IsToggle = "StatusMenu";
        }
        else
        {
            Time.timeScale = 1f;
            UIManager.Instance.IsToggle = null;
        }

    }

    public void ViewPlayer()
    {
        playerImg.GetComponent<RectTransform>().anchoredPosition = new Vector2(startX, 0);
    }

    public void ViewStatus()
    {

    }
}
