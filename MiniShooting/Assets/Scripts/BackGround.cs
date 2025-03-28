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
    public GameObject Cloud1;
    public float speed = 5f;
    private bool FirstIsMain = true;

    private Vector3 startPos;
    private Vector3 waitPos;

    private Renderer rend1;
    private Renderer rend2;

    private int BgChangeCount = 0;

    void Start()
    {
        rend1 = FirstBackground.GetComponent<Renderer>();
        rend2 = SecondBackground.GetComponent<Renderer>();
        startPos = FirstBackground.transform.position;
        waitPos = SecondBackground.transform.position;
        BgInit(FirstBackground, SecondBackground);
    }

    void Update()
    {
        BgOffsetMove(rend1, rend2);
        //if문만 특정 조건에서 작동하도록 하면 됨.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(ChangeBgCo());
            StartCoroutine(MoveCloudToTarget());
        }
    }

    IEnumerator ChangeBgCo()
    {
        // 현재 FirstIsMain 값을 저장해두고, 상태가 바뀔 때까지 반복
        bool initialState = FirstIsMain;
        yield return new WaitForSeconds(3f);
        while (FirstIsMain == initialState)
        {
            BgPositionMove(FirstBackground, SecondBackground);
            yield return null;
        }
    }
    IEnumerator MoveCloudToTarget()
    {
        Vector3 originPos = Cloud.transform.position;
        Vector3 targetPos = Cloud1.transform.position;

        while (Vector3.Distance(Cloud.transform.position, targetPos) > 0.05f)
        {
            float newX = Mathf.MoveTowards(Cloud.transform.position.x, targetPos.x, speed * Time.deltaTime);
            float newY = Mathf.MoveTowards(Cloud.transform.position.y, targetPos.y, speed * Time.deltaTime);
            float newZ = Mathf.MoveTowards(Cloud.transform.position.z, targetPos.z, speed * Time.deltaTime);
            Cloud.transform.position = new Vector3(newX, newY, newZ);
            yield return null;
        }

        // 이동 완료 후 처리
        Cloud.transform.position = originPos;
    }

    public void ChangeMaterial(int index)
    {
        if (index >= 0 && index < materials.Length)
        {
            rend1.material = materials[index];
        }
    }

    public void BgOffsetMove(Renderer BG1, Renderer BG2)
    {
        if(FirstIsMain)
        {
            float newOffsetX = BG1.material.mainTextureOffset.x + scrollSpeed * Time.deltaTime;
            Vector2 newOffset = new Vector2(newOffsetX, 0);
            BG1.material.mainTextureOffset = newOffset;
        }
        else if (!FirstIsMain)
        {
            float newOffsetX = BG2.material.mainTextureOffset.x + scrollSpeed * Time.deltaTime;
            Vector2 newOffset = new Vector2(newOffsetX, 0);
            BG2.material.mainTextureOffset = newOffset;
        }
        else
        {
            Debug.Log("배경 오프셋 오류");
        }
    }

    public void BgPositionMove(GameObject BG1, GameObject BG2)
    {
        if (FirstIsMain)
        {
            //BG2 이동
            BG2.transform.position = Vector3.MoveTowards(BG2.transform.position, startPos, speed * Time.deltaTime);
            if (BG2.transform.position == startPos) 
            {
                FirstIsMain = false;
                BgWait(BG1);
            }
        }
        else if (!FirstIsMain)
        {
            //BG1 이동
            BG1.transform.position = Vector3.MoveTowards(BG1.transform.position, startPos, speed * Time.deltaTime);
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
        BgChange(BG);
    }

    public void BgInit(GameObject BG1, GameObject BG2)
    {
        BG1.GetComponent<Renderer>().material = materials[0];
        BG2.GetComponent<Renderer>().material = materials[1];
    }

    public void BgChange(GameObject BG)
    {
        Debug.Log(BgChangeCount);
        if (BgChangeCount < 5)
        {
            int change = BgChangeCount + 2;
            Debug.Log(change);
            BgChangeCount++;
            if (change == 5) change = 0;
            if (change == 6) change = 1;
            BG.GetComponent<Renderer>().material = materials[change];
        }
        else
        {
            BgChangeCount = 0;
        }
    }
}
