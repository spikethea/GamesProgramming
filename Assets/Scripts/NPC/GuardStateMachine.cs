using UnityEngine;

public class GuardStateMachine : StateMachine
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialise();
    }

    public override void Initialise()
    {
        ChangeState(new GuardPatrolState());
    }

    public void AimPose()
    {
        Debug.Log("Guard Aiming");
    }

    public void DefaultPose()
    {
        Debug.Log("Guard Default Pose");
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }
}
