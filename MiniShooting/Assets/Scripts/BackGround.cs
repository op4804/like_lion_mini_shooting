using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed = 0.05f; //화면 흐르는 속도
    public Material[] materials;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangeMaterial(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ChangeMaterial(1);

        float newOffsetX = rend.material.mainTextureOffset.x + scrollSpeed * Time.deltaTime;

        Vector2 newOffset = new Vector2(newOffsetX, 0);

        rend.material.mainTextureOffset = newOffset;
    }

    public void ChangeMaterial(int index)
    {
        if (index >= 0 && index < materials.Length)
        {
            rend.material = materials[index];
        }
    }
}
