using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;
    public override void Enter()
    {
        npc.Agent.isStopped = false;
        npc.Agent.stoppingDistance = 0f;
        npc.Agent.SetDestination(npc.LastKnownPos);

        searchTimer = 0f;
        moveTimer = 0f;
    }

    public override void Perform()
    {
        if (npc.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
        
        Debug.Log(stateMachine.name);

        if (!npc.Agent.pathPending && npc.Agent.remainingDistance <= 0.2f)
        {
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;

            if (moveTimer > Random.Range(3, 5))
            {
                MoveToRandomNearbyPoint(10f);
                moveTimer = 0;
            }
            
            if (searchTimer > 10f)
            {
                
                if (npc.GetComponent<BanditStateMachine>() != null) {
                    stateMachine.ChangeState(new BanditPatrolState());
                }
                else if (npc.GetComponent<GuardStateMachine>() != null)
                {
                    GuardStateMachine guard = (GuardStateMachine)stateMachine;
                    stateMachine.ChangeState(new GuardDefaultState(guard.botheredClip));
                } else {
                    stateMachine.ChangeState(new PatrolState());
                }
                
            }
            
        }
    }

    private void MoveToRandomNearbyPoint(float radius)
    {

        Vector3 randomDir = Random.insideUnitSphere * radius;
        randomDir += npc.transform.position;

        if (UnityEngine.AI.NavMesh.SamplePosition(
            randomDir,
            out UnityEngine.AI.NavMeshHit hit,
            radius,
            UnityEngine.AI.NavMesh.AllAreas))
        {
            npc.Agent.SetDestination(hit.position);
        }
    }

    public override void Exit()
    {
        
    }
}
