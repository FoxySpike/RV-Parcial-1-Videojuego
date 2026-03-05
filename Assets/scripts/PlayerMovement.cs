using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Ajustes de Movimiento")]
    public float speed = 5f;
    public float rotationSpeed = 10f;

    [Header("Física Manual")]
    public float gravity = -9.81f; // Fuerza de gravedad estándar
    private Vector3 velocity;      // Para guardar la caída acumulada

    private Vector2 moveInput;
    private CharacterController controller;

    void Awake() => controller = GetComponent<CharacterController>();

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void Update()
    {
        // 1. Gravedad: Si está en el suelo, reseteamos la fuerza de caída
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Un pequeño empuje hacia abajo para mantenerlo pegado
        }

        // 2. Movimiento Horizontal
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(move * Time.deltaTime * speed);

        // 3. Aplicar Gravedad Constantemente
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); // Este segundo Move aplica la caída

        // 4. Rotación Suavizada
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}