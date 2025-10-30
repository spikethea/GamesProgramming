using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    private bool isAiming = false;

    private PlayerMotor motor;
    private PlayerLook look;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => motor.Jump();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
;    }

    private void LateUpdate()
    {

        if(onFoot.Aim.IsPressed())
        {
            isAiming = true;
        }
        else
        {
            isAiming = false;
        }

        look.ProcessLook(onFoot.Look.ReadValue<Vector2>(), isAiming);
;
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
