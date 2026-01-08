using System.Collections;
using Unity.Services.Analytics;
using UnityEngine;

public class PanicState : BaseState
{
    AnimationClip clip;
    Animator anim;

    private float panicDuration = 10f;
	private float panicTimer;
    private Coroutine freezeRoutine;
    public override void Enter()
	{
        // Disable the Navmesh control on rotation
        npc.agent.updateRotation = false;
        // Set NPC to freeze and panic animation
        freezeRoutine = npc.StartCoroutine(FreezeAnimationCoroutine());
        panicTimer = 0f;
    }

    IEnumerator FreezeAnimationCoroutine()
        {
        panicTimer = 0f;
        panicDuration = 10f;
        while (panicTimer < panicDuration)
        {
            npc.AnimHandsUp();
            yield return new WaitForSeconds(0.5f);
            //npc.AnimHandsMove();
            yield return null;
        } 
        yield return 0;
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
        if (freezeRoutine != null)
        {
            npc.StopCoroutine(freezeRoutine);
            freezeRoutine = null;
            npc.PoseHandsDown();
        }

        //Re-enable Navmesh rotation control
        npc.agent.updateRotation = false;


        // Any cleanup if necessary when exiting PanicState
    }
}