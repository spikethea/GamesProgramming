//using Cinemachine;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public MeshRenderer renderer;
    public MeshRenderer blueRenderer;

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


        if (Input.GetMouseButtonDown(0))
        {
            ReloadTimer = ReloadTime;

            Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            Source.PlayOneShot(ShootingClip);
            //Impulse.GenerateImpulse();
        }
        
    }
}
