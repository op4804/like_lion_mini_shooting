using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 0.05f; //ȭ�� �帣�� �ӵ�

    Material backgroundMaterial;

    void Start()
    {
        backgroundMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float newOffsetX = backgroundMaterial.mainTextureOffset.x + scrollSpeed * Time.deltaTime;

        Vector2 newOffset = new Vector2(newOffsetX, 0);

        backgroundMaterial.mainTextureOffset = newOffset;
    }
}
