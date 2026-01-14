using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public MainTextUI mainUI;
    public PlayerStatUI playerStat;
    public GraphicsUI graphicsUI;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Restart();
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
