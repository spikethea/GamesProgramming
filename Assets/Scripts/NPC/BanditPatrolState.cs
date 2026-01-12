public class BanditPatrolState : PatrolState
{
    public override void Perform()
    {
        PatrolCycle(); 
        if (npc.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }

        if (npc.isPlayerShootingatMe) {
            stateMachine.ChangeState(new AttackState());
        }
    }
}