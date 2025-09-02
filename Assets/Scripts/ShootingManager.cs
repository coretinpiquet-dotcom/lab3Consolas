using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingManager : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;

    private float nextFireTime = 0f;
    private bool shootInput;

    public void onShoot(InputAction.CallbackContext context)
    {
        if (Time.time >= nextFireTime)
        {
            FireMissile();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Update()
    {
        // if (shootInput && Time.time >= nextFireTime)
        // {
        //    FireMissile();
        //    nextFireTime = Time.time + fireRate;
        // }
    }

    private void FireMissile()
    {
        if (missilePrefab != null && firePoint != null)
        {
            Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}