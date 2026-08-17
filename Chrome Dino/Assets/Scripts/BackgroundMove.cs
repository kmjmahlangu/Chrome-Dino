using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    public float speed = 5f;
    public float backgroundWidth = 20f;

    void Update()
    {
        // Move background to the left
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        
        // move it back to the right
        if (transform.position.x <= -backgroundWidth)
        {
            transform.position += Vector3.right * backgroundWidth * 2f;
        }
    }
}