//using Cinemachine;
using UnityEngine;

public class Gun : MonoBehaviour
{
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

    private void Update()
    {
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
