//using Cinemachine;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public MeshCollider mesh;
    public MeshCollider blueMesh;

    [Header("Bullets")]
    public Bullet BulletPrefab;
    public Transform FirePoint;

    [Header("Reload")]
    public float ReloadTime = 1;
    public float ReloadTimer = 0;

    [Header("Audio")]
    public AudioSource Source;
    public AudioClip ShootingClip;

    //public CinemachineImpulseSource Impulse;

    public void Hide() 
    {
        mesh.enabled = false;
        blueMesh.enabled = false;
    }

    public void Show()
    {
        mesh.enabled = true;
        blueMesh.enabled = true;
    }

    private void Update()
    {
        if (!mesh.enabled)
            return;
        ReloadTimer -= Time.deltaTime;
        if (ReloadTimer > 0)
            return;


        if (Input.GetMouseButtonDown(0))
        {
            ReloadTimer = ReloadTime;

            Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            Source.PlayOneShot(ShootingClip);
            //Impulse.GenerateImpulse();
        }
        
    }
}
