using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PrologueManager : MonoBehaviour
{
    public GameObject confirmationPanel;
    public PlayableDirector timeline;

    private bool isPaused = false;
    private bool isPanelOpen = false;

    void Awake()
    {
        confirmationPanel.SetActive(false);
        timeline.stopped += TimelineEnd;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleConfirmationPanel();
        }

        if (isPanelOpen&&Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Main");
        }
    }

    void ToggleConfirmationPanel()
    {
        isPanelOpen = !isPanelOpen;
        confirmationPanel.SetActive(isPanelOpen);

        isPaused = isPanelOpen;

        if (isPanelOpen)
            timeline.Pause();
        else
            timeline.Play();
    }

    public void OnConfirmSkip()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnCancelSkip()
    {
        confirmationPanel.SetActive(false);
        isPaused = false;
        isPanelOpen = false;
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    private void TimelineEnd(PlayableDirector director)
    {
        if (!isPanelOpen && director == timeline) // 패널이 열려있지 않을 때만 씬 이동
        {
            SceneManager.LoadScene("Main");
        }
    }
}