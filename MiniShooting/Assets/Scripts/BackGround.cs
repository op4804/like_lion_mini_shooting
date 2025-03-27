using System.Collections;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 0.05f; //화면 흐르는 속도
    public Material[] materials;
    public GameObject FirstBackground;
    public GameObject SecondBackground;
    public GameObject Cloud;
    private const float StartX = -6f;
    private const float WaitX = 13f;
    private bool FirstIsMain = true;

    private Vector3 startPos;
    private Vector3 waitPos;

    private Renderer rend1;
    private Renderer rend2;

    private int index;

    void Start()
    {
        rend1 = FirstBackground.GetComponent<Renderer>();
        rend2 = SecondBackground.GetComponent<Renderer>();
        startPos = FirstBackground.transform.position;
        waitPos = SecondBackground.transform.position;
    }

    void Update()
    {
        BgOffseMove(rend1);
        BgOffseMove(rend2);
        if (Input.GetKeyDown(KeyCode.Alpha1)) StartCoroutine("ChangeCoroutine");
    }

    IEnumerator ChangeCoroutine()
    {
        // 현재 FirstIsMain 값을 저장해두고, 상태가 바뀔 때까지 반복
        bool initialState = FirstIsMain;
        while (FirstIsMain == initialState)
        {
            BgPositionMove(FirstBackground, SecondBackground);
            yield return null;
        }
    }

    public void ChangeMaterial(int index)
    {
        if (index >= 0 && index < materials.Length)
        {
            rend1.material = materials[index];
        }
    }

    public void BgOffseMove(Renderer BG)
    {
        float newOffsetX = BG.material.mainTextureOffset.x + scrollSpeed * Time.deltaTime;
        Vector2 newOffset = new Vector2(newOffsetX, 0);
        BG.material.mainTextureOffset = newOffset;
    }

    public void BgPositionMove(GameObject BG1, GameObject BG2)
    {
        if (FirstIsMain)
        {
            //BG2 이동
            BG2.transform.position = Vector3.MoveTowards(BG2.transform.position, startPos, 5f * Time.deltaTime);
            if (BG2.transform.position == startPos) 
            {
                FirstIsMain = false;
                BgWait(BG1);
            }
        }
        else if (!FirstIsMain)
        {
            //BG1 이동
            BG1.transform.position = Vector3.MoveTowards(BG1.transform.position, startPos, 5f * Time.deltaTime);
            if (BG1.transform.position == startPos)
            {
                FirstIsMain = true;
                BgWait(BG2);
            }
        }
    }

    public void BgWait(GameObject BG)
    {
        BG.transform.position = waitPos;
    }
}
