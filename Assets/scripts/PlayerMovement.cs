using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float speed = 5f;
    public float rotationSpeed = 10f;

    [Header("Salto")]
    public float jumpForce = 1.5f;

    [Header("Física Manual")]
    public float gravity = -9.81f;
    private Vector3 velocity;

    private Vector2 moveInput;
    private CharacterController controller;

    private Animator animator;

    public float moveAmount;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void SetAnimator(Animator anim)
    {
        animator = anim;
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        // ESTO TE DIRÁ SI EL TECLADO RESPONDE
        //Debug.Log("Moviendo: " + moveInput);
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    void Update()
    {
        if (moveInput != Vector2.zero) //Debug.Log("Detectando teclas: " + moveInput);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        moveAmount = moveInput.magnitude;

        if (animator != null)
        {
            animator.SetFloat("Speed", moveAmount);
            animator.SetBool("Grounded", controller.isGrounded);
            animator.SetFloat("Yvelocity", velocity.y);
        }
    }
}