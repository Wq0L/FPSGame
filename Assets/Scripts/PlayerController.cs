using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Example : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]private float playerSpeed = 2.0f;
    [SerializeField]private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private InputManager inputManager; 
    [SerializeField] private Transform cameraTransform; // Main Camera'yı sürükle

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        inputManager = InputManager.Instance; 
    }

    void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Giriş vektörü al
        Vector2 movement = inputManager.GetMovementInput(); 

        // Kameraya göre yönleri hesapla
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // Kamera yönüne göre hareket vektörü oluştur
        Vector3 move = camForward * movement.y + camRight * movement.x;

        // Hareketi kısıtla
        move = Vector3.ClampMagnitude(move, 1f);

        // Zıplama
        if (inputManager.PlayerJumpedThisFrame() && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        // Yer çekimi
        playerVelocity.y += gravityValue * Time.deltaTime;

        // Son hareketi hesapla ve uygula
        Vector3 finalMove = (move * playerSpeed) + (playerVelocity.y * Vector3.up);
        controller.Move(finalMove * Time.deltaTime);
    }
}
