using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public MainTextUI mainUI;
    public PlayerStatUI playerStat;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Restart();
        //mainUI.SetTarget("carla", "Robbery", "nv-797", 300);
    }


    public void Restart ()
    {
        mainUI.ClearText();
        playerStat.Restart();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
