using UnityEngine;

public class Test : MonoBehaviour
{
    public LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer.positionCount = 2; // 점 2개 설정
        lineRenderer.SetPosition(0, new Vector3(-2, 0, 0)); // 시작점
        lineRenderer.SetPosition(1, new Vector3(2, 0, 0)); // 끝점

    }
}