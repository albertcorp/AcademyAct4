using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;    // Velocidad de movimiento del jugador
    public float gravity = -9.81f; // Fuerza de gravedad
    public float jumpHeight = 1.5f; // Altura del salto

    [Header("Look Settings")]
    public float lookSensitivity = 2f; // Sensibilidad del mouse
    public Transform cameraTransform;  // Transform de la c�mara (para mirar arriba/abajo)


    private Vector2 moveInput;      // Entrada de movimiento (x, y)
    private Vector2 lookInput;      // Entrada de rotaci�n del mouse
    private Vector3 velocity;       // Velocidad acumulada del jugador
    private CharacterController controller; // Referencia al CharacterController

    private float xRotation = 0f;      // Control de la rotaci�n en el eje X (para la c�mara)

    private Animator animator;
    private bool isWalking = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Bloquea y oculta el cursor
        animator = GetComponent<Animator>();
    }

    // M�todo que recibe la entrada de movimiento desde el Input System
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void Update()
    {
        // Movimiento en el plano XZ basado en la entrada
        Vector3 moveDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);

        if (moveInput.x != 0 || moveInput.y != 0)
        {
            isWalking = true;
        }
        else 
        {
            isWalking = false;
        }

        animator.SetBool("Walk", isWalking);
        animator.SetFloat("Vertical", moveInput.y );
        animator.SetFloat("Horizontal", moveInput.x );

        // Aplicar gravedad
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Restablecer la velocidad vertical al tocar el suelo
        }

        velocity.y += gravity * Time.deltaTime; // Aplicar gravedad acumulativa
        controller.Move(velocity * Time.deltaTime); // Aplicar el movimiento vertical

        // Rotaci�n del jugador y c�mara
        Look();
    }

    private void Look()
    {
        float mouseX = lookInput.x * lookSensitivity;
        transform.Rotate(Vector3.up * mouseX);
    }
}
