using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private UIManager UI;
    [SerializeField] private PlayerMotor motor;
    public void OpenGate()
    {
        if (motor.currentCredits < 900)
        {
            UI.mainUI.SetMainText("You need at least 900 credits to open the gate.");
            Invoke(nameof(CloseInfo), 3f);
            return;
        }
        // Logic to open the gate
        Debug.Log("Gate is now open!");
        gameObject.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void CloseInfo()
    {
        UI.mainUI.ClearCentreText();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
