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
}
