using TMPro;
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

    [Header("Health")]
    public int health = 10;

    public Vector3 LastKnownPos { get => lastKnownPos; set => lastKnownPos = value; }
    [Header("Helper Objects")]
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

    [Header("Animation")]
    public Animator animator;

    [Header("Animation")]
    public AudioSource audioSource;

    [SerializeField]
    private string currentState;

    [Header("Dynamic Info")]
    public bool isPlayerAimingatMe = false;
    public bool isPlayerShootingatMe = false;

    [Header("Scanner")]
    public string Caption = "-";
    public int captionFontSize = 30;
    public GameObject FloatingTextPrefab;
    private GameObject _floatingTextInstance;
    private Camera _cam;

    [Header("Convict")]
    public Convict convict;

    private void Awake()
    {
        // If the NPC is a convict the caption switches to thier name
        if(convict != null)
        Caption = convict.name;

        // Unparenting Helper GameObjects
        if (path != null)
        path.transform.SetParent(null, true);

        if(debugSphere != null)
            debugSphere.transform.SetParent(null, true);

        // Floating Text Caption
        Vector3 pos = new Vector3(0f, 3.5f, 0f);
        _floatingTextInstance = Instantiate(FloatingTextPrefab, this.transform.position + pos, this.transform.rotation);
        _floatingTextInstance.GetComponentInChildren<TextMeshPro>().fontSize = captionFontSize;
        _floatingTextInstance.GetComponentInChildren<TextMeshPro>().text = Caption;
        HideFloatingText();
        _floatingTextInstance.transform.parent = transform;
        _cam = Camera.main;
    }

    private void FaceCaptionAtCamera() {
        var rot = Quaternion.LookRotation(_floatingTextInstance.transform.position - _cam.transform.position);
        _floatingTextInstance.transform.rotation = rot;
    }

    public void ShowFloatingText()
    {
        _floatingTextInstance.SetActive(true);
        Debug.Log("Show");

    }

    public void HideFloatingText()
    {
        _floatingTextInstance.SetActive(false);
        Debug.Log("Hide");
    }

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

        FaceCaptionAtCamera();
    }

    virtual public void takeDamage(int damagePoints) {
        health -= damagePoints;
        isPlayerShootingatMe = true;

        if (health <= 0)
        {
            AnimDeath();
        }
    }

    public void AnimPanic()
    {
        animator.Play("Panic", 0, 0f);
        Debug.Log("AnimPanic EVENT FIRED");
        Debug.Log(this);
    }

    

    public void AnimDeath()
    {
        animator.Play("Death");
        Debug.Log("AnimDeath EVENT FIRED");
        Debug.Log(this);
        Invoke(nameof(Kill), 3f);
    }
    
    public void Kill() {
        Debug.Log("Killing NPC FIRED");
        Debug.Log(this);

        if (convict != null)
            Player.GetComponent<PlayerMotor>().EarnCredits(convict.reward);
        Destroy(this.gameObject);
    }

    public void PoseHandsDown()
    {
        if (health <= 0) return;

        Debug.Log("PoseHandsDown EVENT FIRED");
        Debug.Log(this);
    }

    public void AnimHandsUp() {
        //var state = animator.GetCurrentAnimatorStateInfo(0);
        animator.Play("Civilian");
        Debug.Log("AnimHandsUp EVENT FIRED");
        Debug.Log(this);
    }

    public void AnimHandsMoveToShoot()
    {
        if (health <= 0) return;
        
        animator.Play("Guard");
        Debug.Log("AnimHandsMoveToShoot EVENT FIRED");
        Debug.Log(this);
    }

    public void PoseShooting()
    {
        if (health <= 0) return;

        animator.Play("Shooting");
        Debug.Log("PoseShooting EVENT FIRED");
        Debug.Log(this);
    }


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
