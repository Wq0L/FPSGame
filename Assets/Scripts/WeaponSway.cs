using Unity.Mathematics;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    [Header("Sway Settings")]
    [SerializeField] private float smooth; // Sway miktarı
    [SerializeField] private float swayMultiplier; // Sway miktarı
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Sway();
    }

    private void Sway()
    {
        // Fare hareketini al
        Vector2 mouseDelta = InputManager.Instance.GetMouseDelta() * swayMultiplier;
        
        // Sway miktarını hesapla
        Quaternion rotationX = Quaternion.AngleAxis(-mouseDelta.y, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseDelta.x, Vector3.up);
        Quaternion targetRotation = rotationX * rotationY;
        
      transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smooth);
    }
}
