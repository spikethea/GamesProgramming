using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipManager : MonoBehaviour
{
    public SpaceshipMotor motor;



    public GameObject Player;

    private InputManager PlayerInput;
    public PlayerInput.FlyingActions Flying;
    public PlayerInput.OnFootActions onFoot;
    public Transform sittingPoint;

    public bool inSpaceship = false;

    // Player Reference
    private PlayerLook playerLook;
    private PlayerMotor playerMotor;
    private PlayerInteract playerInteract;

    private void Start()
    {
        PlayerInput = Player.GetComponent<InputManager>();
        playerMotor = Player.GetComponent<PlayerMotor>();
        playerLook = Player.GetComponent<PlayerLook>();
        playerInteract = Player.GetComponent<PlayerInteract>();

        Flying = PlayerInput.Flying;
        onFoot = PlayerInput.onFoot;
    }

    // Update is called once per frame
    void Update()
    {
        if (inSpaceship)
        {

            motor.ResetYPosition();
            MoveSpaceship();
        }
        else {
            motor.ApplyVerticalOscillation();
        }
    }

    public void EnterSpaceship()
    {
        inSpaceship = true;

        playerMotor.transform.position = sittingPoint.position;

        //Debug.Log("sittingPoint.rotation.y: " + sittingPoint.rotation.y);

        Player.transform.rotation = sittingPoint.rotation;
        Player.transform.parent = transform;

        

        PlayerInput.onFoot.Disable();
        PlayerInput.Flying.Enable();
        Debug.Log("Entering Spaceship...");
        playerLook.enabled = false;
        playerMotor.enabled = false;
        
        Camera.main.transform.eulerAngles = new Vector3(0,0,0);

    }

    private void MoveSpaceship()
    {
        motor.ProcessMove(Flying.Movement.ReadValue<Vector2>());
        
    }
}
