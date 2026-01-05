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

    public float speed = 50f;
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
        moveDirection.x = input.y;



        if (!isGrounded && isEngineOn) {
            transform.Translate(moveDirection * speed * Time.fixedDeltaTime, transform);
            //Debug.Log($"Force: {moveDirection * speed} | Vel: {rb.linearVelocity}");
            //Vector3 force = moveDirection * speed;
            //rb.AddForce(force, ForceMode.Force);

            if (input.x == 1) {
            transform.Rotate(Vector3.up * Time.fixedDeltaTime * 50f);
            } else if (input.x == -1) {
                transform.Rotate(Vector3.up * Time.fixedDeltaTime * -50f);
            }
        }

        if (isBoosting && isEngineOn) {
            rb.MovePosition(rb.position + moveDirection * boostSpeed * Time.fixedDeltaTime);
        }
        //controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        //controller.Move(playerVelocity * Time.deltaTime);
    }
}


