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

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider != null) {
            NPC nPC = other.collider.GetComponent<NPC>();
            if (nPC != null)
            {
                nPC.takeDamage(2);
            }

            PlayerMotor player = other.collider.GetComponent<PlayerMotor>();
            if (player != null)
            {
                player.takeDamage(1);
            }
            Destroy(gameObject);
        }
    }
}
