using UnityEngine;

public class BackGorund : MonoBehaviour
{
    private float scrollSpeed = 0.05f; //화면 흐르는 속도

    Material backGroundMaterial;

    void Start()
    {
        backGroundMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float newOffsetX = backGroundMaterial.mainTextureOffset.x + scrollSpeed * Time.deltaTime;

        Vector2 newOffset = new Vector2(newOffsetX, 0);

        backGroundMaterial.mainTextureOffset = newOffset;
    }
}
