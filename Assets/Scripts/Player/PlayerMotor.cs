using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMotor : MonoBehaviour
{
    public GameManager Game;
    [SerializeField] public UIManager UI;
    private CharacterController controller;
    private Vector3 playerVelocity;
    public bool hasGameStarted = false;
    private bool isGrounded;
    private bool isCrouched;

    //Animation
    public Animator animator;

    // Player Stats
    [Header("Player Stats")]
    public int currentHealth = 3;
    public int currentCredits = 0;


    // Weapons
    public bool ScannerIsEquipped = false;
    public Gun gun;
    public Scanner scanner;
    public MeshRenderer lightsaberGlow;

    //Melee
    public Collider meleeCollider;
    private float attackCooldown = 5f;
    private bool meleeReady = true;

    //Spaceship
    public Transform Spaceship;


    public float speed = 5f;
    public float runningSpeed = 12f;
    public float gravity = -8.9f;
    public float jumpHeight = 1.5f;
    public float crouchHeight = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(InitGame), 2f);
        controller = GetComponent<CharacterController>();
    }

    private void InitGame()
    {
        UI.mainUI.ClearTitleScreen();
        UI.graphicsUI.ClearTitleScreen();
        UI.mainUI.PulseMainText("Welcome to Headquarters. Select a bounty to start hunting.");
        hasGameStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasGameStarted) return;
        isGrounded = controller.isGrounded;

        if (transform.position.y < - 30)
        {
            controller.enabled = false;
            transform.position = new Vector3(0, 5, 0);
            Spaceship.position = new Vector3(0.610000014f, -1.35000002f, -18.3199997f);
            Spaceship.rotation = Quaternion.identity;
            controller.enabled = true;
        }
    }

    public void ProcessAim(bool isAiming)
    {
        if (!hasGameStarted) return;
        if (!ScannerIsEquipped) return;

        if (isAiming)
        {
            UI.graphicsUI.ShowScanner();
        }
        else
        {
            UI.graphicsUI.HideScanner();
        }
    }

    // Player stats
    public void takeDamage(int damagePoints)
    {
        currentHealth -= damagePoints;
        UI.playerStat.UpdateHealth(currentHealth);
        UI.graphicsUI.StartFade();

        if (currentHealth < 0) {
            Death();
        }
    }

    public void tryAttack()
    {
        if (!meleeReady) return;


        
        meleeAttack();
    }

    private void meleeAttack() {
        animator.SetTrigger("Attack");
        meleeCollider.enabled = true;
    }

    IEnumerator AttackCooldown()
    {
        meleeReady = false;

        //lerp from grey to emmission
        Material mat = lightsaberGlow.material;
        Color baseColor = mat.color;

        float elapsed = 0f;

        while (elapsed < attackCooldown)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / attackCooldown;

            float brightness = Mathf.Lerp(0.3f, 1, t);
            mat.color = new Color(
                baseColor.r * brightness,
                baseColor.g * brightness,
                baseColor.b * brightness,
                baseColor.a
            );

            yield return null;
        }


        yield return new WaitForSeconds(attackCooldown);

        meleeReady = true;
    }


    //Animation event at end of animation
    public void meleeDisappear() {
        meleeCollider.enabled = false;
        StartCoroutine(AttackCooldown());
    }

    public void Death() {
        UI.mainUI.SetMainText("You Died");
        Invoke(nameof(Reset), 3f);
    }

    void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void EarnCredits(int credits) {
        currentCredits += credits;
        UI.playerStat.UpdateCredits(currentCredits);
        UI.mainUI.CapturedTarget();
    }


    public void ProcessMove(Vector2 input, bool isRunning)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        if (isRunning)
        {
            controller.Move(transform.TransformDirection(moveDirection) * runningSpeed * Time.deltaTime);
        }
        else {
            controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        }

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

    public void switchWeaponsHotkey(bool isAiming) {
        if (isAiming) return;
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

    public void switchWeapons(Vector2 input, bool isAiming)
    {
        if (isAiming) return;
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
