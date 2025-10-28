using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementFPS : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;
    public Animator animator;

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Garante que a animação comece em Idle
        if (animator != null)
            animator.SetFloat("Speed", 0f);
    }

    void Update()
    {
        // Input clássico
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Movimento relativo ao player
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        // Aplica movimento horizontal
        controller.Move(move * speed * Time.deltaTime);

        // Gravidade constante
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
            velocity.y = 0f;

        // Calcula magnitude baseado no input real
        float moveSpeed = new Vector3(horizontal, 0, vertical).magnitude;

        // Threshold para evitar "andar sozinho"
        if (moveSpeed < 0.05f)
            moveSpeed = 0f;

        // Atualiza Animator
        if (animator != null)
            animator.SetFloat("Speed", moveSpeed);
    }
}
