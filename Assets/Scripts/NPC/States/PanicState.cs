using System.Collections;
using UnityEngine;

public class PanicState : BaseState
{
    AnimationClip clip;
    Animator anim;

    private float panicDuration = 10f;
	private float panicTimer;
	public override void Enter()
	{
        // Set NPC to freeze and panic animation
        StartCoroutine(FreezeAnimationCoroutine());
    }

	public void StartFreezeAnimation()
	{
        // Implementation of freeze animation event
		panicTimer = 0f;

    }

	private void FreezeAnimation()
	{
		while (panicTimer < panicDuration)
		{

            if (panicTimer % 1 > 0.5f)
            {
                npc.AnimHandsUp();
            }
            else {
                npc.AnimHandsDown();
            }
        }

    }
    public override void Perform()
	{
		panicTimer += Time.deltaTime;
        npc.transform.LookAt(npc.transform);
        // If reached destination or panic duration exceeded, switch to PatrolState
        if (panicTimer > panicDuration)
		{
			stateMachine.ChangeState(new PatrolState());
		}
	}
	public override void Exit()
	{
        npc.AnimHandsDown();
        // Any cleanup if necessary when exiting PanicState
    }
}