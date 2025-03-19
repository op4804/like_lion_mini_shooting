using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineManager : MonoBehaviour
{
    public PlayableDirector timeline; // Ÿ�Ӷ��� ����

    void Start()
    {
        timeline.stopped += OnTimelineEnd; // Ÿ�Ӷ��� ���� ����
    }

    void OnTimelineEnd(PlayableDirector director)
    {
        if (director == timeline) // ���� Ÿ�Ӷ����� �����ٸ�
        {
            SceneManager.LoadScene("Main"); // "GameScene"���� ��ȯ
        }
    }
}
