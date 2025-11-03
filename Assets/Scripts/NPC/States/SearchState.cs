using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;
    public override void Enter()
    {
        npc.Agent.SetDestination(npc.LastKnownPos);
    }

    public override void Perform()
    {
        if (npc.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }

        if (npc.Agent.remainingDistance < npc.Agent.stoppingDistance)
        {
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;

            if (moveTimer > Random.Range(3, 5))
            {
                npc.Agent.SetDestination(npc.transform.position + (Random.insideUnitSphere * 10));
                moveTimer = 0;
            }
            if (searchTimer > 10)
            {
                stateMachine.ChangeState(new PatrolState());
            }
            
        }
    }

    public override void Exit()
    {
        
    }
}
