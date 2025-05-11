using System;
using System.Collections;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float fireRate = 0.1f;
    private float lastFireTime;

    [Header("Effects")]
    [SerializeField] private GameObject hitEffectPrefab;     // Vurulan yere efekt (çarpma efekti)
    [SerializeField] private LineRenderer bulletLine;        // Geçici çizgi efekti
    [SerializeField] private AudioSource fireAudio;          // M4 ses efekti

    private void Awake()
    {
        // Layer'ları buradan istersen dinamik ayarlarsın
        // layerMask = LayerMask.GetMask("Enemy", "Wall");
    }

    void Update()
    {
        if (InputManager.Instance.IsFiring() && Time.time >= lastFireTime + fireRate)
        {
            Shoot();
            lastFireTime = Time.time;
            PlayFireSound();
        }
        else if (!InputManager.Instance.IsFiring())
        {
            StopFireSound(); // tuş bırakılınca durdur
        }
    }

    private void Shoot()
    {

        RaycastHit hit;
        Vector3 direction = bulletSpawnPoint.forward;

        if (Physics.Raycast(bulletSpawnPoint.position, direction, out hit, 100f, layerMask))
        {
            Debug.DrawRay(bulletSpawnPoint.position, direction * hit.distance, Color.yellow, 1f);
            CreateHitEffect(hit.point, hit.normal);
            StartCoroutine(ShowBulletTrail(bulletSpawnPoint.position, hit.point));
        }
        else
        {
            Debug.DrawRay(bulletSpawnPoint.position, direction * 100f, Color.white, 1f);
            StartCoroutine(ShowBulletTrail(bulletSpawnPoint.position, bulletSpawnPoint.position + direction * 100f));
        }
    }

    private void CreateHitEffect(Vector3 point, Vector3 normal)
    {
        if (hitEffectPrefab == null) return;
        GameObject effect = Instantiate(hitEffectPrefab, point, Quaternion.LookRotation(normal));
        Destroy(effect, 1f);
    }

    private void PlayFireSound()
    {
        if (fireAudio != null && !fireAudio.isPlaying)
        {
            fireAudio.Play();
        }
    }
    private void StopFireSound()
{
    if (fireAudio != null && fireAudio.isPlaying)
    {
        fireAudio.Stop();
    }
}

    private IEnumerator ShowBulletTrail(Vector3 start, Vector3 end)
    {
        if (bulletLine == null) yield break;

        bulletLine.SetPosition(0, start);
        bulletLine.SetPosition(1, end);
        bulletLine.enabled = true;
        yield return new WaitForSeconds(0.05f);
        bulletLine.enabled = false;
    }
}
