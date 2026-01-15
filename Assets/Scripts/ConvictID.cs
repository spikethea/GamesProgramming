using UnityEngine;

public class ConvictID : MonoBehaviour
{

    public int creditsAmount = 0;
    public string convictName;

    // Vertical Oscillation
    private float Yspeed = 5f;
    public GameObject childMesh;
    public float amplitudeY = 0.2f;
    private Vector3 startMeshPos;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip ding;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && creditsAmount > 0)
        {
             PlayerMotor player = other.GetComponent<PlayerMotor>();
            if (player != null)
            {
                if (convictName != player.Game.currentTarget) return;
                audioSource.PlayOneShot(ding);
                Debug.Log($"Player collected {creditsAmount} credits from convict ID.");
                creditsAmount = 0;

                Invoke(nameof(Disappear), 1.5f);
            }
        }
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }

    public void ApplyVerticalOscillation()
    {
        float yOffset = Mathf.Sin(Time.time * Yspeed) * amplitudeY;
        childMesh.transform.localPosition = startMeshPos + new Vector3(0, yOffset, 0);
    }

    void Start()
    {
        startMeshPos = childMesh.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        ApplyVerticalOscillation();
        childMesh.transform.Rotate(Vector3.up * Time.deltaTime * 25f);
    }
}
