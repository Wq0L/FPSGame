using System;
using UnityEngine;
using UnityEngine.InputSystem; // Import the Unity Input System namespace

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput; 

    private static InputManager _instance;
    public static InputManager Instance{ get { return _instance; } }
   
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject); 
            
        }
        else
        {
            _instance = this;
        }
        playerInput = new PlayerInput(); 
    }
    

    
    void Update()
    {

    }

    private void FixedUpdate()
    {
        
    }

    private void OnEnable()
    {
      playerInput.Enable(); 
    }
    private void OnDisable()
    {
       playerInput.Disable();
    }

    public Vector2 GetMovementInput()
    {
        return playerInput.Player.Movement.ReadValue<Vector2>();
    }
     public Vector2 GetMouseDelta()
    {
        return playerInput.Player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumpedThisFrame()
    {
        return playerInput.Player.Jump.triggered;
    }

    public bool IsFiring()
    {
        return playerInput.Player.Fire.ReadValue<float>() > 0.1f;
    }
    
    public bool PlayerReloadThisFrame()
    {
        return playerInput.Player.Reload.triggered;
    }
    

}
