public class GuardPatrolState : PatrolState
{
    public override void Perform()
    {
        PatrolCycle();
        if (npc.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }
}