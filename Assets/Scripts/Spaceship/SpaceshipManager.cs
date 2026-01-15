using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class SpaceshipManager : MonoBehaviour
{
    [SerializeField] private GameManager game;
    [SerializeField] private UIManager UI;
    public SpaceshipMotor motor;
   
    public GameObject Player;

    private InputManager PlayerInput;
    public PlayerInput.FlyingActions Flying;
    public PlayerInput.OnFootActions onFoot;

    public Transform sittingPoint;
    public Transform exitPoint;

    public bool inSpaceship = false;
    public bool isEngineOn = false;
    public float checkDistance = 5.0f;

    [Header("Audio")]
    public AudioSource EngineAudioSource;
    public AudioClip EngineStartClip;

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

        Flying.Engine.performed += OnEngineToggle;

        EngineAudioSource.clip = EngineStartClip;
    }

    void OnEngineToggle(InputAction.CallbackContext context)
    {
        isEngineOn = !isEngineOn;
        if (isEngineOn)
        {
            EngineAudioSource.Play();
            UI.mainUI.SetPrompt("Turn Off Engine & Dock [E] BoostEngine [LShift]");
        }
        else {
            EngineAudioSource.Pause();
            UI.mainUI.SetPrompt("Turn on Engine [E] / Exit Vehicle [Q]");
        }
            Debug.Log("Is Engine On: " + isEngineOn);
    }

    // Update is called once per frame
    void Update()
    {
        if (inSpaceship)
        {
            motor.ResetYPosition();
            
            if (isEngineOn) {
                MoveSpaceship();

                if (Flying.Boost.IsPressed())
                {
                    //EngineAudioSource.Effects
                }
            }

            //Exiting Spaceship
            if (Flying.Exit.IsPressed()) {
                NavMeshHit hit;
                if (!NavMesh.SamplePosition(exitPoint.position, out hit, checkDistance, NavMesh.AllAreas))
                return;
                ExitSpaceship();   
            }
        }
        motor.ApplyVerticalOscillation();
    }

    public void EnterSpaceship()
    {
        if (game.currentTarget == null) {
            UI.mainUI.PulseMainText("Assign a target first");
            return;
        }
        inSpaceship = true;

        playerMotor.transform.position = sittingPoint.position;

        //Debug.Log("sittingPoint.rotation.y: " + sittingPoint.rotation.y);

        
        Player.transform.parent = sittingPoint.transform; 
        Player.transform.rotation = sittingPoint.rotation;

        

        PlayerInput.onFoot.Disable();
        PlayerInput.Flying.Enable();
        Debug.Log("Entering Spaceship...");
        playerLook.enabled = false;
        playerMotor.enabled = false;
        playerInteract.enabled = false;
        
        Camera.main.transform.localRotation = Quaternion.identity;

        UI.mainUI.SetPrompt("Turn on Engine [E] / Exit Vehicle [Q]");
    }

    void ExitSpaceship()
    {

        inSpaceship = false;

        PlayerInput.onFoot.Enable();
        PlayerInput.Flying.Disable();
        Debug.Log("Exiting Spaceship...");
        playerLook.enabled = true;
        playerMotor.enabled = true;
        playerInteract.enabled = true;

        playerMotor.transform.position = exitPoint.position;
        playerMotor.transform.parent = null;
    }

    private void MoveSpaceship()
    {
        motor.ProcessMove(Flying.Movement.ReadValue<Vector2>(), Flying.Boost.IsPressed());
        if (Flying.Boost.IsPressed()) {
            if(EngineAudioSource.pitch < 1.3f)
            {
                EngineAudioSource.pitch += 0.1f* Time.deltaTime;
            }
            
        } else {
            if (EngineAudioSource.pitch > 1.0f)
                EngineAudioSource.pitch -= 0.1f * Time.deltaTime;
        }
        //motor.ProcessMove(Flying.Movement.ReadValue<Vector2>(), Flying);

    }
}
