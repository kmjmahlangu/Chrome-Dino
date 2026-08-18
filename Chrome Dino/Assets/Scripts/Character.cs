using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;

    [SerializeField] private GameObject shieldVisual;
[SerializeField] private GameObject doubleScoreVisual;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += gravity * Time.deltaTime * Vector3.down;

        if (character.isGrounded)
        {
            direction = Vector3.down;
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                direction = Vector3.up * jumpForce;
            }
        }

        character.Move(direction * Time.deltaTime);
    }

   private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Obstacle"))
    {
        if (GameManager.Instance.UseShield())
        {
            return;
        }

        GameManager.Instance.GameOver();
    }
}
public void ShowShield()
{
    shieldVisual.SetActive(true);
}

public void HideShield()
{
    shieldVisual.SetActive(false);
}

public void ShowDoubleScore()
{
    doubleScoreVisual.SetActive(true);
}

public void HideDoubleScore()
{
    doubleScoreVisual.SetActive(false);
}

}
