using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float timeSinceLastShot;
    
    [SerializeField] private GunData gunData;

   private void Start() 
    {
        gunData.maxAmmo = 120;
        gunData.currentAmmo = gunData.magSize;
        gunData.lastBullets = 0;
        gunData.magSize = 30;
    }
    

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void StartReload()
    {
        if(InputManager.Instance.PlayerReloadThisFrame())
        {
            if(!gunData.reloading)
            {
                StartCoroutine(Reload());
        }
            }

    }


    private IEnumerator Reload() 
    {
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        if (gunData.currentAmmo == gunData.magSize || gunData.maxAmmo == 0)
        yield break;
        gunData.lastBullets = gunData.currentAmmo;
        gunData.currentAmmo = gunData.magSize;
        gunData.magSize -= gunData.lastBullets;
        gunData.maxAmmo -= gunData.magSize;
        gunData.magSize = 30;
        gunData.lastBullets = 0;

        gunData.reloading = false;

       
    }

    public void Shoot()
    {
      if(gunData.currentAmmo > 0)
        {
           if (CanShoot())
           {
                if(Physics.Raycast(transform.position, -transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                   
                    Debug.Log(hitInfo.transform.name);
                }
                Debug.DrawRay(transform.position, -transform.forward * gunData.maxDistance, Color.yellow, 999f);

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
           }
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (InputManager.Instance != null && InputManager.Instance.IsFiring())
        {
            Shoot(); // Burası çalışıyor mu?
        }
        if (!InputManager.Instance.IsFiring())
        {
            StartReload();
        }
    
    }


    private void OnGunShot()
    {
       
    }
}
