//using Cinemachine;

using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public MeshRenderer renderer;
    public MeshRenderer blueRenderer;
    public PlayerInput inputManager;
    private PlayerInput.OnFootActions OnFoot;

    [Header("Bullets")]
    public BulletMagazine bulletMagazine;
    public float bulletSpeed = 20f;
    public Transform firePoint;
    

    [Header("Reload")]
    public float ReloadTime = 1;
    public float ReloadTimer = 0;

    [Header("Audio")]
    public AudioSource Source;
    public AudioClip ShootingClip;

    public void Awake()
    {
        OnFoot = inputManager.OnFoot;
    }

    //public CinemachineImpulseSource Impulse;
    public void Hide() 
    {
        renderer.enabled = false;
        blueRenderer.enabled = false;
    }

    public void Show()
    {
        renderer.enabled = true;
        blueRenderer.enabled = true;
    }

    private void Update()
    {
        if (!renderer.enabled)
            return;
        ReloadTimer -= Time.deltaTime;
        if (ReloadTimer > 0)
            return;

        if (OnFoot.Shoot.IsPressed())
        {
            Fire();
        }
    }

    void Fire()
    {
        GameObject bullet = bulletMagazine.GetBullet();
        if (bullet == null) return;

        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = firePoint.forward * bulletSpeed;
    }
    
}