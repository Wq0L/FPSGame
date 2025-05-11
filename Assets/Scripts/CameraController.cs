using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerBody;
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float smoothTime = 0.05f;

    private Vector2 currentMouseDelta;
    private Vector2 currentRotation;
    private Vector2 rotationVelocity;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CameraRotation();
    }

    private void CameraRotation()
    {
         Vector2 mouseInput = InputManager.Instance.GetMouseDelta() * sensitivity;

        // Yumuşatma (smooth rotation)
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, mouseInput, ref rotationVelocity, smoothTime);

        // Yukarı-aşağı bakış (X rotation)
        currentRotation.x -= currentMouseDelta.y;
        currentRotation.x = Mathf.Clamp(currentRotation.x, -60f, 35f);

        // Kamerayı kendi ekseninde döndür (X ekseni)
        transform.localRotation = Quaternion.Euler(currentRotation.x, 0f, 0f);

        // Karakteri sağa sola döndür (Y ekseni)
        playerBody.Rotate(Vector3.up * currentMouseDelta.x);
    }
}
