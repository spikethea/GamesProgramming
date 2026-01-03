using UnityEngine;

public class Guard : StateMachine
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public override void Initialise()
    {
        ChangeState(new GuardPatrolState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
