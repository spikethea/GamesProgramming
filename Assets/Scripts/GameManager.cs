using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public UIManager UI;

    // Game Objectives
    public Convict currentTarget;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}