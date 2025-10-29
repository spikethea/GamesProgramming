using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

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
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
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
