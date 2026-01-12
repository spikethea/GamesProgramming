public class BanditPatrolState : PatrolState
{
    public override void Perform()
    {

        if (npc.path != null)
        {
            PatrolCycle();
        }
        if (npc.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }

        if (npc.isPlayerShootingatMe) {
            stateMachine.ChangeState(new AttackState());
        }
    }
}