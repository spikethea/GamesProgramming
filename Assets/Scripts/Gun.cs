//using Cinemachine;

using UnityEngine.InputSystem;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject Player;
    [Header("Bullets")]
    public BulletMagazine bulletMagazine;
    public float bulletSpeed = 20f;
    public float gunshotSoundRadius = 20f;
    public Transform firePoint;

    [Header("Input")]
    private InputManager inputManager;
    public PlayerInput.OnFootActions OnFoot;
    
    public MeshRenderer MeshRenderer;
    public MeshRenderer blueRenderer;
    
    
    

    [Header("Reload")]
    public float ReloadTime = 1;
    public float ReloadTimer = 0;

    [Header("Audio")]
    public AudioSource Source;
    public AudioClip ShootingClip;

    public void Start()
    {
        ReloadTimer = 0;

        inputManager = Player.GetComponent<InputManager>();

        OnFoot = inputManager.onFoot;
    }

    //public CinemachineImpulseSource Impulse;
    public void Hide() 
    {
        MeshRenderer.enabled = false;
        blueRenderer.enabled = false;
    }

    public void Show()
    {
        MeshRenderer.enabled = true;
        blueRenderer.enabled = true;
    }

    private void Update()
    {
        if (!MeshRenderer.enabled)
            return;
        ReloadTimer -= Time.deltaTime;
        if (ReloadTimer > 0)
            return;

        if (OnFoot.Shoot.IsPressed())
        {
            Fire();
            AlertNearbyNPCs();
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

    void AlertNearbyNPCs()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, gunshotSoundRadius);

        foreach (Collider hit in hits)
        {
            NPC npc = hit.GetComponent<NPC>();
            if (npc != null)
            {
                npc.OnSoundHeard();
            }
        }
    }

}