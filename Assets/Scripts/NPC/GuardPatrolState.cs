public class GuardPatrolState : PatrolState
{
    public override void Perform()
    {
        PatrolCycle(); 
        if (npc.CanSeePlayer() && npc.isPlayerAimingatMe)
        {
            stateMachine.ChangeState(new AttackState());
        }
    }
}