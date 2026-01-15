using UnityEngine;

public class AttackState : BaseState
{
    // Timers
    private float losePlayerTimer;
    private float shotTimer;
    private float reloadTimer;

    // Reloading
    private int bulletCount = 0;
    private int maxBullets = 6;
    private float reloadTime = 3f;

    // Strafing
    private float strafeSpeed = 6f;
    private float strafeDirection = 1f; // 1 = right, -1 = left
    private float strafeChangeInterval = 1.5f;
    private float strafeTimer = 0f;

    private bool IsMelee => npc.meleeWeapon != null;

    public override void Enter()
    {
        npc.Agent.isStopped = false;
        npc.Agent.stoppingDistance = npc.attackRange;
        npc.AnimHandsMoveToShoot();

        shotTimer = npc.fireRate; // allow immediate first shot
        losePlayerTimer = 0f;
        reloadTimer = reloadTime;
        strafeTimer = 0f;
        strafeDirection = 1f;
    }

    public override void Perform()
    {
        if (npc.CanSeePlayer())
        {
            losePlayerTimer = 0f;

            ChaseFaceAndStrafe();
            HandleAttack();
        }
        else
        {
            HandleLosePlayer();
        }
    }

    public override void Exit()
    {
        npc.Agent.isStopped = false;
    }

    // ------------------------
    // Movement & Strafing
    // ------------------------
    private void ChaseFaceAndStrafe()
    {
        Vector3 toPlayer = npc.transform.position - npc.Player.transform.position;
        toPlayer.y = 0;
        float distance = toPlayer.magnitude;

        // Outside attack range → walk toward player
        if (distance > npc.attackRange)
        {
            npc.Agent.isStopped = false;
            npc.Agent.SetDestination(npc.Player.transform.position);
        }
        else
        {
            // Inside attack range → stop and strafe
            npc.Agent.isStopped = true;

            // Strafe timer
            strafeTimer += Time.deltaTime;
            if (strafeTimer >= strafeChangeInterval)
            {
                strafeDirection *= -1; // switch strafe direction
                strafeTimer = 0f;
            }

            // Compute strafing direction perpendicular to player
            Vector3 strafe = Vector3.Cross(Vector3.up, toPlayer).normalized * strafeDirection;

            // Apply strafing movement
            npc.transform.position += strafe * strafeSpeed * Time.deltaTime;

            // Always face player
            Vector3 lookDir = npc.Player.transform.position - npc.transform.position;
            lookDir.y = 0;
            if (lookDir.sqrMagnitude > 0.01f)
                npc.transform.rotation = Quaternion.LookRotation(lookDir);
        }

        npc.LastKnownPos = npc.Player.transform.position;
    }

    // ------------------------
    // Attacking
    // ------------------------
    private void HandleAttack()
    {
        if (IsMelee)
        {
            Melee();
        }
        else
        {
            if (Reloading())
            {
                Debug.Log("Reloading...");
                return;
            }

            Shoot();
        }
    }

    private bool Reloading()
    {
        if (bulletCount < maxBullets)
            return false; // still have ammo → no reload

        reloadTimer += Time.deltaTime;

        if (reloadTimer >= reloadTime)
        {
            bulletCount = 0;
            reloadTimer = 0f;
            return false; // reload finished
        }

        return true; // still reloading
    }

    private void Shoot()
    {
        shotTimer += Time.deltaTime;

        if (shotTimer < npc.fireRate || npc.gunBarrel == null)
            return;

        Transform gunbarrel = npc.gunBarrel;

        GameObject bullet = GameObject.Instantiate(
            Resources.Load("Prefabs/Bullet") as GameObject,
            gunbarrel.position,
            gunbarrel.rotation
        );

        bullet.GetComponent<Rigidbody>().linearVelocity = gunbarrel.forward * 40f;

        npc.audioSource.PlayOneShot(npc.gunShotSound);
        shotTimer = 0f;
        bulletCount++;
    }

    private void Melee()
    {
        // Trigger melee animation
    }

    // ------------------------
    // Losing Player
    // ------------------------
    private void HandleLosePlayer()
    {
        losePlayerTimer += Time.deltaTime;

        if (losePlayerTimer >= npc.waitBeforeSearchTime)
        {
            stateMachine.ChangeState(new SearchState());
        }
    }
}