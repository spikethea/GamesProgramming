using UnityEngine;

public class Bullet : MonoBehaviour
{
    BulletMagazine bulletMagazine;
    public Rigidbody Rigidbody;

    private void Start()
    {
        bulletMagazine = FindAnyObjectByType<BulletMagazine>();
        //Rigidbody.linearVelocity = transform.forward * Speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other != null) {
            NPC nPC = other.GetComponent<NPC>();
            if (nPC != null)
            {
                nPC.takeDamage(2);
                bulletMagazine.ReturnBullet(gameObject);
            }

            PlayerMotor player = other.GetComponent<PlayerMotor>();
            if (player != null)
            {
                player.takeDamage(1);
                bulletMagazine.ReturnBullet(gameObject);
            }
            
        }
    }

    private void OnEnable()
    {
        Invoke(nameof(ReturnToMagazine), 3f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void ReturnToMagazine()
    {
        bulletMagazine.ReturnBullet(gameObject);
    }
}
