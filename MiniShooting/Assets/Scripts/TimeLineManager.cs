using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineManager : MonoBehaviour
{
    public PlayableDirector timeline; // 타임라인 연결

    void Start()
    {
        timeline.stopped += OnTimelineEnd; // 타임라인 종료 감지
    }

    void OnTimelineEnd(PlayableDirector director)
    {
        if (director == timeline) // 현재 타임라인이 끝났다면
        {
            SceneManager.LoadScene("Main"); // "GameScene"으로 전환
        }
    }
}
