public class BanditPatrolState : PatrolState
{
    public override void Perform()
    {
        PatrolCycle(); 
        if (npc.CanSeePlayer() && npc.isPlayerAimingatMe)
        {
            stateMachine.ChangeState(new AttackState());
        }

        if (npc.isPlayerShootingatMe) {
            stateMachine.ChangeState(new AttackState());
        }
    }
}