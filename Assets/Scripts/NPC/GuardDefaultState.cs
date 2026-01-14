using UnityEngine;

internal class GuardDefaultState : BaseState
{
    private AudioClip audioClip;

    private float PatienceTime = 5;
    private float PatienceTimer = 0;

    public GuardDefaultState(AudioClip botheredClip) {
        audioClip = botheredClip;
    }
    public override void Enter()
    {
    }

    // Update is called once per frame
    public override void Perform()
    {
        if (npc.isPlayerShootingatMe)
        {
            stateMachine.ChangeState(new AttackState());
        }

        if (npc.CanSeePlayer() && npc.isPlayerAimingatMe)
        {
        if (PatienceTimer == 0) {
            npc.audioSource.PlayOneShot(audioClip);
        }

        PatienceTimer += Time.deltaTime;
        if (PatienceTimer >= PatienceTime)
        {
            stateMachine.ChangeState(new AttackState());
        }

        }
        else {
            PatienceTimer = 0f;
            npc.isPlayerAimingatMe = false;
            npc.isPlayerShootingatMe = false;
        }
    }

    public override void Exit()
    { }
}