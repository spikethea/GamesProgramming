using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] UIManager UI;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isCrouched;

    // Weapons
    private bool ScannerIsEquipped = false;
    public Gun gun;
    public Scanner scanner;

    public float speed = 5f;
    public float gravity = -8.9f;
    public float jumpHeight = 1.5f;
    public float crouchHeight = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        if (!isCrouched)
        {
            controller.height = crouchHeight;
            isCrouched = true;
        }
        else
        {
            controller.height = 2;
            isCrouched = false;
        }
    }

    public void switchWeapons(Vector2 input)
    {
        if (input.y < 0)
            ScannerIsEquipped = !ScannerIsEquipped;

        if (ScannerIsEquipped) {
            gun.Hide();
            scanner.Show();
            UI.playerStat.isWeaponEquipped(false);
        } else
        {
            gun.Show();
            scanner.Hide();
            UI.playerStat.isWeaponEquipped(true);
        }
    }
}
