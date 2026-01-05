using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    public int waitBeforeSearchTime = 8;
    private float shotTimer;

    private float trackedRotation = 62;
    private int rotationDir = 1;
    // Start is called before the first frame update
    public override void Enter()
    {

    }

    // Update is called once per frame
    public override void Perform()
    {
        if (npc.CanSeePlayer())
        {
            // lock the lose player timer and increment the move
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            npc.transform.LookAt(npc.Player.transform);
            if (shotTimer > npc.fireRate && npc.gunBarrel != null)
            {
                Shoot();
            }

            if (npc.meleeWeapon != null)
             {
                Melee();
            }


            if (moveTimer > UnityEngine.Random.Range(3, 7))
            {
                npc.Agent.SetDestination(npc.transform.position + (UnityEngine.Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
            npc.LastKnownPos = npc.Player.transform.position;
        }
        else 
        {
            losePlayerTimer += Time.deltaTime;
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > waitBeforeSearchTime)
            {
                //Change to the search state.
                stateMachine.ChangeState(new SearchState());
            }
        }
    }

    public void Shoot()
    {
        if (shotTimer < 3)
            return;
        //store reference to the gun barrel.
        Transform gunbarrel = npc.gunBarrel;
        // instantiate a new bullet.
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunbarrel.position, npc.transform.rotation);
        // add force rigidbody of the bullet.
        bullet.GetComponent<Rigidbody>().linearVelocity = gunbarrel.forward * 40f;
        shotTimer = 0;
    }

    public void Melee()
    {
        //store reference to the Melee Item.
        Transform weapon = npc.meleeWeapon;
        if (trackedRotation > 60)
        {
            rotationDir = -1;
        }
        if (trackedRotation < 00)
        {
            rotationDir = 1;
        }

        trackedRotation += rotationDir * 1;

        weapon.rotation = Quaternion.Euler(0, 0, trackedRotation);
    }

    // Collision moved to Hammer.cs
        // Unity function, on collision with other object
        //private void OnCollisionEnter(Collision collision)
        //{
        //    Transform hitTransform = collision.transform;
        //    if (hitTransform.CompareTag("Player") && npc.gunBarrel == null)
        //    {
        //        Debug.Log("Hit Player");
        //        hitTransform.GetComponent<PlayerHealth>().TakeDamage(10);
        //    }

        //}

    public override void Exit()
    { }
}
