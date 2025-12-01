using System;
using UnityEngine;

public class SpaceshipManager : MonoBehaviour
{
    public GameObject Player;
    public PlayerInput PlayerInput;
    public SpaceshipMotor motor;
    public PlayerInput.FlyingActions Flying;

    public bool inSpaceship = false;

    // Update is called once per frame
    void Update()
    {
        if (inSpaceship)
        {
            Player.transform.position = transform.position;
            Player.transform.parent = transform;

            MoveSpaceship();
        }
    }

    public void EnterSpaceship()
    {
        inSpaceship = true;
        Debug.Log("Entering Spaceship...");
        Player.GetComponent<PlayerMotor>().enabled = false;
        Player.GetComponent<PlayerLook>().enabled = false;
        
    }

    private void MoveSpaceship()
    {
        
    }
}
