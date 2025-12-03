using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMotor : MonoBehaviour
{
    [SerializeField] UIManager UI;
    public Rigidbody rb;
    private Vector3 playerVelocity;
    private bool isGrounded = false;
    private bool isEngineOn = true;
    private bool isBoosting;

    public float speed = 5f;
    public float boostSpeed = 5f;
    public float shipHeight = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        if (!isGrounded && isEngineOn) {
            //rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
            rb.AddForce(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        }
        //controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        //controller.Move(playerVelocity * Time.deltaTime);
    }
}


