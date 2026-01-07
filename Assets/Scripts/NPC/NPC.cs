using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    private StateMachine stateMachine;
    public NavMeshAgent agent;
    private GameObject player;
    private Vector3 lastKnownPos;
    public NavMeshAgent Agent { get => agent; }
    public GameObject Player { get => player; }

    [Header("How")]
    public int health = 10;

    public Vector3 LastKnownPos { get => lastKnownPos; set => lastKnownPos = value; }
    // just for debugging purposes
    public CharacterPath path;
    public GameObject debugSphere;
    [Header("Sight Values")]
    public float sightDistance = 20f;
    public float fieldOfView = 85;
    public float eyeHeight = 2f;
    [Header("Weapon Values")]
    public Transform meleeWeapon;
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate;

    [SerializeField]
    private string currentState;

    [Header("Dynamic Info")]
    public bool isPlayerAimingatMe = false;
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
        debugSphere.transform.position = lastKnownPos;
    }

    public void takeDamage(int damagePoints) {
        health -= damagePoints;
    }

    public void AnimHandsDown()
    {
        Debug.Log("AnimHandsDown EVENT FIRED");
        Debug.Log(this);
    }

    public void AnimHandsUp() {
        Debug.Log("AnimHandsUp EVENT FIRED");
        Debug.Log(this);
    }
    public void AnimHandsMove() { }

    public bool CanSeePlayer()
    {
        if(player != null) 
        {
            //is the player close enough to be seen?
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer <= fieldOfView * 0.5f)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.tag == "Player")
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            
                        }
                    }
                    return true;
                }
            }
        }
        return false;
    }
}
