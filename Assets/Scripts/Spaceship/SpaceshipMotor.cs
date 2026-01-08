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

    public float speed = 20f;
    public float boostSpeed = 25f;
    public float shipHeight = 1.5f;

    // Vertical Oscillation
    public float Yspeed = 5f;
    public GameObject childMesh;
    public float amplitudeY = 0.1f;
    private Vector3 startMeshPos;

    // Start is called before the first frame update
    void Start()
    {
        startMeshPos = childMesh.transform.localPosition;
    }

    public void ResetYPosition()
    {
        childMesh.transform.localPosition = startMeshPos;
    }

    public void ApplyVerticalOscillation()
    {
        float yOffset = Mathf.Sin(Time.time * Yspeed) * amplitudeY;
        childMesh.transform.localPosition = startMeshPos + new Vector3(0, yOffset, 0);
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
            if (isBoosting)
            {
                transform.Translate(moveDirection * boostSpeed * Time.fixedDeltaTime, transform);
            }
            else
            {
                transform.Translate(moveDirection * speed * Time.fixedDeltaTime, transform);
            }
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


