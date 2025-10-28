using UnityEngine;

public class MouseLookFPS : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // arraste aqui o Player (objeto com PlayerMovementFPS)

    [Header("Limites de rotação vertical")]
    public float minX = -70f; // olhar pra cima
    public float maxX = 40f;  // olhar pra baixo

    private float xRotation = 0f;

    void Start()
    {
        // Trava o cursor no centro da tela e esconde
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Movimentos do mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotação vertical
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minX, maxX); // Limita vertical

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotação horizontal do player
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
