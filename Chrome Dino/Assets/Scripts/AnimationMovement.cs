using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class AnimationMovement : MonoBehaviour
{
    public Animator animator;

    [Header("Jump Animation")]
    public float jumpAnimationDelay = 0.2f;
    public float jumpAnimationDuration = 0.8f;

    private bool isJumping = false;

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame && !isJumping)
        {
            StartCoroutine(PlayJumpAnimation());
        }
    }

    IEnumerator PlayJumpAnimation()
    {
        isJumping = true;

        
        yield return new WaitForSeconds(jumpAnimationDelay);

        animator.SetBool("IsJumping", true);

        
        yield return new WaitForSeconds(jumpAnimationDuration);

      
        animator.SetBool("IsJumping", false);

        isJumping = false;
    }
}