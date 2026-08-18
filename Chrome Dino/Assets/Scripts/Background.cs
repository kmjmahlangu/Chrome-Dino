using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Background : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    [SerializeField] private float parallaxSpeed = 0.3f;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
{
    float speed = GameManager.Instance.gameSpeed
                  * parallaxSpeed
                  / transform.localScale.x;

    meshRenderer.material.mainTextureOffset +=
        speed * Time.deltaTime * Vector2.right;
}
}