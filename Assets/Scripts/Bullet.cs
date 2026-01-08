using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody Rigidbody;

    [Range(0, 100)]
    public float Speed = 10;

    private void Start()
    {
        Rigidbody.linearVelocity = transform.forward * Speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other != null) {
            NPC nPC = other.GetComponent<NPC>();
            if (nPC != null)
            {
                nPC.takeDamage(2);
                Destroy(gameObject);
            }

            PlayerMotor player = other.GetComponent<PlayerMotor>();
            if (player != null)
            {
                player.takeDamage(1);
                Destroy(gameObject);
            }
            
        }
    }
}
