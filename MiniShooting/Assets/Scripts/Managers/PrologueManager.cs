using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

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
                
        timeline.extrapolationMode = DirectorWrapMode.None;
    }

    //마지막 프레임으로 이동
    //void Start()
    //{
    //    timeline.time = timeline.duration;
    //    timeline.Play();
    //}

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpPart();
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
        Debug.Log("Timeline End Event Triggered");
        if (!isPanelOpen && director == timeline)
        {
            Debug.Log("Loading Main Scene...");
            SceneManager.LoadScene("Main");
        }
    }

    public void JumpToFrame(int frame)
    {
        TimelineAsset asset = timeline.playableAsset as TimelineAsset;

        double fps = asset.editorSettings.fps;
        timeline.time = frame / fps;
        timeline.Evaluate();
    }

    public void JumpPart()
    {
        TimelineAsset asset = timeline.playableAsset as TimelineAsset;
        double fps = asset.editorSettings.fps;
        int currentFrame = Mathf.FloorToInt((float)(timeline.time * fps));

        Debug.Log($"[JumpPart] currentTime: {timeline.time}, currentFrame: {currentFrame}");
        if (currentFrame < 420)
        {
            JumpToFrame(420);
        }
        else if (currentFrame < 840)
        {
            JumpToFrame(840);
        }
        else if (currentFrame < 1260)
        {
            JumpToFrame(1260);
        }
        else if (currentFrame < 2940)
        {
            JumpToFrame(2940);
        }
        else if (currentFrame < 3900)
        {
            JumpToFrame(3900);
        }
    }
}