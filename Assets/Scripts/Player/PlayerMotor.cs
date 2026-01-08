using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private UIManager UI;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    private bool isCrouched;

    // Player Stats
    public int currentHealth = 3;

    // Weapons
    public bool ScannerIsEquipped = false;
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

        if (transform.position.y < - 30)
        {
            controller.enabled = false;
            transform.position = new Vector3(0, 5, 0);
            controller.enabled = true;
        }
    }

    public void ProcessAim(bool isAiming)
    {
        if (!ScannerIsEquipped) return;
        if (isAiming && !UI.graphicsUI.scannerFilter.enabled)
        {
            UI.graphicsUI.ShowScanner();
        }
        else if (isAiming && UI.graphicsUI.scannerFilter.enabled)
        {
            UI.graphicsUI.HideScanner();
        }
    }


    public void takeDamage(int damagePoints)
    {
        currentHealth -= damagePoints;
        UI.playerStat.UpdateHealth(currentHealth);
        UI.graphicsUI.StartFade();
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

    public void switchWeaponsHotkey() {
        ScannerIsEquipped = !ScannerIsEquipped;

        if (ScannerIsEquipped) {
            ScannerIsEquipped = false;
            gun.Show();
            scanner.Hide();
            UI.playerStat.isWeaponEquipped(true);
        } else
        {
            ScannerIsEquipped = true;
            gun.Hide();
            scanner.Show();
            UI.playerStat.isWeaponEquipped(false);
        }
    }

    public void switchWeapons(Vector2 input)
    {
        Debug.Log(input.y);
        if (input.y < 0)
        {
            ScannerIsEquipped = true;
            gun.Hide();
            scanner.Show();
            UI.playerStat.isWeaponEquipped(false);
        }
        else if (input.y > 0)
        {
            ScannerIsEquipped = false;
            gun.Show();
            scanner.Hide();
            UI.playerStat.isWeaponEquipped(true);
        }
    }
}
