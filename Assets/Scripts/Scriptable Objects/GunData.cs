using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("info")]
    public new string name;
    [Header("Shooting")]
    public float damage;
    public float maxDistance;

    [Header("reloading")]
    public int currentAmmo;
    public int maxAmmo;
    public int magSize;
    public int lastBullets;
    public float fireRate;
    public float reloadTime;
    [HideInInspector]
    public bool reloading;

        
    
}
