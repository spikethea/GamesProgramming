using UnityEngine;

public class Rainfall : MonoBehaviour
{
    [SerializeField] private int height;
    [SerializeField] private InputManager player;
    public ParticleSystem particleSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + (Vector3.up * height);
        

        if (player.playerInput.OnFoot.enabled)
        {
            if(!particleSystem.isPlaying)
                particleSystem.Play();
        }
        else {
            particleSystem.Pause();
        }
    }
}
