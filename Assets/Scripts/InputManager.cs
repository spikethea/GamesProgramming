using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    public PlayerInput.FlyingActions Flying;
    public bool isAiming = false;

    private PlayerMotor motor;
    private PlayerLook look;

    public SpaceshipManager spaceshipManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        Flying = playerInput.Flying;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();

        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.HotSwap.performed += ctx => motor.switchWeaponsHotkey();
        onFoot.Melee.performed += ctx => motor.tryAttack();
    }

    void Update()
    {
        if (onFoot.Aim.IsPressed())
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        

        if (playerInput.OnFoot.enabled)
        {
            motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>(), onFoot.Run.IsPressed());
            motor.switchWeapons(onFoot.Scroll.ReadValue<Vector2>());
        } else {
            //spaceshipManager.motor.ProcessMove(Flying.Movement.ReadValue<Vector2>(), Flying.Boost.IsPressed());
        }
    }

    private void LateUpdate()
    {

        

        if (playerInput.OnFoot.enabled)
        {
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>(), isAiming);
            motor.ProcessAim(isAiming);
        }
        else {
            // Spaceship Input
        }
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }
}
