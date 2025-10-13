using System;
using UnityEngine;

public class MyMagicalMovementScript : MonoBehaviour
{
    public Rigidbody Rigidbody;

    [Range(0, 100)]
    public int Force = 10;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }
    public void OnMouseDown()
    {
        Rigidbody.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
    }
}
