using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;

    virtual public void Initialise()
    {

        ChangeState(new PatrolState());
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialise();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        //check activeState !=null
        if (activeState !=null)
        {
            //run cleanup on activeState
            activeState.Exit();
        }
        //change to a new state.
        activeState = newState;

        // fail-safe null check to make sure new state wasnt null
        if (activeState != null)
        {
            //setup new state
            activeState.stateMachine = this;
            activeState.npc = GetComponent<NPC>();
            activeState.Enter();
        }
    }
}
